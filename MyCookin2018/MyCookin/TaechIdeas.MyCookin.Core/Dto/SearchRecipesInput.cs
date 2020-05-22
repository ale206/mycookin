using TaechIdeas.Core.Core.Common.Dto;

namespace TaechIdeas.MyCookin.Core.Dto
{
    public class SearchRecipesInput : PaginationFieldsInput
    {
        //public string SearchQuery { get; set; }
        public int LanguageId { get; set; }
        public bool Vegan { get; set; }
        public bool Vegetarian { get; set; }
        public bool GlutenFree { get; set; }
        public int LightThreshold { get; set; }
        public int QuickThreshold { get; set; }
        public bool Light { get; set; }
        public bool Quick { get; set; }
        public bool IsEmptyFridge { get; set; }
    }
}