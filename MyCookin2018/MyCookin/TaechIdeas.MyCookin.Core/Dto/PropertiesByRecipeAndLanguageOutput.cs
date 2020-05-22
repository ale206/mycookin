using System.Collections.Generic;

namespace TaechIdeas.MyCookin.Core.Dto
{
    public class PropertiesByRecipeAndLanguageOutput
    {
        public IEnumerable<PropertiesByRecipeAndLanguageGroupOutput> PropertiesByRecipeAndLanguageGroups { get; set; }
    }
}