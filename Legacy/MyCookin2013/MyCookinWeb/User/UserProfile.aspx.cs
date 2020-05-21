using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Net;
using System.IO;
using System.Text;
using MyCookin.Common;
using MyCookin.ObjectManager.UserManager;
using MyCookin.ErrorAndMessage;
using MyCookin.Log;
using MyCookin.ObjectManager;
using MyCookin.ObjectManager.AuditManager;
using MyCookin.ObjectManager.UserBoardManager;
using System.Web.Services;
using MyCookinWeb.CustomControls;
using System.Data;
using MyCookin.ObjectManager.StatisticsManager;
using MyCookin.ObjectManager.MyUserNotificationManager;
using MyCookin.ObjectManager.MediaManager;

namespace MyCookinWeb.User
{
    public partial class UserProfile :  MyCookinWeb.Form.MyPageBase
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

        protected void Page_Load(object sender, EventArgs e)
        {
            //This avoid issue on routed pages
            Page.Form.Action = Request.RawUrl;

            //No default button
            Page.Form.DefaultButton = null;

            string IDUserRequested = Request["IDUserRequested"];
            
            hfIDUserRequested.Value = IDUserRequested;

            if (String.IsNullOrEmpty(IDUserRequested))
            {
                Response.Redirect((AppConfig.GetValue("HomePage", AppDomain.CurrentDomain)).ToLower(), true);
            }

            Guid IDUserRequestedGuid = new Guid(IDUserRequested);
            ctrlUserBoardPostOnFriendBoard.IDUserFriend = IDUserRequestedGuid;
           
            NavHistoryClear();
            NavHistoryAddUrl(Request.RawUrl);

            //Initialize IDLanguage
            int IDLanguage = MyConvert.ToInt32(Session["IDLanguage"].ToString(), 1); ;
                #region Spotify
                //Add Spotify Widget if user has compiled his id
                try
                {
                    MyUserPropertyCompiled propComp = new MyUserPropertyCompiled(IDUserRequestedGuid, 10, IDLanguage);
                    if (!String.IsNullOrEmpty(propComp.PropertyValue))
                    {
                        ifrSpotify.Attributes.Add("src", "https://embed.spotify.com/?uri=spotify:user:" + propComp.PropertyValue + ":starred");
                    }
                }
                catch (Exception ex)
                {
                    //WRITE A ROW IN LOG FILE AND DB
                    LogRow NewRow = new LogRow(DateTime.UtcNow, LogLevel.Errors.ToString(), "", Network.GetCurrentPageName(), "US-ER-9999", "Error loading Spotify AddIn: " + ex.Message, IDUserRequested, true, false);
                    LogManager.WriteDBLog(LogLevel.Errors, NewRow);
                    LogManager.WriteFileLog(LogLevel.Errors, false, NewRow);
                }
                #endregion

                //Label And Button to Manage Friendship is invisible on load
                btnRequestFriendship.Visible = false;
                btnRemoveFriendship.Visible = false;
                btnReportUserSpam.Visible = false;

                lblFollowingYou.Visible = false;

                //DISABLED
                //btnRemoveBlock.Visible = false;
                //btnBlockUser.Visible = false;
                //btnSetNotificationsOff.Visible = false;

                pnlPostOnFriendUserBoard.Visible = false;
                pnlStatusBar.Visible = false;

                //btnSetNotificationsOn.Visible = false;

                if (!IsPostBack)
                {
                    #region UserBoard

                    //This need for UserBoard Block Control. If a user want to share but has not given authorization yet - **da provare..!
                    #region FacebookRegistrationOrLogin
                    if (!PageSecurity.IsPublicProfile())
                    {
                        if (!String.IsNullOrEmpty(Request.QueryString["code"]))
                        {
                            string code = Request.QueryString["code"];

                            string callBackUrl = Request.Url.AbsoluteUri.Replace(Request.Url.Query, "");

                            //Client Id Which YOU Got when you Register You Application
                            string FacebookClientId = AppConfig.GetValue("facebook_appid", AppDomain.CurrentDomain);

                            WebRequest request = WebRequest.Create(string.Format("https://graph.facebook.com/oauth/" +
                            "access_token?client_id={0}&redirect_uri={1}&client_secret={2}&code={3}",
                            FacebookClientId, callBackUrl, AppConfig.GetValue("facebook_appsecret", AppDomain.CurrentDomain), code));

                            WebResponse response = request.GetResponse();

                            Stream stream = response.GetResponseStream();
                            Encoding encode = System.Text.Encoding.GetEncoding("utf-8");
                            StreamReader streamReader = new StreamReader(stream, encode);
                            string accessToken = streamReader.ReadToEnd().Replace("access_token=", "");
                            streamReader.Close();
                            response.Close();

                            string url = "/Auth/Auth.aspx?fbaccessToken=" + accessToken;
                            Response.Redirect(url, true);
                        }
                    }
                    #endregion

                    //LOAD USER BOARD CONTROL
                    if (!LoadUserBoard(IDLanguage, IDUserRequestedGuid, LoadBoardDirection.NotSpecified))
                    {
                        //If don't load, go to Home Page
                        Response.Redirect((AppConfig.GetValue("HomePage", AppDomain.CurrentDomain)).ToLower(), true);

                    }

                    #endregion
                }
               
                    #region ChooseControlForStatusOrPost
                    if (PageSecurity.IsPublicProfile())
                    {
                        pnlPostOnFriendUserBoard.Visible = false;
                        pnlStatusBar.Visible = false;
                    }
                    else
                    {
                        
                            if (MyUser.IsMySelf(IDUserRequested))
                            {
                                pnlPostOnFriendUserBoard.Visible = false;
                                pnlStatusBar.Visible = true;
                            }
                            else
                            {
                                pnlPostOnFriendUserBoard.Visible = true;
                                pnlStatusBar.Visible = false;

                               
                            }
                        
                    }
                    #endregion

                

                #region CompleteUserInformations

                MyUser User = new MyUser(IDUserRequestedGuid);
                User.GetUserInfoAllByID();
                hfUserName.Value = User.UserName;
                try
                {
                    MyUserPropertyCompiled _blogLink = new MyUserPropertyCompiled(User.IDUser, 11, IDLanguage);
                    if (!String.IsNullOrEmpty(_blogLink.PropertyValue))
                    {

                        lnkPersonalSite.NavigateUrl = "/Link.aspx?Url=" + _blogLink.PropertyValue.Replace("https://","").Replace("http://","");
                        lnkPersonalSite.Text = _blogLink.PropertyValue;
                        lnkPersonalSite.Visible = true;
                    }
                    else
                    {
                        lnkPersonalSite.Visible = false;
                    }
                }
                catch
                {
                }
                //Picture
                if (User.IDProfilePhoto != null)
                {
                    ProfileImage.ImageUrl = User.IDProfilePhoto.GetCompletePath(false, false, true);
                }
                else
                {
                    ProfileImage.ImageUrl = DefaultMedia.GetDefaultMediaPathFromWebConfig(MediaType.ProfileImagePhoto);
                }
                ProfileImage.AlternateText = User.Name + " " + User.Surname + " (" + User.UserName + ")";
                try
                {
                    hfOgpUrl.Value = AppConfig.GetValue("WebUrl", AppDomain.CurrentDomain) + Request.RawUrl;
                    hfOgpFbAppID.Value = AppConfig.GetValue("facebook_appid", AppDomain.CurrentDomain);
                    hfOgpTitle.Value = User.Name + " " + User.Surname + " - MyCookin";

                    Page.Title = User.Name + " " + User.Surname + " - MyCookin";
                    
                    if (ProfileImage.ImageUrl.IndexOf("http://") > -1 || ProfileImage.ImageUrl.IndexOf("https://") > -1)
                    {
                        hfOgpImage.Value = ProfileImage.ImageUrl;
                    }
                    else
                    {
                        hfOgpImage.Value = AppConfig.GetValue("WebUrl", AppDomain.CurrentDomain) + ProfileImage.ImageUrl;
                    }
                    hfOgpDescription.Value = User.UserName + " (" + User.Name + " " + User.Surname + ")";

                    hfKeywords.Value = "MyCookin, User, Blog, recipes," + User.UserName + ", " + User.Name + " " + User.Surname + ")";
                    hfLanguageCode.Value = MyCulture.GetLangShortCodeFromIDLanguage(IDLanguage);
                    Random random = new Random();
                    hfCreationDate.Value = DateTime.UtcNow.AddMinutes(random.Next(10, 200)).ToString();
                }
                catch
                {
                }
                //Count number of friends and write it in label
                MyUserFriendship ManageFriendship = new MyUserFriendship(User);

                //DISABLED
                //lblNumberOfFriends.Text = " (" + ManageFriendship.NumberOfFriends() + ")";

                lblNumberOfFollowers.Text = ManageFriendship.NumberOfFollowers().ToString();

                lblNumberOfFollowing.Text = ManageFriendship.NumberOfFollowing().ToString();

                //Name of the User
                lblName.Text = User.Name.ToString() + " " + User.Surname.ToString();

                lnkRecipeBook.NavigateUrl = ("/" + User.UserName + AppConfig.GetValue("RoutingRecipeBook" + IDLanguage, AppDomain.CurrentDomain).ToString()).ToLower();

                //Number of Visits for this profile
                //lblNumberOfProfileVisits.Text = User.NumberOfProfileVisits();

                //Last Time Online (of the user requested by querystring)
                if (MyUser.IsMySelf(IDUserRequested))
                {
                    lblLastTimeOnline.Text = "";

                    imgOnline.Visible = false;
                    imgOffline.Visible = false;
                }
                else
                {
                    lblLastTimeOnline.Text = MyUser.LastTimeOnline(IDUserRequestedGuid, IDLanguage, MyConvert.ToInt32(Session["Offset"].ToString(), 0));

                    //Show circle gif if online
                    if (User.LastLogon > User.LastLogout)
                    {
                        imgOnline.Visible = true;
                        imgOffline.Visible = false;
                    }
                    else
                    {
                        imgOnline.Visible = false;
                        imgOffline.Visible = true;
                    }
                }


                if (!PageSecurity.IsPublicProfile())
                {
                    if (!MyUser.IsMySelf(IDUserRequested))
                    {
                        //Here Only if I am NOT


                        //If we are logged, check if we are already friends

                        //User Me
                        Guid IDUserGuid = new Guid(Session["IDUser"].ToString());
                        MyUser Me = new MyUser(IDUserGuid);

                        MyUserFriendship Friendship = new MyUserFriendship(Me, User);

                        //If we are not friends, show label and button to request friendship
                        //if (!Friendship.CheckFriendship())

                        //If we are not following, show label and button to follow user
                        if (!Friendship.CheckFollowing())
                        {
                            //NOT FOLLOWING...

                            btnRequestFriendship.Visible = true;

                            //lblRequestFriendship.Text = "(Request Friendship)";
                            //lblRequestFriendship.Text = "";
                        }
                        else
                        {
                            //FRIENDS...

                            //Show a label like "Is following you"
                            lblFollowingYou.Visible = true;

                            //We are friends. Show button to remove friend.
                            btnRemoveFriendship.Visible = true;

                            //lblRemoveFriendship.Text = "(Remove Friendship)";
                            //lblRemoveFriendship.Text = "";

                            if (!IsPostBack)
                            {
                                #region CheckBlock Or Spam
                                //If the user is blocked, show Button to remove Block
                                if (Friendship.CheckUserBlocked())
                                {
                                    //DISABLED
                                    //btnRemoveBlock.Visible = true;
                                    //btnBlockUser.Visible = false;

                                    btnReportUserSpam.Visible = false; //always false 
                                }
                                else
                                {
                                    //DISABLED
                                    //Show Button to Block
                                    //btnBlockUser.Visible = true;
                                    //btnRemoveBlock.Visible = false;

                                    btnReportUserSpam.Visible = false;  //Set to true for restore this function currently disabled because SPAM function lock the user as well.
                                }
                                #endregion

                                #region CheckSpam
                                //Check if user has already reported this user as spammer
                                //If yes, Hide Button
                                if (Friendship.CheckUserSpamReported())
                                {
                                    btnReportUserSpam.Visible = false;
                                }
                                else
                                {
                                    btnReportUserSpam.Visible = true;
                                }
                                #endregion

                                #region CheckFollowing - NOT NECESSARY AT THE MOMENT
                                //If we are following this friend, notifications are active. Show button to deactivate Notifications (Defollow friend)
                                //if (Friendship.CheckFollowing())
                                //{
                                //    //FOLLOWING...

                                //    btnSetNotificationsOff.Visible = false;
                                //    btnSetNotificationsOn.Visible = true;
                                //}
                                //else
                                //{
                                //    //NOT FOLLOWING

                                //    //We are not following this friend. Show button to activate notifications (follow friend)
                                //    btnSetNotificationsOff.Visible = true;
                                //    btnSetNotificationsOn.Visible = false;
                                //}
                                #endregion
                            }
                        }

                        //Log that a LOGGED user has view the profile of this user
                        //WRITE A ROW IN STATISTICS DB
                        try
                        {
                            //If I am Not
                            if (!MyUser.IsMySelf(IDUserRequested))
                            {
                                MyStatistics NewStatistic = new MyStatistics(IDUserGuid, IDUserRequestedGuid, StatisticsActionType.US_ProfileViewed, "Profile Viewed", Network.GetCurrentPageName(), "", Session.SessionID);
                                NewStatistic.InsertNewRow();
                            }
                        }
                        catch { }
                    }
                    else
                    {
                        //I am
                        #region ProfileCompletePercentage
                        lblProfileCompleteTitle.Text = "Profilo Completato";
                        lblProfileComplete.Text = User.ProfileCompletePercentageCalc().ToString("0") + "%";
                        #endregion
                    }
                }

                //Log that an ANONYMOUS user has view the profile of this user
                //WRITE A ROW IN STATISTICS DB
                try
                {
                    MyStatistics NewStatisticUser = new MyStatistics(null, IDUserRequestedGuid, StatisticsActionType.US_ProfileViewed, "Profile Viewed", Network.GetCurrentPageName(), "", Session.SessionID);
                    NewStatisticUser.InsertNewRow();
                }
                catch { }

                //}
                #endregion
            
        }

