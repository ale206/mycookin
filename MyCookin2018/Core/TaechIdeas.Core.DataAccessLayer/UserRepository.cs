using System;
using System.Collections.Generic;
using System.Data;
using Dapper;
using Microsoft.Extensions.Configuration;
using TaechIdeas.Core.Core;
using TaechIdeas.Core.Core.User;
using TaechIdeas.Core.Core.User.Dto;

namespace TaechIdeas.Core.DataAccessLayer
{
    public class UserRepository : IUserRepository
    {
        private readonly string _connectionString;
        private readonly IDbConnectionFactory _dbConnectionFactory;

        public UserRepository(IDbConnectionFactory dbConnectionFactory, IConfiguration configuration)
        {
            _dbConnectionFactory = dbConnectionFactory;
            _connectionString = configuration.GetConnectionString("DBUsersProfileConnectionString");
        }

        #region NewUser

        public NewUserOut NewUser(NewUserIn newUserIn)
        {
            NewUserOut result;

            using (var connection = _dbConnectionFactory.GetConnection(_connectionString))
            {
                result = connection.ExecuteScalar<NewUserOut>("USP_InsertUser",
                    new
                    {
                        newUserIn.Name,
                        newUserIn.Surname,
                        UserName = newUserIn.Username,
                        eMail = newUserIn.Email,
                        newUserIn.PasswordHash,
                        BirthDate = newUserIn.DateOfBirth,
                        PasswordExpireOn = DateTime.UtcNow.AddYears(30),
                        ChangePasswordNextLogon = false,
                        newUserIn.ContractSigned,
                        IDLanguage = newUserIn.LanguageId,
                        UserEnabled = false,
                        UserLocked = true,
                        MantainanceMode = false,
                        DateMembership = DateTime.UtcNow,
                        AccountExpireOn = DateTime.UtcNow.AddMonths(1),
                        LastIpAddress = newUserIn.Ip,
                        newUserIn.Mobile,
                        IDCity = newUserIn.CityId,
                        UserIsOnline = false,
                        IDGender = newUserIn.GenderId,
                        newUserIn.Offset,
                        WebsiteId = Convert.ToInt32(newUserIn.WebsiteId)
                    },
                    commandType: CommandType.StoredProcedure
                );
            }

            return result;
        }

        #endregion

        #region LoginUser

        public LoginUserOut LoginUser(LoginUserIn loginUserIn)
        {
            LoginUserOut result;

            using (var connection = _dbConnectionFactory.GetConnection(_connectionString))
            {
                result = connection.ExecuteScalar<LoginUserOut>("USP_LoginUser",
                    new
                    {
                        eMail = loginUserIn.Email,
                        loginUserIn.PasswordHash,
                        LastLogon = DateTime.UtcNow,
                        loginUserIn.Offset,
                        UserIsOnline = true,
                        LastIpAddress = loginUserIn.Ip,
                        WebsiteId = Convert.ToInt32(loginUserIn.WebsiteId)
                    },
                    commandType: CommandType.StoredProcedure
                );
            }

            return result;
        }

        #endregion

        #region UsernameAlreadyExists

        public UsernameAlreadyExistsOut UsernameAlreadyExists(UsernameAlreadyExistsIn usernameAlreadyExistsIn)
        {
            UsernameAlreadyExistsOut result;

            using (var connection = _dbConnectionFactory.GetConnection(_connectionString))
            {
                result = connection.ExecuteScalar<UsernameAlreadyExistsOut>("USP_UserNameAlreadyExist",
                    new
                    {
                        UserName = usernameAlreadyExistsIn.Username
                    },
                    commandType: CommandType.StoredProcedure
                );
            }

            return result;
        }

        #endregion

        #region EmailAlreadyExists

        public EmailAlreadyExistsOut EmailAlreadyExists(EmailAlreadyExistsIn emailAlreadyExistsIn)
        {
            EmailAlreadyExistsOut result;

            using (var connection = _dbConnectionFactory.GetConnection(_connectionString))
            {
                result = connection.ExecuteScalar<EmailAlreadyExistsOut>("USP_EmailAlreadyExist",
                    new
                    {
                        emailAlreadyExistsIn.Email
                    },
                    commandType: CommandType.StoredProcedure
                );
            }

            return result;
        }

        #endregion

        #region UpdateLastLogonAndSetUserAsOnLine

        public UpdateLastLogonAndSetUserAsOnLineOut UpdateLastLogonAndSetUserAsOnLine(UpdateLastLogonAndSetUserAsOnLineIn updateLastLogonAndSetUserAsOnLineIn)
        {
            UpdateLastLogonAndSetUserAsOnLineOut result;

            using (var connection = _dbConnectionFactory.GetConnection(_connectionString))
            {
                result = connection.ExecuteScalar<UpdateLastLogonAndSetUserAsOnLineOut>("USP_UpdateLastLogon",
                    new
                    {
                        IDUser = updateLastLogonAndSetUserAsOnLineIn.UserId
                    },
                    commandType: CommandType.StoredProcedure
                );
            }

            return result;
        }

        #endregion

        #region LogoutUser

        public LogoutUserOut LogoutUser(LogoutUserIn logoutUserIn)
        {
            LogoutUserOut result;

            using (var connection = _dbConnectionFactory.GetConnection(_connectionString))
            {
                result = connection.ExecuteScalar<LogoutUserOut>("USP_LogoutUserByToken",
                    new
                    {
                        logoutUserIn.UserToken
                    },
                    commandType: CommandType.StoredProcedure
                );
            }

            return result;
        }

        #endregion

        #region UserIdFromSocialLogins

