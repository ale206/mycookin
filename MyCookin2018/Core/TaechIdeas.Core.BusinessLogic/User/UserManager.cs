using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using AutoMapper;
using TaechIdeas.Core.Core.Common;
using TaechIdeas.Core.Core.Common.Dto;
using TaechIdeas.Core.Core.Common.Enums;
using TaechIdeas.Core.Core.Configuration;
using TaechIdeas.Core.Core.LogAndMessage;
using TaechIdeas.Core.Core.Media;
using TaechIdeas.Core.Core.Media.Dto;
using TaechIdeas.Core.Core.Network;
using TaechIdeas.Core.Core.Network.Dto;
using TaechIdeas.Core.Core.Statistic;
using TaechIdeas.Core.Core.Statistic.Dto;
using TaechIdeas.Core.Core.Token;
using TaechIdeas.Core.Core.Token.Dto;
using TaechIdeas.Core.Core.User;
using TaechIdeas.Core.Core.User.Dto;
using TaechIdeas.Core.Core.Verification;

namespace TaechIdeas.Core.BusinessLogic.User
{
    public class UserManager : IUserManager
    {
        private readonly IMySecurityManager _mySecurityManager;
        private readonly IRetrieveMessageManager _retrieveMessageManager;
        private readonly IStatisticManager _statisticManager;
        private readonly IUserRepository _userRepository;
        private readonly ITokenManager _tokenManager;
        private readonly IUserConfig _userConfig;
        private readonly INetworkConfig _networkConfig;
        private readonly ITokenConfig _tokenConfig;
        private readonly IVerificationManager _verificationManager;
        private readonly IMediaManager _mediaManager;
        private readonly IMyConvertManager _myConvertManager;
        private readonly IMyCultureManager _myCultureManager;
        private readonly INetworkManager _networkManager;
        private readonly IMapper _mapper;

        public UserManager(IMySecurityManager mySecurityManager, IRetrieveMessageManager retrieveMessageManager,
            IStatisticManager statisticManager,
            IUserRepository userRepository, ITokenManager tokenManager, IUserConfig userConfig, ITokenConfig tokenConfig, IVerificationManager verificationManager,
            INetworkConfig networkConfig, IMediaManager mediaManager, IMyConvertManager myConvertManager, IMyCultureManager myCultureManager,
            INetworkManager networkManager, IMapper mapper)
        {
            _mySecurityManager = mySecurityManager;
            _retrieveMessageManager = retrieveMessageManager;
            _statisticManager = statisticManager;
            _userRepository = userRepository;
            _tokenManager = tokenManager;
            _userConfig = userConfig;
            _tokenConfig = tokenConfig;
            _verificationManager = verificationManager;
            _networkConfig = networkConfig;
            _mediaManager = mediaManager;
            _myConvertManager = myConvertManager;
            _myCultureManager = myCultureManager;
            _networkManager = networkManager;
            _mapper = mapper;
        }

        #region NewUser

        /// <summary>
        ///     Register new User
        /// </summary>
        /// <returns>MyUserToken</returns>
        public NewUserOutput NewUser(NewUserInput newUserInput)
        {
            var newUserOutput = new NewUserOutput();

            if (newUserInput == null)
            {
                throw new ArgumentException("MyNewUser Object Is Null");
            }

            if (string.IsNullOrEmpty(newUserInput.Password))
            {
                throw new ArgumentException("Password Is Null or Empty");
            }

            var passwordHash = _mySecurityManager.GenerateSha1Hash(newUserInput.Password);

            //Check Request
            var verificationErrors = _verificationManager.VerifyNewUserRequest(_mapper.Map<VerifyNewUserRequestInput>(newUserInput));

            var errors = verificationErrors as IList<VerificationError> ?? verificationErrors.ToList();
            if (errors.Any())
            {
                throw new ArgumentException(string.Join(";", errors.Select(x => x.RejectionReason)));
            }

            //Check Username Existence
            if (_verificationManager.UsernameAlreadyExists(_mapper.Map<UsernameAlreadyExistsInput>(newUserInput)).UsernameExists)
            {
                throw new ArgumentException("Username already exist");
            }

            //Check Email Existence
            if (_verificationManager.EmailAlreadyExists(_mapper.Map<EmailAlreadyExistsInput>(newUserInput)).EmailExists)
            {
                throw new ArgumentException("Email already exist");
            }

            var newUserIn = new NewUserIn
            {
                LanguageId = newUserInput.LanguageId,
                Offset = newUserInput.Offset,
                Email = newUserInput.Email,
                PasswordHash = passwordHash,
                Surname = newUserInput.Surname,
                Name = newUserInput.Name,
                Mobile = newUserInput.Mobile,
                ContractSigned = newUserInput.ContractSigned,
                Ip = newUserInput.Ip,
                Username = newUserInput.Username,
                DateOfBirth = newUserInput.DateOfBirth.Date,
                GenderId = newUserInput.GenderId,
                CityId = newUserInput.CityId
            };

            var newUserOut = _userRepository.NewUser(newUserIn);

            if (newUserOut.isError)
            {
                throw new Exception(_retrieveMessageManager.RetrieveDbMessage(newUserInput.LanguageId, newUserOut.ResultExecutionCode));
            }

            Guid tryParseResult;
            if (!Guid.TryParse(newUserOut.USPReturnValue, out tryParseResult))
            {
                throw new Exception("USPReturnValue is not a Guid!");
            }

            newUserOutput.UserId = tryParseResult;

            var newTokenInput = new NewTokenInput
            {
                UserId = newUserOutput.UserId,
                TokenExpireMinutes = _tokenConfig.TokenExpireMinutes,
                Source = null
            };

            newUserOutput.UserToken = _tokenManager.NewToken(newTokenInput).UserToken;

            var sendEmailToConfirmRegistrationInput = _mapper.Map<SendEmailToConfirmRegistrationInput>(newUserInput);
            sendEmailToConfirmRegistrationInput.UserId = newUserOutput.UserId;

            SendEmailToConfirmRegistration(sendEmailToConfirmRegistrationInput);

            //WRITE A ROW IN STATISTICS DB
            var newStatistic = new NewStatisticInput
            {
                Comments = "New Registration",
                FileOrigin = "",
                StatisticsActionType = StatisticsActionType.US_NewRegistration,
                IdUser = newUserOutput.UserId
            };

            _statisticManager.NewStatistic(newStatistic);

            return newUserOutput;
        }

