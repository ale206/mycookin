using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using MyCookin.ObjectManager.UserBoardManager;
using MyCookin.Common;
using MyCookin.Log;
using MyCookin.ErrorAndMessage;
using MyCookin.ObjectManager.StatisticsManager;

namespace MyCookinWeb.CustomControls
{
    public partial class ctrlUserBoardLikes : System.Web.UI.UserControl
    {
        #region PublicFields
        //id azione
        public Guid IDUserActionFather
        {
            get { return new Guid(hfIDUserActionFather.Value); }
            set { hfIDUserActionFather.Value = value.ToString(); }
        }

        public string TypeOfLike
        {
            get { return hfTypeOfLike.Value; }
            set { hfTypeOfLike.Value = value.ToString(); }
        }
        #endregion

        public event EventHandler LikeChanged;

        protected void Page_Load(object sender, EventArgs e)
        {
            
        }

        protected override void DataBindChildren()
        {
            
        }

        internal void StartLikeControl()
        {
            ScriptManager.RegisterStartupScript(this, this.GetType(), Guid.NewGuid().ToString(),
                "StartPopBox('#" + pnlUserLikes.ClientID + "','#" + hlCountLikes.ClientID + "','#" + pnlBoxInternal.ClientID + "');", true);

            if (hfControlLikesLoaded.Value != "true")
            {
                LoadControl();
            }
        }

        #region LoadControl
        public void LoadControl()
        {
            try
            {
                Guid IDUserGuid = new Guid(Session["IDUser"].ToString());

                int IDLanguage = MyConvert.ToInt32(Session["IDLanguage"].ToString(), 1);

                //LikesTemplate - Ex: (34 Likes)

                //hlCountLikes.Text = LikesTemplate(IDUserActionFather, IDLanguage);
                hlCountLikes.Text = LikesTemplate((ActionTypes)Convert.ToInt32(TypeOfLike), IDUserActionFather, IDLanguage);
                hlCountLikes.Attributes["onclick"] = "UsersLikesLoad('" + TypeOfLike + "', '" + pnlContainerUserList.ClientID + "', '" + IDUserActionFather + "')";


                //Check if you already like this
                UserBoard NewUserBoardAction = new UserBoard((ActionTypes)Convert.ToInt32(TypeOfLike), IDLanguage, IDUserGuid, IDUserActionFather, "asc", 1);
                //UserBoard NewUserBoardAction = new UserBoard((ActionTypes)UserBoard.GetTypeOfLike((ActionTypes)Convert.ToInt32(TypeOfLike)), IDLanguage, IDUserGuid, IDUserActionFather, "asc", 1);

                if (NewUserBoardAction.CountNumberOfActionsByUserAndType() > 0)
                {
                    //You already Like this
                    ibtnLike.Visible = false;
                    ibtnUnlike.Visible = true;

                    ibtnUnlike.ToolTip = RetrieveMessage.RetrieveDBMessage(IDLanguage, "US-IN-0050");
                }
                else
                {
                    ibtnLike.Visible = true;
                    ibtnUnlike.Visible = false;

                    ibtnLike.ToolTip = RetrieveMessage.RetrieveDBMessage(IDLanguage, "US-IN-0049");
                }

                hfControlLikesLoaded.Value = "true";

            }
            catch (Exception ex)
            {
                //WRITE A ROW IN LOG FILE AND DB
                try
                {
                    LogRow NewRow = new LogRow(DateTime.UtcNow, LogLevel.Errors.ToString(), "", Network.GetCurrentPageName(), "US-ER-9999", "General Error in ctrlUserBoardLikes -> LoadControl(): " + ex.Message, Request["IDUserRequested"].ToString(), true, false);
                    LogManager.WriteDBLog(LogLevel.Errors, NewRow);
                    LogManager.WriteFileLog(LogLevel.Errors, true, NewRow);
                }
                catch { }
            }
        }
        #endregion

        #region LikesTemplate
        /// <summary>
        /// Create Complete Likes Template - Ex.: (34 Likes)
        /// </summary>
        /// <param name="IDUserActionFather"></param>
        /// <param name="IDLanguage"></param>
        /// <returns></returns>
        public string LikesTemplate(ActionTypes TypeOfLike, Guid IDUserActionFather, int IDLanguage)
        {
            try
            {
                UserBoard UserBoardElement = new UserBoard(TypeOfLike, IDLanguage, IDUserActionFather);              

                int NumberOfLikes = UserBoardElement.CountLikesOrComments();

                if (NumberOfLikes == 1)
                {
                    //return "(" + NumberOfLikes + " " + UserBoardElement.UserActionTypeTemplate + ")";
                    //return "(" + NumberOfLikes + ")";
                    return NumberOfLikes.ToString();
                }
                else
                {
                    //return "(" + NumberOfLikes + " " + UserBoardElement.UserActionTypeTemplatePlural + ")";
                    return NumberOfLikes.ToString();
                }
            }
            catch (Exception ex)
            {
                //WRITE A ROW IN LOG FILE AND DB
                try
                {
                    LogRow NewRow = new LogRow(DateTime.UtcNow, LogLevel.Errors.ToString(), "", Network.GetCurrentPageName(), "US-ER-9999", "General Error in ctrlUserBoardLikes -> LikesTemplate: " + ex.Message, Request["IDUserRequested"].ToString(), true, false);
                    LogManager.WriteDBLog(LogLevel.Errors, NewRow);
                    LogManager.WriteFileLog(LogLevel.Errors, true, NewRow);
                }
                catch { }

                return "0";
            }
        }
        #endregion