        public UserIdFromSocialLoginsOut UserIdFromSocialLogins(UserIdFromSocialLoginsIn userIdFromSocialLoginsIn)
        {
            UserIdFromSocialLoginsOut result;

            using (var connection = _dbConnectionFactory.GetConnection(_connectionString))
            {
                result = connection.ExecuteScalar<UserIdFromSocialLoginsOut>("USP_GetIdUserFromSocialLogins",
                    new
                    {
                        IDUserSocial = userIdFromSocialLoginsIn.UserIdOnSocial,
                        IDSocialNetwork = userIdFromSocialLoginsIn.SocialNetworkId
                    },
                    commandType: CommandType.StoredProcedure
                );
            }

            return result;
        }

        #endregion

        #region UserById

        public UserByIdOut UserById(UserByIdIn userByIdIn)
        {
            UserByIdOut result;

            using (var connection = _dbConnectionFactory.GetConnection(_connectionString))
            {
                result = connection.ExecuteScalar<UserByIdOut>("USP_GetUserById",
                    new
                    {
                        IDUser = userByIdIn.UserId
                    },
                    commandType: CommandType.StoredProcedure
                );
            }

            return result;
        }

        #endregion

        #region UpdateSocialTokens

        public UpdateSocialTokensOut UpdateSocialTokens(UpdateSocialTokensIn updateSocialTokensIn)
        {
            UpdateSocialTokensOut result;

            using (var connection = _dbConnectionFactory.GetConnection(_connectionString))
            {
                result = connection.ExecuteScalar<UpdateSocialTokensOut>("USP_UpdateSocialTokens",
                    new
                    {
                        IdSocialNetwork = updateSocialTokensIn.SocialNetworkId,
                        IDUser = updateSocialTokensIn.UserId,
                        updateSocialTokensIn.AccessToken,
                        updateSocialTokensIn.RefreshToken
                    },
                    commandType: CommandType.StoredProcedure
                );
            }

            return result;
        }

        #endregion

        #region UserByEmail

        public UserByEmailOut UserByEmail(UserByEmailIn userByEmail)
        {
            UserByEmailOut result;

            using (var connection = _dbConnectionFactory.GetConnection(_connectionString))
            {
                result = connection.ExecuteScalar<UserByEmailOut>("USP_GetUserByEmail",
                    new
                    {
                        userByEmail.Email
                    },
                    commandType: CommandType.StoredProcedure
                );
            }

            return result;
        }

        #endregion

        #region NewSocialLogin

        public NewSocialLoginOut NewSocialLogin(NewSocialLoginIn newSocialLoginIn)
        {
            NewSocialLoginOut result;

            using (var connection = _dbConnectionFactory.GetConnection(_connectionString))
            {
                result = connection.ExecuteScalar<NewSocialLoginOut>("USP_InsertSocialLogin",
                    new
                    {
                        IdSocialNetwork = newSocialLoginIn.SocialNetworkId,
                        IdUser = newSocialLoginIn.UserId,
                        IdUserSocial = newSocialLoginIn.UserIdOnSocial,
                        newSocialLoginIn.Link,
                        newSocialLoginIn.VerifiedEmail,
                        newSocialLoginIn.PictureUrl,
                        newSocialLoginIn.Locale,
                        newSocialLoginIn.AccessToken,
                        newSocialLoginIn.RefreshToken
                    },
                    commandType: CommandType.StoredProcedure
                );
            }

            return result;
        }

        #endregion

        #region UserIdByTokenId

        public UserIdByUserTokenOut UserIdByTokenId(UserIdByUserTokenIn userIdByUserTokenIn)
        {
            UserIdByUserTokenOut result;

            using (var connection = _dbConnectionFactory.GetConnection(_connectionString))
            {
                result = connection.ExecuteScalar<UserIdByUserTokenOut>("USP_GetUserIdByToken",
                    new
                    {
                        userIdByUserTokenIn.UserToken
                    },
                    commandType: CommandType.StoredProcedure
                );
            }

            return result;
        }

        #endregion

        #region ActivateUser

        public ActivateUserOut ActivateUser(ActivateUserIn activateUserIn)
        {
            ActivateUserOut result;

            using (var connection = _dbConnectionFactory.GetConnection(_connectionString))
            {
                result = connection.ExecuteScalar<ActivateUserOut>("USP_ActivateUser",
                    new
                    {
                        IDUser = activateUserIn.UserId,
                        MailConfirmedOn = DateTime.UtcNow,
                        UserEnabled = true,
                        UserLocked = false,
                        LastIpAddress = activateUserIn.Ip,
                        activateUserIn.AccountExpireOn
                    },
                    commandType: CommandType.StoredProcedure
                );
            }

            return result;
        }

        #endregion

        #region LanguageIdByLanguageCode

        public LanguageIdByLanguageCodeOut LanguageIdByLanguageCode(LanguageIdByLanguageCodeIn languageIdByLanguageCodeIn)
        {
            LanguageIdByLanguageCodeOut result;

            using (var connection = _dbConnectionFactory.GetConnection(_connectionString))
            {
                result = connection.ExecuteScalar<LanguageIdByLanguageCodeOut>("USP_GetIDLanguage",
                    new
                    {
                        languageIdByLanguageCodeIn.LanguageCode
                    },
                    commandType: CommandType.StoredProcedure
                );
            }

            return result;
        }

        #endregion

        #region LanguageCodeByLanguageId

        public LanguageCodeByLanguageIdOut LanguageCodeByLanguageId(LanguageCodeByLanguageIdIn languageCodeByLanguageIdIn)
        {
            LanguageCodeByLanguageIdOut result;

            using (var connection = _dbConnectionFactory.GetConnection(_connectionString))
            {
                result = connection.ExecuteScalar<LanguageCodeByLanguageIdOut>("USP_GetLanguageCodeById",
                    new
                    {
                        IdLanguage = languageCodeByLanguageIdIn.LanguageId
                    },
                    commandType: CommandType.StoredProcedure
                );
            }

            return result;
        }

