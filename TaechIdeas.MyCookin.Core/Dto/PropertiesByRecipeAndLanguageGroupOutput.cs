using System.Collections.Generic;

namespace TaechIdeas.MyCookin.Core.Dto
{
    public class PropertiesByRecipeAndLanguageGroupOutput
    {
        public int RecipePropertyTypeId { get; set; }
        public string RecipePropertyType { get; set; }
        public IEnumerable<string> RecipeProperties { get; set; }
    }
}