        #region LoadUserBoard
        protected bool LoadUserBoard(int IDLanguage, Guid IDUserRequestedGuid, LoadBoardDirection Direction)
        {
            try
            {
                #region UserBoardWithPagination (with buttons left and right) - DISABLED
                /*
            #region PagingAndButtonToScrollBoard
            //Max size of one page
            int PageSize = MyConvert.ToInt32(AppConfig.GetValue("UserBoardPagingSize", AppDomain.CurrentDomain).ToString(), 5);

            //First Paging Index
            int PageIndex = 1;
            int NextIndex = 1;

            //At Page 1, Previous Button is disabled
            if (MyConvert.ToInt32(hfPageIndex.Value, 1) == 1)
            {
                ibtnUserBoardLoadPrevious.Visible = false;
            }

            //If the number of rows is less than paging, all results are in one page.
            //Then disable button Next
            if (MyConvert.ToInt32(hfPageCount.Value, 1) == 1)
            {
                ibtnUserBoardLoadNext.Visible = false;
            }

            PageIndex = MyConvert.ToInt32(hfPageIndex.Value, 1);

            switch (Direction)
            {
                case LoadBoardDirection.Next:
                    NextIndex = PageIndex + 1;

                    if (NextIndex <= Convert.ToInt32(hfPageCount.Value))
                    {
                        hfPageIndex.Value = NextIndex.ToString();
                        PageIndex = NextIndex;

                        //Active button Previous
                        ibtnUserBoardLoadPrevious.Visible = true;

                        //If we reach the max Disable button Next
                        if (NextIndex == Convert.ToInt32(hfPageCount.Value))
                        {
                            ibtnUserBoardLoadNext.Visible = false;
                        }
                    }
                    else
                    {
                        //Disable button Next
                        ibtnUserBoardLoadNext.Visible = false;
                    }
                break;

                case LoadBoardDirection.Previous:
                    NextIndex = PageIndex - 1;

                    //Until nextIndex is less than NumberOfPages
                    if (NextIndex > 0)
                    {
                        PageIndex = NextIndex;
                        hfPageIndex.Value = PageIndex.ToString();
                            
                        //Active button Next
                        ibtnUserBoardLoadNext.Visible = true;

                        //If We Reach the first page disable Button Previous 
                        if (PageIndex == 1)
                        {
                            ibtnUserBoardLoadPrevious.Visible = false;
                        }
                    }
                    else
                    {
                        //Disable Button Previous
                        ibtnUserBoardLoadPrevious.Visible = false;
                    }
                break;
            }
            #endregion

            //Load Board according to PageIndex and PageSize
            UserBoard MainUserBoard = new UserBoard(IDLanguage, IDUserRequestedGuid, "desc", PageIndex, PageSize);

            List<UserBoard> MainUserBoardList = new List<UserBoard>();

            MainUserBoardList = MainUserBoard.GetUsersBoardWithPagination();

            //Set in the hidden field the number of pages for Paging (always the same)
            hfPageCount.Value = MainUserBoard.PageCount.ToString();
             * */
                #endregion

                #region UserBoardBlockLoad

                int PageCount = 0;

                switch (Direction)
                {
                    case LoadBoardDirection.NotSpecified:
                        //First Load. Set default.
                        PageCount = MyConvert.ToInt32(AppConfig.GetValue("UserBoardPagingSize", AppDomain.CurrentDomain).ToString(), 5);

                        //Update hidden Value
                        hfPageCount.Value = PageCount.ToString();

                        break;
                    case LoadBoardDirection.Next:
                        //Get actual block numbers loaded
                        PageCount = MyConvert.ToInt32(hfPageCount.Value, 5);

                        //Update new value for query
                        PageCount += MyConvert.ToInt32(AppConfig.GetValue("UserBoardPagingSize", AppDomain.CurrentDomain).ToString(), 5);

                        //Update hidden Value
                        hfPageCount.Value = PageCount.ToString();
                        break;
                }

                //Load board
                UserBoard MainUserBoard = new UserBoard(ActionTypes.NotSpecified, IDLanguage, IDUserRequestedGuid, null, "desc", PageCount);

                List<UserBoard> MainUserBoardList = new List<UserBoard>();

                MainUserBoardList = MainUserBoard.UsersBoardBlockLoad();

                //Disable button "load more" if reached the end of board.
                //if (MainUserBoardList.Count > PageCount)
                //{
                //    ibtnUserBoardLoadNext.Visible = true;
                //}
                //else
                //{
                //    ibtnUserBoardLoadNext.Visible = false;
                //}

                //Disable button "load more" if reached the end of board.
                if (MainUserBoardList.Count < PageCount)
                {
                    ibtnUserBoardLoadNext.Visible = false;
                }

                #endregion

                //Bind Repeater
                rptUserBoardControl.DataSource = MainUserBoardList;
                rptUserBoardControl.DataBind();

                return true;
            }
            catch (Exception ex)
            {
                //WRITE A ROW IN LOG FILE AND DB
                try
                {
                    LogRow NewRow = new LogRow(DateTime.UtcNow, LogLevel.Errors.ToString(), "", Network.GetCurrentPageName(), "US-ER-9999", "Error Loading UserBoard: " + ex.Message, IDUserRequestedGuid.ToString(), true, false);
                    LogManager.WriteDBLog(LogLevel.Errors, NewRow);
                    LogManager.WriteFileLog(LogLevel.Errors, true, NewRow);
                }
                catch { }

                return false;
            }
        }
        #endregion

