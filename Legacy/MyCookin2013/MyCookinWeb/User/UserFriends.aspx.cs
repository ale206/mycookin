using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using MyCookin.Common;
using MyCookin.ObjectManager.UserManager;
using MyCookin.ErrorAndMessage;
using MyCookin.Log;
using System.Data;

namespace MyCookinWeb.UserInfo
{
    public partial class UserFriends :  MyCookinWeb.Form.MyPageBase
    {
        protected void Page_PreInit(object sender, EventArgs e)
        {
            if (MyUser.CheckUserLogged())
            {
                this.Page.MasterPageFile = "~/Styles/SiteStyle/Private.Master";
            }
            else
            {
                this.Page.MasterPageFile = "~/Styles/SiteStyle/Public.Master";
            }

            //if (!PageSecurity.IsPublicProfile())
            //{
            //    this.Page.MasterPageFile = "~/Styles/SiteStyle/Private.Master";
            //}
        }

        //Variable to check if is my profile
        //In our profile we must not to show buttons such us RequestFriend, Block, ...            
        protected void Page_Load(object sender, EventArgs e)
        {
            //This avoid issue on routed pages
            Page.Form.Action = Request.RawUrl;
            NavHistoryClear();
            if (!IsPostBack)
            {
                //No default button
                Page.Form.DefaultButton = null;

                string IDUserRequested = Request["IDUserRequested"];   

                if (String.IsNullOrEmpty(IDUserRequested))
                {
                    Response.Redirect((AppConfig.GetValue("HomePage", AppDomain.CurrentDomain)).ToLower(), true);
                }

                //If we want to show Friends or Followers List
                string Show = Request["Show"];

                switch (Show.ToLower())
                { 
                    case "friends":
                        upnlFriendsList.Visible = true;
                        upnlFollowersList.Visible = false;
                        upnlFollowingList.Visible = false;

                        if (!IsPostBack)
                        {
                            //Create Lists for Friendship
                            CreateListOfFriends();
                        }

                        break;

                    case "followers":
                        upnlFriendsList.Visible = false;
                        upnlFollowersList.Visible = true;
                        upnlFollowingList.Visible = false;

                        if (!IsPostBack)
                        {
                            //Create Lists for Friendship
                            CreateListOfFollowers();
                        }
                        break;

                    case "following":
                        upnlFollowingList.Visible = true;
                        upnlFriendsList.Visible = false;
                        upnlFollowersList.Visible = false;

                        if (!IsPostBack)
                        {
                            //Create Lists for Friendship
                            CreateListOfFollowing();
                        }
                        break;
                    default:
                        upnlFollowingList.Visible = false;
                        upnlFriendsList.Visible = false;
                        upnlFollowersList.Visible = false;
                        break;
                }

                //NON USATI AL MOMENTO
                //if (!IsPostBack)
                //{
                //    CreateListOfBlockedFriends();
                //    CreateListOfCommonFriends();
                //}
                
                #region CompleteUserInformations
                Guid IDUserGuid = new Guid(IDUserRequested.ToString());

                MyUser User = new MyUser(IDUserGuid);
                User.GetUserBasicInfoByID();

                //Picture
                if (User.IDProfilePhoto != null)
                {
                    ProfileImage.ImageUrl = User.IDProfilePhoto.GetCompletePath(false, false, true);
                }
                else
                {
                    ProfileImage.ImageUrl = "/Images/icon-userNoPic.png";
                }

                lblName.Text = User.Name + " " + User.Surname;

                #endregion

                //Panel with Requests and Blocked Users is set to hidden
                //It will be visible if one of then will contain data
                //pnlFriendsMiddle.Visible = false;

                //upnlFriendshipRequests.Visible = false;
                //upnlBlockedFriendsList.Visible = false;     //DISABLED
            }
        }

        #region FRIENDSHIP

