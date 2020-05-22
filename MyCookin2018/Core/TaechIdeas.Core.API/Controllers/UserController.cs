using System;
using System.Collections.Generic;
using System.Reflection;
using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using TaechIdeas.Core.Core.Common;
using TaechIdeas.Core.Core.Common.Enums;
using TaechIdeas.Core.Core.LogAndMessage;
using TaechIdeas.Core.Core.LogAndMessage.Dto;
using TaechIdeas.Core.Core.Token.Dto;
using TaechIdeas.Core.Core.User;
using TaechIdeas.Core.Core.User.Dto;

namespace TaechIdeas.Core.API.Controllers
{
    /// <summary>
    ///     Definition of API for User
    /// </summary>
    [Route("core")]
    public class UserController : ControllerBase
    {
        private readonly IUserManager _userManager;
        private readonly IUtilsManager _utilsManager;
        private readonly ILogManager _logManager;
        private readonly IMapper _mapper;

        public UserController(IUserManager userManager, IUtilsManager utilsManager, ILogManager logManager, IMapper mapper)
        {
            _userManager = userManager;
            _utilsManager = utilsManager;
            _logManager = logManager;
            _mapper = mapper;
        }

        /// <summary>
        ///     Register New User
        /// </summary>
        /// <param name="newUserRequest"></param>
        /// <returns></returns>
        [HttpPost]
        [Route("user/register")]
        public NewUserResult NewUser(NewUserRequest newUserRequest)
        {
            try
            {
                return _mapper.Map<NewUserResult>(_userManager.NewUser(_mapper.Map<NewUserInput>(newUserRequest)));
            }
            catch (ArgumentException ex)
            {
                //Usually here if there is something wrong with verification (not a proper error)
                var logRowIn = new LogRowIn
                {
                    ErrorMessage = $"{ex.Message}{ex.InnerException ?? ex.InnerException}",
                    ErrorMessageCode = "US-ER-9999",
                    ErrorSeverity = LogLevel.Warnings,
                    FileOrigin = $"{MethodBase.GetCurrentMethod().Name} | {GetType().FullName} | {_utilsManager.GetAllPropertiesAndValues(newUserRequest)}"
                };

                _logManager.WriteLog(logRowIn);

                throw;
            }
            catch (Exception ex)
            {
                var logRowIn = new LogRowIn
                {
                    ErrorMessage = $"{ex.Message}{ex.InnerException ?? ex.InnerException}",
                    ErrorMessageCode = "US-ER-9999",
                    ErrorSeverity = LogLevel.CriticalErrors,
                    FileOrigin = $"{MethodBase.GetCurrentMethod().Name} | {GetType().FullName} | {_utilsManager.GetAllPropertiesAndValues(newUserRequest)}"
                };

                _logManager.WriteLog(logRowIn);

                throw;
            }
        }

        /// <summary>
        ///     Login a user
        /// </summary>
        /// <param name="loginUserRequest"></param>
        /// <returns></returns>
        [HttpPost]
        [Route("user/login")]
        public LoginUserResult LoginUser(LoginUserRequest loginUserRequest)
        {
            try
            {
                return _mapper.Map<LoginUserResult>(_userManager.LoginUser(_mapper.Map<LoginUserInput>(loginUserRequest)));
            }
            catch (ArgumentException ex)
            {
                //Usually here if there is something wrong with verification (not a proper error)
                var logRowIn = new LogRowIn
                {
                    ErrorMessage = $"{ex.Message}{ex.InnerException ?? ex.InnerException}",
                    ErrorMessageCode = "US-ER-9999",
                    ErrorSeverity = LogLevel.Warnings,
                    FileOrigin = $"{MethodBase.GetCurrentMethod().Name} | {GetType().FullName} | {_utilsManager.GetAllPropertiesAndValues(loginUserRequest)}"
                };

                _logManager.WriteLog(logRowIn);

                throw;
            }
            catch (Exception ex)
            {
                var logRowIn = new LogRowIn
                {
                    ErrorMessage = $"{ex.Message}{ex.InnerException ?? ex.InnerException}",
                    ErrorMessageCode = "US-ER-9999",
                    ErrorSeverity = LogLevel.CriticalErrors,
                    FileOrigin = $"{MethodBase.GetCurrentMethod().Name} | {GetType().FullName} | {_utilsManager.GetAllPropertiesAndValues(loginUserRequest)}"
                };

                _logManager.WriteLog(logRowIn);

                throw;
            }
        }

