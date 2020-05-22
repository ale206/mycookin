using System;

namespace TaechIdeas.MyCookin.Core.Dto
{
    public class AddRecipeBeverageIn
    {
        public Guid RecipeId { get; set; }
        public Guid BeverageId { get; set; }
        public Guid UserId { get; set; }
    }
}