        #region ButtonShowFriendProfile
        protected void btnShowFriendProfile_Click(object sender, EventArgs e)
        {
            try
            {
                ImageButton _button = (ImageButton)sender;

                string UserIDClicked = _button.CommandArgument.ToString();

                Guid IDNewFriendGuid = new Guid(UserIDClicked.ToString());

                //Go to Friend Profile
                MyUser _userReq = new MyUser(new Guid(UserIDClicked));
                _userReq.GetUserBasicInfoByID();

                Response.Redirect(("/" + _userReq.UserName + "/").ToLower(), true);
            }
            catch (Exception ex)
            {
                //WRITE A ROW IN LOG FILE AND DB
                try
                {
                    LogRow NewRow = new LogRow(DateTime.UtcNow, LogLevel.Errors.ToString(), "", Network.GetCurrentPageName(), "US-ER-9999", "Error on Button Show Friend Profile: " + ex.Message, Request["IDUserRequested"].ToString(), true, false);
                    LogManager.WriteDBLog(LogLevel.Errors, NewRow);
                    LogManager.WriteFileLog(LogLevel.Errors, true, NewRow);
                }
                catch { }
            }
        }
        #endregion

        #region ButtonRemoveFriendship -- non utilizzata
        ///// <summary>
        ///// Button to Remove friendship.
        ///// </summary>
        ///// <param name="sender"></param>
        ///// <param name="e"></param>
        //protected void btnRemoveFriendship_Click(object sender, EventArgs e)
        //{
        //    int IDLanguage = Convert.ToInt32(Session["IDLanguage"]);

        //    ImageButton _button = (ImageButton)sender;

        //    string UserIDClicked = _button.CommandArgument.ToString();

        //    Guid IDUserGuid = new Guid(Session["IDUser"].ToString());
        //    Guid IDNewFriendGuid = new Guid(UserIDClicked.ToString());

        //    MyUser User = new MyUser(IDUserGuid);

        //    MyUser FriendToRemove = new MyUser(IDNewFriendGuid);

        //    //SP to Request or Accept Friendship
        //    MyUserFriendship ManageFriendship = new MyUserFriendship(User, FriendToRemove);
        //    ManageUSPReturnValue result = ManageFriendship.RemoveFriendship();

        //    if (!result.IsError)
        //    {
        //        //Friendship removed correctly

        //        //Show JQueryUi BoxDialog
        //        string BoxTitle = RetrieveMessage.RetrieveDBMessage(IDLanguage, "US-IN-9999");
        //        string BoxText = RetrieveMessage.RetrieveDBMessage(IDLanguage, "US-IN-0014");
        //        ScriptManager.RegisterStartupScript(this, GetType(), Guid.NewGuid().ToString(), "ShowJQuiBoxDialog('" + BoxTitle.Replace("'", "\\'") + "','" + BoxText.Replace("'", "\\'") + "');", true);

        //        //WRITE A ROW IN LOG FILE AND DB
        //        LogRow NewRowForLog = new LogRow(DateTime.UtcNow, LogLevel.CriticalErrors.ToString(), "", Network.GetCurrentPageName(), result.ResultExecutionCode, "User Removed Friendship of: " + UserIDClicked, Session["IDUser"].ToString(), false, true);
        //        LogManager.WriteDBLog(LogLevel.Warnings, NewRowForLog);
        //        LogManager.WriteFileLog(LogLevel.Warnings, false, NewRowForLog);

        //        //Update Friendship Panels
        //        CreateOrUpdateFriendshipPanels();
        //    }
        //    else //Error in Remove Friendship
        //    {
        //        //Show JQueryUi BoxDialog - JS: ShowJQuiBoxDialog(Title, Text)
        //        string BoxTitle = RetrieveMessage.RetrieveDBMessage(IDLanguage, "US-WN-9999");
        //        string BoxText = RetrieveMessage.RetrieveDBMessage(IDLanguage, result.ResultExecutionCode);
        //        string RedirectUrl = AppConfig.GetValue("HomePage", AppDomain.CurrentDomain);
        //        ScriptManager.RegisterStartupScript(this, GetType(), Guid.NewGuid().ToString(), "ShowJQuiBoxDialogWithRedirect('" + BoxTitle.Replace("'", "\\'") + "','" + BoxText.Replace("'", "\\'") + "', '" + RedirectUrl + "');", true);

