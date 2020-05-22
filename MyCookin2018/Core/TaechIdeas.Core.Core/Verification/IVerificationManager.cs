using System;
using System.Collections.Generic;
using TaechIdeas.Core.Core.Common.Dto;
using TaechIdeas.Core.Core.User.Dto;

namespace TaechIdeas.Core.Core.Verification
{
    public interface IVerificationManager
    {
        UsernameAlreadyExistsOutput UsernameAlreadyExists(UsernameAlreadyExistsInput usernameAlreadyExistsInput);
        EmailAlreadyExistsOutput EmailAlreadyExists(EmailAlreadyExistsInput emailAlreadyExistsInput);

        IEnumerable<VerificationError> VerifyNewUserRequest(VerifyNewUserRequestInput verifyNewUserRequestInput);
        IEnumerable<VerificationError> VerifyNewUserLoginRequest(VerifyNewUserLoginRequestInput verifyNewUserLoginRequest);

        VerificationError CheckName(string name);
        VerificationError CheckSurname(string surname);
        VerificationError CheckUsername(string username);
        VerificationError CheckEmail(string email);
        VerificationError CheckPassword(string password);
        VerificationError CheckBirthDate(DateTime birthDate);
        VerificationError CheckContractSigned(bool contractSigned);
        VerificationError CheckLanguageId(int languageId);
        VerificationError CheckIpAddress(string ipAddress);
        VerificationError CheckMobile(string mobile);
        VerificationError CheckCityId(int? cityId);
        VerificationError CheckGenderId(int? genderId);
        VerificationError CheckOffset(int offset);
    }
}