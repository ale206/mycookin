using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Services;
using System.Web.Script.Services;
using MyCookin.Common;
using MyCookin.ObjectManager.IngredientManager;

namespace MyCookin.WebServices.IngredientWeb
{
    /// <summary>
    /// Summary description for IngredientInfo
    /// </summary>
    [WebService(Namespace = "http://tempuri.org/")]
    [WebServiceBinding(ConformsTo = WsiProfiles.BasicProfile1_1)]
    [System.ComponentModel.ToolboxItem(false)]
    // To allow this Web Service to be called from script, using ASP.NET AJAX, uncomment the following line. 
    [ScriptService]
    public class IngredientInfo : System.Web.Services.WebService
    {

        [WebMethod]
        public List<IngredientQuantityTypeLanguage> IngredientsQuantityType(string IDIngredient, string IDLanguage)
        {
            List<IngredientQuantityTypeLanguage> IngredientQtaTypeLangList = IngredientQuantityTypeLanguage.GetIngredientQtaTypeLangList(IDIngredient,Convert.ToInt32(IDLanguage));

            return IngredientQtaTypeLangList.ToList();
        }

        [WebMethod]
        public List<QuantityNotStdType> IngredientAllowedQuantityNotStd(string IDIngredientQuantityType, string IDLanguage)
        {
            List<QuantityNotStdType> IngredientAllowedQuantityNotStdList = QuantityNotStdType.GetAllowedQtaNotStdLangList(Convert.ToInt32(IDIngredientQuantityType), Convert.ToInt32(IDLanguage));

            return IngredientAllowedQuantityNotStdList.ToList();
        }

        [WebMethod]
        public List<IngredientLanguage> IngredientAlternative(string IDIngredientMain, string IDLanguage)
        {
            List<IngredientLanguage> AlternativeForIngredient = IngredientLanguage.GetIngredientAlternativesLang(new Guid(IDIngredientMain), MyConvert.ToInt32(IDLanguage, 1));

            return AlternativeForIngredient;
        }
    }
}