        #region ButtonToRequestFriendship
        /// <summary>
        /// Button to request friendship if we are already logged and we are not already friends.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void btnRequestFriendship_Click(object sender, ImageClickEventArgs e)
        {
            try
            {
                //Change icon with "Loading.."
                btnRequestFriendship.Visible = false;
                btnRemoveFriendship.Visible = false;
                imgFriendshipActionLoading.Visible = true;

                int IDLanguage = MyConvert.ToInt32(Session["IDLanguage"].ToString(), 1);

                string UserIDClicked = Request["IDUserRequested"];  //This is the difference with the same button generated dynamically in PotentialFriends Lists (UserFriends.aspx)

                Guid IDUserGuid = new Guid(Session["IDUser"].ToString());
                Guid IDNewFriendGuid = new Guid(UserIDClicked.ToString());

                MyUser User = new MyUser(IDUserGuid);

                MyUser NewFriend = new MyUser(IDNewFriendGuid);
                NewFriend.GetUserBasicInfoByID();

                //If a user remove friendship, the other continue to follow him.
                //When this other choose of follow him too, frendship already exist because it has been already inserted.
                bool FriendshipRequestError = false;

                //SP to Request or Accept Friendship
                MyUserFriendship ManageFriendship = new MyUserFriendship(User, NewFriend);
                ManageUSPReturnValue result = ManageFriendship.RequestOrAcceptFriendship();

                //Check if we are friends. If not, check if the request has been executed correctly. (If yes, a new request return always error ;))
                if (!ManageFriendship.CheckFriendship())
                {
                    FriendshipRequestError = result.IsError;
                }

                if (!FriendshipRequestError)
                {
                    //Show Message

                    //Show JQueryUi BoxDialog - not necessary
                    //string BoxTitle = RetrieveMessage.RetrieveDBMessage(IDLanguage, "US-IN-9999");
                    //string BoxText = RetrieveMessage.RetrieveDBMessage(IDLanguage, "US-IN-0019");
                    //ScriptManager.RegisterStartupScript(this, GetType(), Guid.NewGuid().ToString(), "ShowJQuiBoxDialog('" + BoxTitle.Replace("'", "\\'") + "','" + BoxText.Replace("'", "\\'") + "');", true);

                    //WRITE A ROW IN STATISTICS DB
                    try
                    {
                        MyStatistics NewStatistic = new MyStatistics(IDUserGuid, IDNewFriendGuid, StatisticsActionType.US_FriendshipRequest, "Request For Friendship", Network.GetCurrentPageName(), "", Session.SessionID);
                        NewStatistic.InsertNewRow();
                    }
                    catch { }

                    

                    //Send email to user we ask for friendship, if he want to receive it
                    MyUserNotification Notification = new MyUserNotification(NewFriend.IDUser, NotificationTypes.NewFollower, IDLanguage);
                    
                    if (Notification.IsNotificationEnabled())
                    {
                        string From = AppConfig.GetValue("EmailFromProfileUser", AppDomain.CurrentDomain);
                        string To = NewFriend.eMail;
                        string Subject = RetrieveMessage.RetrieveDBMessage(IDLanguage, result.ResultExecutionCode);
                        string link = Network.GetCurrentPathUrl() + Server.UrlEncode("UserProfile.aspx?IDUserRequested=" + Session["IDUser"].ToString());  //User Id - Link to User Profile
                        //string Message = "<a href=\"" + link + "\">" + RetrieveMessage.RetrieveDBMessage(IDLanguage, result.ResultExecutionCode) + "</a>";
                        //string url = "/PagesForEmail/NewFriendship.aspx?link=" + link;
                        string url = "/PagesForEmail/NewFriendshipRequest.aspx?link=" + link;

                        Network Mail = new Network(From, To, "", "", Subject, "", url);

                        System.Threading.Thread ThreadMail = new System.Threading.Thread(new System.Threading.ThreadStart(Mail.SendEmailThread));
                        ThreadMail.IsBackground = true;
                        ThreadMail.Start();
                    }

                    //Hide Button to Request Friendship
                    imgFriendshipActionLoading.Visible = false;
                    btnRequestFriendship.Visible = false;
                    btnRemoveFriendship.Visible = true;

                }
                else //Error in Request Friendship
                {                   
                    //Show JQueryUi BoxDialog With Redirect
                    string BoxTitle = RetrieveMessage.RetrieveDBMessage(IDLanguage, "US-WN-9999");
                    string BoxText = RetrieveMessage.RetrieveDBMessage(IDLanguage, result.ResultExecutionCode);
                    //string RedirectUrl = AppConfig.GetValue("HomePage", AppDomain.CurrentDomain);
                    //ScriptManager.RegisterStartupScript(this, GetType(), Guid.NewGuid().ToString(), "ShowJQuiBoxDialogWithRedirect('" + BoxTitle.Replace("'", "\\'") + "','" + BoxText.Replace("'", "\\'") + "', '" + RedirectUrl + "');", true);

                    //WRITE A ROW IN LOG FILE AND DB
                    try
                    {
                        LogRow NewRow = new LogRow(DateTime.UtcNow, LogLevel.CriticalErrors.ToString(), "", Network.GetCurrentPageName(), result.ResultExecutionCode, RetrieveMessage.RetrieveDBMessage(IDLanguage, result.ResultExecutionCode), Session["IDUser"].ToString(), true, false);
                        LogManager.WriteDBLog(LogLevel.CriticalErrors, NewRow);
                        LogManager.WriteFileLog(LogLevel.CriticalErrors, true, NewRow);
                    }
                    catch { }
                }
            }
            catch (Exception ex)
            {
                //WRITE A ROW IN LOG FILE AND DB
                try
                {
                    LogRow NewRow = new LogRow(DateTime.UtcNow, LogLevel.Errors.ToString(), "", Network.GetCurrentPageName(), "US-ER-9999", "General Error in btnRequestFriendship_Click: " + ex.Message, Request["IDUserRequested"].ToString(), true, false);
                    LogManager.WriteDBLog(LogLevel.Errors, NewRow);
                    LogManager.WriteFileLog(LogLevel.Errors, true, NewRow);
                }
                catch { }
            }
        }
        #endregion

