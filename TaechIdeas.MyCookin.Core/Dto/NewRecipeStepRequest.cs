namespace TaechIdeas.MyCookin.Core.Dto
{
    public class NewRecipeStepRequest
    {
        public string StepGroup { get; set; }
        public int StepNumber { get; set; }
        public string RecipeStep { get; set; }
        public int? StepTimeMinute { get; set; }
        public string ImageUrl { get; set; }
    }
}