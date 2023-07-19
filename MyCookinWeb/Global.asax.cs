using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Security;
using System.Web.SessionState;
using MyCookin.Common;
using MyCookinWeb.UrlRouting;
using MyCookin.ObjectManager.UserManager;
using MyCookin.ObjectManager.MediaManager;
using System.Web.Caching;
using MyCookinWeb.MyAdmin.ScheduledTasks;
using System.Web.Routing;

namespace MyCookinWeb
{
    public class Global : System.Web.HttpApplication
    {

        protected void Application_Start(object sender, EventArgs e)
        {
            RegisterRoute(System.Web.Routing.RouteTable.Routes);
        }

        void RegisterRoute(System.Web.Routing.RouteCollection routes)
        {
            routes.Ignore("{*allaspx}", new {allaspx=@".*\.aspx(\.*)?" });
            routes.Ignore("{*allaxd}", new { allaxd = @".*\.axd(\.*)?" });
            //routes.Add("LangEn", new System.Web.Routing.Route("en", new MyLangRouting()));
            //routes.Add("LangIt", new System.Web.Routing.Route("it", new MyLangRouting()));
            //routes.Add("LangEs", new System.Web.Routing.Route("es", new MyLangRouting()));
            //routes.Add("Lang", new System.Web.Routing.Route("{LangCode}", new MyLangRouting()));
            routes.Add("IngredientsEn", new System.Web.Routing.Route("Ingredient/{IngredientName}", new MyIngredientsRouting()));
            routes.Add("IngredientsIt", new System.Web.Routing.Route("Ingrediente/{IngredientName}", new MyIngredientsRouting()));
            routes.Add("IngredientsEs", new System.Web.Routing.Route("Elemento/{IngredientName}", new MyIngredientsRouting()));
            routes.Add("IngredientsEnLang", new System.Web.Routing.Route("{Lang}/Ingredient/{IngredientName}", new MyIngredientsRoutingLang()));
            routes.Add("IngredientsItLang", new System.Web.Routing.Route("{Lang}/Ingrediente/{IngredientName}", new MyIngredientsRoutingLang()));
            routes.Add("IngredientsEsLang", new System.Web.Routing.Route("{Lang}/Elemento/{IngredientName}", new MyIngredientsRoutingLang()));
            routes.Add("IngredientsEnLang2", new System.Web.Routing.Route("{Lang}/Ingredient", new MyIngredientsRoutingLang()));
            routes.Add("IngredientsItLang2", new System.Web.Routing.Route("{Lang}/Ingrediente", new MyIngredientsRoutingLang()));
            routes.Add("IngredientsEsLang2", new System.Web.Routing.Route("{Lang}/Elemento", new MyIngredientsRoutingLang()));
            routes.Add("IngredientsEnLangWithID", new System.Web.Routing.Route("{Lang}/Ingredient/{IngredientName}/{IDIngredient}", new MyIngredientsRoutingLangWithID()));
            routes.Add("IngredientsItLangWithID", new System.Web.Routing.Route("{Lang}/Ingrediente/{IngredientName}/{IDIngredient}", new MyIngredientsRoutingLangWithID()));
            routes.Add("IngredientsEsLangWithID", new System.Web.Routing.Route("{Lang}/Elemento/{IngredientName}/{IDIngredient}", new MyIngredientsRoutingLangWithID()));
            //routes.Add("NewRecipeIt", new System.Web.Routing.Route("Gestione/Ricetta/{Action}", new MyManageRecipesRouting()));
            //routes.Add("NewRecipeEn", new System.Web.Routing.Route("Manage/Recipe/{Action}", new MyManageRecipesRouting()));
            //routes.Add("NewRecipeEs", new System.Web.Routing.Route("haz/Receta/{Action}", new MyManageRecipesRouting()));
            routes.Add("RecipeIt", new System.Web.Routing.Route("Ricetta/{RecipeName}", new MyRecipesRouting()));
            routes.Add("RecipeEn", new System.Web.Routing.Route("Recipe/{RecipeName}", new MyRecipesRouting()));
            routes.Add("RecipeEs", new System.Web.Routing.Route("Receta/{RecipeName}", new MyRecipesRouting()));
            routes.Add("RecipeItLang", new System.Web.Routing.Route("{Lang}/Ricetta/{RecipeName}", new MyRecipesRoutingLang()));
            routes.Add("RecipeEnLang", new System.Web.Routing.Route("{Lang}/Recipe/{RecipeName}", new MyRecipesRoutingLang()));
            routes.Add("RecipeEsLang", new System.Web.Routing.Route("{Lang}/Receta/{RecipeName}", new MyRecipesRoutingLang()));
            routes.Add("RecipeItLang2", new System.Web.Routing.Route("{Lang}/Ricetta", new MyRecipesRoutingLang()));
            routes.Add("RecipeEnLang2", new System.Web.Routing.Route("{Lang}/Recipe", new MyRecipesRoutingLang()));
            routes.Add("RecipeEsLang2", new System.Web.Routing.Route("{Lang}/Receta", new MyRecipesRoutingLang()));
            routes.Add("RecipeItWithID", new System.Web.Routing.Route("Ricetta/{RecipeName}/{IDRecipe}", new MyRecipesRoutingWithID()));
            routes.Add("RecipeEnWithID", new System.Web.Routing.Route("Recipe/{RecipeName}/{IDRecipe}", new MyRecipesRoutingWithID()));
            routes.Add("RecipeEsWithID", new System.Web.Routing.Route("Receta/{RecipeName}/{IDRecipe}", new MyRecipesRoutingWithID()));
            routes.Add("RecipeItWithIDForceLang", new System.Web.Routing.Route("Ricetta/{RecipeName}/{IDRecipe}/{Lang}", new MyRecipesRoutingWithIDForceLang()));
            routes.Add("RecipeEnWithIDForceLang", new System.Web.Routing.Route("Recipe/{RecipeName}/{IDRecipe}/{Lang}", new MyRecipesRoutingWithIDForceLang()));
            routes.Add("RecipeEsWithIDForceLang", new System.Web.Routing.Route("Receta/{RecipeName}/{IDRecipe}/{Lang}", new MyRecipesRoutingWithIDForceLang()));
            routes.Add("RecipeItWithIDForceLang2", new System.Web.Routing.Route("{Lang}/Ricetta/{RecipeName}/{IDRecipe}", new MyRecipesRoutingWithIDForceLang()));
            routes.Add("RecipeEnWithIDForceLang2", new System.Web.Routing.Route("{Lang}/Recipe/{RecipeName}/{IDRecipe}", new MyRecipesRoutingWithIDForceLang()));
            routes.Add("RecipeEsWithIDForceLang2", new System.Web.Routing.Route("{Lang}/Receta/{RecipeName}/{IDRecipe}", new MyRecipesRoutingWithIDForceLang()));
            routes.Add("RecipeItEdit", new System.Web.Routing.Route("{Lang}/Ricetta/{RecipeName}/{IDRecipe}/Modifica", new MyRecipesRoutingEdit()));
            routes.Add("RecipeEnEdit", new System.Web.Routing.Route("{Lang}/Recipe/{RecipeName}/{IDRecipe}/Edit", new MyRecipesRoutingEdit()));
            routes.Add("RecipeEsEdit", new System.Web.Routing.Route("{Lang}/Receta/{RecipeName}/{IDRecipe}/Editar", new MyRecipesRoutingEdit()));
            routes.Add("Users", new System.Web.Routing.Route("{UserName}", new MyUsersRouting()));
            routes.Add("Users2", new System.Web.Routing.Route("User/{UserName}", new MyUsersRouting()));
            routes.Add("Users3", new System.Web.Routing.Route("Blog/{UserName}", new MyUsersRouting()));
            routes.Add("UsersDetails", new System.Web.Routing.Route("{UserName}/{Detail}", new MyUsersRouting()));
            routes.Add("UsersDetails2", new System.Web.Routing.Route("User/{UserName}/{Detail}", new MyUsersRouting()));
            routes.Add("UsersDetails3", new System.Web.Routing.Route("Blog/{UserName}/{Detail}", new MyUsersRouting()));
        } 

