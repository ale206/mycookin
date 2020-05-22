using System;
using System.Collections.Generic;
using AutoMapper;
using TaechIdeas.Core.Core.Common;
using TaechIdeas.Core.Core.LogAndMessage;
using TaechIdeas.Core.Core.Network;
using TaechIdeas.Core.Core.Social;
using TaechIdeas.Core.Core.User;
using TaechIdeas.Core.Core.User.Dto;

namespace TaechIdeas.Core.BusinessLogic.Social
{
    public class SocialManager : ISocialManager
    {
        private readonly INetworkManager _networkManager;
        private readonly IUserRepository _userRepository;
        private readonly IUtilsManager _utilsManager;
        private readonly ILogManager _logManager;
        private readonly IMapper _mapper;

        public SocialManager(ILogManager logManager, IUtilsManager utilsManager,
            INetworkManager networkManager, IUserRepository userRepository, IMapper mapper)
        {
            _logManager = logManager;
            _utilsManager = utilsManager;
            _networkManager = networkManager;
            _userRepository = userRepository;
            _mapper = mapper;
        }

        #region GetIDUserFromSocialLogins

        /// <summary>
        ///     Get IDUser from Social Logins Table
        /// </summary>
        /// <param name="userIdFromSocialLoginsInput"></param>
        /// <returns></returns>
        public UserIdFromSocialLoginsOutput UserIdFromSocialLogins(UserIdFromSocialLoginsInput userIdFromSocialLoginsInput)
        {
            return _mapper.Map<UserIdFromSocialLoginsOutput>(_userRepository.UserIdFromSocialLogins(_mapper.Map<UserIdFromSocialLoginsIn>(userIdFromSocialLoginsInput)));
        }

        #endregion

        #region UpdateSocialTokens

        public UpdateSocialTokensOutput UpdateSocialTokens(UpdateSocialTokensInput updateSocialTokensInput)
        {
            return _mapper.Map<UpdateSocialTokensOutput>(_userRepository.UpdateSocialTokens(_mapper.Map<UpdateSocialTokensIn>(updateSocialTokensInput)));
        }

        #endregion

        #region NewSocialLogin

        public NewSocialLoginOutput NewSocialLogin(NewSocialLoginInput newSocialLoginInput)
        {
            return _mapper.Map<NewSocialLoginOutput>(_userRepository.NewSocialLogin(_mapper.Map<NewSocialLoginIn>(newSocialLoginInput)));
        }

        public UserSocialInformationOutput UserSocialInformation(UserSocialInformationInput userSocialInformationInput)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<SocialFriendsByIdUserOutput> SocialFriendsByIdUser(SocialFriendsByIdUserInput socialFriendsByIdUserInput)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<SocialFriendsOutput> SocialFriends(SocialFriendsInput socialFriendsInput)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<UsersIdWithOldFriendsRetrievedOnOutput> UsersIdWithOldFriendsRetrievedOn(UsersIdWithOldFriendsRetrievedOnInput usersIdWithOldFriendsRetrievedOnInput)
        {
            throw new NotImplementedException();
        }

        public UserIdSocialFromIdUserAndIdSocialNetworkOutput UserIdSocialFromIdUserAndIdSocialNetwork(UserIdSocialFromIdUserAndIdSocialNetworkInput userIdSocialFromIdUserAndIdSocialNetworkInput)
        {
            throw new NotImplementedException();
        }

        public UpdateFriendsRetrievedOnOutput UpdateFriendsRetrievedOn(UpdateFriendsRetrievedOnInput updateFriendsRetrievedOnInput)
        {
            throw new NotImplementedException();
        }

        public FriendsFromSocialNetworkOutput FriendsFromSocialNetwork(FriendsFromSocialNetworkInput friendsFromSocialNetworkInput)
        {
            //TODO: RESTORE...
            throw new NotImplementedException();

            //var resultExecute = false;

            //try
            //{
            //    switch (idSocialNetwork)
            //    {
            //        case 1:
            //            //Google

            //            //Memorize All User Contacts Informations
            //            var applicationName = ConfigurationManager.AppSettings["google_applicationName"].ToString();

            //            var googleAuthentication = _socialGoogleAuthenticationManager.GetSocialGoogleAuthentication(accessToken, refreshToken);
            //            googleAuthentication.Token = accessToken;

            //            googleAuthentication.RefreshToken = refreshToken;

            //            var parameters = _socialGoogleAuthenticationManager.GetParameters(googleAuthentication);

            //            //Memorize All User Contacts Informations
            //            MemorizeGoogleContacts(applicationName, parameters, idUser);

            //            resultExecute = true;
            //            break;

            //        case 2:
            //            //Facebook
            //            MemorizeFacebookContact(accessToken, idUser);

            //            resultExecute = true;
            //            break;
            //    }
            //}
            //catch
            //{
            //}

            //return resultExecute;
        }

