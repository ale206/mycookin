using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.Caching;
using System.Web.Services;
using System.Web.Script.Services;
using MyCookin.ObjectManager.RecipeManager;
using MyCookin.ObjectManager.UserManager;
using MyCookin.ObjectManager.MediaManager;
using MyCookin.Common;

namespace MyCookin.WebServices.Recipe
{
    /// <summary>
    /// Summary description for GetRecipesByType
    /// </summary>
    [WebService(Namespace = "http://tempuri.org/")]
    [WebServiceBinding(ConformsTo = WsiProfiles.BasicProfile1_1)]
    [System.ComponentModel.ToolboxItem(false)]
    // To allow this Web Service to be called from script, using ASP.NET AJAX, uncomment the following line. 
    [ScriptService]
    public class GetRecipesByType : System.Web.Services.WebService
    {

        [WebMethod]
        public List<string> GetRecipesListHTML(string RecipeType, string OffsetRows, string FetchRows, string Vegan, string Vegetarian
                                                        , string GlutenFree, string LightThreshold, string QuickThreshold, string IDLanguage)
        {
            int _RecipeType = MyConvert.ToInt32(RecipeType, 0);
            int _OffsetRows = MyConvert.ToInt32(OffsetRows, 0);
            int _FetchRows = MyConvert.ToInt32(FetchRows, 0);
            bool _Vegan = MyConvert.ToBoolean(Vegan, false);
            bool _Vegetarian = MyConvert.ToBoolean(Vegetarian, false);
            bool _GlutenFree = MyConvert.ToBoolean(GlutenFree, false);
            int _LightThreshold = MyConvert.ToInt32(LightThreshold, 0);
            int _QuickThreshold = MyConvert.ToInt32(QuickThreshold, 0);
            int _IDLanguage = MyConvert.ToInt32(IDLanguage, 1);

            DBRecipesConfigParameter _elementHTML = new DBRecipesConfigParameter(2);

            List<RecipeLanguage> RecipeList = new List<RecipeLanguage>();
            RecipeList = RecipeLanguage.GetRecipesByType(_RecipeType, _OffsetRows, _FetchRows, _Vegan, _Vegetarian, _GlutenFree, 
                                                            _LightThreshold, _QuickThreshold, _IDLanguage);

            List<string> _return = new List<string>();
            MyUser _user;

            foreach (RecipeLanguage _recipeLang in RecipeList)
            {
                if(HttpRuntime.Cache[_recipeLang.Owner.IDUser.ToString()]!=null)
                {
                    _user = (MyUser)HttpRuntime.Cache[_recipeLang.Owner.IDUser.ToString()];
                }
                else
                {
                    _user = new MyUser(_recipeLang.Owner.IDUser);
                    _user.GetUserBasicInfoByID();
                    HttpRuntime.Cache.Insert(_recipeLang.Owner.IDUser.ToString(), _user, null,Cache.NoAbsoluteExpiration,new TimeSpan(1,0,0));
                }
                string _recipePhoto = "";
                #region GetPhoto
                try
                {
                    if (_recipeLang.RecipeImage.IDMedia != null)
                    {
                        _recipeLang.RecipeImage.QueryMediaInfo();
                        try
                        {
                            _recipePhoto = _recipeLang.RecipeImage.GetAlternativeSizePath(MediaSizeTypes.Small, false, false, true);
                        }
                        catch
                        {
                        }
                        if (_recipePhoto == "")
                        {
                            _recipePhoto = _recipeLang.RecipeImage.GetCompletePath(false, false, true);
                        }

                        if (_recipePhoto == "")
                        {
                            _recipePhoto = DefaultMedia.GetDefaultMediaPathFromWebConfig(MediaType.RecipePhoto);
                        }
                    }
                    else
                    {
                        _recipePhoto = DefaultMedia.GetDefaultMediaPathFromWebConfig(MediaType.RecipePhoto);
                    }
                }
                catch
                {
                    _recipePhoto = DefaultMedia.GetDefaultMediaPathFromWebConfig(MediaType.RecipePhoto);
                }
                #endregion

                string recipeLink = ("/" + MyCulture.GetLangShortCodeFromIDLanguage(_IDLanguage) + AppConfig.GetValue("RoutingRecipe" + IDLanguage.ToString(), AppDomain.CurrentDomain) + _recipeLang.RecipeName.Replace(" ", "-") + "/" + _recipeLang.IDRecipe.ToString()).ToLower();
                string userLink = ("/" +  _user.UserName + "/").ToLower();

                _return.Add(_elementHTML.DBRecipeConfigParameterValue.Replace("{ImagePath}", _recipePhoto).Replace("{RecipeName}",_recipeLang.RecipeName).Replace("{RecipeLink}",recipeLink.ToLower()).Replace("{OwnerLink}",userLink.ToLower()).Replace("{OwnerName}", _user.UserName));
            }

            return _return.ToList();

        }


