using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using MyCookinWeb.Utilities;
using MyCookin.Common;

namespace MyCookinWeb.Utilities
{
    /// <summary>
    /// Summary description for GetBackgroundPath
    /// </summary>
    public class GetBackgroundPath : IHttpHandler
    {

        public void ProcessRequest(HttpContext context)
        {

            //string _background = "background: url({0}) no-repeat center center fixed;margin:0;padding:80px 0 20px 0;-webkit-background-size: cover;-moz-background-size: cover;-o-background-size: cover;background-size: cover;";
            DateTime _currentTime = MyConvert.ToLocalTime(DateTime.UtcNow,MyConvert.ToInt32(context.Request["OffSet"].ToString(), 0));
           // _background = _background.Replace("{0}", SelectSiteBackground.GetBackgroundURL(_currentTime.Hour));

            //body.Attributes.Add("style", _background);

            string _responcePath = SelectSiteBackground.GetBackgroundURL(_currentTime.Hour);
            context.Response.ContentType = "text/plain";
            context.Response.Write(_responcePath);
        }

        public bool IsReusable
        {
            get
            {
                return false;
            }
        }
    }
}