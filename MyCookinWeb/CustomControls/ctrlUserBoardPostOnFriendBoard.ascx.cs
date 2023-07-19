using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using MyCookin.ObjectManager.UserBoardManager;
using MyCookin.ErrorAndMessage;
using MyCookin.Log;
using MyCookin.Common;

namespace MyCookinWeb.CustomControls
{
    public partial class ctrlUserBoardPostOnFriendBoard : System.Web.UI.UserControl
    {
        //Add Click event to control
        public event EventHandler StatusAdded;

        public Guid IDUserFriend
        {
            get { return new Guid(hfIDUserFriend.Value); }
            set { hfIDUserFriend.Value = value.ToString(); }
        }

        protected void Page_Load(object sender, EventArgs e)
        {
            hfIDLanguage.Value = Session["IDLanguage"].ToString();

            ScriptManager.RegisterStartupScript(this, GetType(), Guid.NewGuid().ToString(), "TextAreaAutoGrow('" + txtStatus.ClientID + "');", true);

            //Set correct default button when press Enter
            txtStatus.Attributes.Add("onkeypress", "return clickButton(event,'" + ibtnPostStatus.ClientID + "')");
        }

        #region ibtnPostStatus_Click
        protected void ibtnPostStatus_Click(object sender, ImageClickEventArgs e)
        {
            try
            {
                //Save only wether not empty.
                if (!String.IsNullOrEmpty(txtStatus.Text))
                {
                    Guid IDUserGuid = new Guid(Session["IDUser"].ToString());

                    UserBoard NewUserBoardAction = new UserBoard(IDUserGuid, null, ActionTypes.PostOnFriendUserBoard, IDUserFriend, txtStatus.Text, null, DateTime.UtcNow, MyConvert.ToInt32(HttpContext.Current.Session["IDLanguage"].ToString(), 1));
                    NewUserBoardAction.InsertAction();

                    txtStatus.Text = "";

                    //Reload UserBoard
                    StatusAdded(this, EventArgs.Empty);
                }
            }
            catch (Exception ex)
            {
                string ErrorMessage = ex.Message;
                int IDLanguage = MyConvert.ToInt32(Session["IDLanguage"].ToString(), 1);

                //Show JQueryUi BoxDialog - JS: ShowJQuiBoxDialog(Title, Text)
                string BoxTitle = RetrieveMessage.RetrieveDBMessage(IDLanguage, "US-WN-9999");
                string BoxText = RetrieveMessage.RetrieveDBMessage(IDLanguage, "US-ER-9999");

                ScriptManager.RegisterStartupScript(this, GetType(), Guid.NewGuid().ToString(), "ShowJQuiBoxDialog('" + BoxTitle.Replace("'", "\\'") + "','" + BoxText.Replace("'", "\\'") + "');", true);

                //WRITE A ROW IN LOG FILE AND DB
                try
                {
                    LogRow NewRow = new LogRow(DateTime.UtcNow, LogLevel.Errors.ToString(), "", Network.GetCurrentPageName(), "US-ER-9999", "Error on Post on Friend UserBoard: " + ErrorMessage, Session["IDUser"].ToString(), true, false);
                    LogManager.WriteDBLog(LogLevel.Errors, NewRow);
                    LogManager.WriteFileLog(LogLevel.Errors, true, NewRow);
                }
                catch { }
            }
        }
        #endregion
    }
}