        #endregion

        #region SendEmailToConfirmRegistration

        public SendEmailToConfirmRegistrationOutput SendEmailToConfirmRegistration(SendEmailToConfirmRegistrationInput sendEmailToConfirmRegistrationInput)
        {
            var sendEmailToConfirmRegistrationOutput = new SendEmailToConfirmRegistrationOutput();

            //TODO: IMPROVE

            //EmailPasswordHash will be send by email to new user.
            //It will be use to confirm user registration as well.
            //var passwordHash = _mySecurityManager.GenerateSha1Hash(sendEmailToConfirmRegistrationInput.Password);
            //var emailPasswordHash = _mySecurityManager.GenerateSha1Hash(sendEmailToConfirmRegistrationInput.Email + passwordHash);

            var confirmationCode = CreateConfirmationCode(sendEmailToConfirmRegistrationInput.Password, sendEmailToConfirmRegistrationInput.Email);

            //Link to activate account sent by email
            var linkEncoded = $"#/email/confirm?Id={HttpUtility.UrlEncode(sendEmailToConfirmRegistrationInput.UserId.ToString())}&ConfirmationCode={HttpUtility.UrlEncode(confirmationCode)}";
            //var linkEncoded = _networkManager.GetCurrentPathUrl() + HttpUtility.UrlEncode(link);
            //var linkEncoded = HttpUtility.UrlEncode(linkWithParameters);

            //if (linkEncoded == null) return;
            var baseUri = new Uri(_networkConfig.WebUrl);
            var link = new Uri(baseUri, linkEncoded);

            var from = _userConfig.EmailFromProfileUser;
            var to = sendEmailToConfirmRegistrationInput.Email;
            var subject = _retrieveMessageManager.RetrieveDbMessage(sendEmailToConfirmRegistrationInput.LanguageId, "US-IN-0022");
            //var message = "/PagesForEmail/WelcomeUser.aspx?link=" + linkEncoded;
            //var message = "Welcome to MyCookin. Please click here to" + linkEncoded;

            var message = $"{_retrieveMessageManager.RetrieveDbMessage(sendEmailToConfirmRegistrationInput.LanguageId, "US-IN-0004")}{link}";

            var saveEmailToSendInput = new SaveEmailToSendInput
            {
                Message = message,
                Bcc = null,
                Cc = null,
                From = from,
                HtmlFilePath = null,
                Subject = subject,
                To = to
            };

            var saveEmailToSendOutput = _networkManager.SaveEmailToSend(saveEmailToSendInput);

            sendEmailToConfirmRegistrationOutput.EmailSent = saveEmailToSendOutput.EmailSaved;

            return sendEmailToConfirmRegistrationOutput;
        }

        #endregion

        #region SendEmailToResetPassword

        public SendEmailToResetPasswordOutput SendEmailToResetPassword(SendEmailToResetPasswordInput sendEmailToResetPasswordInput)
        {
            var sendEmailToResetPasswordOutput = new SendEmailToResetPasswordOutput();

            //TODO: IMPROVE

            //Link to activate account sent by email
            var linkEncoded =
                $"#/password/reset?Id={HttpUtility.UrlEncode(sendEmailToResetPasswordInput.UserId.ToString())}&ConfirmationCode={HttpUtility.UrlEncode(sendEmailToResetPasswordInput.ConfirmationCode)}";
            //var linkEncoded = _networkManager.GetCurrentPathUrl() + HttpUtility.UrlEncode(link);
            //var linkEncoded = HttpUtility.UrlEncode(linkWithParameters);

            //if (linkEncoded == null) return;
            var baseUri = new Uri(_networkConfig.WebUrl);
            var link = new Uri(baseUri, linkEncoded);

            var from = _userConfig.EmailFromProfileUser;
            var to = sendEmailToResetPasswordInput.Email;
            var subject = _retrieveMessageManager.RetrieveDbMessage(sendEmailToResetPasswordInput.LanguageId, "US-IN-0016");

            var message = $"<a href=\"{link}\">{_retrieveMessageManager.RetrieveDbMessage(sendEmailToResetPasswordInput.LanguageId, "US-IN-0016")}</a>";

            var saveEmailToSendInput = new SaveEmailToSendInput
            {
                Message = message,
                Bcc = null,
                Cc = null,
                From = from,
                HtmlFilePath = null,
                Subject = subject,
                To = to
            };

            var saveEmailToSendOutput = _networkManager.SaveEmailToSend(saveEmailToSendInput);

            sendEmailToResetPasswordOutput.EmailSent = saveEmailToSendOutput.EmailSaved;

            return sendEmailToResetPasswordOutput;
        }

        #endregion

        #region LoginUser

