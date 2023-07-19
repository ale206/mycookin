using System;
using TaechIdeas.Core.Core.Token.Dto;

namespace TaechIdeas.MyCookin.Core.Dto
{
    public class NewRecipeInput : TokenRequiredInput
    {
        public string RecipeName { get; set; }
        public int LanguageId { get; set; }
        public Guid ImageId { get; set; }
    }
}