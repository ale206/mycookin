using System;

namespace TaechIdeas.MyCookin.Core.Dto
{
    public class CheckIfRecipeIsInBookInput
    {
        public Guid UserId { get; set; }
        public Guid RecipeId { get; set; }
    }
}