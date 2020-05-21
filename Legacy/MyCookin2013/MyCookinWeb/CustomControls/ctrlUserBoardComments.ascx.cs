using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using MyCookin.ObjectManager.UserBoardManager;
using MyCookin.Common;
using MyCookin.ObjectManager.UserManager;
using System.Data;
using MyCookin.Log;

namespace MyCookinWeb.CustomControls
{
    public partial class ctrlUserBoardComments : System.Web.UI.UserControl
    {

        public event EventHandler CommentDeleted;

        #region PublicFields
        //id azione
        public Guid IDUserActionFather
        {
            get { return new Guid(hfIDUserActionFather.Value); }
            set { hfIDUserActionFather.Value = value.ToString(); }
        }

        public Guid IDUser
        {
            get { return new Guid(hfIDUser.Value); }
            set { hfIDUser.Value = value.ToString(); }
        }

        public DateTime UserActionDate
        {
            get { return Convert.ToDateTime(hfUserActionDate.Value); }
            set { hfUserActionDate.Value = value.ToString(); }
        }

        public string UserActionMessage
        {
            get { return hfUserActionMessage.Value; }
            set { hfUserActionMessage.Value = value.ToString(); }
        }

        public Guid IDUserAction
        {
            get { return new Guid(hfIDUserAction.Value); }
            set { hfIDUserAction.Value = value.ToString(); }
        }
        #endregion

        protected void Page_Load(object sender, EventArgs e)
        {
            
        }

        protected override void DataBindChildren()
        {
            //ctrlUserBoardLikes.IDUserActionFather = IDUserAction;
            if (hfControlCommentsLoaded.Value != "true")
            {
                LoadControl();
                ctrlUserBoardLikes.StartLikeControl();
            }
        }

        #region LoadControl
        public void LoadControl()
        {
            try
            {
                //Those will be enabled only if not public profile
                ibtnDelete.Visible = false;

                #region EnableDeleteBotton
                try
                {
                    //Show Delete Botton only for our actions
                    Guid me = new Guid(Session["IDUser"].ToString());

                    UserBoard ActionBoard = new UserBoard(IDUserAction, false);
                    if (ActionBoard.GetIDUserFromIDUserAction() == me)
                    {
                        ibtnDelete.Visible = true;
                    }
                }
                catch
                { 
                    //No log here.
                }
                #endregion
                                                                              
                int IDLanguage = MyConvert.ToInt32(Session["IDLanguage"].ToString(), 1);

                lblDate.Text = UserActionDate.ToString(AppConfig.GetValue("DateTimeFormatCSharp", AppDomain.CurrentDomain)); ;

                //Link of the User
                //****************
                //New User Object
                MyUser UserInAction = new MyUser(IDUser);
                UserInAction.GetUserBasicInfoByID();

                hlUser.Text = UserInAction.Name + " " + UserInAction.Surname;
                hlUser.NavigateUrl = ("/" + UserInAction.UserName + "/").ToLower();
                //****************

                lblComment.Text = UserActionMessage;

                //Likes Repeater
                if (!PageSecurity.IsPublicProfile())
                {
                    upnLikes.Visible = true;
                    ctrlUserBoardLikes.IDUserActionFather = IDUserAction;
                }
                else
                {
                    upnLikes.Visible = false;
                }

                hfControlCommentsLoaded.Value = "true";
            }
            catch (Exception ex)
            {
                //WRITE A ROW IN LOG FILE AND DB
                try
                {
                    LogRow NewRow = new LogRow(DateTime.UtcNow, LogLevel.Errors.ToString(), "", Network.GetCurrentPageName(), "US-ER-9999", "General Error in ctrlUserBoardComments -> LoadControl(): " + ex.Message, Request["IDUserRequested"].ToString(), true, false);
                    LogManager.WriteDBLog(LogLevel.Errors, NewRow);
                    LogManager.WriteFileLog(LogLevel.Errors, true, NewRow);
                }
                catch { }
            }
        }
        #endregion

        //Get User Info from ID on data binding
        #region hlCommentOwner_OnDataBinding
        protected void hlCommentOwner_OnDataBinding(object sender, EventArgs e)
        {
            try
            {
                HyperLink _lnk = (HyperLink)sender;

                Guid IDUserGuid = new Guid(_lnk.Text);

                MyUser UserInfo = new MyUser(IDUserGuid);
                UserInfo.GetUserBasicInfoByID();

                _lnk.Text = UserInfo.Name + " " + UserInfo.Surname;
                _lnk.NavigateUrl = ("/" + UserInfo.UserName + "/").ToLower();
            }
            catch (Exception ex)
            {
                //WRITE A ROW IN LOG FILE AND DB
                try
                {
                    LogRow NewRow = new LogRow(DateTime.UtcNow, LogLevel.Errors.ToString(), "", Network.GetCurrentPageName(), "US-ER-9999", "General Error in ctrlUserBoardComments -> hlCommentOwner_OnDataBinding: " + ex.Message, Request["IDUserRequested"].ToString(), true, false);
                    LogManager.WriteDBLog(LogLevel.Errors, NewRow);
                    LogManager.WriteFileLog(LogLevel.Errors, true, NewRow);
                }
                catch { }
            }
        }
        #endregion

        #region lblDate_OnDataBinding
        //Convert to Small Date on Data Binding
        protected void lblDate_OnDataBinding(object sender, EventArgs e)
        {
            try
            {
                Label _lbl = (Label)sender;

                DateTime DateOfComment = Convert.ToDateTime(_lbl.Text);

                _lbl.Text = DateOfComment.ToShortDateString();
            }
            catch (Exception ex)
            {
                //WRITE A ROW IN LOG FILE AND DB
                try
                {
                    LogRow NewRow = new LogRow(DateTime.UtcNow, LogLevel.Errors.ToString(), "", Network.GetCurrentPageName(), "US-ER-9999", "General Error in ctrlUserBoardComments -> lblDate_OnDataBinding: " + ex.Message, Request["IDUserRequested"].ToString(), true, false);
                    LogManager.WriteDBLog(LogLevel.Errors, NewRow);
                    LogManager.WriteFileLog(LogLevel.Errors, true, NewRow);
                }
                catch { }
            }
        }
        #endregion

        #region ibtnDelete_Click
        protected void ibtnDelete_Click(object sender, EventArgs e)
        {
            try
            {
                UserBoard NewUserBoardAction = new UserBoard(IDUserAction, false);
                NewUserBoardAction.UpdateActionDeletedOn();

                //pnlComments.Visible = false;

            }
            catch (Exception ex)
            {
                //WRITE A ROW IN LOG FILE AND DB
                try
                {
                    LogRow NewRow = new LogRow(DateTime.UtcNow, LogLevel.Errors.ToString(), "", Network.GetCurrentPageName(), "US-ER-9999", "General Error in ctrlUserBoardComments -> ibtnDelete_Click: " + ex.Message, Request["IDUserRequested"].ToString(), true, false);
                    LogManager.WriteDBLog(LogLevel.Errors, NewRow);
                    LogManager.WriteFileLog(LogLevel.Errors, true, NewRow);
                }
                catch { }
            }
            finally
            {
                CommentDeleted(this, EventArgs.Empty);
            }
        }
        #endregion

        protected void LikeChangedEvent(object sender, EventArgs e)
        {
            ctrlUserBoardLikes.StartLikeList();
        }
    }
}