        #endregion

        public IEnumerable<SecurityQuestionsByLanguageOut> SecurityQuestionsByLanguage(SecurityQuestionsByLanguageIn securityQuestionsByLanguageIn)
        {
            IEnumerable<SecurityQuestionsByLanguageOut> result;

            using (var connection = _dbConnectionFactory.GetConnection(_connectionString))
            {
                result = connection.ExecuteScalar<IEnumerable<SecurityQuestionsByLanguageOut>>("USP_SecurityQuestionsByLanguage",
                    new
                    {
                        IdLanguage = securityQuestionsByLanguageIn.LanguageId
                    },
                    commandType: CommandType.StoredProcedure
                );
            }

            return result;
        }

        public IEnumerable<GenderByLanguageOut> GendersByLanguage(GenderByLanguageIn genderByLanguageIn)
        {
            IEnumerable<GenderByLanguageOut> result;

            using (var connection = _dbConnectionFactory.GetConnection(_connectionString))
            {
                result = connection.ExecuteScalar<IEnumerable<GenderByLanguageOut>>("USP_GetGenderByLanguage",
                    new
                    {
                        IdLanguage = genderByLanguageIn.LanguageId
                    },
                    commandType: CommandType.StoredProcedure
                );
            }

            return result;
        }

        public DeleteAccountOut DeleteAccount(DeleteAccountIn deleteAccountIn)
        {
            DeleteAccountOut result;

            using (var connection = _dbConnectionFactory.GetConnection(_connectionString))
            {
                result = connection.ExecuteScalar<DeleteAccountOut>("USP_DeleteAccount",
                    new
                    {
                        deleteAccountIn.UserToken
                    },
                    commandType: CommandType.StoredProcedure
                );
            }

            return result;
        }

        public UpdateUserInfoOut UpdateUserInfo(UpdateUserInfoIn updateUserInfoIn)
        {
            UpdateUserInfoOut result;

            using (var connection = _dbConnectionFactory.GetConnection(_connectionString))
            {
                result = connection.ExecuteScalar<UpdateUserInfoOut>("USP_UpdateUserInfo",
                    new
                    {
                        IDUser = updateUserInfoIn.UserId,
                        updateUserInfoIn.Name,
                        updateUserInfoIn.Surname,
                        UserName = updateUserInfoIn.Username,
                        updateUserInfoIn.BirthDate,
                        eMail = updateUserInfoIn.Email,
                        updateUserInfoIn.Mobile,
                        IDLanguage = updateUserInfoIn.LanguageId,
                        IDCity = updateUserInfoIn.CityId,
                        IDProfilePhoto = updateUserInfoIn.ProfilePhotoId,
                        IDSecurityQuestion = updateUserInfoIn.SecurityQuestionId,
                        updateUserInfoIn.SecurityAnswer,
                        updateUserInfoIn.Offset,
                        updateUserInfoIn.LastIpAddress,
                        IDGender = updateUserInfoIn.GenderId
                    },
                    commandType: CommandType.StoredProcedure
                );
            }

            return result;
        }

        public IEnumerable<GetSecurityUserGroupsOut> GetSecurityUserGroups(GetSecurityUserGroupsIn getSecurityUserGroupsIn)
        {
            IEnumerable<GetSecurityUserGroupsOut> result;

            using (var connection = _dbConnectionFactory.GetConnection(_connectionString))
            {
                result = connection.ExecuteScalar<IEnumerable<GetSecurityUserGroupsOut>>("USP_GetSecurityUserGroups",
                    new
                    {
                        IDUser = getSecurityUserGroupsIn.UserId
                    },
                    commandType: CommandType.StoredProcedure
                );
            }

            return result;
        }

        public UserByUsernameOut UserByUsername(UserByUsernameIn userByUsernameIn)
        {
            UserByUsernameOut result;

            using (var connection = _dbConnectionFactory.GetConnection(_connectionString))
            {
                result = connection.ExecuteScalar<UserByUsernameOut>("USP_UserByUsername",
                    new
                    {
                        UserName = userByUsernameIn.Username
                    },
                    commandType: CommandType.StoredProcedure
                );
            }

            return result;
        }

        public GetLanguageListOut GetLanguageList(GetLanguageListIn getLanguageListIn)
        {
            GetLanguageListOut result;

            using (var connection = _dbConnectionFactory.GetConnection(_connectionString))
            {
                result = connection.ExecuteScalar<GetLanguageListOut>("USP_GetLanguageList",
                    commandType: CommandType.StoredProcedure
                );
            }

            return result;
        }

        public NumberOfUsersOut NumberOfUsers(NumberOfUsersIn numberOfUsersIn)
        {
            NumberOfUsersOut result;

            using (var connection = _dbConnectionFactory.GetConnection(_connectionString))
            {
                result = connection.ExecuteScalar<NumberOfUsersOut>("USP_GetMediaById",
                    commandType: CommandType.StoredProcedure
                );
            }

            return result;
        }

        public UpdatePasswordOut UpdatePassword(UpdatePasswordIn updatePasswordIn)
        {
            UpdatePasswordOut result;

            using (var connection = _dbConnectionFactory.GetConnection(_connectionString))
            {
                result = connection.ExecuteScalar<UpdatePasswordOut>("USP_UpdatePassword",
                    new
                    {
                        PasswordHash = updatePasswordIn.NewPasswordHash,
                        LastPasswordChange = DateTime.UtcNow,
                        PasswordExpireOn = DateTime.UtcNow.AddYears(30),
                        IDUser = updatePasswordIn.UserId
                    },
                    commandType: CommandType.StoredProcedure
                );
            }

            return result;
        }

