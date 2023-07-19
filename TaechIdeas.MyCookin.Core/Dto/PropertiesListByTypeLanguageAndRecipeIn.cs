using System;

namespace TaechIdeas.MyCookin.Core.Dto
{
    public class PropertiesListByTypeLanguageAndRecipeIn
    {
        public int LanguageId { get; set; }
        public int RecipePropertyTypeId { get; set; }
        public Guid RecipeId { get; set; }
    }
}