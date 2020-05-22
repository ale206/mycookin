namespace TaechIdeas.MyCookin.Core.Dto
{
    public class IngredientCategory
    {
        public int IdIngredientCategory { get; set; }
        public int? IdIngredientCategoryFather { get; set; }
        public bool Enabled { get; set; }
    }
}