        protected void Session_Start(object sender, EventArgs e)
        {
            MyCulture BrowserCulture = new MyCulture(MyCulture.GetBrowserCurrentCulture());

            Session["Name"] = "";
            Session["Surname"] = "";
            Session["Username"] = "";
            Session["eMail"] = "";
            Session["IDUser"] = "";
            Session["IDLanguage"] = BrowserCulture.IDLanguage;
            Session["IDGender"] = "";
            Session["IDSecurityGroupList"] = "";
            Session["Offset"] = "";
            Session["hfBgPath"] = "";
            Session["navHistory"] = "";
            Session["FoudRecipeList"] = "";
            Session["RecipeInRecipesBookList"] = "";
            Session["imgName"] = "";
            Session["FileMD5"] = "";
        }

        protected void Application_BeginRequest(object sender, EventArgs e)
        {

        }

        protected void Application_AuthenticateRequest(object sender, EventArgs e)
        {

        }

        protected void Application_Error(object sender, EventArgs e)
        {

        }

        protected void Session_End(object sender, EventArgs e)
        {
            try
            {
                MyUser User = new MyUser(new Guid(Session["IDUser"].ToString()));
                User.GetUserBasicInfoByID();

                User.LogoutUser();
            }
            catch
            { 
            }

            //Destroy Session
            Session["Name"] = "";
            Session["Surname"] = "";
            Session["Username"] = "";
            Session["eMail"] = "";
            Session["IDUser"] = "";
            Session["IDLanguage"] = "";
            Session["imgName"] = "";
            Session["FileMD5"] = "";
            Session["Offset"] = "";
            Session["hfBgPath"] = "";
            Session["navHistory"] = "";
            Session["FoudRecipeList"] = "";
            Session["RecipeInRecipesBookList"] = "";
            Session.Clear();
            Session.RemoveAll();
        }

        protected void Application_End(object sender, EventArgs e)
        {

        }
    }
}