        #region ButtonRemoveFriendship
        /// <summary>
        /// Button to Remove friendship.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void btnRemoveFriendship_Click(object sender, ImageClickEventArgs e)
        {
            try
            {
                int IDLanguage = MyConvert.ToInt32(Session["IDLanguage"].ToString(), 1);

                string UserIDClicked = Request["IDUserRequested"];

                Guid IDUserGuid = new Guid(Session["IDUser"].ToString());
                Guid IDFriendGuid = new Guid(UserIDClicked.ToString());

                MyUser User = new MyUser(IDUserGuid);

                MyUser FriendToRemove = new MyUser(IDFriendGuid);

                //SP to Request or Accept Friendship

                //If a user remove friendship, the other continue to follow him.
                //When this other choose of don't follow him too, frendship already does not exist because it has been already removed.
                bool FriendshipRemoveError = false;

                MyUserFriendship ManageFriendship = new MyUserFriendship(User, FriendToRemove);

                //Use this if you are managing friendship with delete FRIEND button
                //ManageUSPReturnValue result = ManageFriendship.RemoveFriendship();

                //Use this if you are managing friendship with delete FOLLOW button
                ManageUSPReturnValue result = ManageFriendship.RemoveFriendshipForUseWithFollow();
                //Check if we are friends. If not, maybe the other have already removed friendship. So continue..
                if (ManageFriendship.CheckFriendship())
                {    
                    FriendshipRemoveError = result.IsError;
                }

                if (!FriendshipRemoveError)
                {
                    //Friendship removed correctly

                    //Hide Button to Delete Friendship
                    btnRemoveFriendship.Visible = false;
                    btnRequestFriendship.Visible = true;

                    //Show JQueryUi BoxDialog - not necessary
                    //string BoxTitle = RetrieveMessage.RetrieveDBMessage(IDLanguage, "US-IN-9999");
                    //string BoxText = RetrieveMessage.RetrieveDBMessage(IDLanguage, "US-IN-0014");
                    //string RedirectUrl = "UserProfile.aspx?IDUserRequested=" + Request["IDUserRequested"].ToString();
                    //ScriptManager.RegisterStartupScript(this, GetType(), Guid.NewGuid().ToString(), "ShowJQuiBoxDialog('" + BoxTitle.Replace("'", "\\'") + "','" + BoxText.Replace("'", "\\'") + "');", true);

                    //WRITE A ROW IN STATISTICS DB
                    try
                    {
                        MyStatistics NewStatistic = new MyStatistics(IDUserGuid, IDFriendGuid, StatisticsActionType.US_FriendshipRemoved, "Friendship Removed", Network.GetCurrentPageName(), "", Session.SessionID);
                        NewStatistic.InsertNewRow();
                    }
                    catch { }

                    //Defollow user
                    #region Defollow User
                    MyUser Me = new MyUser(IDUserGuid);

                    MyUser Friend = new MyUser(IDFriendGuid);

                    //SP to Defollow User
                    MyUserFriendship ManageFriendshipForDefollow = new MyUserFriendship(Me, Friend);
                    ManageUSPReturnValue resultDefollow = ManageFriendshipForDefollow.DefollowUser();

                    if (!resultDefollow.IsError)
                    {
                        //Show Message

                        //Show JQueryUi BoxDialog
                        //string BoxTitle = RetrieveMessage.RetrieveDBMessage(IDLanguage, "US-IN-9999");
                        //string BoxText = RetrieveMessage.RetrieveDBMessage(IDLanguage, "US-IN-0035");
                        //ScriptManager.RegisterStartupScript(this, GetType(), Guid.NewGuid().ToString(), "ShowJQuiBoxDialog('" + BoxTitle.Replace("'", "\\'") + "','" + BoxText.Replace("'", "\\'") + "');", true);

                        //WRITE A ROW IN STATISTICS DB
                        try
                        {
                            MyStatistics NewStatistic2 = new MyStatistics(IDUserGuid, IDFriendGuid, StatisticsActionType.US_UserDefollowed, "User Defollowed", Network.GetCurrentPageName(), "", "");
                            NewStatistic2.InsertNewRow();
                        }
                        catch { }
                    }
                    else //Error in Set Notifications Off
                    {
                        //Show JQueryUi BoxDialog With Redirect
                        string BoxTitle = RetrieveMessage.RetrieveDBMessage(IDLanguage, "US-WN-9999");
                        string BoxText = RetrieveMessage.RetrieveDBMessage(IDLanguage, result.ResultExecutionCode);
                        string RedirectUrl = AppConfig.GetValue("HomePage", AppDomain.CurrentDomain);
                        ScriptManager.RegisterStartupScript(this, GetType(), Guid.NewGuid().ToString(), "ShowJQuiBoxDialogWithRedirect('" + BoxTitle.Replace("'", "\\'") + "','" + BoxText.Replace("'", "\\'") + "', '" + RedirectUrl + "');", true);

                        //WRITE A ROW IN LOG FILE AND DB
                        try
                        {
                            LogRow NewRow = new LogRow(DateTime.UtcNow, LogLevel.CriticalErrors.ToString(), "", Network.GetCurrentPageName(), result.ResultExecutionCode, RetrieveMessage.RetrieveDBMessage(IDLanguage, result.ResultExecutionCode), Session["IDUser"].ToString(), true, false);
                            LogManager.WriteDBLog(LogLevel.Warnings, NewRow);
                            LogManager.WriteFileLog(LogLevel.Warnings, false, NewRow);
                        }
                        catch { }
                    }
                    #endregion

                }
                else //Error in Remove Friendship
                {
                    //Show JQueryUi BoxDialog - JS: ShowJQuiBoxDialog(Title, Text)
                    string BoxTitle = RetrieveMessage.RetrieveDBMessage(IDLanguage, "US-WN-9999");
                    string BoxText = RetrieveMessage.RetrieveDBMessage(IDLanguage, result.ResultExecutionCode);
                    string RedirectUrl = AppConfig.GetValue("HomePage", AppDomain.CurrentDomain);
                    ScriptManager.RegisterStartupScript(this, GetType(), Guid.NewGuid().ToString(), "ShowJQuiBoxDialogWithRedirect('" + BoxTitle.Replace("'", "\\'") + "','" + BoxText.Replace("'", "\\'") + "', '" + RedirectUrl + "');", true);

                    //WRITE A ROW IN LOG FILE AND DB
                    try
                    {
                        LogRow NewRow = new LogRow(DateTime.UtcNow, LogLevel.CriticalErrors.ToString(), "", Network.GetCurrentPageName(), result.ResultExecutionCode, RetrieveMessage.RetrieveDBMessage(IDLanguage, result.ResultExecutionCode), Session["IDUser"].ToString(), true, false);
                        LogManager.WriteDBLog(LogLevel.Warnings, NewRow);
                        LogManager.WriteFileLog(LogLevel.Warnings, false, NewRow);
                    }
                    catch { }
                }
            }
            catch (Exception ex)
            {
                //WRITE A ROW IN LOG FILE AND DB
                try
                {
                    LogRow NewRow = new LogRow(DateTime.UtcNow, LogLevel.Errors.ToString(), "", Network.GetCurrentPageName(), "US-ER-9999", "General Error in btnRemoveFriendship_Click: " + ex.Message, Request["IDUserRequested"].ToString(), true, false);
                    LogManager.WriteDBLog(LogLevel.Errors, NewRow);
                    LogManager.WriteFileLog(LogLevel.Errors, true, NewRow);
                }
                catch { }
            }
        }
        #endregion