        public GetSecurityQuestionOut GetSecurityQuestion(GetSecurityQuestionIn getSecurityQuestionIn)
        {
            GetSecurityQuestionOut result;

            using (var connection = _dbConnectionFactory.GetConnection(_connectionString))
            {
                result = connection.ExecuteScalar<GetSecurityQuestionOut>("USP_GetSecurityQuestion",
                    new
                    {
                        IDUser = getSecurityQuestionIn.UserId,
                        IDLanguage = getSecurityQuestionIn.LanguageId
                    },
                    commandType: CommandType.StoredProcedure
                );
            }

            return result;
        }

        public UpdateLastProfileUpdateDateOut UpdateLastProfileUpdateDate(UpdateLastProfileUpdateDateIn updateLastProfileUpdateDateIn)
        {
            UpdateLastProfileUpdateDateOut result;

            using (var connection = _dbConnectionFactory.GetConnection(_connectionString))
            {
                result = connection.ExecuteScalar<UpdateLastProfileUpdateDateOut>("USP_UpdateLastProfileUpdateDate",
                    new
                    {
                        IDUser = updateLastProfileUpdateDateIn.UserId,
                        LastProfileUpdate = DateTime.UtcNow
                    },
                    commandType: CommandType.StoredProcedure
                );
            }

            return result;
        }

        public UpdateTemporarySecurityAnswerOut UpdateTemporarySecurityAnswer(UpdateTemporarySecurityAnswerIn updateTemporarySecurityAnswerIn)
        {
            UpdateTemporarySecurityAnswerOut result;

            using (var connection = _dbConnectionFactory.GetConnection(_connectionString))
            {
                result = connection.ExecuteScalar<UpdateTemporarySecurityAnswerOut>("USP_UpdateTemporarySecurityAnswer",
                    new
                    {
                        IDUser = updateTemporarySecurityAnswerIn.UserId,
                        SecurityAnswer = updateTemporarySecurityAnswerIn.TemporaryCode
                    },
                    commandType: CommandType.StoredProcedure
                );
            }

            return result;
        }

        public UpdateSecurityAnswerOut UpdateSecurityAnswer(UpdateSecurityAnswerIn updateSecurityAnswerIn)
        {
            UpdateSecurityAnswerOut result;

            using (var connection = _dbConnectionFactory.GetConnection(_connectionString))
            {
                result = connection.ExecuteScalar<UpdateSecurityAnswerOut>("USP_UpdateSecurityAnswer",
                    new
                    {
                        IDUser = updateSecurityAnswerIn.UserId,
                        updateSecurityAnswerIn.SecurityAnswer,
                        IDSecurityQuestion = updateSecurityAnswerIn.SecurityQuestionId
                    },
                    commandType: CommandType.StoredProcedure
                );
            }

            return result;
        }

        public GetIdGenderByGenderNameAndIdLanguageOut GetIdGenderByGenderNameAndIdLanguage(GetIdGenderByGenderNameAndIdLanguageIn getIdGenderByGenderNameAndIdLanguageIn)
        {
            GetIdGenderByGenderNameAndIdLanguageOut result;

            using (var connection = _dbConnectionFactory.GetConnection(_connectionString))
            {
                result = connection.ExecuteScalar<GetIdGenderByGenderNameAndIdLanguageOut>("USP_GetIdGenderByGenderNameAndIdLanguage",
                    new
                    {
                        getIdGenderByGenderNameAndIdLanguageIn.Gender,
                        IDLanguage = getIdGenderByGenderNameAndIdLanguageIn.LanguageId
                    },
                    commandType: CommandType.StoredProcedure
                );
            }

            return result;
        }

        public DeleteSecurityQuestionAndAnswerOut DeleteSecurityQuestionAndAnswer(DeleteSecurityQuestionAndAnswerIn deleteSecurityQuestionAndAnswerIn)
        {
            DeleteSecurityQuestionAndAnswerOut result;

            using (var connection = _dbConnectionFactory.GetConnection(_connectionString))
            {
                result = connection.ExecuteScalar<DeleteSecurityQuestionAndAnswerOut>("USP_DeleteSecurityQuestionAndAnswer",
                    new
                    {
                        IDUser = deleteSecurityQuestionAndAnswerIn.UserId
                    },
                    commandType: CommandType.StoredProcedure
                );
            }

            return result;
        }

        public IEnumerable<FindUserByWordsOut> FindUserByWords(FindUserByWordsIn findUserByWordsIn)
        {
            IEnumerable<FindUserByWordsOut> result;

            using (var connection = _dbConnectionFactory.GetConnection(_connectionString))
            {
                result = connection.ExecuteScalar<IEnumerable<FindUserByWordsOut>>("USP_FindUser",
                    new
                    {
                        findUserByWordsIn.Words,
                        findUserByWordsIn.NumberOfResults
                    },
                    commandType: CommandType.StoredProcedure
                );
            }

            return result;
        }

        public IEnumerable<SuggestedUsersOut> SuggestedUsers(SuggestedUsersIn suggestedUsersIn)
        {
            IEnumerable<SuggestedUsersOut> result;

            using (var connection = _dbConnectionFactory.GetConnection(_connectionString))
            {
                result = connection.ExecuteScalar<IEnumerable<SuggestedUsersOut>>("USP_SuggestNewUser",
                    new
                    {
                        suggestedUsersIn.FetchRows,
                        IDUserRequester = suggestedUsersIn.RequesterId,
                        OffsetRows = suggestedUsersIn.RowOffset
                    },
                    commandType: CommandType.StoredProcedure
                );
            }

            return result;
        }

