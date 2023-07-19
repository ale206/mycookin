using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using MyCookin.ObjectManager.UserManager;

namespace MyCookinWeb.User
{
    public partial class Contact :  MyCookinWeb.Form.MyPageBase
    {
        protected void Page_PreInit(object sender, EventArgs e)
        {
            if (MyUser.CheckUserLogged())
            {
                this.Page.MasterPageFile = "~/Styles/SiteStyle/Private.Master";
            }
            else
            {
                this.Page.MasterPageFile = "~/Styles/SiteStyle/Public.Master";
            }

            //if (!PageSecurity.IsPublicProfile())
            //{
            //    this.Page.MasterPageFile = "~/Styles/SiteStyle/Private.Master";
            //}
        }

        protected void Page_Load(object sender, EventArgs e)
        {

        }
    }
}