        //        //WRITE A ROW IN LOG FILE AND DB
        //        LogRow NewRow = new LogRow(DateTime.UtcNow, LogLevel.CriticalErrors.ToString(), "", Network.GetCurrentPageName(), result.ResultExecutionCode, RetrieveMessage.RetrieveDBMessage(IDLanguage, result.ResultExecutionCode), Session["IDUser"].ToString(), true, false);
        //        LogManager.WriteDBLog(LogLevel.Warnings, NewRow);
        //        LogManager.WriteFileLog(LogLevel.Warnings, false, NewRow);
        //    }
        //}
        #endregion

        #region CreateListOfFriends
        /// <summary>
        /// Create a List with Users: Name of the user - Button to show his profile - Button to Delete friendship
        /// </summary>
        /// <returns></returns>
        protected void CreateListOfFriends()
        {
            try
            {
                Guid IDUserGuid = new Guid();

                //If is a Friend that is watching our profile
                if (!MyUser.IsMySelf(Request["IDUserRequested"].ToString()))
                {
                    IDUserGuid = new Guid(Request["IDUserRequested"].ToString());
                }
                else
                {
                    IDUserGuid = new Guid(Session["IDUser"].ToString());
                }

                MyUser User = new MyUser(IDUserGuid);

                MyUserFriendship ManageFriendship = new MyUserFriendship(User);

                DataTable FriendsList = new DataTable();

                FriendsList = ManageFriendship.CreateListOfFriends();

                //Count number of friends and write it in label
                lblNumberOfFriends.Text = " (" + FriendsList.Rows.Count.ToString() + ")";

                rptFriendsList.DataSource = FriendsList;

                rptFriendsList.DataBind();
            }
            catch (Exception ex)
            {
                //WRITE A ROW IN LOG FILE AND DB
                try
                {
                    LogRow NewRow = new LogRow(DateTime.UtcNow, LogLevel.Errors.ToString(), "", Network.GetCurrentPageName(), "US-ER-9999", "Error in create List of Friends: " + ex.Message, (Request["IDUserRequested"].ToString()), true, false);
                    LogManager.WriteDBLog(LogLevel.Errors, NewRow);
                    LogManager.WriteFileLog(LogLevel.Errors, true, NewRow);
                }
                catch { }
            }
        }

        #endregion

        #region CreateListOfCommonFriends - DISABLED
        /// <summary>
        /// Create a List of Friends in Common      **da ricontrollare se si attiva
        /// </summary>
        /// <returns></returns>
        //protected void CreateListOfCommonFriends()
        //{
        //    try
        //    {
        //        Guid IDUserGuid = new Guid();

        //        //If is a Friend that is watching our profile
        //        if (!MyUser.IsMySelf(Request["IDUserRequested"].ToString()))
        //        {
        //            IDUserGuid = new Guid(Request["IDUserRequested"].ToString());
        //        }

        //        MyUser User1 = new MyUser(new Guid(Session["IDUser"].ToString()));

        //        MyUser User2 = new MyUser(IDUserGuid);

        //        MyUserFriendship ManageFriendship = new MyUserFriendship(User1, User2);

        //        DataTable FriendsCommonList = new DataTable();

        //        FriendsCommonList = ManageFriendship.CreateListOfCommonFriends();

        //        //DISABLED
        //        //Count number of friends and write it in label
        //        //lblNumberOfCommonFriends.Text = " (" + FriendsCommonList.Rows.Count.ToString() + ")";

        //        //rptCommonFriendsList.DataSource = FriendsCommonList;

