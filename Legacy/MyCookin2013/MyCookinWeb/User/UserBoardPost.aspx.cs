using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using MyCookin.ObjectManager.UserBoardManager;
using MyCookin.Common;
using MyCookin.ObjectManager.UserManager;

namespace MyCookinWeb.User
{
    public partial class UserBoardPost :  MyCookinWeb.Form.MyPageBase
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            Guid IDUserGuid = new Guid();
            int IDLanguage = 1;

            NavHistoryClear();
            NavHistoryAddUrl(Request.RawUrl);

            //If not logged go to Login
            //*****************************
            if (!MyUser.CheckUserLogged())
            {
                Response.Redirect((AppConfig.GetValue("LoginPage", AppDomain.CurrentDomain) + "?requestedPage=" + Network.GetCurrentPageUrl()).ToLower(), true);
            }
            else
            {
                IDUserGuid = new Guid(Session["IDUser"].ToString());

                IDLanguage = MyConvert.ToInt32(Session["IDLanguage"].ToString(), 1);
            }
            //*****************************


            //No default button
            Page.Form.DefaultButton = null;

            string IDUserAction = Request["IDUserAction"];

            if (String.IsNullOrEmpty(IDUserAction))
            {
                Response.Redirect((AppConfig.GetValue("HomePage", AppDomain.CurrentDomain)).ToLower(), true);
            }

            Guid IDUserActionGuid = new Guid(IDUserAction);

            if (!IsPostBack)
            {
                ShowPost(IDUserActionGuid);
            }
        }

        protected void ShowPost(Guid IDUserAction)
        { 
            //If IDUserActionFather is a comment, get the father of the comment to show original post..
            UserBoard UserActionObj = new UserBoard(IDUserAction, true);

            //Instatiate
            Guid CorrectIDUserAction = IDUserAction;

            try
            {
                switch (UserActionObj.IDUserActionType)
                {
                    case ActionTypes.Comment:
                        CorrectIDUserAction = (Guid)UserActionObj.IDUserActionFather;
                        break;
                }
            }
            catch
            { 
            }

            //Load Single Post on userboard
            UserBoard MainUserBoard = new UserBoard(CorrectIDUserAction, true);

            List<UserBoard> MainUserBoardList = new List<UserBoard>();

            MainUserBoardList = MainUserBoard.GetUsersBoardBlockElement();

            //Bind Repeater
            rptUserBoardControl.DataSource = MainUserBoardList;
            rptUserBoardControl.DataBind();
        }
    }
}