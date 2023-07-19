using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using MyCookin.Common;

namespace MyCookinWeb.Styles.SiteStyle
{
    public partial class PagesForEmail : System.Web.UI.MasterPage
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            //BOTTOM
            lblCopyright.Text = "MyCookin &copy; " + DateTime.UtcNow.Year;

            lnkLogo.ImageUrl = AppConfig.GetValue("WebUrl", AppDomain.CurrentDomain) + "/Images/MyCookinLogo-105x105.png";
            lnkLogo.NavigateUrl = AppConfig.GetValue("WebUrl", AppDomain.CurrentDomain);
        }
    }
}