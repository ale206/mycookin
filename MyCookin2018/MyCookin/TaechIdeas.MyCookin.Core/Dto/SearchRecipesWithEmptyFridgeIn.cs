using TaechIdeas.Core.Core.Common.Dto;

namespace TaechIdeas.MyCookin.Core.Dto
{
    /// <summary>
    ///     Use same column names
    /// </summary>
    public class SearchRecipesWithEmptyFridgeIn : PaginationFieldsIn
    {
        public int LanguageId { get; set; }
        public bool Vegan { get; set; }
        public bool Vegetarian { get; set; }
        public bool GlutenFree { get; set; }
        public int LightThreshold { get; set; }
        public int QuickThreshold { get; set; }
    }
}