        /// <summary>
        ///     Login User
        /// </summary>
        /// <param name="loginUserInput"></param>
        /// <returns>MyUserToken</returns>
        public LoginUserOutput LoginUser(LoginUserInput loginUserInput)
        {
            var loginUserOutput = new LoginUserOutput();

            if (loginUserInput == null)
            {
                throw new ArgumentException("MyLogin Object Empty");
            }

            var verificationErrors = _verificationManager.VerifyNewUserLoginRequest(_mapper.Map<VerifyNewUserLoginRequestInput>(loginUserInput));

            var errors = verificationErrors as IList<VerificationError> ?? verificationErrors.ToList();
            if (errors.Any())
            {
                throw new ArgumentException(string.Join(";", errors.Select(x => x.RejectionReason)));
            }

            var passwordHash = !loginUserInput.IsPasswordHashed ? _mySecurityManager.GenerateSha1Hash(loginUserInput.Password) : loginUserInput.Password;

            var loginUserIn = _mapper.Map<LoginUserIn>(loginUserInput);

            loginUserIn.PasswordHash = passwordHash;

            var loginUserOut = _userRepository.LoginUser(loginUserIn);

            if (loginUserOut.isError)
            {
                throw new ArgumentException(_retrieveMessageManager.RetrieveDbMessage(loginUserInput.LanguageId, loginUserOut.ResultExecutionCode));
            }

            loginUserOutput.UserId = new Guid(loginUserOut.USPReturnValue);

            var newTokenInput = new NewTokenInput
            {
                UserId = loginUserOutput.UserId,
                TokenExpireMinutes = _tokenConfig.TokenExpireMinutes,
                Source = null
            };

            loginUserOutput.UserToken = _tokenManager.NewToken(newTokenInput).UserToken;

            //UpdateLastLogonAndSetUserAsOnLine(myUserToken.UserId);

            //SetLoginSessionVariables(myUserToken.UserId);

            //WRITE A ROW IN STATISTICS DB - not necessary here

            return loginUserOutput;
        }

        #endregion

        #region LoginUserById

        public LoginUserByIdOutput LoginUserById(LoginUserByIdInput loginUserByIdInput)
        {
            return _mapper.Map<LoginUserByIdOutput>(_userRepository.LoginUserById(_mapper.Map<LoginUserByIdIn>(loginUserByIdInput)));
        }

        #endregion

        #region UpdateLastLogonAndSetUserAsOnLine

        public UpdateLastLogonAndSetUserAsOnLineOutput UpdateLastLogonAndSetUserAsOnLine(UpdateLastLogonAndSetUserAsOnLineInput updateLastLogonAndSetUserAsOnLineInput)
        {
            var updateLastLogonAndSetUserAsOnLineOutput = new UpdateLastLogonAndSetUserAsOnLineOutput
            {
                ExecutionResult = !_userRepository.UpdateLastLogonAndSetUserAsOnLine(_mapper.Map<UpdateLastLogonAndSetUserAsOnLineIn>(updateLastLogonAndSetUserAsOnLineInput)).isError
            };

            return updateLastLogonAndSetUserAsOnLineOutput;
        }

        #endregion

        #region LogoutUser

        public LogoutUserOutput LogoutUser(LogoutUserInput logoutUserInput)
        {
            var logoutUserOutput = new LogoutUserOutput
            {
                UserLoggedOut = false
            };

            logoutUserOutput = _mapper.Map<LogoutUserOutput>(_userRepository.LogoutUser(_mapper.Map<LogoutUserIn>(logoutUserInput)));

            if (!logoutUserOutput.UserLoggedOut)
            {
                return logoutUserOutput;
            }

            //WRITE A ROW IN STATISTICS DB
            var newStatistic = new NewStatisticInput
            {
                Comments = "Logout",
                FileOrigin = "",
                StatisticsActionType = StatisticsActionType.US_Logout,
                OtherInfo = $"Token: {logoutUserInput.UserToken}"
            };

            _statisticManager.NewStatistic(newStatistic);

            return logoutUserOutput;
        }

        #endregion

        #region UserById

        public UserByIdOutput UserById(UserByIdInput userByIdInput, bool avoidCheckToken = false)
        {
            if (!avoidCheckToken)
            {
                //Check for Valid Token
                _tokenManager.CheckToken(userByIdInput.CheckTokenInput);
            }

            var userByIdOutput = _mapper.Map<UserByIdOutput>(_userRepository.UserById(_mapper.Map<UserByIdIn>(userByIdInput)));

            if (userByIdOutput == null)
            {
                return null;
            }

            //Add Images
            userByIdOutput.ImageUrl = userByIdOutput.ProfilePhotoId == null
                ? null
                : _mediaManager.MediaPathByMediaId(new MediaPathByMediaIdInput {MediaId = (Guid) userByIdOutput.ProfilePhotoId}).MediaPath;

            return userByIdOutput;
        }

        #endregion

        #region UserByEmail

        public UserByEmailOutput UserByEmail(UserByEmailInput userByEmailInput)
        {
            var userByEmailOutput = _mapper.Map<UserByEmailOutput>(_userRepository.UserByEmail(_mapper.Map<UserByEmailIn>(userByEmailInput)));

            if (userByEmailOutput == null)
            {
                return null;
            }

            //Add Images
            userByEmailOutput.ImageUrl = userByEmailOutput.ProfilePhotoId == null
                ? null
                : _mediaManager.MediaPathByMediaId(new MediaPathByMediaIdInput {MediaId = (Guid) userByEmailOutput.ProfilePhotoId}).MediaPath;

            return userByEmailOutput;
        }

        #endregion

        #region UserIdByUserToken

        public UserIdByUserTokenOutput UserIdByUserToken(UserIdByUserTokenInput userIdByUserTokenInput)
        {
            return _mapper.Map<UserIdByUserTokenOutput>(_userRepository.UserIdByTokenId(_mapper.Map<UserIdByUserTokenIn>(userIdByUserTokenInput)));
        }

        #endregion

        #region ActivateUser

