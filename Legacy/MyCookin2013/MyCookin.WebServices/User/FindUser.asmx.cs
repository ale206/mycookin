using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Services;
using System.Web.Script.Services;
using MyCookin.Common;
using MyCookin.ObjectManager.UserManager;
using MyCookin.ObjectManager.MediaManager;

namespace MyCookin.WebServices.User
{
    /// <summary>
    /// Descrizione di riepilogo per FindUser
    /// </summary>
    [WebService(Namespace = "http://tempuri.org/")]
    [WebServiceBinding(ConformsTo = WsiProfiles.BasicProfile1_1)]
    [System.ComponentModel.ToolboxItem(false)]
    // To allow this Web Service to be called from script, using ASP.NET AJAX, uncomment the following line. 
    [ScriptService]
    public class FindUser : System.Web.Services.WebService
    {
        [WebMethod]
        public List<MyUser> SearchUser(string words)
        {
            MyUser User = new MyUser();

            List<MyUser> UserList = User.GetUsersList(words);

            return UserList.ToList();
        }

        [WebMethod]
        public List<MyUser> UserFromAutocomplete(string words, string otherField)
        {
            MyUser User = new MyUser();

            List<MyUser> UserList = User.GetUsersList(words);

            return UserList.ToList();
        }

        [WebMethod]
        public List<MyUser> FindFriendsByWord(string words, string IDUser)
        {
            MyUserFriendship FriendshipObj = new MyUserFriendship(new Guid(IDUser));

            List<MyUser> UserList = FriendshipObj.FindFriendsByWords(words);

            return UserList.ToList();
        }

        [WebMethod]
        public List<string> GetUsersSuggestionListHTML(string IDRequester, string RowOffset, string FetchRows)
        {
            Guid _IDRequester = new Guid();
            if(!String.IsNullOrEmpty(IDRequester))
            {
                _IDRequester = new Guid(IDRequester);
            }
            int _RowOffSet = MyConvert.ToInt32(RowOffset, 0);
            int _FetchRows = MyConvert.ToInt32(FetchRows, 0);
           
            List<string> _return = new List<string>();

            List<MyUser> _userList = MyUser.SuggestUsers(_IDRequester, _RowOffSet, _FetchRows).ToList();

            MyUserConfigParameter _elementHTML = new MyUserConfigParameter("UserListHTML");

            foreach(MyUser _user in _userList)
            {
                string _userPhoto = "";
                #region GetPhoto
                try
                {
                    if (_user.IDProfilePhoto.IDMedia != null)
                    {
                        _user.IDProfilePhoto.QueryMediaInfo();
                        try
                        {
                            _userPhoto = _user.IDProfilePhoto.GetAlternativeSizePath(MediaSizeTypes.Small, false, false, true);
                        }
                        catch
                        {
                        }
                        if (_userPhoto == "")
                        {
                            _userPhoto = _user.IDProfilePhoto.GetCompletePath(false, false, true);
                        }

                        if (_userPhoto == "")
                        {
                            _userPhoto = DefaultMedia.GetDefaultMediaPathFromWebConfig(MediaType.ProfileImagePhoto);
                        }
                    }
                    else
                    {
                        _userPhoto = DefaultMedia.GetDefaultMediaPathFromWebConfig(MediaType.ProfileImagePhoto);
                    }
                }
                catch
                {
                    _userPhoto = DefaultMedia.GetDefaultMediaPathFromWebConfig(MediaType.ProfileImagePhoto);
                }
                #endregion

                string userLink = ("/" + _user.UserName + "/").ToLower();
                _return.Add(_elementHTML.ConfigurationValue.Replace("{ImagePath}", _userPhoto).Replace("{UserLink}", userLink.ToLower()).Replace("{UserName}", _user.UserName).Replace("{UserCompleteName}", _user.Name + " " + _user.Surname));
            }

            return _return;
        }
    }
}
