using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Services;
using System.Web.Script.Services;
using MyCookin.Common;
using MyCookin.ObjectManager.IngredientManager;

using System.Configuration;
using System.Text;

namespace MyCookin.WebServices.BeverageWeb
{
    /// <summary>
    /// Summary description for SearchBeverage
    /// </summary>
    [WebService(Namespace = "http://tempuri.org/")]
    [WebServiceBinding(ConformsTo = WsiProfiles.BasicProfile1_1)]
    [System.ComponentModel.ToolboxItem(false)]
    // To allow this Web Service to be called from script, using ASP.NET AJAX, uncomment the following line. 
    [ScriptService]
    public class SearchBeverage : System.Web.Services.WebService
    {

        [WebMethod]
        public List<BeverageLanguage> SearchBeverages(string words, string IDLanguage)
        {
            List<BeverageLanguage> BeverageList = BeverageLanguage.GetBeverageLanguageList(words, MyConvert.ToInt32(IDLanguage, 1));

            return BeverageList.ToList();
        }
    }
}
