using System;
using System.Collections.Generic;
using System.Linq;
using System.Data;
using System.Text;
using MyCookin.DAL.Recipe.ds_RecipeTableAdapters;
using MyCookin.Common;

namespace MyCookin.ObjectManager.AI.Recipe
{

    public class RecipeAI
    {

        public static string CalculateRecipeTags(Guid IDRecipe, int IDLanguage, 
                                                    bool IncludeIngredientList, bool IncludeDynamicProp,
                                                    bool IncludeIngredientCategory)
        {
            string RecipeTags = "";

            CalculateRecipesValDAL calcRecipeTagsDAL = new CalculateRecipesValDAL();
            DataTable dtCalcRecipeTagsDAL = calcRecipeTagsDAL.USP_CalculateRecipeTags(IDRecipe, IDLanguage, IncludeIngredientList, 
                                                                                        IncludeDynamicProp, IncludeIngredientCategory);
            if (dtCalcRecipeTagsDAL.Rows.Count > 0)
            {
                RecipeTags = dtCalcRecipeTagsDAL.Rows[0][1].ToString();
            }

            return RecipeTags;
        }

        public static Dictionary<string, string> CalculateRecipeNutritionalFacts(Guid IDRecipe)
        {
            CalculateRecipesValDAL calcRecipeValDAL = new CalculateRecipesValDAL();
            DataTable dtCalcRecipeValDAL = calcRecipeValDAL.USP_CalculateRecipeNutritionalFacts(IDRecipe);

            Dictionary<string, string> _retun = new Dictionary<string, string>();

            if (dtCalcRecipeValDAL.Rows.Count > 0)
            {
                foreach(DataRow drCalcValue in dtCalcRecipeValDAL.Rows)
                {
                    _retun.Add(drCalcValue.Field<string>("PropKey"), drCalcValue.Field<string>("PropValue"));
                }
            }

            return _retun;
        }
    }
}
