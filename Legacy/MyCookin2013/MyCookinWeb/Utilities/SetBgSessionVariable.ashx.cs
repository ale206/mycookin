using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.SessionState;

namespace MyCookinWeb.Utilities
{
    /// <summary>
    /// Summary description for SetBgSessionVariable
    /// </summary>
    public class SetBgSessionVariable : IHttpHandler, IRequiresSessionState
    {

        public void ProcessRequest(HttpContext context)
        {
            try
            {
                HttpContext.Current.Session["hfBgPath"] = context.Request["BgPath"].ToString();
            }
            catch
            {
            }
            //context.Response.ContentType = "text/plain";
            //context.Response.Write("Hello World");
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