        /// <summary>
        ///     Logout User
        /// </summary>
        /// <param name="logoutUserRequest"></param>
        /// <returns></returns>
        [HttpPost]
        [Route("user/logout")]
        public LogoutUserResult LogoutUser(LogoutUserRequest logoutUserRequest)
        {
            try
            {
                return _mapper.Map<LogoutUserResult>(_userManager.LogoutUser(_mapper.Map<LogoutUserInput>(logoutUserRequest)));
            }
            catch (Exception ex)
            {
                var logRowIn = new LogRowIn
                {
                    ErrorMessage = $"{ex.Message}{ex.InnerException ?? ex.InnerException}",
                    ErrorMessageCode = "US-ER-9999",
                    ErrorSeverity = LogLevel.CriticalErrors,
                    FileOrigin = $"{MethodBase.GetCurrentMethod().Name} | {GetType().FullName} | {_utilsManager.GetAllPropertiesAndValues(logoutUserRequest)}"
                };

                _logManager.WriteLog(logRowIn);

                throw;
            }
        }

        /// <summary>
        /// Get User Id from table Social Logins according to Social Network
        /// </summary>
        /// <param name="userIdOnSocial"></param>
        /// <param name="socialNetworkId"></param>
        /// <returns></returns>
        //[HttpGet]
        //[Route("user/getidfromsociallogins")]
        //public UserIdFromSocialLoginsResult UserIdFromSocialLogins(string userIdOnSocial, int socialNetworkId)
        //{
        //    try
        //    {
        //        var userIdFromSocialLoginsInput = new UserIdFromSocialLoginsInput()
        //        {
        //            SocialNetworkId = socialNetworkId,
        //            UserIdOnSocial = userIdOnSocial
        //        };
        //        return _mapper.Map<UserIdFromSocialLoginsResult>(_socialManager.UserIdFromSocialLogins(userIdFromSocialLoginsInput));
        //    }
        //    catch (Exception ex)
        //    {
        //        var logRowIn = new LogRowIn()
        //        {
        //            ErrorMessage = $"{ex.Message}{ex.InnerException ?? ex.InnerException}",
        //            ErrorMessageCode = "US-ER-9999",
        //            ErrorSeverity = LogLevel.CriticalErrors,
        //            FileOrigin = $"{MethodBase.GetCurrentMethod().Name} | {GetType().FullName} | UserIdOnSocial: {userIdOnSocial}; SocialNetworkId: {socialNetworkId}",
        //        };

        //        _logManager.WriteLog(logRowIn);

        //        throw;
        //    }
        //}

