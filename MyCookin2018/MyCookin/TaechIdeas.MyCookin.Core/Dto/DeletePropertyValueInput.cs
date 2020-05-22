using System;
using TaechIdeas.Core.Core.Token.Dto;

namespace TaechIdeas.MyCookin.Core.Dto
{
    public class DeletePropertyValueInput : TokenRequiredInput
    {
        public Guid RecipeId { get; set; }
        public Guid RecipePropertyValueId { get; set; }
    }
}