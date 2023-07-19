using System;

namespace TaechIdeas.MyCookin.Core.Dto
{
    public class SaveVoteInput
    {
        public Guid IdRecipeVote { get; set; }
        public Guid IdRecipe { get; set; }
        public Guid IdUser { get; set; }
        public DateTime RecipeVoteDate { get; set; }
        public double Vote { get; set; }
    }
}