        //        //rptCommonFriendsList.DataBind();
        //    }
        //    catch (Exception ex)
        //    {
        //        //WRITE A ROW IN LOG FILE AND DB
        //        LogRow NewRow = new LogRow(DateTime.UtcNow, LogLevel.Errors.ToString(), "", Network.GetCurrentPageName(), "US-ER-9999", "Error in Create List of Common Friends: " + ex.Message, Session["IDUser"].ToString(), true, false);
        //        LogManager.WriteDBLog(LogLevel.Errors, NewRow);
        //        LogManager.WriteFileLog(LogLevel.Errors, true, NewRow);
        //    }
        //}

        #endregion

        #region CreateListOfFollowers
        /// <summary>
        /// Create a List with Users: Name of the user - Button to show his profile
        /// </summary>
        /// <returns></returns>
        protected void CreateListOfFollowers()
        {
            try
            {
                Guid IDUserGuid = new Guid();

                //If is a Friend that is watching our profile
                if (!MyUser.IsMySelf(Request["IDUserRequested"].ToString()))
                {
                    IDUserGuid = new Guid(Request["IDUserRequested"].ToString());
                }
                else
                {
                    IDUserGuid = new Guid(Session["IDUser"].ToString());
                }

                MyUser User = new MyUser(IDUserGuid);

                MyUserFriendship ManageFriendship = new MyUserFriendship(User);

                DataTable FollowersList = new DataTable();

                FollowersList = ManageFriendship.CreateListOfFollower();

                //Count number of friends and write it in label
                lblNumberOfFollowers.Text = " (" + FollowersList.Rows.Count.ToString() + ")";

                rptFollowersList.DataSource = FollowersList;

                rptFollowersList.DataBind();
            }
            catch (Exception ex)
            {
                //WRITE A ROW IN LOG FILE AND DB
                try
                {
                    LogRow NewRow = new LogRow(DateTime.UtcNow, LogLevel.Errors.ToString(), "", Network.GetCurrentPageName(), "US-ER-9999", "Error in Create List of Followers: " + ex.Message, Request["IDUserRequested"].ToString(), true, false);
                    LogManager.WriteDBLog(LogLevel.Errors, NewRow);
                    LogManager.WriteFileLog(LogLevel.Errors, true, NewRow);
                }
                catch { }
            }
        }

        #endregion

        #region CreateListOfFollowingPeople
        /// <summary>
        /// Create a List with Users: Name of the user - Button to show his profile
        /// </summary>
        /// <returns></returns>
        protected void CreateListOfFollowing()
        {
            try
            {
                Guid IDUserGuid = new Guid();

                //If is a Friend that is watching our profile
                if (!MyUser.IsMySelf(Request["IDUserRequested"].ToString()))
                {
                    IDUserGuid = new Guid(Request["IDUserRequested"].ToString());
                }
                else
                {
                    IDUserGuid = new Guid(Session["IDUser"].ToString());
                }

                MyUser User = new MyUser(IDUserGuid);

                MyUserFriendship ManageFriendship = new MyUserFriendship(User);

                DataTable FollowingList = new DataTable();

                FollowingList = ManageFriendship.CreateListOfUserFollowed();

                //Count number of friends and write it in label
                lblNumberOfFollowing.Text = " (" + FollowingList.Rows.Count.ToString() + ")";

                rptFollowingList.DataSource = FollowingList;

                rptFollowingList.DataBind();
            }
            catch (Exception ex)
            {
                //WRITE A ROW IN LOG FILE AND DB
                try
                {
                    LogRow NewRow = new LogRow(DateTime.UtcNow, LogLevel.Errors.ToString(), "", Network.GetCurrentPageName(), "US-ER-9999", "Error in Create List of User Followed: " + ex.Message, Request["IDUserRequested"].ToString(), true, false);
                    LogManager.WriteDBLog(LogLevel.Errors, NewRow);
                    LogManager.WriteFileLog(LogLevel.Errors, true, NewRow);
                }
                catch { }
            }
        }

        #endregion

        #region CreateListOfBlockedFriends - NOT USED NOW
        /// <summary>
        /// Create a List with Users: Name of the user - Button to show his profile - Button to Delete friendship
        /// </summary>
        /// <returns></returns>
        //protected void CreateListOfBlockedFriends()
        //{
        //    try
        //    {
        //        Guid IDUserGuid = new Guid(Session["IDUser"].ToString());

