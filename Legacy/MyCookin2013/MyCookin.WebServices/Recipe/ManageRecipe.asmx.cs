using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.Services;
using System.Web.Script.Services;
using MyCookin.ObjectManager.RecipeManager;
using MyCookin.Common;

namespace MyCookin.WebServices.Recipe
{
    /// <summary>
    /// Summary description for ManageRecipe
    /// </summary>
    [WebService(Namespace = "http://tempuri.org/")]
    [WebServiceBinding(ConformsTo = WsiProfiles.BasicProfile1_1)]
    [System.ComponentModel.ToolboxItem(false)]
    // To allow this Web Service to be called from script, using ASP.NET AJAX, uncomment the following line. 
    [ScriptService]
    public class WSManageRecipe : System.Web.Services.WebService
    {

        [WebMethod]
        public void VoteRecipe(string IDObjectToVote, string IDUser, string VoteValue)
        {
            double _voteValue = MyConvert.ToDouble(VoteValue, 0);
            RecipeVote _vote = new RecipeVote(new Guid(IDObjectToVote), new Guid(IDUser), _voteValue);
            if (_voteValue > 0)
            {
                _vote.Save();
            }
            else
            {
                _vote.Delete();
            }
        }

        [WebMethod]
        public void ChangeIngredientRelevance(string IDRecipeIngredient, string IngredientRelevance)
        {
            int _IngredientRelevance = MyConvert.ToInt32(IngredientRelevance, 1);
            RecipeIngrediets _recipeIngr = new RecipeIngrediets(new Guid(IDRecipeIngredient));
            _recipeIngr.IngredientRelevance = (RecipeInfo.IngredientRelevances)_IngredientRelevance;
            _recipeIngr.Add(_recipeIngr.Recipe.IDRecipe);
        }
    }
}
