using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Services;
using System.Web.Script.Services;
using MyCookin.Common;
using MyCookin.ObjectManager.CityManager;

namespace MyCookin.WebServices.CityWeb
{
    /// <summary>
    /// Summary description for FindCity
    /// </summary>
    [WebService(Namespace = "http://tempuri.org/")]
    [WebServiceBinding(ConformsTo = WsiProfiles.BasicProfile1_1)]
    [System.ComponentModel.ToolboxItem(false)]
    // To allow this Web Service to be called from script, using ASP.NET AJAX, uncomment the following line. 
    [System.Web.Script.Services.ScriptService]
    public class FindCity : System.Web.Services.WebService
    {
        [WebMethod]
        public List<City> SearchCities(string words, string LangCode)
        {
            List<City> CityList = City.CitiesList(words+"%", LangCode);

            return CityList.ToList();
        }
     }
}
