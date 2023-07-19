using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.Services;
using System.Web.Script.Services;
using MyCookin.DAL.Recipe.ds_RecipeTableAdapters;
using MyCookin.ObjectManager.RecipeManager;
using MyCookin.Common;

namespace MyCookin.WebServices.Recipe
{
    /// <summary>
    /// Summary description for GetRecipeInfo
    /// </summary>
    [WebService(Namespace = "http://tempuri.org/")]
    [WebServiceBinding(ConformsTo = WsiProfiles.BasicProfile1_1)]
    [System.ComponentModel.ToolboxItem(false)]
    // To allow this Web Service to be called from script, using ASP.NET AJAX, uncomment the following line. 
    [ScriptService]
    public class GetRecipeInfo : System.Web.Services.WebService
    {

        [WebMethod]
        [ScriptMethod(ResponseFormat = ResponseFormat.Json, UseHttpGet = true)]
        public string GetRecipeFullInfoByID()
        {
            string args = this.Context.Request.QueryString["IDRecipe"];

            string returnValue = string.Empty;

            GetRecipesStepsDAL dalRecipeSteps = new GetRecipesStepsDAL();


            //TEST
            //args = "212efe7f-18e5-4860-ade2-000d77ee7d44";

            returnValue = dalRecipeSteps.GetStepsByIDRecipe(new Guid(args)).Rows[0][4].ToString();

            return returnValue;
        }

        [WebMethod]
        public List<RecipeLanguageTag> GetRecipeLangTags(string TagStartWord, string IDLanguage)
        {
            List<RecipeLanguageTag> RecipeLangTags = RecipeLanguageTag.GetRecipeLangTags(TagStartWord, Convert.ToInt32(IDLanguage));

            return RecipeLangTags.ToList();
        }

        [WebMethod]
        public List<RecipeProperty> GetRecipePropertiesValues(string PropertyStartWord, string IDLanguage, string IDRecipePropertyType)
        {
            List<RecipeProperty> _return = RecipeProperty.SearchPropertiesValues(MyConvert.ToInt32(IDRecipePropertyType,1), MyConvert.ToInt32(IDLanguage,1), PropertyStartWord);

            return _return.ToList();

        }
    }
}
