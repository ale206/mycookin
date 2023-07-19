using System;
using TaechIdeas.MyCookin.Core.Enums;

namespace TaechIdeas.MyCookin.Core.Dto
{
    public class TopRecipesByLanguageOutput
    {
        public Guid RecipeId { get; set; }
        public string ImageUrl { get; set; }
        public string RecipeName { get; set; }
        public string FriendlyId { get; set; }

        public int PreparationTimeMinutes { get; set; }
        public int? CookingTimeMinutes { get; set; }
        public DateTime? CreationDate { get; set; }
        public double RecipeAvgRating { get; set; }
        public double? RecipePortionKcal { get; set; }
        public bool? IsVegetarian { get; set; }
        public bool? IsVegan { get; set; }
        public bool? IsGlutenFree { get; set; }
        public bool IsHotSpicy { get; set; }
        public Guid? RecipeImageId { get; set; }
        public RecipeDifficulty RecipeDifficulty { get; set; }
        public Guid RecipeLanguageId { get; set; }
        public string PropertiesJoined { get; set; }
        public RecipeOwnerOutput RecipeOwner { get; set; }
        public Guid OwnerId { get; set; }
    }
}