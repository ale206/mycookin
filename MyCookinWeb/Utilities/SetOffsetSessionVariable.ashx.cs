using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.SessionState;

namespace MyCookinWeb.Utilities
{
    /// <summary>
    /// Summary description for SetOffsetSessionVariable
    /// </summary>
    public class SetOffsetSessionVariable : IHttpHandler, IRequiresSessionState
    {

        public void ProcessRequest(HttpContext context)
        {
            try
            {
                HttpContext.Current.Session["Offset"] = context.Request["Offset"].ToString();
            }
            catch
            {
            }
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