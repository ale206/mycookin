using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Services;
using System.Web.Script.Services;
using MyCookin.Common;
using MyCookin.ObjectManager.IngredientManager;
using MyCookin.ObjectManager.RecipeManager;
using MyCookin.ObjectManager.MediaManager;

namespace MyCookin.WebServices.IngredientWeb
{
    /// <summary>
    /// Summary description for SearchIngredient
    /// </summary>
    [WebService(Namespace = "http://tempuri.org/")]
    [WebServiceBinding(ConformsTo = WsiProfiles.BasicProfile1_1)]
    [System.ComponentModel.ToolboxItem(false)]
    // To allow this Web Service to be called from script, using ASP.NET AJAX, uncomment the following line. 
    [ScriptService]
    public class SearchIngredient : System.Web.Services.WebService
    {
        [WebMethod]
        public List<IngredientLanguage> SearchIngredients(string words, string IDLanguage)
        {
            List<IngredientLanguage> IngredientList = IngredientLanguage.IngredientList(words, MyConvert.ToInt32(IDLanguage,1));

            return IngredientList.ToList();
        }

        [WebMethod]
        public List<string> IngredientsList(string StartWith, string OffSetRow, string FetchRows, string Vegan, string Vegetarian,
                                                                    string GlutenFree, string HotSpicy, string IDLanguage)
        {
            int _OffSetRows = MyConvert.ToInt32(OffSetRow, 0);
            int _FetchRows = MyConvert.ToInt32(FetchRows, 0);
            bool _Vegan = MyConvert.ToBoolean(Vegan, false);
            bool _Vegetarian = MyConvert.ToBoolean(Vegetarian, false);
            bool _GlutenFree = MyConvert.ToBoolean(GlutenFree, false);
            bool _HotSpicy = MyConvert.ToBoolean(HotSpicy, false);
            int _IDLanguage = MyConvert.ToInt32(IDLanguage, 1);

            List<IngredientLanguage> IngredientsList = IngredientLanguage.IngredientList(StartWith, _Vegan, _Vegetarian,
                                                                    _GlutenFree, _HotSpicy, _IDLanguage, _OffSetRows, _FetchRows);

            DBRecipesConfigParameter _elementHTML = new DBRecipesConfigParameter(4);

            List<string> _return = new List<string>();

            foreach (IngredientLanguage _ingredient in IngredientsList)
            {
                string _ingrPhoto = "";
                #region GetPhoto
                try
                {
                    if (_ingredient.IngredientImage.IDMedia != null)
                    {
                        _ingredient.IngredientImage.QueryMediaInfo();
                        try
                        {
                            _ingrPhoto = _ingredient.IngredientImage.GetAlternativeSizePath(MediaSizeTypes.Small, false, false, true);
                        }
                        catch
                        {
                        }
                        if (_ingrPhoto == "")
                        {
                            _ingrPhoto = _ingredient.IngredientImage.GetCompletePath(false, false, true);
                        }

                        if (_ingrPhoto == "")
                        {
                            _ingrPhoto = DefaultMedia.GetDefaultMediaPathFromWebConfig(MediaType.RecipePhoto);
                        }
                    }
                    else
                    {
                        _ingrPhoto = DefaultMedia.GetDefaultMediaPathFromWebConfig(MediaType.RecipePhoto);
                    }
                }
                catch
                {
                    _ingrPhoto = DefaultMedia.GetDefaultMediaPathFromWebConfig(MediaType.RecipePhoto);
                }
                #endregion

                string ingredientLink = ("/" + MyCulture.GetLangShortCodeFromIDLanguage(_IDLanguage) + AppConfig.GetValue("RoutingIngredient" + IDLanguage.ToString(), AppDomain.CurrentDomain) + _ingredient.IngredientPlural.Replace(" ", "-") + "/" + _ingredient.IDIngredient).ToLower();
                _return.Add(_elementHTML.DBRecipeConfigParameterValue.Replace("{ImagePath}", _ingrPhoto).Replace("{IngredientName}", _ingredient.IngredientPlural).Replace("{IngredientLink}", ingredientLink.ToLower()));
            }

            return _return;
        }
    }
}