        #region ibtnLike_Click
        protected void ibtnLike_Click(object sender, ImageClickEventArgs e)
        {
            try
            {
                Guid IDUserGuid = new Guid(Session["IDUser"].ToString());

                UserBoard NewUserBoardAction = new UserBoard(IDUserGuid, IDUserActionFather, (ActionTypes)Convert.ToInt32(TypeOfLike), null, null, null, DateTime.UtcNow, MyConvert.ToInt32(HttpContext.Current.Session["IDLanguage"].ToString(), 1));
                //UserBoard NewUserBoardAction = new UserBoard(IDUserGuid, IDUserActionFather, (ActionTypes)UserBoard.GetTypeOfLike((ActionTypes)Convert.ToInt32(TypeOfLike)), null, null, null, DateTime.UtcNow, MyConvert.ToInt32(HttpContext.Current.Session["IDLanguage"].ToString(), 1));

                
                ManageUSPReturnValue result = NewUserBoardAction.InsertAction();

                if (!result.IsError)
                {
                    //WRITE A ROW IN STATISTICS DB - not necessary
                    //MyStatistics NewStatisticUser = new MyStatistics(IDUserGuid, IDUserActionFather, StatisticsActionType.SC_Like, "Like", Network.GetCurrentPageName(), "", "");
                    //NewStatisticUser.InsertNewRow();

                    LoadControl();
                    LikeChanged(this, EventArgs.Empty);
                }
                else
                {
                    //WRITE A ROW IN LOG FILE AND DB
                    try
                    {
                        LogRow NewRow = new LogRow(DateTime.UtcNow, LogLevel.Errors.ToString(), "", Network.GetCurrentPageName(), "US-ER-9999", "Error On Insert Like - IDUserActionFather: " + IDUserActionFather, IDUserGuid.ToString(), true, false);
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
                    LogRow NewRow = new LogRow(DateTime.UtcNow, LogLevel.Errors.ToString(), "", Network.GetCurrentPageName(), "US-ER-9999", "General Error in ctrlUserBoardLikes -> ibtnLike_Click: " + ex.Message, Request["IDUserRequested"].ToString(), true, false);
                    LogManager.WriteDBLog(LogLevel.Errors, NewRow);
                    LogManager.WriteFileLog(LogLevel.Errors, true, NewRow);
                }
                catch { }
            }
        }
        #endregion

        #region ibtnUnlike_Click
        protected void ibtnUnlike_Click(object sender, ImageClickEventArgs e)
        {
            try
            {
                Guid IDUserGuid = new Guid(Session["IDUser"].ToString());

                int IDLanguage = MyConvert.ToInt32(Session["IDLanguage"].ToString(), 1);

                UserBoard NewUserBoardAction = new UserBoard((ActionTypes)Convert.ToInt32(TypeOfLike), IDLanguage, IDUserGuid, IDUserActionFather, "asc", 1);

                if (NewUserBoardAction.DeleteLike())
                {
                    //WRITE A ROW IN STATISTICS DB - not necessary
                    //MyStatistics NewStatisticUser = new MyStatistics(IDUserGuid, IDUserActionFather, StatisticsActionType.SC_DontLikeMore, "Don't Like More", Network.GetCurrentPageName(), "", "");
                    //NewStatisticUser.InsertNewRow();

                    LoadControl();
                    LikeChanged(this, EventArgs.Empty);
                }
                else
                {
                    //WRITE A ROW IN LOG FILE AND DB
                    try
                    {
                        LogRow NewRow = new LogRow(DateTime.UtcNow, LogLevel.Errors.ToString(), "", Network.GetCurrentPageName(), "US-ER-9999", "Error On Delete UserBoard - IDUserActionFather: " + IDUserActionFather, IDUserGuid.ToString(), true, false);
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
                    LogRow NewRow = new LogRow(DateTime.UtcNow, LogLevel.Errors.ToString(), "", Network.GetCurrentPageName(), "US-ER-9999", "General Error in ctrlUserBoardLikes -> ibtnUnlike_Click: " + ex.Message, Request["IDUserRequested"].ToString(), true, false);
                    LogManager.WriteDBLog(LogLevel.Errors, NewRow);
                    LogManager.WriteFileLog(LogLevel.Errors, true, NewRow);
                }
                catch { }
            }
        }
        #endregion

        internal void StartLikeList()
        {
            ScriptManager.RegisterStartupScript(this, this.GetType(), Guid.NewGuid().ToString(),
               "StartPopBox('#" + pnlUserLikes.ClientID + "','#" + hlCountLikes.ClientID + "','#" + pnlBoxInternal.ClientID + "');", true);
        }
    }
}