        public MemorizeGoogleContactsOutput MemorizeGoogleContacts(MemorizeGoogleContactsInput memorizeGoogleContactsInput)
        {
            //TODO: RESTORE...
            throw new NotImplementedException();

            //var socialFriend = new MemorizeSocialContactFriendInput();

            ////Get All User Contacts Informations
            //try
            //{
            //    var settings = new RequestSettings(applicationName, parameters);

            //    var cr = new ContactsRequest(settings); //Request all contacts
            //    settings.AutoPaging = true; //Allow autopaging - IMPORTANT!
            //    var f = cr.GetContacts(); //Get all contacts

            //    //Get All Contacts
            //    foreach (var cc in f.Entries)
            //    {
            //        var n = cc.Name;
            //        socialFriend.FamilyName = n.FamilyName;
            //        socialFriend.FullName = n.FullName;
            //        socialFriend.GivenName = n.GivenName;

            //        //string IdUserOnSocial = cc.Id;
            //        socialFriend.Emails = "";
            //        socialFriend.Phones = "";

            //        foreach (var email in cc.Emails)
            //        {
            //            socialFriend.Emails += email.Address + ";";
            //        }

            //        //Extract Phone Numbers
            //        foreach (var ph in cc.Phonenumbers)
            //        {
            //            socialFriend.Phones += ph.Value + ";";
            //        }

            //        //INSERT ONLY IF THE CONTACT HAS AN EMAIL
            //        //Is possible to have a contact with the phone only and no email.
            //        try
            //        {
            //            socialFriend.IdUserOnSocial = cc.Emails[0].Address;
            //            //We can not have ID friend user on Google because is simply our Address Book, so we get just the first email

            //            var regex = new Regex(@"^[\w\-\.]*[\w\.]\@[\w\.]*[\w\-\.]+[\w\-]+[\w]\.+[\w]+[\w $]");

            //            var match = regex.Match(socialFriend.IdUserOnSocial);
            //            if (match.Success)
            //            {
            //                //MyUser UserForSocial = new MyUser(_IDUser, (int)SocialNetworks.Google, null, null, FullName, GivenName, FamilyName, emails, phones, "", IdUserOnSocial);

            //                var result = MemorizeSocialContactFriend(socialFriend);

            //                if (!result.isError)
            //                {
            //                    //Memorize Contact OK
            //                }
            //            }
            //        }
            //        catch
            //        {
            //            //string message = "this User Contact has not an email.";
            //        }
            //    }

            //    //Update Data of this friends retrieve
            //    //User.IDSocialNetwork = (int)SocialNetworks.Google;
            //    UpdateFriendsRetrievedOn(idUser, (int)SocialNetwork.Google);
            //}
            //catch (AppsException a)
            //{
            //    //WRITE A ROW IN LOG FILE AND DB
            //    try
            //    {
            //        var logRow = new LogRowIn()
            //        {
            //            ErrorMessage = $"Error in Memorize Social Friends. Error code: {a.ErrorCode}. Invalid input: {a.InvalidInput}. Reason: {a.Reason}",
            //            ErrorMessageCode = "US-ER-9999",
            //            ErrorSeverity = LogLevel.Warnings,
            //            FileOrigin = _networkManager.GetCurrentPageName(),
            //            IdUser = idUser.ToString(),
            //        };

            //        _logManager.WriteLog(logRow);
            //    }
            //    catch
            //    {
            //    }
            //}
        }

        public MemorizeFacebookContactOutput MemorizeFacebookContact(MemorizeFacebookContactInput memorizeFacebookContactInput)
        {
            //TODO: RESTORE...
            throw new NotImplementedException();

            //var myUserSocial = new MemorizeSocialContactFriendInput();

            //try
            //{
            //    var idUserSocial = UserIdSocialFromIdUserAndIdSocialNetwork((int)SocialNetwork.Facebook, idUser);

            //    var app = new FacebookClient(accessToken);
            //    var result = (JsonObject)app.Get("/" + idUserSocial + "/friends");
            //    var model = new List<FbUserInfo>(); //model = friendlist array

            //    foreach (var friend in (JsonArray)result["data"])
            //    {
            //        model.Add(new FbUserInfo()
            //        {
            //            ID = (string)(((JsonObject)friend)["id"]),
            //            name = (string)(((JsonObject)friend)["name"])
            //        });
            //    }

            //    foreach (var res in model)
            //    {
            //        myUserSocial.FullName = res.name;
            //        myUserSocial.IdUserOnSocial = res.ID;

            //        var resultFbInsert = MemorizeSocialContactFriend(myUserSocial);

            //        if (!resultFbInsert.isError)
            //        {
            //            //Memorize Contact OK
            //        }
            //    }

            //    //Update Data of this friends retrieve
            //    //User.IDSocialNetwork = (int)SocialNetworks.Faceboook;
            //    UpdateFriendsRetrievedOn(idUser, (int)SocialNetwork.Facebook);
            //}
            //catch
            //{
            //    //WRITE A ROW IN LOG FILE AND DB
            //    try
            //    {
            //        var logRow = new LogRowIn()
            //        {
            //            ErrorMessage = $"Error in Memorize Social Friends, Facebook Retrieve",
            //            ErrorMessageCode = "US-ER-9999",
            //            ErrorSeverity = LogLevel.Errors,
            //            FileOrigin = _networkManager.GetCurrentPageName(),
            //            IdUser = idUser.ToString(),
            //        };

            //        _logManager.WriteLog(logRow);
            //    }
            //    catch
            //    {
            //    }
            //}
        }

        public MemorizeSocialContactFriendOutput MemorizeSocialContactFriend(MemorizeSocialContactFriendInput memorizeSocialContactFriendInput)
        {
            throw new NotImplementedException();
        }

        public IsUserRegisteredToThisSocialOutput IsUserRegisteredToThisSocial(IsUserRegisteredToThisSocialInput isUserRegisteredToThisSocialInput)
        {
            throw new NotImplementedException();
        }

        #endregion
    }
}