        public IEnumerable<NewUsersForMailchimpOut> NewUsersForMailchimp(NewUsersForMailchimpIn newUsersForMailchimpIn)
        {
            IEnumerable<NewUsersForMailchimpOut> result;

            using (var connection = _dbConnectionFactory.GetConnection(_connectionString))
            {
                result = connection.ExecuteScalar<IEnumerable<NewUsersForMailchimpOut>>("USP_GetMediaById",
                    commandType: CommandType.StoredProcedure
                );
            }

            return result;
        }

        public FollowUserOut FollowUser(FollowUserIn followUserIn)
        {
            FollowUserOut result;

            using (var connection = _dbConnectionFactory.GetConnection(_connectionString))
            {
                result = connection.ExecuteScalar<FollowUserOut>("USP_FollowUser",
                    new
                    {
                        IDUser = followUserIn.UserIdFriend1,
                        IDUserFollowed = followUserIn.UserIdFriend2,
                        UserFollowerFrom = DateTime.UtcNow
                    },
                    commandType: CommandType.StoredProcedure
                );
            }

            return result;
        }

        public DefollowUserOut DefollowUser(DefollowUserIn defollowUserIn)
        {
            DefollowUserOut result;

            using (var connection = _dbConnectionFactory.GetConnection(_connectionString))
            {
                result = connection.ExecuteScalar<DefollowUserOut>("USP_DefollowUser",
                    new
                    {
                        IDUser = defollowUserIn.UserIdFriend1,
                        IDUserFollowed = defollowUserIn.UserIdFriend2
                    },
                    commandType: CommandType.StoredProcedure
                );
            }

            return result;
        }

        public IEnumerable<FollowingOut> Following(FollowingIn followingIn)
        {
            IEnumerable<FollowingOut> result;

            using (var connection = _dbConnectionFactory.GetConnection(_connectionString))
            {
                result = connection.ExecuteScalar<IEnumerable<FollowingOut>>("USP_FollowingIn",
                    new
                    {
                        Me = followingIn.UserId
                    },
                    commandType: CommandType.StoredProcedure
                );
            }

            return result;
        }

        public IEnumerable<FollowersOut> Followers(FollowersIn followersIn)
        {
            IEnumerable<FollowersOut> result;

            using (var connection = _dbConnectionFactory.GetConnection(_connectionString))
            {
                result = connection.ExecuteScalar<IEnumerable<FollowersOut>>("USP_Followers",
                    new
                    {
                        Me = followersIn.UserId
                    },
                    commandType: CommandType.StoredProcedure
                );
            }

            return result;
        }

        public CheckFollowingOut CheckFollowing(CheckFollowingIn checkFollowingIn)
        {
            CheckFollowingOut result;

            using (var connection = _dbConnectionFactory.GetConnection(_connectionString))
            {
                result = connection.ExecuteScalar<CheckFollowingOut>("USP_CheckFollowing",
                    new
                    {
                        IDUser1 = checkFollowingIn.UserIdFriend1,
                        IDUser2 = checkFollowingIn.UserIdFriend2
                    },
                    commandType: CommandType.StoredProcedure
                );
            }

            return result;
        }

        public NumberOfFollowersOut NumberOfFollowers(NumberOfFollowersIn numberOfFollowersIn)
        {
            NumberOfFollowersOut result;

            using (var connection = _dbConnectionFactory.GetConnection(_connectionString))
            {
                result = connection.ExecuteScalar<NumberOfFollowersOut>("USP_NumberOfFollowers",
                    new
                    {
                        Me = numberOfFollowersIn.UserIdFriend1
                    },
                    commandType: CommandType.StoredProcedure
                );
            }

            return result;
        }

        public NumberOfFollowingOut NumberOfFollowing(NumberOfFollowingIn numberOfFollowingIn)
        {
            NumberOfFollowingOut result;

            using (var connection = _dbConnectionFactory.GetConnection(_connectionString))
            {
                result = connection.ExecuteScalar<NumberOfFollowingOut>("USP_NumberOfFollowing",
                    new
                    {
                        Me = numberOfFollowingIn.UserIdFriend1
                    },
                    commandType: CommandType.StoredProcedure
                );
            }

            return result;
        }

        public IEnumerable<UsersYouMayKnowByFollowerOut> UsersYouMayKnowByFollower(UsersYouMayKnowByFollowerIn usersYouMayKnowByFollowerIn)
        {
            IEnumerable<UsersYouMayKnowByFollowerOut> result;

            using (var connection = _dbConnectionFactory.GetConnection(_connectionString))
            {
                result = connection.ExecuteScalar<IEnumerable<UsersYouMayKnowByFollowerOut>>("USP_PeopleYouMayKnowByFollower",
                    new
                    {
                        Me = usersYouMayKnowByFollowerIn.UserId,
                        NumberOfResults = 1000
                    },
                    commandType: CommandType.StoredProcedure
                );
            }

            return result;
        }

        public IEnumerable<FriendshipRequestsOut> FriendshipRequests(FriendshipRequestsIn friendshipRequestsIn)
        {
            IEnumerable<FriendshipRequestsOut> result;

            using (var connection = _dbConnectionFactory.GetConnection(_connectionString))
            {
                result = connection.ExecuteScalar<IEnumerable<FriendshipRequestsOut>>("USP_FriendshipRequests",
                    new
                    {
                        IDUser = friendshipRequestsIn.UserId
                    },
                    commandType: CommandType.StoredProcedure
                );
            }

            return result;
        }