        [WebMethod]
        public List<string> GetRecipesInRecipesBook(string IDUser, string IDRequester, string RecipeType, string ShowFilter, string RecipeNameFilter, 
                                                    string RowOffSet, string FetchRows, string Vegan, string Vegetarian, string GlutenFree, 
                                                    string LightThreshold, string QuickThreshold, string IDLanguage)
        {
            Guid _IDUser = new Guid(IDUser);
            Guid _IDRequester = new Guid(IDRequester);
            int _RecipeType = MyConvert.ToInt32(RecipeType, 0);
            int _ShowFilter = MyConvert.ToInt32(ShowFilter, 0);
            int _RowOffSet = MyConvert.ToInt32(RowOffSet, 0);
            int _FetchRows = MyConvert.ToInt32(FetchRows, 0);
            bool _Vegan = MyConvert.ToBoolean(Vegan, false);
            bool _Vegetarian = MyConvert.ToBoolean(Vegetarian, false);
            bool _GlutenFree = MyConvert.ToBoolean(GlutenFree, false);
            bool _ShowDraft = false;
            int _LightThreshold = MyConvert.ToInt32(LightThreshold, 0);
            int _QuickThreshold = MyConvert.ToInt32(QuickThreshold, 0);
            int _IDLanguage = MyConvert.ToInt32(IDLanguage, 1);

            //If user open itself recipe book recipe in draft are avaible
            if(_IDUser==_IDRequester)
            {
                _ShowDraft = true;
            }

            DBRecipesConfigParameter _elementHTML = new DBRecipesConfigParameter(2);

            List<RecipeLanguage> RecipeList = new List<RecipeLanguage>();
            DataTable _dtRecipeList;
            _dtRecipeList = RecipeBook.GetRecipes(_IDUser, _RecipeType, _ShowFilter, RecipeNameFilter, _Vegan, _Vegetarian, _GlutenFree, _LightThreshold, _QuickThreshold, _ShowDraft, _IDLanguage, _RowOffSet, _FetchRows);

            if (_dtRecipeList.Rows.Count > 0)
            {
                for (int i = 0; i < _dtRecipeList.Rows.Count; i++)
                {
                    RecipeList.Add(new RecipeLanguage(_dtRecipeList.Rows[i].Field<Guid>("IDRecipe"), _IDLanguage)
                    {
                        RecipeName = _dtRecipeList.Rows[i].Field<string>("RecipeName"),
                        RecipeImage = _dtRecipeList.Rows[i].Field<Guid>("IDRecipeImage"),
                        Owner = _dtRecipeList.Rows[i].Field<Guid>("IDUser"),
                    });
                }
            }

            List<string> _return = new List<string>();
            MyUser _user;

            foreach (RecipeLanguage _recipeLang in RecipeList)
            {
                if (HttpRuntime.Cache[_recipeLang.Owner.IDUser.ToString()] != null)
                {
                    _user = (MyUser)HttpRuntime.Cache[_recipeLang.Owner.IDUser.ToString()];
                }
                else
                {
                    _user = new MyUser(_recipeLang.Owner.IDUser);
                    _user.GetUserBasicInfoByID();
                    HttpRuntime.Cache.Insert(_recipeLang.Owner.IDUser.ToString(), _user, null, Cache.NoAbsoluteExpiration, new TimeSpan(1, 0, 0));
                }
                string _recipePhoto = "";
                #region GetPhoto
                try
                {
                    if (_recipeLang.RecipeImage.IDMedia != null)
                    {
                        _recipeLang.RecipeImage.QueryMediaInfo();
                        try
                        {
                            _recipePhoto = _recipeLang.RecipeImage.GetAlternativeSizePath(MediaSizeTypes.Small, false, false, true);
                        }
                        catch
                        {
                        }
                        if (_recipePhoto == "")
                        {
                            _recipePhoto = _recipeLang.RecipeImage.GetCompletePath(false, false, true);
                        }

                        if (_recipePhoto == "")
                        {
                            _recipePhoto = DefaultMedia.GetDefaultMediaPathFromWebConfig(MediaType.RecipePhoto);
                        }
                    }
                    else
                    {
                        _recipePhoto = DefaultMedia.GetDefaultMediaPathFromWebConfig(MediaType.RecipePhoto);
                    }
                }
                catch
                {
                    _recipePhoto = DefaultMedia.GetDefaultMediaPathFromWebConfig(MediaType.RecipePhoto);
                }
                #endregion

                string recipeLink = ("/" + MyCulture.GetLangShortCodeFromIDLanguage(_IDLanguage) + AppConfig.GetValue("RoutingRecipe" + IDLanguage.ToString(), AppDomain.CurrentDomain) + _recipeLang.RecipeName.Replace(" ", "-") + "/" + _recipeLang.IDRecipe.ToString()).ToLower();
                string userLink = ""; 

                string _html = "";

                if (_recipeLang.Owner.IDUser == _IDRequester && _IDUser == _IDRequester)
                {
                    switch(_IDLanguage)
                    {
                        case 1:
                            userLink = recipeLink + "/edit";
                            break;
                        case 2:
                            userLink = recipeLink + "/modifica";
                            break;
                        case 3:
                            userLink = recipeLink + "/editar";
                            break;
                        default:
                            userLink = ("/RecipeMng/EditRecipes.aspx?IDRecipe="+_recipeLang.IDRecipe).ToLower();
                            break;
                    }
                    
                    _html = _elementHTML.DBRecipeConfigParameterValue.Replace("{ImagePath}", _recipePhoto).Replace("{RecipeName}", _recipeLang.RecipeName).Replace("{RecipeLink}", recipeLink.ToLower()).Replace("{OwnerLink}", userLink.ToLower()).Replace("{OwnerName}", "{EditRecipe}").Replace("{RecipeOf}", "").Replace("{RecipeOf2}", "");
                }
                else
                {
                    userLink = ("/" + _user.UserName + "/").ToLower();
                    _html = _elementHTML.DBRecipeConfigParameterValue.Replace("{ImagePath}", _recipePhoto).Replace("{RecipeName}", _recipeLang.RecipeName).Replace("{RecipeLink}", recipeLink.ToLower()).Replace("{OwnerLink}", userLink.ToLower()).Replace("{OwnerName}", _user.UserName);
                }

                _return.Add(_html);
            }

            return _return.ToList();

        }


