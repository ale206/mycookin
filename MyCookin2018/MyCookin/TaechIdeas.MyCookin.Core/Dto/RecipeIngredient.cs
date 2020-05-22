using System;
using System.Collections.Generic;
using TaechIdeas.Core.Core.Common.Enums;

namespace TaechIdeas.MyCookin.Core.Dto
{
    public class RecipeIngredient : IngredientLanguage
    {
        public Guid IdRecipeIngredient { get; set; }

        public Guid IdRecipe { get; set; }

        //public Guid IngredientId { get; set; }
        public bool IsPrincipalIngredient { get; set; }
        public string QuantityNotStd { get; set; }
        public int? QuantityNotStdType { get; set; }
        public double? Quantity { get; set; }
        public int? QuantityType { get; set; }
        public bool QuantityNotSpecified { get; set; }
        public int RecipeIngredientGroupNumber { get; set; }
        public bool RecipeIngredientGroupNumberChange { get; set; }
        public Guid? IdRecipeIngredientAlternative { get; set; }
        public IEnumerable<RecipeIngredient> RecipeIngredientAlternatives { get; set; }
        public IngredientRelevances IngredientRelevance { get; set; }
    }
}