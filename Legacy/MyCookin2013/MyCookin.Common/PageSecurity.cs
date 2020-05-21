using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using MyCookin.DAL;
using System.Web;

namespace MyCookin.Common
{
    public class PageSecurity
    {
        #region PrivateFields
        private string _IDUser;
        private string _pageName;
        private Guid _IDUserGuid;
        #endregion

        #region PublicFields
        public string IDUser
        {
            get { return _IDUser; }
        }
        public string PageName
        {
            get { return _pageName; }
        }
        #endregion

        #region Constructors
        /// <summary>
        /// Check User Privileges to visualize this page
        /// </summary>
        /// <param name="IDUser">IDUser from Session - String</param>
        /// <param name="PageName">Current Page</param>
        public PageSecurity(string IDUser, string PageName)
        {
            _IDUser = IDUser;
            _pageName = PageName;
        }
        #endregion

        #region Methods

        #region CheckAuthorization
        /// <summary>
        /// Check User Authorization to visualize this page
        /// </summary>
        /// <returns>True if autorized</returns>
        public bool CheckAuthorization()
        {
            //If we have not IDUser, return false
            if (_IDUser.Equals(""))
            {
                return false;
            }
            else
            {
                //Check if IDUser exists
                _IDUserGuid = new Guid(_IDUser);

                DAL.User.ds_UserInfoTableAdapters.GetUsersDAL numUsersByIdDal = new DAL.User.ds_UserInfoTableAdapters.GetUsersDAL();
                int? numUsersById = numUsersByIdDal.NumUsersById(_IDUserGuid);

                if (numUsersById == 0 || numUsersById == null)
                {
                    //IDUser does not exists
                    return false;
                }
                else
                {
                    return true;
                }
            }
        }
        #endregion

        #region IsPublicProfile?
        /// <summary>
        /// Check if tha page is for a Public Profile
        /// </summary>
        /// <returns></returns>
        public static bool IsPublicProfile()
        {
            bool IsPublicProfile = false;

            //If lost Session, don't go to Login, it will be a public profile
            try
            {
                Guid IDUserGuid = new Guid();
                IDUserGuid = new Guid(HttpContext.Current.Session["IDUser"].ToString());

                IsPublicProfile = false;
            }
            catch
            {
                IsPublicProfile = true;
            }

            return IsPublicProfile;
        }

            #endregion

        #endregion
    }
}
