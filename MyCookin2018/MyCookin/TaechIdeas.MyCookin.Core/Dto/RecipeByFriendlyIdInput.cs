namespace TaechIdeas.MyCookin.Core.Dto
{
    public class RecipeByFriendlyIdInput
    {
        public string FriendlyId { get; set; }
        public bool IncludeIngredients { get; set; }
        public bool IncludeSteps { get; set; }
        public bool IncludeProperties { get; set; }
    }
}