        /// <summary>
        ///     Activate user and set email as confirmed
        /// </summary>
        /// <param name="activateUserInput"></param>
        /// <returns></returns>
        public ActivateUserOutput ActivateUser(ActivateUserInput activateUserInput)
        {
            var activateUserOutput = new ActivateUserOutput {ExecutionResult = false};

            //Get Email + Password Hash according to IDUser
            var myUser = UserById(_mapper.Map<UserByIdInput>(activateUserInput), true);

            var emailPasswordHash = _mySecurityManager.GenerateSha1Hash(myUser.Email + myUser.PasswordHash);

            if (!activateUserInput.ConfirmationCode.Equals(emailPasswordHash)) return activateUserOutput;

            var activateUserIn = _mapper.Map<ActivateUserIn>(activateUserInput);
            activateUserIn.AccountExpireOn = DateTime.UtcNow.AddYears(_userConfig.YearsAfterAccountExpire);

            var activateResult = _userRepository.ActivateUser(activateUserIn);

            //TODO: GET MESSAGE FROM CODE AND RETURN!

            if (activateResult.isError)
            {
                throw new Exception("Error on USP_ActivateUser");
            }

            //WRITE A ROW IN STATISTICS DB

            var newStatistic = new NewStatisticInput
            {
                Comments = "User Activated",
                FileOrigin = "",
                IdRelatedObject = null,
                IdUser = activateUserInput.UserId,
                StatisticsActionType = StatisticsActionType.US_UserActivated
            };

            _statisticManager.NewStatistic(newStatistic);

            activateUserOutput.ExecutionResult = true;

            return activateUserOutput;
        }

        #endregion

        #region UserProfilePath

        public UserProfilePathOutput UserProfilePath(UserProfilePathInput userProfilePathInput)
        {
            var baseUri = new Uri(_networkConfig.WebUrl);

            var myUser = UserById(_mapper.Map<UserByIdInput>(userProfilePathInput));

            var url = new Uri(baseUri, $"{_networkConfig.RoutingUser}{myUser.Username}");

            return new UserProfilePathOutput {ProfilePath = url.ToString()};
        }

        #endregion

        #region SecurityQuestionsByLanguage

        public IEnumerable<SecurityQuestionsByLanguageOutput> SecurityQuestionsByLanguage(SecurityQuestionsByLanguageInput securityQuestionsByLanguageInput)
        {
            return
                _mapper.Map<IEnumerable<SecurityQuestionsByLanguageOutput>>(_userRepository.SecurityQuestionsByLanguage(_mapper.Map<SecurityQuestionsByLanguageIn>(securityQuestionsByLanguageInput)));
        }

        #endregion

        #region GendersByLanguage

        public IEnumerable<GenderByLanguageOutput> GendersByLanguage(GenderByLanguageInput genderByLanguageInput)
        {
            return _mapper.Map<IEnumerable<GenderByLanguageOutput>>(_userRepository.GendersByLanguage(_mapper.Map<GenderByLanguageIn>(genderByLanguageInput)));
        }

        #endregion

        #region DeleteAccount

        public DeleteAccountOutput DeleteAccount(DeleteAccountInput deleteAccountInput)
        {
            //Check for Valid Token
            var checkTokenOutput = _tokenManager.CheckToken(deleteAccountInput.CheckTokenInput);

            if (!checkTokenOutput.IsTokenValid)
            {
                throw new Exception("Token not valid for the user.");
            }

            return _mapper.Map<DeleteAccountOutput>(_userRepository.DeleteAccount(_mapper.Map<DeleteAccountIn>(deleteAccountInput)));
        }

        #endregion

        #region UpdateUserInfo

        public UpdateUserInfoOutput UpdateUserInfo(UpdateUserInfoInput updateUserInfoInput)
        {
            return _mapper.Map<UpdateUserInfoOutput>(_userRepository.UpdateUserInfo(_mapper.Map<UpdateUserInfoIn>(updateUserInfoInput)));
        }

        #endregion

        #region GetSecurityUserGroups

        public IEnumerable<GetSecurityUserGroupsOutput> GetSecurityUserGroups(GetSecurityUserGroupsInput getSecurityUserGroupsInput)
        {
            //TODO: Check for Valid Token
            //var checkTokenOutput = _tokenManager.CheckToken(deleteAccountInput.CheckTokenInput);

            //if (!checkTokenOutput.IsTokenValid)
            //    throw new Exception("Token not valid for the user.");

            return _mapper.Map<IEnumerable<GetSecurityUserGroupsOutput>>(_userRepository.GetSecurityUserGroups(_mapper.Map<GetSecurityUserGroupsIn>(getSecurityUserGroupsInput)));
        }

        #endregion

        #region UserByUsername

        public UserByUsernameOutput UserByUsername(UserByUsernameInput userByUsernameInput)
        {
            //TODO: Check for Valid Token
            //var checkTokenOutput = _tokenManager.CheckToken(deleteAccountInput.CheckTokenInput);

            //if (!checkTokenOutput.IsTokenValid)
            //    throw new Exception("Token not valid for the user.");

            return _mapper.Map<UserByUsernameOutput>(_userRepository.UserByUsername(_mapper.Map<UserByUsernameIn>(userByUsernameInput)));
        }

        #endregion

        #region LastTimeOnline

        public LastTimeOnlineOutput LastTimeOnline(LastTimeOnlineInput lastTimeOnlineInput)
        {
            //TODO: RESTORE...
            throw new NotImplementedException();

            //var userByIdInput = new UserByIdInput()
            //{
            //    UserId = userId
            //};

            ////var UserRequested = new MyUser(IDUser);
            //var myUser = UserById(userByIdInput);

            //var lastTimeOnline = _retrieveMessageManager.RetrieveDbMessage(Convert.ToInt32(languageId), "US-IN-0028");

            ////Check if user is online
            ////if (!CheckUserLogged())
            //if (myUser.LastLogon < myUser.LastLogout)
            //{
            //    if (string.IsNullOrEmpty(myUser.LastLogon.ToString()))
            //    {
            //        lastTimeOnline += " " +
            //                          $"{_myConvertManager.ToLocalTime(Convert.ToDateTime(myUser.DateMembership), offset):dd/MM/yyyy HH:mm}";
            //    }
            //    else
            //    {
            //        lastTimeOnline += " " +
            //                          $"{_myConvertManager.ToLocalTime(Convert.ToDateTime(myUser.LastLogout), offset):dd/MM/yyyy HH:mm}";
            //    }
            //}
            //else
            //{
            //    lastTimeOnline = "Online";
            //}

            //return lastTimeOnline;
        }

        #endregion