        /// <summary>
        ///     Get User Information by Id
        /// </summary>
        /// <param name="userId"></param>
        /// <param name="userToken"></param>
        /// <returns></returns>
        [HttpGet]
        [Route("user/getuserbyid")]
        public UserByIdResult UserById(Guid userId, Guid userToken)
        {
            var checkTokenRequest = new CheckTokenRequest
            {
                UserId = userId,
                UserToken = userToken
            };

            var userByIdRequest = new UserByIdRequest {CheckTokenRequest = checkTokenRequest};

            try
            {
                return _mapper.Map<UserByIdResult>(_userManager.UserById(_mapper.Map<UserByIdInput>(userByIdRequest)));
            }
            catch (Exception ex)
            {
                var logRowIn = new LogRowIn
                {
                    ErrorMessage = $"{ex.Message}{ex.InnerException ?? ex.InnerException}",
                    ErrorMessageCode = "US-ER-9999",
                    ErrorSeverity = LogLevel.CriticalErrors,
                    FileOrigin = $"{MethodBase.GetCurrentMethod().Name} | {GetType().FullName} | {_utilsManager.GetAllPropertiesAndValues(userByIdRequest)}"
                };

                _logManager.WriteLog(logRowIn);

                throw;
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="updateSocialTokensRequest"></param>
        /// <returns></returns>
        //[HttpPost]
        //[Route("user/updatesocialtokens")]
        //public UpdateSocialTokensResult UpdateSocialTokens(UpdateSocialTokensRequest updateSocialTokensRequest)
        //{
        //    try
        //    {
        //        return _mapper.Map<UpdateSocialTokensResult>(_socialManager.UpdateSocialTokens(_mapper.Map<UpdateSocialTokensInput>(updateSocialTokensRequest)));
        //    }
        //    catch (Exception ex)
        //    {
        //        var logRowIn = new LogRowIn()
        //        {
        //            ErrorMessage = $"{ex.Message}{ex.InnerException ?? ex.InnerException}",
        //            ErrorMessageCode = "US-ER-9999",
        //            ErrorSeverity = LogLevel.CriticalErrors,
        //            FileOrigin = $"{MethodBase.GetCurrentMethod().Name} | {GetType().FullName} | {_utilsManager.GetAllPropertiesAndValues(updateSocialTokensRequest)}",
        //        };

        //        _logManager.WriteLog(logRowIn);

        //        throw;
        //    }
        //}

        /// <summary>
        ///     Get User Information by Email
        /// </summary>
        /// <param name="email"></param>
        /// <returns></returns>
        [HttpGet]
        [Route("user/getuserbyemail")]
        public UserByEmailResult UserByEmail(string email)
        {
            try
            {
                return _mapper.Map<UserByEmailResult>(_userManager.UserByEmail(new UserByEmailInput {Email = email}));
            }
            catch (Exception ex)
            {
                var logRowIn = new LogRowIn
                {
                    ErrorMessage = $"{ex.Message}{ex.InnerException ?? ex.InnerException}",
                    ErrorMessageCode = "US-ER-9999",
                    ErrorSeverity = LogLevel.CriticalErrors,
                    FileOrigin = $"{MethodBase.GetCurrentMethod().Name} | {GetType().FullName} | Email: {email}"
                };

                _logManager.WriteLog(logRowIn);

                throw;
            }
        }

        /// <summary>
        /// Insert User Social Logins info
        /// </summary>
        /// <param name="newSocialLoginRequest"></param>
        /// <returns></returns>
        //[HttpPut]
        //[Route("user/insertsociallogin")]
        //public NewSocialLoginResult NewSocialLogin(NewSocialLoginRequest newSocialLoginRequest)
        //{
        //    try
        //    {
        //        return _mapper.Map<NewSocialLoginResult>(_socialManager.NewSocialLogin(_mapper.Map<NewSocialLoginInput>(newSocialLoginRequest)));
        //    }
        //    catch (Exception ex)
        //    {
        //        var logRowIn = new LogRowIn()
        //        {
        //            ErrorMessage = $"{ex.Message}{ex.InnerException ?? ex.InnerException}",
        //            ErrorMessageCode = "US-ER-9999",
        //            ErrorSeverity = LogLevel.CriticalErrors,
        //            FileOrigin = $"{MethodBase.GetCurrentMethod().Name} | {GetType().FullName} | {_utilsManager.GetAllPropertiesAndValues(newSocialLoginRequest)}",
        //        };

        //        _logManager.WriteLog(logRowIn);

        //        throw;
        //    }
        //}

        /// <summary>
        ///     Get UserId by token
        /// </summary>
        /// <param name="userToken"></param>
        /// <returns></returns>
        [HttpGet]
        [Route("user/getuseridbytoken")]
        public UserIdByUserTokenResult GetUserIdByToken(Guid userToken)
        {
            try
            {
                return _mapper.Map<UserIdByUserTokenResult>(_userManager.UserIdByUserToken(new UserIdByUserTokenInput {UserToken = userToken}));
            }
            catch (Exception ex)
            {
                var logRowIn = new LogRowIn
                {
                    ErrorMessage = $"{ex.Message}{ex.InnerException ?? ex.InnerException}",
                    ErrorMessageCode = "US-ER-9999",
                    ErrorSeverity = LogLevel.CriticalErrors,
                    FileOrigin = $"{MethodBase.GetCurrentMethod().Name} | {GetType().FullName} | UserToken: {userToken}"
                };

                _logManager.WriteLog(logRowIn);

                throw;
            }
        }

        /// <summary>
        ///     GetSecurity Questions By Language
        /// </summary>
        /// <param name="languageId"></param>
        /// <returns></returns>
        [HttpGet]
        [Route("user/securityquestions")]
        public IEnumerable<SecurityQuestionsByLanguageResult> SecurityQuestionsByLanguage(int languageId)
        {
            try
            {
                return _mapper.Map<IEnumerable<SecurityQuestionsByLanguageResult>>(_userManager.SecurityQuestionsByLanguage(new SecurityQuestionsByLanguageInput {LanguageId = languageId}));
            }
            catch (Exception ex)
            {
                var logRowIn = new LogRowIn
                {
                    ErrorMessage = $"{ex.Message}{ex.InnerException ?? ex.InnerException}",
                    ErrorMessageCode = "US-ER-9999",
                    ErrorSeverity = LogLevel.CriticalErrors,
                    FileOrigin = $"{MethodBase.GetCurrentMethod().Name} | {GetType().FullName} | LanguageId: {languageId}"
                };

                _logManager.WriteLog(logRowIn);

                throw;
            }
        }

        /// <summary>
        ///     Get Genders By Language
        /// </summary>
        /// <param name="languageId"></param>
        /// <returns></returns>
        [HttpGet]
        [Route("user/getgenderbylanguage")]
        public IEnumerable<GenderByLanguageResult> GendersByLanguage(int languageId)
        {
            try
            {
                return _mapper.Map<IEnumerable<GenderByLanguageResult>>(_userManager.GendersByLanguage(new GenderByLanguageInput {LanguageId = languageId}));
            }
            catch (Exception ex)
            {
                var logRowIn = new LogRowIn
                {
                    ErrorMessage = $"{ex.Message}{ex.InnerException ?? ex.InnerException}",
                    ErrorMessageCode = "US-ER-9999",
                    ErrorSeverity = LogLevel.CriticalErrors,
                    FileOrigin = $"{MethodBase.GetCurrentMethod().Name} | {GetType().FullName} | LanguageId: {languageId}"
                };

                _logManager.WriteLog(logRowIn);

                throw;
            }
        }

        /// <summary>
        ///     Get Genders By Language
        /// </summary>
        /// <param name="deleteAccountRequest"></param>
        /// <returns></returns>
        [HttpDelete]
        [Route("user/deleteaccount")]
        public DeleteAccountResult DeleteAccount(DeleteAccountRequest deleteAccountRequest)
        {
            try
            {
                return _mapper.Map<DeleteAccountResult>(_userManager.DeleteAccount(_mapper.Map<DeleteAccountInput>(deleteAccountRequest)));
            }
            catch (Exception ex)
            {
                var logRowIn = new LogRowIn
                {
                    ErrorMessage = $"{ex.Message}{ex.InnerException ?? ex.InnerException}",
                    ErrorMessageCode = "US-ER-9999",
                    ErrorSeverity = LogLevel.CriticalErrors,
                    FileOrigin = $"{MethodBase.GetCurrentMethod().Name} | {GetType().FullName} | {_utilsManager.GetAllPropertiesAndValues(deleteAccountRequest)}"
                };

                _logManager.WriteLog(logRowIn);

                throw;
            }
        }

        /// <summary>
        ///     Update user information
        /// </summary>
        /// <param name="updateUserInfoRequest"></param>
        /// <returns></returns>
        [HttpPost]
        [Route("user/update")]
        public UpdateUserInfoResult UpdateUserInfo(UpdateUserInfoRequest updateUserInfoRequest)
        {
            try
            {
                return _mapper.Map<UpdateUserInfoResult>(_userManager.UpdateUserInfo(_mapper.Map<UpdateUserInfoInput>(updateUserInfoRequest)));
            }
            catch (Exception ex)
            {
                var logRowIn = new LogRowIn
                {
                    ErrorMessage = $"{ex.Message}{ex.InnerException ?? ex.InnerException}",
                    ErrorMessageCode = "US-ER-9999",
                    ErrorSeverity = LogLevel.CriticalErrors,
                    FileOrigin = $"{MethodBase.GetCurrentMethod().Name} | {GetType().FullName} | {_utilsManager.GetAllPropertiesAndValues(updateUserInfoRequest)}"
                };

                _logManager.WriteLog(logRowIn);

                throw;
            }
        }

        /// <summary>
        ///     Activate user after Registration
        /// </summary>
        /// <param name="activateUserRequest"></param>
        /// <returns></returns>
        [HttpPost]
        [Route("user/activate")]
        public ActivateUserResult ActivateUser(ActivateUserRequest activateUserRequest)
        {
            try
            {
                return _mapper.Map<ActivateUserResult>(_userManager.ActivateUser(_mapper.Map<ActivateUserInput>(activateUserRequest)));
            }
            catch (Exception ex)
            {
                var logRowIn = new LogRowIn
                {
                    ErrorMessage = $"{ex.Message}{ex.InnerException ?? ex.InnerException}",
                    ErrorMessageCode = "US-ER-9999",
                    ErrorSeverity = LogLevel.CriticalErrors,
                    FileOrigin = $"{MethodBase.GetCurrentMethod().Name} | {GetType().FullName} | {_utilsManager.GetAllPropertiesAndValues(activateUserRequest)}"
                };

                _logManager.WriteLog(logRowIn);

                throw;
            }
        }

        /// <summary>
        ///     Start Reset Password Process
        /// </summary>
        /// <param name="resetPasswordProcessRequest"></param>
        /// <returns></returns>
        [HttpPost]
        [Route("user/resetpasswordprocess")]
        public ResetPasswordProcessResult ResetPasswordProcess(ResetPasswordProcessRequest resetPasswordProcessRequest)
        {
            try
            {
                return _mapper.Map<ResetPasswordProcessResult>(_userManager.ResetPasswordProcess(_mapper.Map<ResetPasswordProcessInput>(resetPasswordProcessRequest)));
            }
            catch (Exception ex)
            {
                var logRowIn = new LogRowIn
                {
                    ErrorMessage = $"{ex.Message}{ex.InnerException ?? ex.InnerException}",
                    ErrorMessageCode = "US-ER-9999",
                    ErrorSeverity = LogLevel.CriticalErrors,
                    FileOrigin = $"{MethodBase.GetCurrentMethod().Name} | {GetType().FullName} | {_utilsManager.GetAllPropertiesAndValues(resetPasswordProcessRequest)}"
                };

                _logManager.WriteLog(logRowIn);

                throw;
            }
        }

        /// <summary>
        ///     Check Id and Confirmation Code in Reset Password Process
        /// </summary>
        /// <param name="checkForValidResetPasswordProcessRequest"></param>
        /// <returns></returns>
        [HttpPost]
        [Route("user/checkpasswordprocess")]
        public CheckForValidResetPasswordProcessResult CheckForValidResetPasswordProcess(CheckForValidResetPasswordProcessRequest checkForValidResetPasswordProcessRequest)
        {
            try
            {
                return
                    _mapper.Map<CheckForValidResetPasswordProcessResult>(
                        _userManager.CheckForValidResetPasswordProcess(_mapper.Map<CheckForValidResetPasswordProcessInput>(checkForValidResetPasswordProcessRequest)));
            }
            catch (Exception ex)
            {
                var logRowIn = new LogRowIn
                {
                    ErrorMessage = $"{ex.Message}{ex.InnerException ?? ex.InnerException}",
                    ErrorMessageCode = "US-ER-9999",
                    ErrorSeverity = LogLevel.CriticalErrors,
                    FileOrigin = $"{MethodBase.GetCurrentMethod().Name} | {GetType().FullName} | {_utilsManager.GetAllPropertiesAndValues(checkForValidResetPasswordProcessRequest)}"
                };

                _logManager.WriteLog(logRowIn);

                throw;
            }
        }

        /// <summary>
        ///     Update Password
        /// </summary>
        /// <param name="updatePasswordRequest"></param>
        /// <returns></returns>
        [HttpPost]
        [Route("user/password/update")]
        public UpdatePasswordResult UpdatePassword(UpdatePasswordRequest updatePasswordRequest)
        {
            try
            {
                return _mapper.Map<UpdatePasswordResult>(_userManager.UpdatePassword(_mapper.Map<UpdatePasswordInput>(updatePasswordRequest)));
            }
            catch (Exception ex)
            {
                var logRowIn = new LogRowIn
                {
                    ErrorMessage = $"{ex.Message}{ex.InnerException ?? ex.InnerException}",
                    ErrorMessageCode = "US-ER-9999",
                    ErrorSeverity = LogLevel.CriticalErrors,
                    FileOrigin = $"{MethodBase.GetCurrentMethod().Name} | {GetType().FullName} | {_utilsManager.GetAllPropertiesAndValues(updatePasswordRequest)}"
                };

                _logManager.WriteLog(logRowIn);

                throw;
            }
        }

        /// <summary>
        /// Login o Regiter user via Facebook
        /// </summary>
        /// <param name="facebookLogin"></param>
        /// <returns></returns>
        //[HttpPost]
        //[Route("user/facebooklogin")]
        //public FacebookLoginResult FacebookLogin(FacebookLoginRequest facebookLogin)
        //{
        //    try
        //    {
        //        return _mapper.Map<FacebookLoginResult>(_userManager.FacebookLogin(_mapper.Map<FacebookLoginInput>(facebookLogin)));
        //    }
        //    catch (Exception ex)
        //    {
        //        var logRowIn = new LogRowIn()
        //        {
        //            ErrorMessage = $"{ex.Message}{ex.InnerException ?? ex.InnerException}",
        //            ErrorMessageCode = "US-ER-9999",
        //            ErrorSeverity = LogLevel.CriticalErrors,
        //            FileOrigin = $"{MethodBase.GetCurrentMethod().Name} | {GetType().FullName} | {_utilsManager.GetAllPropertiesAndValues(facebookLogin)}",
        //        };

        //        _logManager.WriteLog(logRowIn);

        //        throw;
        //    }
        //}
    }
}