namespace TaechIdeas.MyCookin.Core.Dto
{
    public class IngredientCategoryLanguage : IngredientCategory
    {
        #region publicFields

        public int IdIngredientCategoryLanguage { get; set; }
        public int IdLanguage { get; set; }
        public string IngredientCategoryByLanguage { get; set; }
        public string IngredientCategoryLanguageDesc { get; set; }

        #endregion
    }
}