        #region GetLanguageList

        public GetLanguageListOutput GetLanguageList(GetLanguageListInput getLanguageListInput)
        {
            //TODO: Check for Valid Token
            //var checkTokenOutput = _tokenManager.CheckToken(deleteAccountInput.CheckTokenInput);

            //if (!checkTokenOutput.IsTokenValid)
            //    throw new Exception("Token not valid for the user.");

            return _mapper.Map<GetLanguageListOutput>(_userRepository.GetLanguageList(_mapper.Map<GetLanguageListIn>(getLanguageListInput)));
        }

        #endregion

        #region NumberOfUsers

        public NumberOfUsersOutput NumberOfUsers(NumberOfUsersInput numberOfUsersInput)
        {
            //TODO: Check for Valid Token
            //var checkTokenOutput = _tokenManager.CheckToken(deleteAccountInput.CheckTokenInput);

            //if (!checkTokenOutput.IsTokenValid)
            //    throw new Exception("Token not valid for the user.");

            return _mapper.Map<NumberOfUsersOutput>(_userRepository.NumberOfUsers(_mapper.Map<NumberOfUsersIn>(numberOfUsersInput)));
        }

        #endregion

        #region UpdatePassword

        public UpdatePasswordOutput UpdatePassword(UpdatePasswordInput updatePasswordInput)
        {
            var checkForValidResetPasswordProcessOutput = CheckForValidResetPasswordProcess(_mapper.Map<CheckForValidResetPasswordProcessInput>(updatePasswordInput));

            if (!checkForValidResetPasswordProcessOutput.IsValid)
            {
                throw new Exception("Wrong Confirmation Code or Id");
            }

            //UPDATE ConfirmationCode in User Table (column SecurityAnswer reset to null)
            UpdateTemporarySecurityAnswer(_mapper.Map<UpdateTemporarySecurityAnswerInput>(updatePasswordInput));

            var updatePasswordIn = _mapper.Map<UpdatePasswordIn>(updatePasswordInput);

            updatePasswordIn.NewPasswordHash = _mySecurityManager.GenerateSha1Hash(updatePasswordInput.NewPassword);

            return _mapper.Map<UpdatePasswordOutput>(_userRepository.UpdatePassword(updatePasswordIn));
        }

        #endregion

        #region GetSecurityQuestion

        public GetSecurityQuestionOutput GetSecurityQuestion(GetSecurityQuestionInput getSecurityQuestionInput)
        {
            //TODO: Check for Valid Token
            //var checkTokenOutput = _tokenManager.CheckToken(deleteAccountInput.CheckTokenInput);

            //if (!checkTokenOutput.IsTokenValid)
            //    throw new Exception("Token not valid for the user.");

            return _mapper.Map<GetSecurityQuestionOutput>(_userRepository.GetSecurityQuestion(_mapper.Map<GetSecurityQuestionIn>(getSecurityQuestionInput)));
        }

        #endregion

        #region UpdateLastProfileUpdateDate

        public UpdateLastProfileUpdateDateOutput UpdateLastProfileUpdateDate(UpdateLastProfileUpdateDateInput updateLastProfileUpdateDateInput)
        {
            //TODO: Check for Valid Token
            //var checkTokenOutput = _tokenManager.CheckToken(deleteAccountInput.CheckTokenInput);

            //if (!checkTokenOutput.IsTokenValid)
            //    throw new Exception("Token not valid for the user.");

            return _mapper.Map<UpdateLastProfileUpdateDateOutput>(_userRepository.UpdateLastProfileUpdateDate(_mapper.Map<UpdateLastProfileUpdateDateIn>(updateLastProfileUpdateDateInput)));
        }

        #endregion

        #region GenerateNewTemporaryCode

        public GenerateNewTemporaryCodeOutput GenerateNewTemporaryCode(GenerateNewTemporaryCodeInput generateNewTemporaryCodeInput)
        {
            var newGuid = new Guid();
            var newGuidString = newGuid.ToString();

            var temporaryCode = _mySecurityManager.GenerateSha1Hash(newGuidString + generateNewTemporaryCodeInput.UserId + generateNewTemporaryCodeInput.PasswordHash);

            return new GenerateNewTemporaryCodeOutput
            {
                TemporaryCode = temporaryCode,
                UserId = generateNewTemporaryCodeInput.UserId
            };
        }

        #endregion

        #region UpdateTemporarySecurityAnswer

        /// <summary>
        ///     Update Temporary Security Answer
        /// </summary>
        /// <param name="updateTemporarySecurityAnswerInput">Sometimes NULL to reset after recovery password</param>
        /// <returns></returns>
        public UpdateTemporarySecurityAnswerOutput UpdateTemporarySecurityAnswer(UpdateTemporarySecurityAnswerInput updateTemporarySecurityAnswerInput)
        {
            return _mapper.Map<UpdateTemporarySecurityAnswerOutput>(_userRepository.UpdateTemporarySecurityAnswer(_mapper.Map<UpdateTemporarySecurityAnswerIn>(updateTemporarySecurityAnswerInput)));
        }

        #endregion

        #region UpdateSecurityAnswer

        public UpdateSecurityAnswerOutput UpdateSecurityAnswer(UpdateSecurityAnswerInput updateSecurityAnswerInput)
        {
            //TODO: Check for Valid Token
            //var checkTokenOutput = _tokenManager.CheckToken(deleteAccountInput.CheckTokenInput);

            //if (!checkTokenOutput.IsTokenValid)
            //    throw new Exception("Token not valid for the user.");

            return _mapper.Map<UpdateSecurityAnswerOutput>(_userRepository.UpdateSecurityAnswer(_mapper.Map<UpdateSecurityAnswerIn>(updateSecurityAnswerInput)));
        }

        #endregion

        #region GetIdGenderByGenderNameAndIdLanguage

