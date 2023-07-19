using System;

namespace TaechIdeas.MyCookin.Core.Dto
{
    public class RecipeVoteInput
    {
        public Guid RecipeId { get; set; }
        public Guid UserId { get; set; }
    }
}