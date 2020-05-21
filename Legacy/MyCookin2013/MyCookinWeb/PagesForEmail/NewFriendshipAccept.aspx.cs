using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using MyCookin.Common;
using MyCookin.ErrorAndMessage;

namespace MyCookinWeb.PagesForEmail
{
    public partial class NewFriendshipAccept :  MyCookinWeb.Form.MyPageBase
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            //UTILIZED BY "ForgotPassword.aspx"
            
            

            lblNoReply.Text = RetrieveMessage.RetrieveDBMessage(MyConvert.ToInt32(HttpContext.Current.Session["IDLanguage"].ToString(), 1), "US-IN-0057");
            lblNoMoreEmail.Text = RetrieveMessage.RetrieveDBMessage(MyConvert.ToInt32(HttpContext.Current.Session["IDLanguage"].ToString(), 1), "US-IN-0058");

            string link = Request.QueryString["link"];

            if (String.IsNullOrEmpty(link))
            {
                link = "#";
            }

            lnkMessage.NavigateUrl = ResolveUrl(link);
            lnkMessage.Target = "_new";

            string TextToShow = RetrieveMessage.RetrieveDBMessage(MyConvert.ToInt32(HttpContext.Current.Session["IDLanguage"].ToString(), 1), "US-IN-0053");

            lnkMessage.Text = TextToShow;

            lblLinkText.Text = link;
        }
    }
}