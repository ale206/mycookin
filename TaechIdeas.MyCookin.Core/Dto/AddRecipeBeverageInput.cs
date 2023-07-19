using System;
using TaechIdeas.Core.Core.Token.Dto;

namespace TaechIdeas.MyCookin.Core.Dto
{
    public class AddRecipeBeverageInput : TokenRequiredInput
    {
        public Guid RecipeId { get; set; }
        public Guid BeverageId { get; set; }
    }
}