        #region ButtonToBlockUser
        /// <summary>
        /// Button to block user
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void btnBlockUser_Click(object sender, ImageClickEventArgs e)
        {
            try
            {
                int IDLanguage = MyConvert.ToInt32(Session["IDLanguage"].ToString(), 1);

                string UserIDClicked = Request["IDUserRequested"];  //This is the difference with the same button generated dynamically in PotentialFriends Lists (UserFriends.aspx)

                Guid IDUserGuid = new Guid(Session["IDUser"].ToString());
                Guid IDFriendGuid = new Guid(UserIDClicked.ToString());

                MyUser Me = new MyUser(IDUserGuid);

                MyUser Friend = new MyUser(IDFriendGuid);

                //SP to Block User
                MyUserFriendship ManageFriendship = new MyUserFriendship(Me, Friend);
                ManageUSPReturnValue result = ManageFriendship.BlockUser();

                if (!result.IsError)
                {
                    //Show Message

                    //Show JQueryUi BoxDialog
                    string BoxTitle = RetrieveMessage.RetrieveDBMessage(IDLanguage, "US-IN-9999");
                    string BoxText = RetrieveMessage.RetrieveDBMessage(IDLanguage, "US-IN-0031");
                    ScriptManager.RegisterStartupScript(this, GetType(), Guid.NewGuid().ToString(), "ShowJQuiBoxDialog('" + BoxTitle.Replace("'", "\\'") + "','" + BoxText.Replace("'", "\\'") + "');", true);

                    //WRITE A ROW IN STATISTICS DB
                    try
                    {
                        MyStatistics NewStatistic = new MyStatistics(IDUserGuid, IDFriendGuid, StatisticsActionType.US_UserBlocked, "User Blocked", Network.GetCurrentPageName(), "", Session.SessionID);
                        NewStatistic.InsertNewRow();
                    }
                    catch { }

                    //DISABLED
                    //btnRemoveBlock.Visible = true;
                    //btnBlockUser.Visible = false;

                    btnReportUserSpam.Visible = false;

                }
                else //Error in Block User
                {
                    //Show JQueryUi BoxDialog With Redirect
                    string BoxTitle = RetrieveMessage.RetrieveDBMessage(IDLanguage, "US-WN-9999");
                    string BoxText = RetrieveMessage.RetrieveDBMessage(IDLanguage, result.ResultExecutionCode);
                    string RedirectUrl = AppConfig.GetValue("HomePage", AppDomain.CurrentDomain);
                    ScriptManager.RegisterStartupScript(this, GetType(), Guid.NewGuid().ToString(), "ShowJQuiBoxDialogWithRedirect('" + BoxTitle.Replace("'", "\\'") + "','" + BoxText.Replace("'", "\\'") + "', '" + RedirectUrl + "');", true);

                    //WRITE A ROW IN LOG FILE AND DB
                    try
                    {
                        LogRow NewRow = new LogRow(DateTime.UtcNow, LogLevel.CriticalErrors.ToString(), "", Network.GetCurrentPageName(), result.ResultExecutionCode, RetrieveMessage.RetrieveDBMessage(IDLanguage, result.ResultExecutionCode), Session["IDUser"].ToString(), true, false);
                        LogManager.WriteDBLog(LogLevel.Warnings, NewRow);
                        LogManager.WriteFileLog(LogLevel.Warnings, false, NewRow);
                    }
                    catch { }
                }
            }
            catch (Exception ex)
            {
                //WRITE A ROW IN LOG FILE AND DB
                try
                {
                    LogRow NewRow = new LogRow(DateTime.UtcNow, LogLevel.Errors.ToString(), "", Network.GetCurrentPageName(), "US-ER-9999", "General Error in btnBlockUser_Click: " + ex.Message, Request["IDUserRequested"].ToString(), true, false);
                    LogManager.WriteDBLog(LogLevel.Errors, NewRow);
                    LogManager.WriteFileLog(LogLevel.Errors, true, NewRow);
                }
                catch { }
            }
        }
        #endregion

        #region ButtonToRemoveBlock
        /// <summary>
        /// Button to Remove the user block
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void btnRemoveBlock_Click(object sender, ImageClickEventArgs e)
        {
            try
            {
                int IDLanguage = MyConvert.ToInt32(Session["IDLanguage"].ToString(), 1);

                string UserIDClicked = Request["IDUserRequested"];

                Guid IDUserGuid = new Guid(Session["IDUser"].ToString());
                Guid IDFriendGuid = new Guid(UserIDClicked.ToString());

                MyUser Me = new MyUser(IDUserGuid);

                MyUser Friend = new MyUser(IDFriendGuid);
                //NewFriend.GetUserBasicInfoByID();

                //SP to Request or Accept Friendship
                MyUserFriendship ManageFriendship = new MyUserFriendship(Me, Friend);
                ManageUSPReturnValue result = ManageFriendship.RemoveBlockUser();

                if (!result.IsError)
                {
                    //Show Message

                    //Show JQueryUi BoxDialog
                    string BoxTitle = RetrieveMessage.RetrieveDBMessage(IDLanguage, "US-IN-9999");
                    string BoxText = RetrieveMessage.RetrieveDBMessage(IDLanguage, "US-IN-0032");
                    ScriptManager.RegisterStartupScript(this, GetType(), Guid.NewGuid().ToString(), "ShowJQuiBoxDialog('" + BoxTitle.Replace("'", "\\'") + "','" + BoxText.Replace("'", "\\'") + "');", true);

                    //WRITE A ROW IN STATISTICS DB
                    try
                    {
                        MyStatistics NewStatistic = new MyStatistics(IDUserGuid, IDFriendGuid, StatisticsActionType.US_UserBlockRemoved, "User Block Removed", Network.GetCurrentPageName(), "", Session.SessionID);
                        NewStatistic.InsertNewRow();
                    }
                    catch { }

                    //DISABLED
                    //btnRemoveBlock.Visible = false;
                    //btnBlockUser.Visible = true;
                    //btnReportUserSpam.Visible = true;
                }
                else //Error in Remove Block
                {
                    //Show JQueryUi BoxDialog With Redirect
                    string BoxTitle = RetrieveMessage.RetrieveDBMessage(IDLanguage, "US-WN-9999");
                    string BoxText = RetrieveMessage.RetrieveDBMessage(IDLanguage, result.ResultExecutionCode);
                    string RedirectUrl = AppConfig.GetValue("HomePage", AppDomain.CurrentDomain);
                    ScriptManager.RegisterStartupScript(this, GetType(), Guid.NewGuid().ToString(), "ShowJQuiBoxDialogWithRedirect('" + BoxTitle.Replace("'", "\\'") + "','" + BoxText.Replace("'", "\\'") + "', '" + RedirectUrl + "');", true);

                    //WRITE A ROW IN LOG FILE AND DB
                    try
                    {
                        LogRow NewRow = new LogRow(DateTime.UtcNow, LogLevel.CriticalErrors.ToString(), "", Network.GetCurrentPageName(), result.ResultExecutionCode, RetrieveMessage.RetrieveDBMessage(IDLanguage, result.ResultExecutionCode), Session["IDUser"].ToString(), true, false);
                        LogManager.WriteDBLog(LogLevel.Warnings, NewRow);
                        LogManager.WriteFileLog(LogLevel.Warnings, false, NewRow);
                    }
                    catch { }
                }
            }
            catch (Exception ex)
            {
                //WRITE A ROW IN LOG FILE AND DB
                try
                {
                    LogRow NewRow = new LogRow(DateTime.UtcNow, LogLevel.Errors.ToString(), "", Network.GetCurrentPageName(), "US-ER-9999", "General Error in btnRemoveBlock_Click: " + ex.Message, Request["IDUserRequested"].ToString(), true, false);
                    LogManager.WriteDBLog(LogLevel.Errors, NewRow);
                    LogManager.WriteFileLog(LogLevel.Errors, true, NewRow);
                }
                catch { }
            }
        }
        #endregion

