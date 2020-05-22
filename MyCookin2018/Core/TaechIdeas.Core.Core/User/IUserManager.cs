using System.Collections.Generic;
using TaechIdeas.Core.Core.User.Dto;

namespace TaechIdeas.Core.Core.User
{
    public interface IUserManager
    {
        NewUserOutput NewUser(NewUserInput newUserInput);
        SendEmailToConfirmRegistrationOutput SendEmailToConfirmRegistration(SendEmailToConfirmRegistrationInput sendEmailToConfirmRegistrationInput);
        SendEmailToResetPasswordOutput SendEmailToResetPassword(SendEmailToResetPasswordInput sendEmailToResetPasswordInput);
        LoginUserOutput LoginUser(LoginUserInput loginUserInput);
        LoginUserByIdOutput LoginUserById(LoginUserByIdInput loginUserByIdInput);
        UpdateLastLogonAndSetUserAsOnLineOutput UpdateLastLogonAndSetUserAsOnLine(UpdateLastLogonAndSetUserAsOnLineInput updateLastLogonAndSetUserAsOnLineInput);
        LogoutUserOutput LogoutUser(LogoutUserInput logoutUserInput);
        UserByIdOutput UserById(UserByIdInput userByIdInput, bool avoidCheckToken = false);
        UserByEmailOutput UserByEmail(UserByEmailInput userByEmailInput);
        UserIdByUserTokenOutput UserIdByUserToken(UserIdByUserTokenInput userIdByUserTokenInput);
        ActivateUserOutput ActivateUser(ActivateUserInput activateUserInput);
        UserProfilePathOutput UserProfilePath(UserProfilePathInput userProfilePathInput);
        IEnumerable<SecurityQuestionsByLanguageOutput> SecurityQuestionsByLanguage(SecurityQuestionsByLanguageInput securityQuestionsByLanguageInput);
        IEnumerable<GenderByLanguageOutput> GendersByLanguage(GenderByLanguageInput genderByLanguageInput);
        DeleteAccountOutput DeleteAccount(DeleteAccountInput deleteAccountInput);
        UpdateUserInfoOutput UpdateUserInfo(UpdateUserInfoInput updateUserInfoInput);

        /****************************************************************************************/

        IEnumerable<GetSecurityUserGroupsOutput> GetSecurityUserGroups(GetSecurityUserGroupsInput getSecurityUserGroupsInput);

        UserByUsernameOutput UserByUsername(UserByUsernameInput userByUsernameInput);

        LastTimeOnlineOutput LastTimeOnline(LastTimeOnlineInput lastTimeOnlineInput);

        GetLanguageListOutput GetLanguageList(GetLanguageListInput getLanguageListInput);

        NumberOfUsersOutput NumberOfUsers(NumberOfUsersInput numberOfUsersInput);

        UpdatePasswordOutput UpdatePassword(UpdatePasswordInput updatePasswordInput);

        GetSecurityQuestionOutput GetSecurityQuestion(GetSecurityQuestionInput getSecurityQuestionInput);

        UpdateLastProfileUpdateDateOutput UpdateLastProfileUpdateDate(UpdateLastProfileUpdateDateInput updateLastProfileUpdateDateInput);

        GenerateNewTemporaryCodeOutput GenerateNewTemporaryCode(GenerateNewTemporaryCodeInput generateNewTemporaryCodeInput);

        UpdateTemporarySecurityAnswerOutput UpdateTemporarySecurityAnswer(UpdateTemporarySecurityAnswerInput updateTemporarySecurityAnswerInput);

        UpdateSecurityAnswerOutput UpdateSecurityAnswer(UpdateSecurityAnswerInput updateSecurityAnswerInput);

        GetIdGenderByGenderNameAndIdLanguageOutput GetIdGenderByGenderNameAndIdLanguage(GetIdGenderByGenderNameAndIdLanguageInput getIdGenderByGenderNameAndIdLanguageInput);

        DeleteSecurityQuestionAndAnswerOutput DeleteSecurityQuestionAndAnswer(DeleteSecurityQuestionAndAnswerInput deleteSecurityQuestionAndAnswerInput);

        IEnumerable<FindUserByWordsOutput> FindUserByWords(FindUserByWordsInput findUserByWordsInput);

        IsValidPasswordOutput IsValidPassword(IsValidPasswordInput isValidPasswordInput);

        ProfileCompletePercentageCalcOutput ProfileCompletePercentageCalc(ProfileCompletePercentageCalcInput profileCompletePercentageCalcInput);

        IEnumerable<SuggestedUsersOutput> SuggestedUsers(SuggestedUsersInput suggestedUsersInput);

        IEnumerable<NewUsersForMailchimpOutput> NewUsersForMailchimp(NewUsersForMailchimpInput newUsersForMailchimpInput);

        NumberOfProfileVisitsOutput NumberOfProfileVisits(NumberOfProfileVisitsInput numberOfProfileVisitsInput);
        ResetPasswordProcessOutput ResetPasswordProcess(ResetPasswordProcessInput resetPasswordProcessInput);
        CheckForValidResetPasswordProcessOutput CheckForValidResetPasswordProcess(CheckForValidResetPasswordProcessInput checkForValidResetPasswordProcessInput);

        //FacebookLoginOutput FacebookLogin(FacebookLoginInput facebookLoginInput);
        string CreateConfirmationCode(string password, string email);
    }
}