        //        MyUser User = new MyUser(IDUserGuid);

        //        MyUserFriendship ManageFriendship = new MyUserFriendship(User);

        //        DataTable BlockedFriendsList = new DataTable();

        //        BlockedFriendsList = ManageFriendship.CreateListOfBlockedFriends();

        //        //DISABLED
        //        //Blocked Users can be shown just to me
        //        //if (BlockedFriendsList.Rows.Count > 0 && myself)
        //        //{
        //        //    upnlBlockedFriendsList.Visible = true;

        //        //    rptBlockedFriendsList.DataSource = BlockedFriendsList;

        //        //    rptBlockedFriendsList.DataBind();
        //        //}
        //        //else
        //        //{
        //        //    upnlBlockedFriendsList.Visible = false;
        //        //}
        //    }
        //    catch (Exception ex)
        //    {
        //        //WRITE A ROW IN LOG FILE AND DB
        //        LogRow NewRow = new LogRow(DateTime.UtcNow, LogLevel.Errors.ToString(), "", Network.GetCurrentPageName(), "US-ER-9999", "Error in Create List of Blocked users: " + ex.Message, Session["IDUser"].ToString(), true, false);
        //        LogManager.WriteDBLog(LogLevel.Errors, NewRow);
        //        LogManager.WriteFileLog(LogLevel.Errors, true, NewRow);
        //    }
        //}

        #endregion

        #region ActionsOnDataBinding
        /// <summary>
        /// Set imageUrl for button (User Profile Photo)
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void btnIDFriendsList_DataBinding(object sender, EventArgs e)
        {
            try
            {
                ImageButton imgButton = (ImageButton)sender;
                //Label _label = (Label)sender;
                Guid IDUserGuid = new Guid(imgButton.CommandArgument.ToString());

                MyUser _user = new MyUser(IDUserGuid);
                _user.GetUserInfoAllByID();

                if (_user.IDProfilePhoto != null)
                {
                    imgButton.ImageUrl = _user.IDProfilePhoto.GetCompletePath(false, false, true);
                }
                else
                {
                    imgButton.ImageUrl = "/Images/icon-userNoPic.png";
                }
            }
            catch(Exception ex)
            {
                //WRITE A ROW IN LOG FILE AND DB
                try
                {
                    LogRow NewRow = new LogRow(DateTime.UtcNow, LogLevel.Errors.ToString(), "", Network.GetCurrentPageName(), "US-ER-9999", "Error in btnIDFriendsList_DataBinding: " + ex.Message, Request["IDUserRequested"].ToString(), true, false);
                    LogManager.WriteDBLog(LogLevel.Errors, NewRow);
                    LogManager.WriteFileLog(LogLevel.Errors, true, NewRow);
                }
                catch { }
            }
        }

        #region WriteNameOfUser_DataBinding
        /// <summary>
        /// Name of User written in labels
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void WriteNameOfUser_DataBinding(object sender, EventArgs e)
        {
            try
            {
                Label _label = (Label)sender;
                Guid IDUserGuid = new Guid(_label.Text);
                MyUser _user = new MyUser(IDUserGuid);
                _user.GetUserBasicInfoByID();
                _label.Text = _user.Name;
            }
            catch (Exception ex)
            {
                //WRITE A ROW IN LOG FILE AND DB
                try
                {
                    LogRow NewRow = new LogRow(DateTime.UtcNow, LogLevel.Errors.ToString(), "", Network.GetCurrentPageName(), "US-ER-9999", "Error in WriteNameOfUser_DataBinding: " + ex.Message, Request["IDUserRequested"].ToString(), true, false);
                    LogManager.WriteDBLog(LogLevel.Errors, NewRow);
                    LogManager.WriteFileLog(LogLevel.Errors, true, NewRow);
                }
                catch { }
            }
        }
        #endregion

        #endregion

        #endregion

    }
}