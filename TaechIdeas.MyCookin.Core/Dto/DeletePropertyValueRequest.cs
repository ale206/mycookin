using System;
using TaechIdeas.Core.Core.Token.Dto;

namespace TaechIdeas.MyCookin.Core.Dto
{
    public class DeletePropertyValueRequest
    {
        public CheckTokenRequest CheckTokenRequest { get; set; }
        public Guid RecipeId { get; set; }
        public Guid RecipePropertyValueId { get; set; }
    }
}