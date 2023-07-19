using System;
using TaechIdeas.Core.Core.Token.Dto;

namespace TaechIdeas.MyCookin.Core.Dto
{
    public class DeleteRecipeInput : TokenRequiredInput
    {
        public Guid RecipeLanguageId { get; set; }
    }
}