        [WebMethod]
        public List<string> GetSimilarRecipesListHTML(string RecipeName, string IDRecipe, string Vegan, string Vegetarian, string GlutenFree, string IDLanguage)
        {
            bool _Vegan = MyConvert.ToBoolean(Vegan, false);
            bool _Vegetarian = MyConvert.ToBoolean(Vegetarian, false);
            bool _GlutenFree = MyConvert.ToBoolean(GlutenFree, false);
            Guid _IDRecipe = new Guid(IDRecipe);
            int _IDLanguage = MyConvert.ToInt32(IDLanguage, 1);

            DBRecipesConfigParameter _elementHTML = new DBRecipesConfigParameter(2);
            RecipeLanguage _recipe = new RecipeLanguage(_IDRecipe, _IDLanguage);

            List<MyCookin.ObjectManager.RecipeManager.Recipe> _similarRecipes = _recipe.GetSimilarRecipes(RecipeName, _Vegan, _Vegetarian, _GlutenFree, _IDLanguage);

            List<string> _return = new List<string>();
            MyUser _user;

            foreach (RecipeLanguage _recipeLang in _similarRecipes)
            {
                if (HttpRuntime.Cache[_recipeLang.Owner.IDUser.ToString()] != null)
                {
                    _user = (MyUser)HttpRuntime.Cache[_recipeLang.Owner.IDUser.ToString()];
                }
                else
                {
                    _user = new MyUser(_recipeLang.Owner.IDUser);
                    _user.GetUserBasicInfoByID();
                    HttpRuntime.Cache.Insert(_recipeLang.Owner.IDUser.ToString(), _user, null, Cache.NoAbsoluteExpiration, new TimeSpan(1, 0, 0));
                }
                string _recipePhoto = "";
                #region GetPhoto
                try
                {
                    if (_recipeLang.RecipeImage.IDMedia != null)
                    {
                        _recipeLang.RecipeImage.QueryMediaInfo();
                        try
                        {
                            _recipePhoto = _recipeLang.RecipeImage.GetAlternativeSizePath(MediaSizeTypes.Small, false, false, true);
                        }
                        catch
                        {
                        }
                        if (_recipePhoto == "")
                        {
                            _recipePhoto = _recipeLang.RecipeImage.GetCompletePath(false, false, true);
                        }

                        if (_recipePhoto == "")
                        {
                            _recipePhoto = DefaultMedia.GetDefaultMediaPathFromWebConfig(MediaType.RecipePhoto);
                        }
                    }
                    else
                    {
                        _recipePhoto = DefaultMedia.GetDefaultMediaPathFromWebConfig(MediaType.RecipePhoto);
                    }
                }
                catch
                {
                    _recipePhoto = DefaultMedia.GetDefaultMediaPathFromWebConfig(MediaType.RecipePhoto);
                }
                #endregion

                string recipeLink = ("/" + MyCulture.GetLangShortCodeFromIDLanguage(_IDLanguage) + AppConfig.GetValue("RoutingRecipe" + IDLanguage.ToString(), AppDomain.CurrentDomain) + _recipeLang.RecipeName.Replace(" ", "-") + "/" + _recipeLang.IDRecipe.ToString()).ToLower();
                string userLink = ("/" + _user.UserName + "/").ToLower();

                _return.Add(_elementHTML.DBRecipeConfigParameterValue.Replace("{ImagePath}", _recipePhoto).Replace("{RecipeName}", _recipeLang.RecipeName).Replace("{RecipeLink}", recipeLink.ToLower()).Replace("{OwnerLink}", userLink.ToLower()).Replace("{OwnerName}", _user.UserName));
            }

            return _return.ToList();

        }

    }
}
