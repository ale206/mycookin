using System;
using TaechIdeas.Core.Core.Token.Dto;

namespace TaechIdeas.MyCookin.Core.Dto
{
    public class PropertiesListByTypeLanguageAndRecipeRequest
    {
        public CheckTokenRequest CheckTokenRequest { get; set; }
        public int LanguageId { get; set; }
        public int RecipePropertyTypeId { get; set; }
        public Guid RecipeId { get; set; }
    }
}