        public GetIdGenderByGenderNameAndIdLanguageOutput GetIdGenderByGenderNameAndIdLanguage(GetIdGenderByGenderNameAndIdLanguageInput getIdGenderByGenderNameAndIdLanguageInput)
        {
            //TODO: Check for Valid Token
            //var checkTokenOutput = _tokenManager.CheckToken(deleteAccountInput.CheckTokenInput);

            //if (!checkTokenOutput.IsTokenValid)
            //    throw new Exception("Token not valid for the user.");

            return
                _mapper.Map<GetIdGenderByGenderNameAndIdLanguageOutput>(
                    _userRepository.GetIdGenderByGenderNameAndIdLanguage(_mapper.Map<GetIdGenderByGenderNameAndIdLanguageIn>(getIdGenderByGenderNameAndIdLanguageInput)));
        }

        #endregion

        #region DeleteSecurityQuestionAndAnswer

        public DeleteSecurityQuestionAndAnswerOutput DeleteSecurityQuestionAndAnswer(DeleteSecurityQuestionAndAnswerInput deleteSecurityQuestionAndAnswerInput)
        {
            //TODO: Check for Valid Token
            //var checkTokenOutput = _tokenManager.CheckToken(deleteAccountInput.CheckTokenInput);

            //if (!checkTokenOutput.IsTokenValid)
            //    throw new Exception("Token not valid for the user.");

            return
                _mapper.Map<DeleteSecurityQuestionAndAnswerOutput>(_userRepository.DeleteSecurityQuestionAndAnswer(_mapper.Map<DeleteSecurityQuestionAndAnswerIn>(deleteSecurityQuestionAndAnswerInput)));
        }

        #endregion

        #region FindUserByWords

        public IEnumerable<FindUserByWordsOutput> FindUserByWords(FindUserByWordsInput findUserByWordsInput)
        {
            //TODO: Check for Valid Token
            //var checkTokenOutput = _tokenManager.CheckToken(deleteAccountInput.CheckTokenInput);

            //if (!checkTokenOutput.IsTokenValid)
            //    throw new Exception("Token not valid for the user.");

            return _mapper.Map<IEnumerable<FindUserByWordsOutput>>(_userRepository.FindUserByWords(_mapper.Map<FindUserByWordsIn>(findUserByWordsInput)));
        }

        #endregion

        #region IsValidPassword

        public IsValidPasswordOutput IsValidPassword(IsValidPasswordInput isValidPasswordInput)
        {
            //TODO: RESTORE...
            throw new NotImplementedException();

            //var userByIdInput = new UserByIdInput()
            //{
            //    UserId = userId
            //};

            //var myUser = UserById(userByIdInput);

            //var isValidPassword = false;

            //try
            //{
            //    //Check if is set ChangePasswordNextLogon
            //    isValidPassword = !Convert.ToBoolean(myUser.ChangePasswordNextLogon);

            //    //Check if password is expired
            //    isValidPassword = DateTime.Compare(DateTime.UtcNow, Convert.ToDateTime(myUser.PasswordExpireOn)) <= 0;
            //}
            //catch
            //{
            //}

            //return isValidPassword;
        }

        #endregion

        #region ProfileCompletePercentageCalc

        public ProfileCompletePercentageCalcOutput ProfileCompletePercentageCalc(ProfileCompletePercentageCalcInput profileCompletePercentageCalcInput)
        {
            //TODO: RESTORE...
            throw new NotImplementedException();

            //double percentage = 0;
            //var totProperties = 0;
            //var compiledByUser = 0;

            //try
            //{
            //    var userByIdInput = new UserByIdInput()
            //    {
            //        UserId = userId
            //    };

            //    var myUser = UserById(userByIdInput);

            //    //var userProfilePropertiesId = _userConfig.UserProfilePropertiesId;

            //    //var propertiesId = userProfilePropertiesId.Split(',');

            //    //foreach (var propertyId in propertiesId)
            //    //{
            //    //    var propList = GetAllMyUserPropertyByCategory(Convert.ToInt32(propertyId),
            //    //        languageId);

            //    //    totProperties += propList.Count;

            //    //    compiledByUser = _myUserPropertyCompiledManager.GetCountPropertyCompiledByUser(userId).Rows.Count;
            //    //}

            //    //Add the number of the fields of personal user info (of the table Users) 
            //    totProperties += 7;
            //    if (!string.IsNullOrEmpty(myUser.Name))
            //    {
            //        compiledByUser += 1;
            //    }
            //    if (!string.IsNullOrEmpty(myUser.Surname))
            //    {
            //        compiledByUser += 1;
            //    }
            //    if (!string.IsNullOrEmpty(myUser.Username))
            //    {
            //        compiledByUser += 1;
            //    }
            //    if (!string.IsNullOrEmpty(myUser.Mobile))
            //    {
            //        compiledByUser += 1;
            //    }
            //    if (!string.IsNullOrEmpty(myUser.DateOfBirth.ToString()))
            //    {
            //        compiledByUser += 1;
            //    }
            //    if (!string.IsNullOrEmpty(myUser.GenderId.ToString()))
            //    {
            //        compiledByUser += 1;
            //    }
            //    if (!string.IsNullOrEmpty(myUser.CityId.ToString()))
            //    {
            //        compiledByUser += 1;
            //    }

            //    percentage = ((Convert.ToDouble(compiledByUser) / Convert.ToDouble(totProperties)) * 100);
            //}
            //catch (Exception ex)
            //{
            //    //Error calculating Percentage
            //    //WRITE A ROW IN LOG FILE AND DB
            //    try
            //    {
            //        var logRow = new LogRowIn()
            //        {
            //            ErrorMessage = $"Error in ProfileCompletePercentageCalc: {ex.Message}",
            //            ErrorMessageCode = "US-ER-9999",
            //            ErrorSeverity = LogLevel.CriticalErrors,
            //            FileOrigin = _networkManager.GetCurrentPageName(),
            //            IdUser = userId.ToString(),
            //        };

            //        _logManager.WriteLog(logRow);
            //    }
            //    catch
            //    {
            //    }
            //}

            //return percentage;
        }

