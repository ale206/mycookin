using System;
using TaechIdeas.Core.Core.Token.Dto;

namespace TaechIdeas.MyCookin.Core.Dto
{
    public class AddRecipeBeverageRequest
    {
        public CheckTokenRequest CheckTokenRequest { get; set; }
        public Guid RecipeId { get; set; }
        public Guid BeverageId { get; set; }
    }
}