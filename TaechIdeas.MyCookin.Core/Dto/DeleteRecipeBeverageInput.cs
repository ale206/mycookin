using System;
using TaechIdeas.Core.Core.Token.Dto;

namespace TaechIdeas.MyCookin.Core.Dto
{
    public class DeleteRecipeBeverageInput : TokenRequiredInput
    {
        public Guid BeverageRecipeId { get; set; }
    }
}