namespace TaechIdeas.MyCookin.Core.Dto
{
    public class BestRecipesByLanguageInput
    {
        public int LanguageId { get; set; }
        public bool IncludeIngredients { get; set; }
        public bool IncludeSteps { get; set; }
        public bool IncludeProperties { get; set; }
    }
}