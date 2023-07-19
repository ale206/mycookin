using System;
using System.Collections.Generic;
using System.Web;
using System.Data;
using System.Web.Routing;
using System.Web.Compilation;
using System.Web.UI;
using MyCookin.Common;
using MyCookin.ObjectManager.UserManager;
using MyCookin.ObjectManager.IngredientManager;
using MyCookin.ObjectManager.RecipeManager;

namespace MyCookinWeb.UrlRouting
{
    public class MyRecipesRoutingEdit : IRouteHandler
    {

        public MyRecipesRoutingEdit() { }
        public IHttpHandler GetHttpHandler(RequestContext requestContext)
        {
            string queryString = "";
            string IDRecipe = requestContext.RouteData.Values["IDRecipe"] as string;
            queryString = "?IDRecipe=" + IDRecipe;

            HttpContext.Current.RewritePath(
                              string.Concat(
                              "~/RecipeMng/EditRecipes.aspx",
                              queryString));
            return BuildManager.CreateInstanceFromVirtualPath("~/RecipeMng/EditRecipes.aspx", typeof(Page)) as Page;
        }
    }
    public class MyUsersRouting : IRouteHandler
    {
        public MyUsersRouting() { }

        public IHttpHandler GetHttpHandler(RequestContext requestContext)
        {
            try
            {

                string UserName = requestContext.RouteData.Values["UserName"] as string;

                if (("en,it,es,fr,de").IndexOf(UserName.ToLower()) > -1)
                {
                    string queryString = "";
                    string LangCode = UserName;
                    queryString = "?IDLanguage=" + MyCulture.GetIDLanguageFromLangShortCode(LangCode).ToString();

                    HttpContext.Current.RewritePath(
                                      string.Concat(
                                      "~/Default.aspx",
                                      queryString));
                    return BuildManager.CreateInstanceFromVirtualPath("~/Default.aspx", typeof(Page)) as Page;
                }
                else
                {
                    string Detail = "";

                    try
                    {
                        Detail = requestContext.RouteData.Values["Detail"] as string;
                    }
                    catch
                    {
                    }

                    MyUser user = new MyUser("", UserName);
                    if (user.IDUser != new Guid())
                    {
                        if (String.IsNullOrEmpty(Detail))
                        {
                            string queryString = "?IDUserRequested=" + user.IDUser.ToString();
                            HttpContext.Current.RewritePath(
                              string.Concat(
                              "~/User/UserProfile.aspx",
                              queryString));

                            return BuildManager.CreateInstanceFromVirtualPath("~/User/UserProfile.aspx", typeof(Page)) as Page;
                        }
                        else
                        {
                            if (Detail.ToLower() == "recipebook" || Detail.ToLower() == "ricettario" || Detail.ToLower() == "libroderecetas")
                            {
                                string queryString = "?IDUser=" + user.IDUser.ToString();
                                if (HttpContext.Current.Request.QueryString["SearchQuery"] != null
                                    && HttpContext.Current.Request.QueryString["RecipeSource"] != null
                                    && HttpContext.Current.Request.QueryString["RecipeType"] != null
                                    && HttpContext.Current.Request.QueryString["RowOffset"] != null)
                                {
                                    try
                                    {
                                        queryString += "&SearchQuery=" + HttpContext.Current.Request.QueryString["SearchQuery"].ToString() + "&RecipeSource=" + HttpContext.Current.Request.QueryString["RecipeSource"].ToString() + "&RecipeType=" + HttpContext.Current.Request.QueryString["RecipeType"].ToString() + "&RowOffset=" + HttpContext.Current.Request.QueryString["RowOffset"].ToString();
                                    }
                                    catch
                                    {
                                    }
                                }
                                HttpContext.Current.RewritePath(
                                  string.Concat(
                                  "~/RecipeMng/RecipesBooks.aspx",
                                  queryString));

                                return BuildManager.CreateInstanceFromVirtualPath("~/RecipeMng/RecipesBooks.aspx", typeof(Page)) as Page;
                            }
                            else if (Detail.ToLower()=="news")
                            {
                                return BuildManager.CreateInstanceFromVirtualPath("~/User/MyNews.aspx", typeof(Page)) as Page;
                            }
                            else
                            {
                                string queryString = "?IDUserRequested=" + user.IDUser.ToString() + "&Show=" + Detail;
                                HttpContext.Current.RewritePath(
                                  string.Concat(
                                  "~/User/UserFriends.aspx",
                                  queryString));

                                return BuildManager.CreateInstanceFromVirtualPath("~/User/UserFriends.aspx", typeof(Page)) as Page;
                            }
                        }
                    }
                    else
                    {
                        return BuildManager.CreateInstanceFromVirtualPath("~/Default.aspx", typeof(Page)) as Page;
                    }
                }
            }
            catch
            {
                return BuildManager.CreateInstanceFromVirtualPath("~/Error.aspx", typeof(Page)) as Page;
            }
        }
    }