        public IEnumerable<FriendsOut> Friends(FriendsIn friendsIn)
        {
            IEnumerable<FriendsOut> result;

            using (var connection = _dbConnectionFactory.GetConnection(_connectionString))
            {
                result = connection.ExecuteScalar<IEnumerable<FriendsOut>>("USP_Friends",
                    new
                    {
                        IDUser1 = friendsIn.UserId
                    },
                    commandType: CommandType.StoredProcedure
                );
            }

            return result;
        }

        public IEnumerable<CommonFriendsOut> CommonFriends(CommonFriendsIn commonFriendsIn)
        {
            IEnumerable<CommonFriendsOut> result;

            using (var connection = _dbConnectionFactory.GetConnection(_connectionString))
            {
                result = connection.ExecuteScalar<IEnumerable<CommonFriendsOut>>("USP_CommonFriends",
                    new
                    {
                        IDUser1 = commonFriendsIn.UserId1,
                        IDUser2 = commonFriendsIn.UserId2
                    },
                    commandType: CommandType.StoredProcedure
                );
            }

            return result;
        }

        public IEnumerable<BlockedFriendsOut> BlockedFriends(BlockedFriendsIn blockedFriendsIn)
        {
            IEnumerable<BlockedFriendsOut> result;

            using (var connection = _dbConnectionFactory.GetConnection(_connectionString))
            {
                result = connection.ExecuteScalar<IEnumerable<BlockedFriendsOut>>("USP_BlockedFriends",
                    new
                    {
                        IDUser = blockedFriendsIn.UserId
                    },
                    commandType: CommandType.StoredProcedure
                );
            }

            return result;
        }

        public RequestOrAcceptFriendshipOut RequestOrAcceptFriendship(RequestOrAcceptFriendshipIn requestOrAcceptFriendshipIn)
        {
            RequestOrAcceptFriendshipOut result;

            using (var connection = _dbConnectionFactory.GetConnection(_connectionString))
            {
                result = connection.ExecuteScalar<RequestOrAcceptFriendshipOut>("USP_RequestOrAcceptFriendship",
                    new
                    {
                        IDUser1 = requestOrAcceptFriendshipIn.UserId1,
                        IDUser2 = requestOrAcceptFriendshipIn.UserId2,
                        FriendshipDate = DateTime.UtcNow
                    },
                    commandType: CommandType.StoredProcedure
                );
            }

            return result;
        }

        public DeclineFriendshipOut DeclineFriendship(DeclineFriendshipIn declineFriendshipIn)
        {
            DeclineFriendshipOut result;

            using (var connection = _dbConnectionFactory.GetConnection(_connectionString))
            {
                result = connection.ExecuteScalar<DeclineFriendshipOut>("USP_DeclineFriendship",
                    new
                    {
                        IDUser1 = declineFriendshipIn.UserId1,
                        IDUser2 = declineFriendshipIn.UserId2,
                        UserBlocked = false
                    },
                    commandType: CommandType.StoredProcedure
                );
            }

            return result;
        }

        public BlockUserOut BlockUser(BlockUserIn blockUserIn)
        {
            BlockUserOut result;

            using (var connection = _dbConnectionFactory.GetConnection(_connectionString))
            {
                result = connection.ExecuteScalar<BlockUserOut>("USP_BlockUser",
                    new
                    {
                        IDUser1 = blockUserIn.UserId1,
                        IDUser2 = blockUserIn.UserId2,
                        UserBlocked = DateTime.UtcNow
                    },
                    commandType: CommandType.StoredProcedure
                );
            }

            return result;
        }

        public RemoveBlockUserOut RemoveBlockUser(RemoveBlockUserIn removeBlockUserIn)
        {
            RemoveBlockUserOut result;

            using (var connection = _dbConnectionFactory.GetConnection(_connectionString))
            {
                result = connection.ExecuteScalar<RemoveBlockUserOut>("USP_RemoveBlock",
                    new
                    {
                        IDUser1 = removeBlockUserIn.UserId1,
                        IDUser2 = removeBlockUserIn.UserId2
                    },
                    commandType: CommandType.StoredProcedure
                );
            }

            return result;
        }

        public RemoveFriendshipOut RemoveFriendship(RemoveFriendshipIn removeFriendshipIn)
        {
            RemoveFriendshipOut result;

            using (var connection = _dbConnectionFactory.GetConnection(_connectionString))
            {
                result = connection.ExecuteScalar<RemoveFriendshipOut>("USP_RemoveFriendship",
                    new
                    {
                        IDUser1 = removeFriendshipIn.UserId1,
                        IDUser2 = removeFriendshipIn.UserId2
                    },
                    commandType: CommandType.StoredProcedure
                );
            }

            return result;
        }

        public RemoveFriendshipForUseWithFollowOut RemoveFriendshipForUseWithFollow(RemoveFriendshipForUseWithFollowIn removeFriendshipForUseWithFollowIn)
        {
            RemoveFriendshipForUseWithFollowOut result;

            using (var connection = _dbConnectionFactory.GetConnection(_connectionString))
            {
                result = connection.ExecuteScalar<RemoveFriendshipForUseWithFollowOut>("USP_RemoveFriendshipForUseWithFollow",
                    new
                    {
                        IDUser1 = removeFriendshipForUseWithFollowIn.UserId1,
                        IDUser2 = removeFriendshipForUseWithFollowIn.UserId2
                    },
                    commandType: CommandType.StoredProcedure
                );
            }

            return result;
        }

        public CheckFriendshipOut CheckFriendship(CheckFriendshipIn checkFriendshipIn)
        {
            CheckFriendshipOut result;

            using (var connection = _dbConnectionFactory.GetConnection(_connectionString))
            {
                result = connection.ExecuteScalar<CheckFriendshipOut>("USP_CheckFriendship",
                    new
                    {
                        IDUser1 = checkFriendshipIn.UserId1,
                        IDUser2 = checkFriendshipIn.UserId2
                    },
                    commandType: CommandType.StoredProcedure
                );
            }

            return result;
        }

