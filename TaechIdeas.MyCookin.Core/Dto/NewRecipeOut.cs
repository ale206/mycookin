using System;

namespace TaechIdeas.MyCookin.Core.Dto
{
    public class NewRecipeOut
    {
        public Guid RecipeId { get; set; }
        public Guid RecipeLanguageId { get; set; }
        public string FriendlyId { get; set; }
    }
}