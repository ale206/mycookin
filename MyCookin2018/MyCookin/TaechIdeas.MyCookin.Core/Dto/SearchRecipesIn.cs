using TaechIdeas.Core.Core.Common.Dto;

namespace TaechIdeas.MyCookin.Core.Dto
{
    public class SearchRecipesIn : PaginationFieldsIn
    {
        public int LanguageId { get; set; }
        public bool Vegan { get; set; }
        public bool Vegetarian { get; set; }
        public bool GlutenFree { get; set; }
        public int LightThreshold { get; set; }
        public int QuickThreshold { get; set; }
    }
}