    public class MyIngredientsRouting : IRouteHandler
    {
        private string _Culture;
        private int _IDLanguage;

        public MyIngredientsRouting() 
        {
        }

        public IHttpHandler GetHttpHandler(RequestContext requestContext)
        {
            try
            {
                if (requestContext.HttpContext.Request.Url.ToString().ToLower().IndexOf("/ingredient/") > -1)
                {
                    _IDLanguage = 1;
                }
                else if (requestContext.HttpContext.Request.Url.ToString().ToLower().IndexOf("/ingrediente/") > -1)
                {
                    _IDLanguage = 2;
                }
                else if (requestContext.HttpContext.Request.Url.ToString().ToLower().IndexOf("/elemento/") > -1)
                {
                    _IDLanguage = 3;
                }
                string _allName = "all,tutti,todo,list,lista,todas,todos";
                string IngrName = requestContext.RouteData.Values["IngredientName"] as string;
                if (_allName.IndexOf(IngrName.ToLower()) > -1)
                {
                    return BuildManager.CreateInstanceFromVirtualPath("~/Ingredient/IngredientList.aspx", typeof(Page)) as Page;
                }
                else
                {
                    IngrName.Replace("-", " ");
                    IngredientLanguage IngrLang = new IngredientLanguage(IngrName, _IDLanguage, true);
                    if (IngrLang.IDIngredient == new Guid())
                    {
                        IngrLang = new IngredientLanguage(IngrName);
                    }
                    if (IngrLang.IDIngredient != new Guid())
                    {
                        string queryString = "?IDIngr=" + IngrLang.IDIngredient.ToString() + "&IngrName=" + IngrName;
                        HttpContext.Current.RewritePath(
                          string.Concat(
                          "~/Ingredient/ShowIngredient.aspx",
                          queryString));
                        return BuildManager.CreateInstanceFromVirtualPath("~/Ingredient/ShowIngredient.aspx", typeof(Page)) as Page;
                    }
                    else
                    {
                        return BuildManager.CreateInstanceFromVirtualPath("~/Default.aspx", typeof(Page)) as Page;
                    }
                }
            }
            catch
            {
                return BuildManager.CreateInstanceFromVirtualPath("~/Error.aspx", typeof(Page)) as Page;
            }
        }
    }

    public class MyIngredientsRoutingLang : IRouteHandler
    {
        private string _Culture;
        private int _IDLanguage;

        public MyIngredientsRoutingLang()
        {
        }

        public IHttpHandler GetHttpHandler(RequestContext requestContext)
        {
            try
            {

                _IDLanguage = MyCulture.GetIDLanguageFromLangShortCode(requestContext.RouteData.Values["Lang"].ToString());
                string _allName = "all,tutti,todo,list,lista,todas,todos";
                string IngrName = requestContext.RouteData.Values["IngredientName"] as string;
                if (_allName.IndexOf(IngrName.ToLower()) > -1)
                {
                    return BuildManager.CreateInstanceFromVirtualPath("~/Ingredient/IngredientsList.aspx", typeof(Page)) as Page;
                }
                else
                {
                    IngrName.Replace("-", " ");
                    IngredientLanguage IngrLang = new IngredientLanguage(IngrName, _IDLanguage, true);
                    if (IngrLang.IDIngredient == new Guid())
                    {
                        IngrLang = new IngredientLanguage(IngrName);
                    }
                    if (IngrLang.IDIngredient != new Guid())
                    {
                        string queryString = "?IDIngr=" + IngrLang.IDIngredient.ToString() + "&IngrName=" + IngrName + "&IDLanguage=" + _IDLanguage.ToString();
                        HttpContext.Current.RewritePath(
                          string.Concat(
                          "~/Ingredient/ShowIngredient.aspx",
                          queryString));
                        return BuildManager.CreateInstanceFromVirtualPath("~/Ingredient/ShowIngredient.aspx", typeof(Page)) as Page;
                    }
                    else
                    {
                        return BuildManager.CreateInstanceFromVirtualPath("~/Ingredient/IngredientsList.aspx", typeof(Page)) as Page;
                    }
                }
            }
            catch
            {
                return BuildManager.CreateInstanceFromVirtualPath("~/Ingredient/IngredientsList.aspx", typeof(Page)) as Page;
            }
        }
    }