        public CheckUserBlockedOut CheckUserBlocked(CheckUserBlockedIn checkUserBlockedIn)
        {
            CheckUserBlockedOut result;

            using (var connection = _dbConnectionFactory.GetConnection(_connectionString))
            {
                result = connection.ExecuteScalar<CheckUserBlockedOut>("USP_CheckUserBlocked",
                    new
                    {
                        IDUser1 = checkUserBlockedIn.UserId1,
                        IDUser2 = checkUserBlockedIn.UserId2
                    },
                    commandType: CommandType.StoredProcedure
                );
            }

            return result;
        }

        public FriendshipCompletedDateOut FriendshipCompletedDate(FriendshipCompletedDateIn friendshipCompletedDateIn)
        {
            FriendshipCompletedDateOut result;

            using (var connection = _dbConnectionFactory.GetConnection(_connectionString))
            {
                result = connection.ExecuteScalar<FriendshipCompletedDateOut>("USP_FriendshipCompletedDate",
                    new
                    {
                        IDUser1 = friendshipCompletedDateIn.UserId1,
                        IDUser2 = friendshipCompletedDateIn.UserId2
                    },
                    commandType: CommandType.StoredProcedure
                );
            }

            return result;
        }

        public NumberOfFriendsOut NumberOfFriends(NumberOfFriendsIn numberOfFriendsIn)
        {
            NumberOfFriendsOut result;

            using (var connection = _dbConnectionFactory.GetConnection(_connectionString))
            {
                result = connection.ExecuteScalar<NumberOfFriendsOut>("USP_NumberOfFriends",
                    new
                    {
                        IDUser = numberOfFriendsIn.UserId
                    },
                    commandType: CommandType.StoredProcedure
                );
            }

            return result;
        }

        public IEnumerable<FindFriendsByWordsOut> FindFriendsByWords(FindFriendsByWordsIn findFriendsByWordsIn)
        {
            IEnumerable<FindFriendsByWordsOut> result;

            using (var connection = _dbConnectionFactory.GetConnection(_connectionString))
            {
                result = connection.ExecuteScalar<IEnumerable<FindFriendsByWordsOut>>("USP_FindFriendsByWords",
                    new
                    {
                        IDUser1 = findFriendsByWordsIn.UserId,
                        findFriendsByWordsIn.Words,
                        NumberOfResults = 10
                    },
                    commandType: CommandType.StoredProcedure
                );
            }

            return result;
        }

        public IEnumerable<NotificationListOut> NotificationList(NotificationListIn notificationListIn)
        {
            IEnumerable<NotificationListOut> result;

            using (var connection = _dbConnectionFactory.GetConnection(_connectionString))
            {
                result = connection.ExecuteScalar<IEnumerable<NotificationListOut>>("USP_GetNotificationsByUser",
                    new
                    {
                        IDUser = notificationListIn.idUser,
                        IdLanguage = notificationListIn.idLanguage
                    },
                    commandType: CommandType.StoredProcedure
                );
            }

            return result;
        }

        public UpdateUserNotificationSettingOut UpdateUserNotificationSetting(UpdateUserNotificationSettingIn notificationSettingIn)
        {
            UpdateUserNotificationSettingOut result;

            using (var connection = _dbConnectionFactory.GetConnection(_connectionString))
            {
                result = connection.ExecuteScalar<UpdateUserNotificationSettingOut>("USP_UpdateUserNotification",
                    new
                    {
                        IDUserNotification = notificationSettingIn.idUserNotification,
                        IsEnabled = notificationSettingIn.isEnabled
                    },
                    commandType: CommandType.StoredProcedure
                );
            }

            return result;
        }

        public IsNotificationEnabledOut IsNotificationEnabled(IsNotificationEnabledIn isNotificationEnabledIn)
        {
            IsNotificationEnabledOut result;

            using (var connection = _dbConnectionFactory.GetConnection(_connectionString))
            {
                result = connection.ExecuteScalar<IsNotificationEnabledOut>("USP_IsNotificationEnabled",
                    new
                    {
                        IDUser = isNotificationEnabledIn.idUser,
                        IDUserNotificationType = isNotificationEnabledIn.idUserNotificationType,
                        IdLanguage = isNotificationEnabledIn.idLanguage
                    },
                    commandType: CommandType.StoredProcedure
                );
            }

            return result;
        }

        public UserSocialInformationOut UserSocialInformation(UserSocialInformationIn userSocialInformationIn)
        {
            UserSocialInformationOut result;

            using (var connection = _dbConnectionFactory.GetConnection(_connectionString))
            {
                result = connection.ExecuteScalar<UserSocialInformationOut>("USP_UserSocialInformation",
                    new
                    {
                        IDUser = userSocialInformationIn.UserId,
                        IDSocialNetwork = userSocialInformationIn.SocialNetworkId
                    },
                    commandType: CommandType.StoredProcedure
                );
            }

            return result;
        }

        public IEnumerable<SocialFriendsByIdUserOut> SocialFriendsByIdUser(SocialFriendsByIdUserIn socialFriendsByIdUserIn)
        {
            IEnumerable<SocialFriendsByIdUserOut> result;

            using (var connection = _dbConnectionFactory.GetConnection(_connectionString))
            {
                result = connection.ExecuteScalar<IEnumerable<SocialFriendsByIdUserOut>>("USP_SocialFriendsByIdUser",
                    new
                    {
                        IDUser = socialFriendsByIdUserIn.UserId
                    },
                    commandType: CommandType.StoredProcedure
                );
            }

            return result;
        }

