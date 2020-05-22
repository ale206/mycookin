using System;

namespace TaechIdeas.Core.Core.Common.Dto
{
    public class PageSecurity
    {
        #region PrivateFields

        private Guid _IDUserGuid;

        #endregion

        #region PublicFields

        public string IDUser { get; }

        public string PageName { get; }

        #endregion

        #region Constructors

        /// <summary>
        ///     Check User Privileges to visualize this page
        /// </summary>
        /// <param name="IDUser">IDUser from Session - String</param>
        /// <param name="PageName">Current Page</param>
        public PageSecurity(string IDUser, string PageName)
        {
            this.IDUser = IDUser;
            this.PageName = PageName;
        }

        #endregion

        // TODO: MOVE TO BUSINESS LOGIC!!

        #region Methods

        #region CheckAuthorization

        /// <summary>
        ///     Check User Authorization to visualize this page
        /// </summary>
        /// <returns>True if autorized</returns>
        public bool CheckAuthorization()
        {
            //If we have not IDUser, return false
            if (IDUser.Equals(""))
            {
                return false;
            }

            //Check if IDUser exists
            _IDUserGuid = new Guid(IDUser);

            //TODO: RESTORE
            //var numUsersByIdDal = new DAL.User.ds_UserInfoTableAdapters.GetUsersDAL();
            //var numUsersById = numUsersByIdDal.NumUsersById(_IDUserGuid);

            //if (numUsersById == 0 || numUsersById == null)
            //{
            //    //IDUser does not exists
            //    return false;
            //}
            //else
            //{
            //    return true;
            //}
            return true;
        }

        #endregion

        #region IsPublicProfile?

        /// <summary>
        /// Check if tha page is for a Public Profile
        /// </summary>
        /// <returns></returns>
        //public static bool IsPublicProfile()
        //{
        //    var IsPublicProfile = false;

        //    //If lost Session, don't go to Login, it will be a public profile
        //    try
        //    {
        //        var IDUserGuid = new Guid();
        //        IDUserGuid = new Guid(HttpContext.Current.Session["IDUser"].ToString());

        //        IsPublicProfile = false;
        //    }
        //    catch
        //    {
        //        IsPublicProfile = true;
        //    }

        //    return IsPublicProfile;
        //}

        #endregion

        #endregion
    }
}