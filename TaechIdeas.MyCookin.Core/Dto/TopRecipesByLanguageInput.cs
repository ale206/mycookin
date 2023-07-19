namespace TaechIdeas.MyCookin.Core.Dto
{
    public class TopRecipesByLanguageInput
    {
        public int LanguageId { get; set; }
        public int RecipeToShow { get; set; }
        public bool IncludeIngredients { get; set; }
        public bool IncludeSteps { get; set; }
        public bool IncludeProperties { get; set; }
    }
}