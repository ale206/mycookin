using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace MyCookinWeb.Message
{
    public partial class TestScroll : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            ActivateScrollbarComment();
        }

        #region ActivateScrollbarComment
        protected void ActivateScrollbarComment()
        {
            
                //Activate scrollbar if necessary
                //if (rptComments.Items.Count >= MyConvert.ToInt32(AppConfig.GetValue("NumberOfCommentsBeforeScrollActivate", AppDomain.CurrentDomain), 5))
                //{
                    //pnlScroll.CssClass = "content";
                    //ScriptManager.RegisterStartupScript(this, GetType(), Guid.NewGuid().ToString(), "ActivateScrollerWithCallback('" + pnlScroll.ClientID + "');", true);
                //}
                //else
                //{
                //    pnlComments.CssClass = "contentNoScroll";
                //}
            //}
            
        }
        #endregion
    }

    
}