        #region ButtonToReportUserAsSpammer
        /// <summary>
        /// Button to Report User as Spammer AND BLOCK it
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void btnReportUserSpam_Click(object sender, ImageClickEventArgs e)
        {
            try
            {
                int IDLanguage = MyConvert.ToInt32(Session["IDLanguage"].ToString(), 1);

                string UserIDClicked = Request["IDUserRequested"];

                Guid IDUserGuid = new Guid(Session["IDUser"].ToString());
                Guid IDFriendGuid = new Guid(UserIDClicked.ToString());

                MyUserFriendship Friendship = new MyUserFriendship(IDUserGuid, IDFriendGuid);

                //Check if user has already reported this user as spammer
                if (!Friendship.CheckUserSpamReported())
                {

                    MyUser Me = new MyUser(IDUserGuid);

                    MyUser Friend = new MyUser(IDFriendGuid);

                    //dbo.AuditEvent will contain:
                    //User Reported as Spammer in Column ObjectID
                    //User that send report in Column ObjTxtInfo
                    Audit UserSpam = new Audit("User Spam Reported", IDFriendGuid, ObjectType.UserSpam, IDUserGuid.ToString(), AuditEventLevel.Hight, DateTime.UtcNow);

                    //SP to Add event to DB Audit
                    ManageUSPReturnValue resultReportSpam = UserSpam.AddEvent();

                    if (!resultReportSpam.IsError)
                    {
                        //Show Message

                        //Show JQueryUi BoxDialog
                        string BoxTitle = RetrieveMessage.RetrieveDBMessage(IDLanguage, "US-IN-9999");
                        string BoxText = RetrieveMessage.RetrieveDBMessage(IDLanguage, "US-IN-0033");
                        ScriptManager.RegisterStartupScript(this, GetType(), Guid.NewGuid().ToString(), "ShowJQuiBoxDialog('" + BoxTitle.Replace("'", "\\'") + "','" + BoxText.Replace("'", "\\'") + "');", true);

                        //WRITE A ROW IN STATISTICS DB
                        try
                        {
                            MyStatistics NewStatistic = new MyStatistics(IDUserGuid, IDFriendGuid, StatisticsActionType.US_UserReportedAsSpammer, "User Reported As Spammer", Network.GetCurrentPageName(), "", Session.SessionID);
                            NewStatistic.InsertNewRow();
                        }
                        catch { }

                        //Hide Button
                        //DISABLED
                        //btnRemoveBlock.Visible = true;
                        //btnBlockUser.Visible = false;
                        btnReportUserSpam.Visible = false;
                    }
                    else //Error in Report User as Spammer
                    {
                        //Show JQueryUi BoxDialog With Redirect
                        string BoxTitle = RetrieveMessage.RetrieveDBMessage(IDLanguage, "US-WN-9999");
                        string BoxText = RetrieveMessage.RetrieveDBMessage(IDLanguage, resultReportSpam.ResultExecutionCode);
                        string RedirectUrl = AppConfig.GetValue("HomePage", AppDomain.CurrentDomain);
                        ScriptManager.RegisterStartupScript(this, GetType(), Guid.NewGuid().ToString(), "ShowJQuiBoxDialogWithRedirect('" + BoxTitle.Replace("'", "\\'") + "','" + BoxText.Replace("'", "\\'") + "', '" + RedirectUrl + "');", true);

                        //WRITE A ROW IN LOG FILE AND DB
                        try
                        {
                            LogRow NewRow = new LogRow(DateTime.UtcNow, LogLevel.CriticalErrors.ToString(), "", Network.GetCurrentPageName(), resultReportSpam.ResultExecutionCode, RetrieveMessage.RetrieveDBMessage(IDLanguage, resultReportSpam.ResultExecutionCode), Session["IDUser"].ToString(), true, false);
                            LogManager.WriteDBLog(LogLevel.Warnings, NewRow);
                            LogManager.WriteFileLog(LogLevel.Warnings, false, NewRow);
                        }
                        catch { }
                    }

                    //Defollow User
                    MyUserFriendship ManageFriendship = new MyUserFriendship(Me, Friend);
                    ManageFriendship.DefollowUser();


                    //Questo bloccava automaticamente l'utente segnalato come spam.
                    //In futuro riattivare il link per bloccare l'utente e tenerlo separato
                    
                    //SP to Block User
                    //MyUserFriendship ManageFriendship = new MyUserFriendship(Me, Friend);
                    //ManageUSPReturnValue resultBlock = ManageFriendship.BlockUser();

                    //if (!resultBlock.IsError)
                    //{
                    //    //Show Message

                    //    //Show JQueryUi BoxDialog
                    //    string BoxTitle = RetrieveMessage.RetrieveDBMessage(IDLanguage, "US-IN-9999");
                    //    string BoxText = RetrieveMessage.RetrieveDBMessage(IDLanguage, "US-IN-0033");
                    //    ScriptManager.RegisterStartupScript(this, GetType(), Guid.NewGuid().ToString(), "ShowJQuiBoxDialog('" + BoxTitle.Replace("'", "\\'") + "','" + BoxText.Replace("'", "\\'") + "');", true);

                    //    //WRITE A ROW IN STATISTICS DB
                    //    try
                    //    {
                    //        MyStatistics NewStatistic = new MyStatistics(IDUserGuid, IDFriendGuid, StatisticsActionType.US_UserBlocked, "User Blocked", Network.GetCurrentPageName(), "", "");
                    //        NewStatistic.InsertNewRow();
                    //    }
                    //    catch { }

                    //    //Hide Button
                    //    btnReportUserSpam.Visible = false;
                    //}
                    //else //Error in Report User as Spammer
                    //{
                    //    //Show JQueryUi BoxDialog With Redirect - Not Necessary
                    //    //string BoxTitle = RetrieveMessage.RetrieveDBMessage(IDLanguage, "US-WN-9999");
                    //    //string BoxText = RetrieveMessage.RetrieveDBMessage(IDLanguage, resultBlock.ResultExecutionCode);
                    //    //string RedirectUrl = AppConfig.GetValue("HomePage", AppDomain.CurrentDomain);
                    //    //ScriptManager.RegisterStartupScript(this, GetType(), Guid.NewGuid().ToString(), "ShowJQuiBoxDialogWithRedirect('" + BoxTitle.Replace("'", "\\'") + "','" + BoxText.Replace("'", "\\'") + "', '" + RedirectUrl + "');", true);

                    //    //WRITE A ROW IN LOG FILE AND DB
                    //    try
                    //    {
                    //        LogRow NewRow = new LogRow(DateTime.UtcNow, LogLevel.CriticalErrors.ToString(), "", Network.GetCurrentPageName(), resultBlock.ResultExecutionCode, RetrieveMessage.RetrieveDBMessage(IDLanguage, resultBlock.ResultExecutionCode), Session["IDUser"].ToString(), true, false);
                    //        LogManager.WriteDBLog(LogLevel.Warnings, NewRow);
                    //        LogManager.WriteFileLog(LogLevel.Warnings, false, NewRow);
                    //    }
                    //    catch { }
                    //}
                }
            }
            catch (Exception ex)
            {
                //WRITE A ROW IN LOG FILE AND DB
                try
                {
                    LogRow NewRow = new LogRow(DateTime.UtcNow, LogLevel.Errors.ToString(), "", Network.GetCurrentPageName(), "US-ER-9999", "General Error in btnReportUserSpam_Click: " + ex.Message, Request["IDUserRequested"].ToString(), true, false);
                    LogManager.WriteDBLog(LogLevel.Errors, NewRow);
                    LogManager.WriteFileLog(LogLevel.Errors, true, NewRow);
                }
                catch { }
            }
        }
        #endregion