        public IEnumerable<SocialFriendsOut> SocialFriends(SocialFriendsIn socialFriendsIn)
        {
            IEnumerable<SocialFriendsOut> result;

            using (var connection = _dbConnectionFactory.GetConnection(_connectionString))
            {
                result = connection.ExecuteScalar<IEnumerable<SocialFriendsOut>>("USP_SocialFriends",
                    new
                    {
                        IDUser = socialFriendsIn.UserId,
                        IDSocialNetwork = socialFriendsIn.SocialNetworkId
                    },
                    commandType: CommandType.StoredProcedure
                );
            }

            return result;
        }

        public IEnumerable<UsersIdWithOldFriendsRetrievedOnOut> UsersIdWithOldFriendsRetrievedOn(UsersIdWithOldFriendsRetrievedOnIn usersIdWithOldFriendsRetrievedOnIn)
        {
            IEnumerable<UsersIdWithOldFriendsRetrievedOnOut> result;

            using (var connection = _dbConnectionFactory.GetConnection(_connectionString))
            {
                result = connection.ExecuteScalar<IEnumerable<UsersIdWithOldFriendsRetrievedOnOut>>("USP_UsersIdWithOldFriendsRetrievedOn",
                    new
                    {
                        usersIdWithOldFriendsRetrievedOnIn.FriendsRetrievedOn
                    },
                    commandType: CommandType.StoredProcedure
                );
            }

            return result;
        }

        public UserIdSocialFromIdUserAndIdSocialNetworkOut UserIdSocialFromIdUserAndIdSocialNetwork(UserIdSocialFromIdUserAndIdSocialNetworkIn userIdSocialFromIdUserAndIdSocialNetworkIn)
        {
            UserIdSocialFromIdUserAndIdSocialNetworkOut result;

            using (var connection = _dbConnectionFactory.GetConnection(_connectionString))
            {
                result = connection.ExecuteScalar<UserIdSocialFromIdUserAndIdSocialNetworkOut>("USP_UserIdSocialFromIdUserAndIdSocialNetwork",
                    new
                    {
                        IDUser = userIdSocialFromIdUserAndIdSocialNetworkIn.UserId,
                        IDSocialNetwork = userIdSocialFromIdUserAndIdSocialNetworkIn.SocialNetworkId
                    },
                    commandType: CommandType.StoredProcedure
                );
            }

            return result;
        }

        public UpdateFriendsRetrievedOnOut UpdateFriendsRetrievedOn(UpdateFriendsRetrievedOnIn updateFriendsRetrievedOnIn)
        {
            UpdateFriendsRetrievedOnOut result;

            using (var connection = _dbConnectionFactory.GetConnection(_connectionString))
            {
                result = connection.ExecuteScalar<UpdateFriendsRetrievedOnOut>("USP_UpdateFriendsRetrievedOn",
                    new
                    {
                        IDUser = updateFriendsRetrievedOnIn.UserId,
                        IDSocialNetwork = updateFriendsRetrievedOnIn.SocialNetworkId,
                        FriendsRetrievedOn = DateTime.UtcNow
                    },
                    commandType: CommandType.StoredProcedure
                );
            }

            return result;
        }

        public MemorizeSocialContactFriendOut MemorizeSocialContactFriend(MemorizeSocialContactFriendIn memorizeSocialContactFriendIn)
        {
            MemorizeSocialContactFriendOut result;

            using (var connection = _dbConnectionFactory.GetConnection(_connectionString))
            {
                result = connection.ExecuteScalar<MemorizeSocialContactFriendOut>("USP_MemorizeSocialContactFriend",
                    new
                    {
                        IDSocialNetwork = memorizeSocialContactFriendIn.IdSocialNetwork,
                        IDUser = memorizeSocialContactFriendIn.IdUser,
                        memorizeSocialContactFriendIn.FullName,
                        memorizeSocialContactFriendIn.GivenName,
                        memorizeSocialContactFriendIn.FamilyName,
                        memorizeSocialContactFriendIn.Emails,
                        memorizeSocialContactFriendIn.Phones,
                        memorizeSocialContactFriendIn.PhotoUrl,
                        IDUserOnSocial = memorizeSocialContactFriendIn.IdUserOnSocial
                    },
                    commandType: CommandType.StoredProcedure
                );
            }

            return result;
        }

        public IsUserRegisteredToThisSocialOut IsUserRegisteredToThisSocial(IsUserRegisteredToThisSocialIn isUserRegisteredToThisSocialIn)
        {
            IsUserRegisteredToThisSocialOut result;

            using (var connection = _dbConnectionFactory.GetConnection(_connectionString))
            {
                result = connection.ExecuteScalar<IsUserRegisteredToThisSocialOut>("USP_IsUserRegisteredToThisSocial",
                    new
                    {
                        IDUser = isUserRegisteredToThisSocialIn.UserId,
                        IDSocialNetwork = isUserRegisteredToThisSocialIn.SocialNetworkId
                    },
                    commandType: CommandType.StoredProcedure
                );
            }

            return result;
        }

        #region LoginUser

        public LoginUserByIdOut LoginUserById(LoginUserByIdIn loginUserByIdIn)
        {
            LoginUserByIdOut result;

            using (var connection = _dbConnectionFactory.GetConnection(_connectionString))
            {
                result = connection.ExecuteScalar<LoginUserByIdOut>("USP_LoginUserById",
                    new
                    {
                        IDUser = loginUserByIdIn.UserId,
                        loginUserByIdIn.Offset,
                        LastIpAddress = loginUserByIdIn.Ip
                    },
                    commandType: CommandType.StoredProcedure
                );
            }

            return result;
        }

        #endregion
    }
}