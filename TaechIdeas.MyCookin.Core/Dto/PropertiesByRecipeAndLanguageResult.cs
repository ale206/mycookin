using System.Collections.Generic;

namespace TaechIdeas.MyCookin.Core.Dto
{
    public class PropertiesByRecipeAndLanguageResult
    {
        public IEnumerable<PropertiesByRecipeAndLanguageGroupResult> PropertiesByRecipeAndLanguageGroups { get; set; }
    }
}