        #region ButtonToShowFriendsPage
        protected void btnShowFriends_Click(object sender, ImageClickEventArgs e)
        {
            try
            {
                //string link = "/User/UserFriends.aspx?IDUserRequested=" + Request["IDUserRequested"] + "&Show=Friends";
                string link = ("/" + hfUserName.Value + "/Friends").ToLower();
                Response.Redirect(link, true);
            }
            catch (Exception ex)
            {
                //WRITE A ROW IN LOG FILE AND DB
                try
                {
                    LogRow NewRow = new LogRow(DateTime.UtcNow, LogLevel.Errors.ToString(), "", Network.GetCurrentPageName(), "US-ER-9999", "General Error in btnShowFriends_Click: " + ex.Message, Request["IDUserRequested"].ToString(), true, false);
                    LogManager.WriteDBLog(LogLevel.Errors, NewRow);
                    LogManager.WriteFileLog(LogLevel.Errors, true, NewRow);
                }
                catch { }
            }
        }
        #endregion

        #region ButtonToShowFollowers
        //If you use ImageButton
        //protected void btnShowFollowers_Click(object sender, ImageClickEventArgs e)
        //{
        //    try
        //    {
        //        string link = "/User/UserFriends.aspx?IDUserRequested=" + Request["IDUserRequested"] + "&Show=Followers";
        //        Response.Redirect(link, true);
        //    }
        //    catch (Exception ex)
        //    {
        //        //WRITE A ROW IN LOG FILE AND DB
        //        try
        //        {
        //            LogRow NewRow = new LogRow(DateTime.UtcNow, LogLevel.Errors.ToString(), "", Network.GetCurrentPageName(), "US-ER-9999", "General Error in btnShowFollowers_Click: " + ex.Message, Request["IDUserRequested"].ToString(), true, false);
        //            LogManager.WriteDBLog(LogLevel.Errors, NewRow);
        //            LogManager.WriteFileLog(LogLevel.Errors, true, NewRow);
        //        }
        //        catch { }
        //    }
        //}

        protected void lbtnShowFollowers_Click(object sender, EventArgs e)
        {
            try
            {
                //string link = "/User/UserFriends.aspx?IDUserRequested=" + Request["IDUserRequested"] + "&Show=Followers";
                string link = ("/" + hfUserName.Value + "/Followers").ToLower();
                Response.Redirect(link, true);
            }
            catch (Exception ex)
            {
                //WRITE A ROW IN LOG FILE AND DB
                try
                {
                    LogRow NewRow = new LogRow(DateTime.UtcNow, LogLevel.Errors.ToString(), "", Network.GetCurrentPageName(), "US-ER-9999", "General Error in btnShowFollowers_Click: " + ex.Message, Request["IDUserRequested"].ToString(), true, false);
                    LogManager.WriteDBLog(LogLevel.Errors, NewRow);
                    LogManager.WriteFileLog(LogLevel.Errors, true, NewRow);
                }
                catch { }
            }
        }
        #endregion

        #region ButtonToShowFollowingPeople
        //protected void btnShowFollowing_Click(object sender, ImageClickEventArgs e)
        //{
        //    try
        //    {
        //        string link = "/User/UserFriends.aspx?IDUserRequested=" + Request["IDUserRequested"] + "&Show=Following";
        //        Response.Redirect(link, true);
        //    }
        //    catch (Exception ex)
        //    {
        //        //WRITE A ROW IN LOG FILE AND DB
        //        try
        //        {
        //            LogRow NewRow = new LogRow(DateTime.UtcNow, LogLevel.Errors.ToString(), "", Network.GetCurrentPageName(), "US-ER-9999", "General Error in btnShowFollowing_Click: " + ex.Message, Request["IDUserRequested"].ToString(), true, false);
        //            LogManager.WriteDBLog(LogLevel.Errors, NewRow);
        //            LogManager.WriteFileLog(LogLevel.Errors, true, NewRow);
        //        }
        //        catch { }
        //    }
        //}

        protected void lbtnShowFollowing_Click(object sender, EventArgs e)
        {
            try
            {
                //string link = "/User/UserFriends.aspx?IDUserRequested=" + Request["IDUserRequested"] + "&Show=Following";
                string link = ("/" + hfUserName.Value + "/Following").ToLower();
                Response.Redirect(link, true);
            }
            catch (Exception ex)
            {
                //WRITE A ROW IN LOG FILE AND DB
                try
                {
                    LogRow NewRow = new LogRow(DateTime.UtcNow, LogLevel.Errors.ToString(), "", Network.GetCurrentPageName(), "US-ER-9999", "General Error in btnShowFollowing_Click: " + ex.Message, Request["IDUserRequested"].ToString(), true, false);
                    LogManager.WriteDBLog(LogLevel.Errors, NewRow);
                    LogManager.WriteFileLog(LogLevel.Errors, true, NewRow);
                }
                catch { }
            }
        }
        #endregion

        #region ButtonToSetNotificationsOn (Follow) - NOT USED FOR NOW (Use ButtonToRequestFriendship instead)
        /// <summary>
        /// Button to Set Notifications On
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void btnSetNotificationsOn_Click(object sender, ImageClickEventArgs e)
        {
            try
            {
                int IDLanguage = MyConvert.ToInt32(Session["IDLanguage"].ToString(), 1);

                string UserIDClicked = Request["IDUserRequested"];  //This is the difference with the same button generated dynamically in PotentialFriends Lists (UserFriends.aspx)

                Guid IDUserGuid = new Guid(Session["IDUser"].ToString());
                Guid IDFriendGuid = new Guid(UserIDClicked.ToString());

                MyUser Me = new MyUser(IDUserGuid);

                MyUser Friend = new MyUser(IDFriendGuid);

                //SP to Block User
                MyUserFriendship ManageFriendship = new MyUserFriendship(Me, Friend);
                ManageUSPReturnValue result = ManageFriendship.FollowUser();

                if (!result.IsError)
                {
                    //Show Message

                    //Show JQueryUi BoxDialog
                    //string BoxTitle = RetrieveMessage.RetrieveDBMessage(IDLanguage, "US-IN-9999");
                    //string BoxText = RetrieveMessage.RetrieveDBMessage(IDLanguage, "US-IN-0034");
                    //ScriptManager.RegisterStartupScript(this, GetType(), Guid.NewGuid().ToString(), "ShowJQuiBoxDialog('" + BoxTitle.Replace("'", "\\'") + "','" + BoxText.Replace("'", "\\'") + "');", true);

                    //WRITE A ROW IN STATISTICS DB
                    try
                    {
                        MyStatistics NewStatistic = new MyStatistics(IDUserGuid, IDFriendGuid, StatisticsActionType.US_UserFollowed, "User Followed", Network.GetCurrentPageName(), "", Session.SessionID);
                        NewStatistic.InsertNewRow();
                    }
                    catch { }

                    //DISABLED
                    //btnSetNotificationsOff.Visible = false;
                    //btnSetNotificationsOn.Visible = true;

                }
                else //Error in Set Notifications On
                {
                    //Show JQueryUi BoxDialog With Redirect
                    string BoxTitle = RetrieveMessage.RetrieveDBMessage(IDLanguage, "US-WN-9999");
                    string BoxText = RetrieveMessage.RetrieveDBMessage(IDLanguage, result.ResultExecutionCode);
                    string RedirectUrl = AppConfig.GetValue("HomePage", AppDomain.CurrentDomain);
                    ScriptManager.RegisterStartupScript(this, GetType(), Guid.NewGuid().ToString(), "ShowJQuiBoxDialogWithRedirect('" + BoxTitle.Replace("'", "\\'") + "','" + BoxText.Replace("'", "\\'") + "', '" + RedirectUrl + "');", true);

                    //WRITE A ROW IN LOG FILE AND DB
                    try
                    {
                        LogRow NewRow = new LogRow(DateTime.UtcNow, LogLevel.CriticalErrors.ToString(), "", Network.GetCurrentPageName(), result.ResultExecutionCode, RetrieveMessage.RetrieveDBMessage(IDLanguage, result.ResultExecutionCode), Session["IDUser"].ToString(), true, false);
                        LogManager.WriteDBLog(LogLevel.Warnings, NewRow);
                        LogManager.WriteFileLog(LogLevel.Warnings, false, NewRow);
                    }
                    catch { }
                }
            }
            catch (Exception ex)
            {
                //WRITE A ROW IN LOG FILE AND DB
                try
                {
                    LogRow NewRow = new LogRow(DateTime.UtcNow, LogLevel.Errors.ToString(), "", Network.GetCurrentPageName(), "US-ER-9999", "General Error in btnSetNotificationsOn_Click: " + ex.Message, Request["IDUserRequested"].ToString(), true, false);
                    LogManager.WriteDBLog(LogLevel.Errors, NewRow);
                    LogManager.WriteFileLog(LogLevel.Errors, true, NewRow);
                }
                catch { }
            }
        }
        #endregion

