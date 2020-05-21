using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using MyCookin.Common;
using MyCookin.ObjectManager.MessageManager;
using MyCookin.ObjectManager.UserManager;

namespace MyCookinWeb.Message
{
    public partial class Messages :  MyCookinWeb.Form.MyPageBase
    {
        protected void Page_Load(object sender, EventArgs e)
        {
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

                IDLanguage = MyConvert.ToInt32(Session["IDLanguage"].ToString(), 1);
            }
            //*****************************

            hfIDUser.Value = Session["IDUser"].ToString();
            hfName.Value = Session["Name"].ToString();
            hfSurname.Value = Session["Surname"].ToString();

            hfDateFormat.Value = AppConfig.GetValue("DateTimeFormat", AppDomain.CurrentDomain);

            hlWriteNewMessage.Attributes["onclick"] = "OpenNewMessageDialog();";

            if (!IsPostBack)
            {
                //Just to inizialize
                //*****************************
                hfPageSize.Value = AppConfig.GetValue("NumberOfMessagesPerPage", AppDomain.CurrentDomain);  //How many messages per page
                hfNumberOfMessages.Value = "0";
                hfNumberOfPages.Value = "1";
                hfPagingOffset.Value = "0";         //This will be incremented dinamically
                hfCurrentPagingOffset.Value = "0";  //This will be incremented dinamically
                //*****************************                
            }

            acRecipient.MethodName = "/User/FindUser.asmx/FindFriendsByWord";
            acRecipient.LanguageCode = IDUserGuid.ToString();                   //Actually is an optional parameters ;) - 
            acRecipient.ObjectLabelIdentifier = "CompleteName";
            acRecipient.ObjectIDIdentifier = "IDUser";
            acRecipient.ObjectLabelText = "";
            acRecipient.LangFieldLabel = "IDUser";                               //Actually is an optional parameters ;) - Call it like in asmx.
            acRecipient.WordFieldLabel = "words";
            acRecipient.MinLenght = "1";

            //Start MyCtrl
            acRecipient.StatAutoComplete();
            
        }
    }
}