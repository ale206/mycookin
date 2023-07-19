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


namespace MyCookinWeb.User
{
    public partial class MyNews :  MyCookinWeb.Form.MyPageBase
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            //No default button
            Page.Form.DefaultButton = null;

            Guid IDUserGuid = new Guid();
            int IDLanguage = 1;

            //If not logged go to Login
            //*****************************
            if (!MyUser.CheckUserLogged())
            {
                Response.Redirect((AppConfig.GetValue("LoginPage", AppDomain.CurrentDomain) + "?requestedPage=" + Network.GetCurrentPageUrl()).ToLower(), true);
            }
            else
            {
                IDUserGuid = new Guid(Session["IDUser"].ToString());
                hfIDUser.Value = Session["IDUser"].ToString();
                IDLanguage = MyConvert.ToInt32(Session["IDLanguage"].ToString(), 1);
                hfIDLanguage.Value = IDLanguage.ToString();
            }
            //*****************************
            ibtnUserBoardLoadNext.Attributes.Add("onclick", " this.disabled = true; " + ClientScript.GetPostBackEventReference(ibtnUserBoardLoadNext, null) + ";");

            NavHistoryClear();
            NavHistoryAddUrl(Request.RawUrl);

            if (!IsPostBack)
            {
                try
                {
                    string _allValue = "all";
                    switch (IDLanguage)
                    {
                        case 1:
                            _allValue = "all";
                            break;
                        case 2:
                            _allValue = "tutte";
                            break;
                        case 3:
                            _allValue = "todo";
                            break;
                        default:
                            _allValue = "all";
                            break;
                    }
                    lnkOtherRecipes.NavigateUrl = (AppConfig.GetValue("RoutingRecipe" + IDLanguage, AppDomain.CurrentDomain) + _allValue).ToLower();
                }
                catch
                {
                    lnkOtherRecipes.NavigateUrl = "/recipemng/AllRecipes.aspx";
                }

                #region UserBoard

                //This need for UserBoard Block Control. If a user want to share but has not given authorization yet - **da provare..!
                #region FacebookRegistrationOrLogin
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
                #endregion

                //LOAD USER BOARD CONTROL
                LoadUserBoard(IDLanguage, IDUserGuid, LoadBoardDirection.NotSpecified);

                #endregion
            }
        }

        protected void LoadUserBoard(int IDLanguage, Guid IDUserGuid, LoadBoardDirection Direction)
        {
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
            UserBoard MainUserBoard = new UserBoard(ActionTypes.NotSpecified, IDLanguage, IDUserGuid, null, "desc", PageCount);

            List<UserBoard> MainUserBoardList = new List<UserBoard>();

            MainUserBoardList = MainUserBoard.UsersBoardMyNewsBlockLoad();

            //Disable button "load more" if reached the end of board.
            if (MainUserBoardList.Count < PageCount)
            {
                ibtnUserBoardLoadNext.Visible = false;
            }
            
            #endregion

            //Bind Repeater
            rptUserBoardControl.DataSource = MainUserBoardList;
            rptUserBoardControl.DataBind();
        }

        #region ReloadBoard
        protected void ReloadBoard(object sender, EventArgs e)
        {
            Guid IDUserGuid = new Guid(Session["IDUser"].ToString());

            int IDLanguage = MyConvert.ToInt32(Session["IDLanguage"].ToString(), 1);

            //Load UserBoard Control
            LoadUserBoard(IDLanguage, IDUserGuid, LoadBoardDirection.NotSpecified);
        }
        #endregion

        #region ibtnUserBoardLoadNext_Click
        protected void ibtnUserBoardLoadNext_Click(object sender, ImageClickEventArgs e)
        {
            Guid IDUserGuid = new Guid(Session["IDUser"].ToString());

            int IDLanguage = MyConvert.ToInt32(Session["IDLanguage"].ToString(), 1);

            //Load UserBoard Control
            LoadUserBoard(IDLanguage, IDUserGuid, LoadBoardDirection.Next);
        }
        #endregion
    }
}