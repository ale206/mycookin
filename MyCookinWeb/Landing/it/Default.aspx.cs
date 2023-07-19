using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using MyCookin.Common;

namespace MyCookinWeb.Landing.it
{
    public partial class Default : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
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

        }

        #region btnFacebook_Click
        protected void btnFacebook_Click(object sender, ImageClickEventArgs e)
        {
            //To redirect to the requested page after login via Social Networks
            string requestedPage = Request.QueryString["requestedPage"];

            if (!String.IsNullOrEmpty(requestedPage))
            {
                Session["requestedPage"] = requestedPage;
            }

            //Your Website Url which needs to Redirected
            string callBackUrl = "";
            if (!String.IsNullOrEmpty(Request.Url.Query))
            {
                callBackUrl = Request.Url.AbsoluteUri.Replace(Request.Url.Query, "");
            }
            else
            {
                callBackUrl = Request.Url.AbsoluteUri;
            }

            string FacebookClientId = AppConfig.GetValue("facebook_appid", AppDomain.CurrentDomain);
            string FacebookScopes = AppConfig.GetValue("facebook_scopes", AppDomain.CurrentDomain);

            Response.Redirect(string.Format("https://graph.facebook.com/oauth/" +
              "authorize?client_id={0}&redirect_uri={1}&scope={2}",
              FacebookClientId, callBackUrl, FacebookScopes));
        }
        #endregion

    }
}