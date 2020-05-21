using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using MyCookin.ObjectManager.StatisticsManager;

namespace MyCookinWeb
{
    public partial class Link : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if(Request.QueryString["Url"]!=null)
            {
                string _url = Request.QueryString["Url"].ToString();
                if (_url.IndexOf("https://") == -1 || _url.IndexOf("http://") == -1)
                {
                    _url = "http://" + _url;
                }
                Guid _iduser = new Guid();
                try
                {
                    if (!String.IsNullOrEmpty(Session["IDUser"].ToString()))
                    {
                        _iduser = new Guid(Session["IDUser"].ToString());
                    }
                    MyStatistics NewStatistic = new MyStatistics(_iduser, null, StatisticsActionType.US_NavigateExternalLink, "External Link Nav", Request.UrlReferrer.OriginalString, _url, Session.SessionID);
                    NewStatistic.InsertNewRow();
                }
                catch
                { }

                Response.Redirect(_url, true);
            }
            else
            {
                Response.Redirect("/", true);
            }
        }
    }
}