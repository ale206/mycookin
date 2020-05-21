using System;
using System.Collections.Generic;
using System.Linq;
using System.Data;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Text;
using MyCookin.ObjectManager.RecipeManager;
using MyCookin.Common;

namespace MyCookinWeb
{
    public partial class sitemap_recipes : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            Response.Clear();
            Response.ContentType = "text/xml";
            Response.ContentEncoding = Encoding.UTF8;

            int IDLanguage = 1;
            if (Request.QueryString["lang"] != null)
            {
                IDLanguage = MyConvert.ToInt32(Request.QueryString["lang"].ToString(), 1);
            }
            StringBuilder _xmlResponce = new StringBuilder();

            _xmlResponce.AppendLine("<?xml version=\"1.0\" encoding=\"UTF-8\"?>");
            _xmlResponce.AppendLine("<urlset xmlns:xsi=\"http://www.w3.org/2001/XMLSchema-instance\" xsi:schemaLocation=\"http://www.sitemaps.org/schemas/sitemap/0.9 http://www.sitemaps.org/schemas/sitemap/0.9/sitemap.xsd\" xmlns=\"http://www.sitemaps.org/schemas/sitemap/0.9\">");

            DataTable dtSiteMapRecipe = RecipeLanguage.GetRecipeSiteMap(IDLanguage);

            if (dtSiteMapRecipe.Rows.Count > 0)
            {
                foreach (DataRow _dr in dtSiteMapRecipe.Rows)
                {
                    _xmlResponce.AppendLine("<url>");
                    _xmlResponce.AppendLine("<loc>" + _dr[0].ToString().Replace("\"", "'").Replace("&","-") + "</loc>");
                    _xmlResponce.AppendLine("</url>");
                }
            }

            _xmlResponce.AppendLine("</urlset>");

            Response.Write(_xmlResponce);
            Response.End();
        }
    }
}