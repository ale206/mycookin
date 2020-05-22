using System;
using System.Collections.Generic;
using System.Linq;
using AutoMapper;
using TaechIdeas.Core.Core.Common;
using TaechIdeas.Core.Core.Common.Dto;
using TaechIdeas.Core.Core.Configuration;
using TaechIdeas.Core.Core.User;
using TaechIdeas.Core.Core.User.Dto;
using TaechIdeas.Core.Core.Verification;

namespace TaechIdeas.Core.BusinessLogic.Verification
{
    public class VerificationManager : IVerificationManager
    {
        private readonly IUserRepository _userRepository;
        private readonly IUtilsManager _utilsManager;
        private readonly IUserConfig _userConfig;
        private readonly IMapper _mapper;

        public VerificationManager(IUserRepository userRepository, IUtilsManager utilsManager, IUserConfig userConfig, IMapper mapper)
        {
            _userRepository = userRepository;
            _utilsManager = utilsManager;
            _userConfig = userConfig;
            _mapper = mapper;
        }

        #region UsernameAlreadyExists

        public UsernameAlreadyExistsOutput UsernameAlreadyExists(UsernameAlreadyExistsInput usernameAlreadyExistsInput)
        {
            var verificationError = CheckUsername(usernameAlreadyExistsInput.Username);
            if (verificationError.RejectionReason != null)
            {
                throw new ArgumentException(verificationError.RejectionReason);
            }

            return _mapper.Map<UsernameAlreadyExistsOutput>(_userRepository.UsernameAlreadyExists(_mapper.Map<UsernameAlreadyExistsIn>(usernameAlreadyExistsInput)));
        }

        #endregion

        #region EmailAlreadyExists

        public EmailAlreadyExistsOutput EmailAlreadyExists(EmailAlreadyExistsInput emailAlreadyExistsInput)
        {
            var verificationError = CheckEmail(emailAlreadyExistsInput.Email);
            if (verificationError.RejectionReason != null)
            {
                throw new ArgumentException(verificationError.RejectionReason);
            }

            return _mapper.Map<EmailAlreadyExistsOutput>(_userRepository.EmailAlreadyExists(_mapper.Map<EmailAlreadyExistsIn>(emailAlreadyExistsInput)));
        }

        #endregion

        #region VerifyNewUserRequest

        public IEnumerable<VerificationError> VerifyNewUserRequest(VerifyNewUserRequestInput verifyNewUserRequestInput)
        {
            var verificationResults = new List<VerificationError>
            {
                CheckName(verifyNewUserRequestInput.Name),
                CheckSurname(verifyNewUserRequestInput.Surname),
                CheckUsername(verifyNewUserRequestInput.UserName),
                CheckEmail(verifyNewUserRequestInput.Email),
                CheckPassword(verifyNewUserRequestInput.Password),
                CheckBirthDate(verifyNewUserRequestInput.DateOfBirth),
                CheckContractSigned(verifyNewUserRequestInput.ContractSigned),
                CheckLanguageId(verifyNewUserRequestInput.LanguageId),
                CheckIpAddress(verifyNewUserRequestInput.Ip),
                CheckMobile(verifyNewUserRequestInput.Mobile),
                CheckCityId(verifyNewUserRequestInput.CityId),
                CheckGenderId(verifyNewUserRequestInput.GenderId),
                CheckOffset(verifyNewUserRequestInput.Offset)
            };

            return verificationResults.Where(x => x.RejectionReason != null);
        }

        #endregion

        #region VerifyNewUserLoginRequest

        public IEnumerable<VerificationError> VerifyNewUserLoginRequest(VerifyNewUserLoginRequestInput verifyNewUserLoginRequest)
        {
            var verificationResults = new List<VerificationError>
            {
                CheckEmail(verifyNewUserLoginRequest.Email),
                CheckPassword(verifyNewUserLoginRequest.Password),
                CheckLanguageId(verifyNewUserLoginRequest.LanguageId),
                CheckIpAddress(verifyNewUserLoginRequest.Ip)
            };

            return verificationResults.Where(x => x.RejectionReason != null);
        }

        #endregion

        #region CheckName

        public VerificationError CheckName(string name)
        {
            var verificationError = new VerificationError();

            if (!string.IsNullOrEmpty(name)) return verificationError;
            verificationError.RejectionReason = "Empty Name";

            return verificationError;
        }

        #endregion

        #region CheckSurname

