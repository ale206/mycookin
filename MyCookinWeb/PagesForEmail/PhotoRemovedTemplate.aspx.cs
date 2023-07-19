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
    public partial class PhotoRemoved :  MyCookinWeb.Form.MyPageBase
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            //UTILIZED BY "MyAdmin/AuditCheckPhoto.aspx"

            
            

            lblNoReply.Text = RetrieveMessage.RetrieveDBMessage(MyConvert.ToInt32(HttpContext.Current.Session["IDLanguage"].ToString(), 1), "US-IN-0057");

            lnkMessage.NavigateUrl = AppConfig.GetValue("WebUrl", AppDomain.CurrentDomain) + "/User/TermsAndConditions.aspx";
            lnkMessage.Target = "_new";

        }
    }
}