using System;

namespace TaechIdeas.MyCookin.Core.Dto
{
    /// <summary>
    ///     Model from Database, use the same column names
    /// </summary>
    public class RecipeByFriendlyIdOut
    {
        public int IDLanguage { get; set; }

        public Guid IDRecipe { get; set; }
        public Guid? IDRecipeFather { get; set; }
        public Guid? IdOwner { get; set; }
        public int NumberOfPerson { get; set; }
        public int PreparationTimeMinute { get; set; }
        public int? CookingTimeMinute { get; set; }
        public int RecipeDifficulties { get; set; }
        public Guid? IDRecipeImage { get; set; }
        public Guid? IDRecipeVideo { get; set; }
        public int? IDCity { get; set; }
        public DateTime? CreationDate { get; set; }
        public DateTime? LastUpdate { get; set; }
        public Guid? UpdatedByUser { get; set; }
        public int RecipeConsulted { get; set; }
        public double RecipeAvgRating { get; set; }
        public bool IsStarterRecipe { get; set; }
        public DateTime? DeletedOn { get; set; }
        public bool BaseRecipe { get; set; }
        public bool RecipeEnabled { get; set; }
        public bool Checked { get; set; }
        public int? RecipeCompletePerc { get; set; }
        public double? RecipePortionKcal { get; set; }
        public double? RecipePortionProteins { get; set; }
        public double? RecipePortionFats { get; set; }
        public double? RecipePortionCarbohydrates { get; set; }
        public double? RecipePortionAlcohol { get; set; }
        public double? RecipePortionQta { get; set; }
        public bool? Vegetarian { get; set; }
        public bool? Vegan { get; set; }
        public bool? GlutenFree { get; set; }
        public bool? HotSpicy { get; set; }
        public bool? Draft { get; set; }
        public int? RecipeRated { get; set; }
    }
}