        public VerificationError CheckSurname(string surname)
        {
            var verificationError = new VerificationError();

            if (!string.IsNullOrEmpty(surname)) return verificationError;
            verificationError.RejectionReason = "Empty Surname";

            return verificationError;
        }

        #endregion

        #region CheckUsername

        public VerificationError CheckUsername(string username)
        {
            var verificationError = new VerificationError();

            if (string.IsNullOrEmpty(username))
            {
                verificationError.RejectionReason = "Empty Username";
                return verificationError;
            }

            return verificationError;
        }

        #endregion

        #region CheckEmail

        public VerificationError CheckEmail(string email)
        {
            var verificationError = new VerificationError();

            if (string.IsNullOrEmpty(email))
            {
                verificationError.RejectionReason = "Empty Email";
                return verificationError;
            }

            if (!_utilsManager.IsEmailValid(email))
            {
                verificationError.RejectionReason = "Wrong Email";
                return verificationError;
            }

            return verificationError;
        }

        #endregion

        #region CheckPassword

        public VerificationError CheckPassword(string password)
        {
            var verificationError = new VerificationError();

            if (string.IsNullOrEmpty(password))
            {
                verificationError.RejectionReason = "Empty Password";
                return verificationError;
            }

            if (password.Length < _userConfig.MinPasswordLength || password.Length > 30)
            {
                verificationError.RejectionReason = "Password must be between 5 and 30 characters";
                return verificationError;
            }

            return verificationError;
        }

        #endregion

        #region CheckBirthDate

        public VerificationError CheckBirthDate(DateTime birthDate)
        {
            var verificationError = new VerificationError();

            if (birthDate.Equals(new DateTime()))
            {
                verificationError.RejectionReason = "Wrong Birthdate";
                return verificationError;
            }

            if (birthDate.Date.AddYears(18) > DateTime.UtcNow.Date)
            {
                verificationError.RejectionReason = "18 years old minimum required";
                return verificationError;
            }

            if (birthDate.Date.AddYears(120) < DateTime.UtcNow.Date)
            {
                verificationError.RejectionReason = "Are you really more than 120 years old? Congratulations!";
                return verificationError;
            }

            return verificationError;
        }

        #endregion

        #region CheckContractSigned

        public VerificationError CheckContractSigned(bool contractSigned)
        {
            var verificationError = new VerificationError();

            if (!contractSigned)
            {
                verificationError.RejectionReason = "ContractSigned must be set to true";
            }

            return verificationError;
        }

        #endregion

        #region CheckLanguageId

        public VerificationError CheckLanguageId(int languageId)
        {
            var verificationError = new VerificationError();

            if (languageId <= 0 || languageId > 3)
            {
                verificationError.RejectionReason = "RecipeLanguageId must be a value between 0 and 3";
            }

            return verificationError;
        }

        #endregion

        #region CheckIpAddress

        public VerificationError CheckIpAddress(string ipAddress)
        {
            var verificationError = new VerificationError();

            if (string.IsNullOrEmpty(ipAddress))
            {
                verificationError.RejectionReason = "Empty Ip";
                return verificationError;
            }

            if (ipAddress.Length < 7 || ipAddress.Length > 15)
            {
                verificationError.RejectionReason = "Wrong Ip Length";
                return verificationError;
            }

            if (!_utilsManager.IsIpValid(ipAddress))
            {
                verificationError.RejectionReason = "Wrong Ip";
                return verificationError;
            }

            return verificationError;
        }

        #endregion

        #region CheckMobile

        public VerificationError CheckMobile(string mobile)
        {
            var verificationError = new VerificationError();

            //TODO
            return verificationError;
        }

        #endregion

        #region CheckCityId

        public VerificationError CheckCityId(int? cityId)
        {
            var verificationError = new VerificationError();

            if (cityId != null && cityId <= 0)
            {
                verificationError.RejectionReason = "CityId must be a value greater than 0";
                return verificationError;
            }

            return verificationError;
        }

        #endregion

        #region CheckGenderId

        public VerificationError CheckGenderId(int? genderId)
        {
            var verificationError = new VerificationError();

            if (genderId != null && (genderId <= 0 || genderId > 2))
            {
                verificationError.RejectionReason = "GenderId must be 1 or 2";
            }

            return verificationError;
        }

        #endregion

        #region CheckOffset

        public VerificationError CheckOffset(int offset)
        {
            var verificationError = new VerificationError();

            //60 mins for 12 timezones
            if (offset < -720 || offset > 720)
            {
                verificationError.RejectionReason = "Offset must be a value between -720 and 720";
            }

            return verificationError;
        }

        #endregion
    }
}