    public class MyIngredientsRoutingLangWithID : IRouteHandler
    {
        private string _Culture;
        private int _IDLanguage;

        public MyIngredientsRoutingLangWithID()
        {
        }

        public IHttpHandler GetHttpHandler(RequestContext requestContext)
        {
            try
            {

                _IDLanguage = MyCulture.GetIDLanguageFromLangShortCode(requestContext.RouteData.Values["Lang"].ToString());
                string queryString = "?IDIngr=" + requestContext.RouteData.Values["IDIngredient"].ToString() + "&IngrName=" + requestContext.RouteData.Values["IngredientName"].ToString() + "&IDLanguage=" + _IDLanguage.ToString();
                HttpContext.Current.RewritePath(
                    string.Concat(
                    "~/Ingredient/ShowIngredient.aspx",
                    queryString));
                return BuildManager.CreateInstanceFromVirtualPath("~/Ingredient/ShowIngredient.aspx", typeof(Page)) as Page;

            }
            catch
            {
                return BuildManager.CreateInstanceFromVirtualPath("~/Error.aspx", typeof(Page)) as Page;
            }
        }
    }

    public class MyRecipesRouting:IRouteHandler
    {
        private int _IDLanguage;

        public MyRecipesRouting()
        {
        }
        public IHttpHandler GetHttpHandler(RequestContext requestContext)
        {
            try
            {
                string _allName = "all,tutte,todo,list,lista,todas,todos";
                string _newRecipeValue = "crea,aggiungi,add,new,nuevo,añadir";

                string _requestURL = requestContext.HttpContext.Request.Url.ToString().ToLower();
                if (_requestURL.IndexOf("/recipe/") > -1 || _requestURL.IndexOf("/recipes/") > -1)
                {
                    _IDLanguage = 1;
                }
                else if (_requestURL.IndexOf("/ricetta/") > -1 || _requestURL.IndexOf("/ricette/") > -1)
                {
                    _IDLanguage = 2;
                }
                else if (_requestURL.IndexOf("/receta/") > -1 || _requestURL.IndexOf("/recetas/") > -1)
                {
                    _IDLanguage = 3;
                }
                
                string RecipeName = requestContext.RouteData.Values["RecipeName"] as string;
                try
                {
                    if (_allName.IndexOf(RecipeName.ToLower())>-1)
                    {
                        return BuildManager.CreateInstanceFromVirtualPath("~/RecipeMng/AllRecipes.aspx", typeof(Page)) as Page;
                    }
                    else if(_newRecipeValue.IndexOf(RecipeName.ToLower())>-1)
                    {
                        return BuildManager.CreateInstanceFromVirtualPath("~/RecipeMng/CreateRecipe.aspx", typeof(Page)) as Page;
                    }
                    else
                    {
                        RecipeName = RecipeName.Replace("-", " ");
                        DataTable dtSearchResult = null;
                        SearchRecipes _search = new SearchRecipes(RecipeName, false, false, false, 10000, 10000, false, 1, 0, _IDLanguage);
                        dtSearchResult = _search.Find();

                        if (dtSearchResult.Rows.Count > 0)
                        {
                            string queryString = "?IDRecipe=" + dtSearchResult.Rows[0].Field<Guid>("IDRecipe").ToString();
                            HttpContext.Current.RewritePath(
                              string.Concat(
                              "~/RecipeMng/ShowRecipe.aspx",
                              queryString));
                            return BuildManager.CreateInstanceFromVirtualPath("~/RecipeMng/ShowRecipe.aspx", typeof(Page)) as Page;
                        }
                        else
                        {
                            return BuildManager.CreateInstanceFromVirtualPath("~/Default.aspx", typeof(Page)) as Page;
                        }
                    }
                }
                catch
                {
                }
                return BuildManager.CreateInstanceFromVirtualPath("~/Default.aspx", typeof(Page)) as Page;
            }
            catch
            {
                return BuildManager.CreateInstanceFromVirtualPath("~/Error.aspx", typeof(Page)) as Page;
            }
        }
    }

    public class MyRecipesRoutingLang : IRouteHandler
    {
        //private string _Culture;
        private int _IDLanguage;