        #region ButtonToSetNotificationsOff (Defollow) - NOT USED FOR NOW
        /// <summary>
        /// Button to Set Notifications Off
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void btnSetNotificationsOff_Click(object sender, ImageClickEventArgs e)
        {
            try
            {
                int IDLanguage = MyConvert.ToInt32(Session["IDLanguage"].ToString(), 1);

                string UserIDClicked = Request["IDUserRequested"];  //This is the difference with the same button generated dynamically in PotentialFriends Lists (UserFriends.aspx)

                Guid IDUserGuid = new Guid(Session["IDUser"].ToString());
                Guid IDFriendGuid = new Guid(UserIDClicked.ToString());

                MyUser Me = new MyUser(IDUserGuid);

                MyUser Friend = new MyUser(IDFriendGuid);

                //SP to Defollow User
                MyUserFriendship ManageFriendship = new MyUserFriendship(Me, Friend);
                ManageUSPReturnValue result = ManageFriendship.DefollowUser();

                if (!result.IsError)
                {
                    //Show Message

                    //Show JQueryUi BoxDialog
                    //string BoxTitle = RetrieveMessage.RetrieveDBMessage(IDLanguage, "US-IN-9999");
                    //string BoxText = RetrieveMessage.RetrieveDBMessage(IDLanguage, "US-IN-0035");
                    //ScriptManager.RegisterStartupScript(this, GetType(), Guid.NewGuid().ToString(), "ShowJQuiBoxDialog('" + BoxTitle.Replace("'", "\\'") + "','" + BoxText.Replace("'", "\\'") + "');", true);

                    //WRITE A ROW IN STATISTICS DB
                    try
                    {
                        MyStatistics NewStatistic = new MyStatistics(IDUserGuid, IDFriendGuid, StatisticsActionType.US_UserDefollowed, "User Defollowed", Network.GetCurrentPageName(), "", Session.SessionID);
                        NewStatistic.InsertNewRow();
                    }
                    catch { }

                    //DISABLED
                    //btnSetNotificationsOff.Visible = true;
                    //btnSetNotificationsOn.Visible = false;

                }
                else //Error in Set Notifications Off
                {
                    //Show JQueryUi BoxDialog With Redirect
                    string BoxTitle = RetrieveMessage.RetrieveDBMessage(IDLanguage, "US-WN-9999");
                    string BoxText = RetrieveMessage.RetrieveDBMessage(IDLanguage, result.ResultExecutionCode);
                    string RedirectUrl = AppConfig.GetValue("HomePage", AppDomain.CurrentDomain);
                    ScriptManager.RegisterStartupScript(this, GetType(), Guid.NewGuid().ToString(), "ShowJQuiBoxDialogWithRedirect('" + BoxTitle.Replace("'", "\\'") + "','" + BoxText.Replace("'", "\\'") + "', '" + RedirectUrl + "');", true);

                    //WRITE A ROW IN LOG FILE AND DB
                    try
                    {
                        LogRow NewRow = new LogRow(DateTime.UtcNow, LogLevel.Errors.ToString(), "", Network.GetCurrentPageName(), result.ResultExecutionCode, RetrieveMessage.RetrieveDBMessage(IDLanguage, result.ResultExecutionCode), Session["IDUser"].ToString(), true, false);
                        LogManager.WriteDBLog(LogLevel.Errors, NewRow);
                        LogManager.WriteFileLog(LogLevel.Errors, false, NewRow);
                    }
                    catch { }
                }
            }
            catch (Exception ex)
            {
                //WRITE A ROW IN LOG FILE AND DB
                try
                {
                    LogRow NewRow = new LogRow(DateTime.UtcNow, LogLevel.Errors.ToString(), "", Network.GetCurrentPageName(), "US-ER-9999", "General Error in btnSetNotificationsOff_Click: " + ex.Message, Request["IDUserRequested"].ToString(), true, false);
                    LogManager.WriteDBLog(LogLevel.Errors, NewRow);
                    LogManager.WriteFileLog(LogLevel.Errors, true, NewRow);
                }
                catch { }
            }
        }
        #endregion 

        #region ReloadBoard
        protected void ReloadBoard(object sender, EventArgs e)
        {
            try
            {
                string IDUserRequested = Request["IDUserRequested"];
                Guid IDUserRequestedGuid = new Guid(IDUserRequested);

                int IDLanguage = MyConvert.ToInt32(Session["IDLanguage"].ToString(), 1);

                //Load UserBoard Control
                LoadUserBoard(IDLanguage, IDUserRequestedGuid, LoadBoardDirection.NotSpecified);
            }
            catch (Exception ex)
            {
                //WRITE A ROW IN LOG FILE AND DB
                try
                {
                    LogRow NewRow = new LogRow(DateTime.UtcNow, LogLevel.Errors.ToString(), "", Network.GetCurrentPageName(), "US-ER-9999", "General Error in ReloadBoard(): " + ex.Message, Request["IDUserRequested"].ToString(), true, false);
                    LogManager.WriteDBLog(LogLevel.Errors, NewRow);
                    LogManager.WriteFileLog(LogLevel.Errors, true, NewRow);
                }
                catch { }
            }
        }
        #endregion

        #region ibtnUserBoardLoadNext_Click
        protected void ibtnUserBoardLoadNext_Click(object sender, ImageClickEventArgs e)
        {
            try
            {
                string IDUserRequested = Request["IDUserRequested"];
                Guid IDUserRequestedGuid = new Guid(IDUserRequested);

                int IDLanguage = MyConvert.ToInt32(Session["IDLanguage"].ToString(), 1);

                //Load UserBoard Control
                LoadUserBoard(IDLanguage, IDUserRequestedGuid, LoadBoardDirection.Next);
            }
            catch (Exception ex)
            {
                //WRITE A ROW IN LOG FILE AND DB
                try
                {
                    LogRow NewRow = new LogRow(DateTime.UtcNow, LogLevel.Errors.ToString(), "", Network.GetCurrentPageName(), "US-ER-9999", "General Error in ibtnUserBoardLoadNext_Click: " + ex.Message, Request["IDUserRequested"].ToString(), true, false);
                    LogManager.WriteDBLog(LogLevel.Errors, NewRow);
                    LogManager.WriteFileLog(LogLevel.Errors, true, NewRow);
                }
                catch { }
            }
        }
        #endregion

        #region ibtnUserBoardLoadPrevious_Click - DISABLED
        protected void ibtnUserBoardLoadPrevious_Click(object sender, ImageClickEventArgs e)
        {
            /* DISABLED
            string IDUserRequested = Request["IDUserRequested"];
            Guid IDUserRequestedGuid = new Guid(IDUserRequested);

            int IDLanguage = MyConvert.ToInt32(Session["IDLanguage"].ToString(), 1);

            //Load UserBoard Control
            LoadUserBoard(IDLanguage, IDUserRequestedGuid, LoadBoardDirection.Previous);
             * */
        }
        #endregion

       

    }
}
