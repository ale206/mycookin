using System;
using TaechIdeas.Core.Core.Token.Dto;

namespace TaechIdeas.MyCookin.Core.Dto
{
    public class DeleteRecipeRequest
    {
        public Guid RecipeLanguageId { get; set; }
        public CheckTokenRequest CheckTokenRequest { get; set; }
    }
}