        public MyRecipesRoutingLang()
        {
        }
        public IHttpHandler GetHttpHandler(RequestContext requestContext)
        {
            try
            {
                string _allName = "all,tutte,todo,list,lista,todas";
                string _newRecipeValue = "crea,aggiungi,add,new,nuevo,añadir";
                string _requestURL = requestContext.HttpContext.Request.Url.ToString().ToLower();

                _IDLanguage = MyCulture.GetIDLanguageFromLangShortCode(requestContext.RouteData.Values["Lang"].ToString());

                string RecipeName = requestContext.RouteData.Values["RecipeName"] as string;

                if (_allName.IndexOf(RecipeName.ToLower()) > -1)
                {
                    return BuildManager.CreateInstanceFromVirtualPath("~/RecipeMng/AllRecipes.aspx", typeof(Page)) as Page;
                }
                else if(_newRecipeValue.IndexOf(RecipeName.ToLower())>-1)
                {
                    return BuildManager.CreateInstanceFromVirtualPath("~/RecipeMng/CreateRecipe.aspx", typeof(Page)) as Page;
                }
                else
                {
                    RecipeName = RecipeName.Replace("-", " ");
                    DataTable dtSearchResult = null;
                    SearchRecipes _search = new SearchRecipes(RecipeName, false, false, false, 10000, 10000, false, 1, 0, _IDLanguage);
                    dtSearchResult = _search.Find();

                    if (dtSearchResult.Rows.Count > 0)
                    {
                        string queryString = "?IDRecipe=" + dtSearchResult.Rows[0].Field<Guid>("IDRecipe").ToString() + "&IDLanguage=" + _IDLanguage.ToString();
                        HttpContext.Current.RewritePath(
                            string.Concat(
                            "~/RecipeMng/ShowRecipe.aspx",
                            queryString));
                        return BuildManager.CreateInstanceFromVirtualPath("~/RecipeMng/ShowRecipe.aspx", typeof(Page)) as Page;
                    }
                    else
                    {
                        return BuildManager.CreateInstanceFromVirtualPath("~/RecipeMng/AllRecipes.aspx", typeof(Page)) as Page;
                    }
                }
            }
            catch
            {
                return BuildManager.CreateInstanceFromVirtualPath("~/RecipeMng/AllRecipes.aspx", typeof(Page)) as Page;
            }
        }
    }

    public class MyRecipesRoutingWithID : IRouteHandler
    {
        private string _Culture;

        public MyRecipesRoutingWithID()
        {
        }
        public IHttpHandler GetHttpHandler(RequestContext requestContext)
        {
            try
            {

                string RecipeName = requestContext.RouteData.Values["RecipeName"] as string;

                Guid _idRecipe = new Guid();
                try
                {
                    _idRecipe = new Guid(requestContext.RouteData.Values["IDRecipe"].ToString());
                }
                catch
                {
                }
                if (_idRecipe != new Guid())
                {
                    string queryString = "?IDRecipe=" + _idRecipe.ToString();
                    HttpContext.Current.RewritePath(
                      string.Concat(
                      "~/RecipeMng/ShowRecipe.aspx",
                      queryString));
                    return BuildManager.CreateInstanceFromVirtualPath("~/RecipeMng/ShowRecipe.aspx", typeof(Page)) as Page;
                }
                else
                {
                    return BuildManager.CreateInstanceFromVirtualPath("~/Default.aspx", typeof(Page)) as Page;
                }
            }
            catch
            {
                return BuildManager.CreateInstanceFromVirtualPath("~/Error.aspx", typeof(Page)) as Page;
            }
        }
    }

    public class MyRecipesRoutingWithIDForceLang : IRouteHandler
    {
        private string _Culture;
        private int _IDLanguage;

        public MyRecipesRoutingWithIDForceLang()
        {
        }
        public IHttpHandler GetHttpHandler(RequestContext requestContext)
        {
            try
            {
                _IDLanguage = MyCulture.GetIDLanguageFromLangShortCode(requestContext.RouteData.Values["Lang"].ToString());

                string RecipeName = requestContext.RouteData.Values["RecipeName"] as string;

                Guid _idRecipe = new Guid();
                try
                {
                    _idRecipe = new Guid(requestContext.RouteData.Values["IDRecipe"].ToString());
                }
                catch
                {
                }
                if (_idRecipe != new Guid())
                {
                    string queryString = "?IDRecipe=" + _idRecipe.ToString() + "&IDLanguage=" + _IDLanguage.ToString();
                    HttpContext.Current.RewritePath(
                      string.Concat(
                      "~/RecipeMng/ShowRecipe.aspx",
                      queryString));
                    return BuildManager.CreateInstanceFromVirtualPath("~/RecipeMng/ShowRecipe.aspx", typeof(Page)) as Page;
                }
                else
                {
                    return BuildManager.CreateInstanceFromVirtualPath("~/Default.aspx", typeof(Page)) as Page;
                }
            }
            catch
            {
                return BuildManager.CreateInstanceFromVirtualPath("~/Error.aspx", typeof(Page)) as Page;
            }
        }
    }

}