        #endregion

        #region SuggestedUsers

        public IEnumerable<SuggestedUsersOutput> SuggestedUsers(SuggestedUsersInput suggestedUsersInput)
        {
            //TODO: Check for Valid Token
            //var checkTokenOutput = _tokenManager.CheckToken(deleteAccountInput.CheckTokenInput);

            //if (!checkTokenOutput.IsTokenValid)
            //    throw new Exception("Token not valid for the user.");

            return _mapper.Map<IEnumerable<SuggestedUsersOutput>>(_userRepository.SuggestedUsers(_mapper.Map<SuggestedUsersIn>(suggestedUsersInput)));
        }

        #endregion

        #region NewUsersForMailchimp

        public IEnumerable<NewUsersForMailchimpOutput> NewUsersForMailchimp(NewUsersForMailchimpInput newUsersForMailchimpInput)
        {
            //TODO: Check for Valid Token
            //var checkTokenOutput = _tokenManager.CheckToken(deleteAccountInput.CheckTokenInput);

            //if (!checkTokenOutput.IsTokenValid)
            //    throw new Exception("Token not valid for the user.");

            return _mapper.Map<IEnumerable<NewUsersForMailchimpOutput>>(_userRepository.NewUsersForMailchimp(_mapper.Map<NewUsersForMailchimpIn>(newUsersForMailchimpInput)));
        }

        #endregion

        #region NumberOfProfileVisits

        public NumberOfProfileVisitsOutput NumberOfProfileVisits(NumberOfProfileVisitsInput numberOfProfileVisitsInput)
        {
            //TODO: RESTORE FROM STATISTICS
            throw new NotImplementedException();

            //var numberOfProfileVisits = _retrieveMessageManager.RetrieveDbMessage(Convert.ToInt32(languageId), "US-IN-0030");

            ////TODO: RESTORE FROM STATISTICS

            ////var NewStat = new DBStatisticsEntities();

            ////ObjectResult uspResult = NewStat.USP_CountByIDRelatedObjectAndActionType(userId,
            ////    (int) StatisticsActionType.US_ProfileViewed);

            ////TODO: Continue..

            ////numberOfProfileVisits += uspResult; // ???????????

            //return numberOfProfileVisits;
        }

        #endregion

        #region ResetPasswordProcess

        public ResetPasswordProcessOutput ResetPasswordProcess(ResetPasswordProcessInput resetPasswordProcessInput)
        {
            var resetPasswordProcessOutput = new ResetPasswordProcessOutput();

            var userByEmailOutput = UserByEmail(_mapper.Map<UserByEmailInput>(resetPasswordProcessInput));

            //Check If Email is Correct
            if (userByEmailOutput == null)
            {
                throw new Exception("Email not found"); //TODO: "US-WN-0003"
            }

            //If IDSecurityQuestion is set, email is not necessary.
            //Could be change password with Security Answer
            //TODO: IMPLEMENT CASE WHERE A SECURITY QUESTION IS SET
            //if (string.IsNullOrEmpty(userByEmailOutput.SecurityQuestionId.ToString())) //IDSecurityQuestion NOT set
            //{
            //User needs a code to reset psw (sent by email or shown in link)
            //ResetPassword will check the confirmationCode generated by HASH[(SecurityAnswer + IDUser + PasswordHash)]

            //Generate New TemporaryCode
            //var temporaryCode = GenerateNewTemporaryCode(userByEmailOutput.UserId, userByEmailOutput.PasswordHash);
            var generateNewTemporaryCodeOutput = GenerateNewTemporaryCode(_mapper.Map<GenerateNewTemporaryCodeInput>(userByEmailOutput));

            //SecurityAnswer Column is not yet completed, then insert this code in that column.
            //UPDATE ConfirmationCode in User Table (column SecurityAnswer)
            var updateTemporarySecurityAnswerOutput = UpdateTemporarySecurityAnswer(_mapper.Map<UpdateTemporarySecurityAnswerInput>(generateNewTemporaryCodeOutput));

            if (!updateTemporarySecurityAnswerOutput.TemporarySecurityAnswerUpdated)
            {
                throw new Exception("Error Updating TemporarySecurityAnswer");
            }

            //ConfirmationCode will be HASH[(SecurityAnswer (BUT FOR NOW IS TEMPORARYCODE) + IDUser + PasswordHash)]
            var confirmationCode = _mySecurityManager.GenerateSha1Hash($"{generateNewTemporaryCodeOutput.TemporaryCode}{generateNewTemporaryCodeOutput.UserId}{userByEmailOutput.PasswordHash}");

            //Link to Reset Password sent by email
            var sendEmailToResetPasswordInput = new SendEmailToResetPasswordInput
            {
                LanguageId = _myConvertManager.ToInt32(userByEmailOutput.LanguageId, 1),
                UserId = generateNewTemporaryCodeOutput.UserId,
                ConfirmationCode = confirmationCode,
                Email = resetPasswordProcessInput.Email
            };

            var sendEmailToResetPasswordOutput = SendEmailToResetPassword(sendEmailToResetPasswordInput);

            if (!sendEmailToResetPasswordOutput.EmailSent)
            {
                throw new Exception("Email to recover password not Sent"); //TODO: MANAGE!
            }
            //}
            //else //IDSecurityQuestion is set.
            //{
            //TODO: CONTINUE IMPLEMENTATION...
            //    //Get Security Question Text
            //    var getSecurityQuestionInput = new GetSecurityQuestionInput()
            //    {
            //        LanguageId = _myConvertManager.ToInt32(userByEmailOutput.LanguageId, 1),
            //        UserId = userByEmailOutput.UserId
            //    };

            //    var securityQuestion = GetSecurityQuestion(getSecurityQuestionInput);
            //}

            resetPasswordProcessOutput.ResetPasswordProcessCompleted = true;

            return resetPasswordProcessOutput;
        }

        #endregion

