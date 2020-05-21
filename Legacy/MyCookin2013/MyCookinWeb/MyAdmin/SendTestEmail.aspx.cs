using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using MyCookin.Common;

namespace MyCookinWeb
{
    public partial class testEmail :  MyCookinWeb.Form.MyPageBase
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            try
            {
                Network newMail = new Network("noreply@mycookin.com", "alessio@mycookin.com", "saverio@mycookin.com", "", "prova", "ciao ciao", "");

                if (newMail.SendEmail())
                {
                    Label1.Text = "Email inviata";
                }
            }
            catch(Exception ex)
            {
                Label1.Text = "Errore: " + ex.Message;
            }
        }
    }
}