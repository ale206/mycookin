using System;
using TaechIdeas.Core.Core.Token.Dto;

namespace TaechIdeas.MyCookin.Core.Dto
{
    public class DeleteIngredientInput : TokenRequiredInput
    {
        public Guid IngredientId { get; set; }
    }
}