        #region CheckForValidResetPasswordProcess

        public CheckForValidResetPasswordProcessOutput CheckForValidResetPasswordProcess(CheckForValidResetPasswordProcessInput checkForValidResetPasswordProcessInput)
        {
            //Get User Info by Id
            var userIdInput = new UserByIdInput
            {
                CheckTokenInput = new CheckTokenInput
                {
                    UserId = checkForValidResetPasswordProcessInput.UserId
                }
            };

            var userByIdOutput = UserById(userIdInput, true);

            //Check ConfirmationCode that will be HASH[(SecurityAnswer (BUT FOR NOW IS TEMPORARYCODE) + IDUser + PasswordHash)]
            var confirmationCodeVerify = _mySecurityManager.GenerateSha1Hash($"{userByIdOutput.SecurityAnswer}{userByIdOutput.UserId}{userByIdOutput.PasswordHash}");

            if (!checkForValidResetPasswordProcessInput.ConfirmationCode.Equals(confirmationCodeVerify)) return new CheckForValidResetPasswordProcessOutput {IsValid = false};

            //UPDATE ConfirmationCode in User Table (column SecurityAnswer reset to null)
            //UpdateTemporarySecurityAnswer(_mapper.Map<UpdateTemporarySecurityAnswerInput>(checkForValidResetPasswordProcessInput));

            //Return OK
            return new CheckForValidResetPasswordProcessOutput {IsValid = true};
        }

        #endregion

        //public FacebookLoginOutput FacebookLogin(FacebookLoginInput facebookLoginInput)
        //{
        //    var userIdFromSocialLoginsInput = new UserIdFromSocialLoginsInput()
        //    {
        //        SocialNetworkId = 2,
        //        UserIdOnSocial = facebookLoginInput.Id
        //    };

        //    var userIdFromSocialLoginsOutput = _socialManager.UserIdFromSocialLogins(userIdFromSocialLoginsInput);

        //    Guid userId;

        //    //USER ALREADY LOGGED VIA SOCIAL, DO LOGIN
        //    if (userIdFromSocialLoginsOutput != null)
        //    {
        //        if (userIdFromSocialLoginsOutput.UserId == null)
        //            throw new Exception("User Id Null in FacebookLogin");

        //        userId = (Guid) userIdFromSocialLoginsOutput.UserId;
        //    }
        //    else
        //    {
        //        var randomPassword = Guid.NewGuid().ToString();

        //        //Check if the user is already registered by Email
        //        var userByEmailOutput = UserByEmail(_mapper.Map<UserByEmailInput>(facebookLoginInput));

        //        if (userByEmailOutput == null)
        //        {
        //            var rnd = new Random();

        //            //Register new user
        //            var newUserInput = new NewUserInput()
        //            {
        //                Ip = facebookLoginInput.Ip,
        //                Offset = facebookLoginInput.Timezone,
        //                LanguageId = _myCultureManager.LanguageIdByLanguageCode(facebookLoginInput.Locale.Substring(0, 2)),
        //                Email = facebookLoginInput.Email,
        //                Password = randomPassword,
        //                Username = $"{facebookLoginInput.FirstName}.{facebookLoginInput.LastName}.{rnd.Next(100, 999)}", //TODO: IMPROVE
        //                CityId = 1, //TODO: IMPROVE
        //                ContractSigned = true,
        //                DateOfBirth = facebookLoginInput.Birthday,
        //                GenderId = facebookLoginInput.Gender.Equals("female") ? 2 : 1,
        //                Mobile = null,
        //                Name = facebookLoginInput.FirstName,
        //                Surname = facebookLoginInput.LastName
        //            };

        //            var newUserOutput = NewUser(newUserInput);

        //            userId = newUserOutput.UserId;
        //        }
        //        else
        //        {
        //            userId = userByEmailOutput.UserId;
        //        }

        //        //Insert Social Login                 
        //        var newSocialLoginInput = new NewSocialLoginInput()
        //        {
        //            UserId = userId,
        //            UserIdOnSocial = facebookLoginInput.Id,
        //            SocialNetworkId = 2,
        //            AccessToken = null,
        //            FriendsRetrievedOn = null,
        //            Link = facebookLoginInput.Link,
        //            Locale = facebookLoginInput.Locale,
        //            PictureUrl = null,
        //            RefreshToken = null,
        //            VerifiedEmail = facebookLoginInput.Verified //TODO: Improve this
        //        };

        //        _socialManager.NewSocialLogin(newSocialLoginInput);

        //        //Activate
        //        var activateUserInput = new ActivateUserInput()
        //        {
        //            UserId = userId,
        //            ConfirmationCode = CreateConfirmationCode(randomPassword, facebookLoginInput.Email),
        //            IpAddress = facebookLoginInput.Ip
        //        };

        //        ActivateUser(activateUserInput);
        //    }

        //    //Login by Id
        //    var loginUserByIdInput = new LoginUserByIdInput()
        //    {
        //        UserId = userId,
        //        Ip = facebookLoginInput.Ip,
        //        Offset = facebookLoginInput.Timezone
        //    };

        //    var loginUserByIdOutput = LoginUserById(loginUserByIdInput);

        //    //CREATE TOKEN
        //    var newTokenInput = new NewTokenInput()
        //    {
        //        UserId = loginUserByIdOutput.UserId,
        //        TokenExpireMinutes = _tokenConfig.TokenExpireMinutes,
        //        Source = null
        //    };

        //    loginUserByIdOutput.UserToken = _tokenManager.NewToken(newTokenInput).UserToken;

        //    //Return
        //    return _mapper.Map<FacebookLoginOutput>(loginUserByIdOutput);
        //}

        #region CreateConfirmationCode

        public string CreateConfirmationCode(string password, string email)
        {
            var passwordHash = _mySecurityManager.GenerateSha1Hash(password);
            return _mySecurityManager.GenerateSha1Hash(email + passwordHash);
        }

        #endregion
    }
}