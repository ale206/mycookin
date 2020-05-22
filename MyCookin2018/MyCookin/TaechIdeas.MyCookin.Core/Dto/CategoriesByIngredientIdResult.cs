namespace TaechIdeas.MyCookin.Core.Dto
{
    public class CategoriesByIngredientIdResult
    {
        public int IngredientCategoryId { get; set; }

        public int? IngredientCategoryFatherId { get; set; }

        public bool Enabled { get; set; }

        //FROM CATEGORIESLANGUAGES
        public int IngredientCategoryLanguageId { get; set; }

        public int LanguageId { get; set; }

        public string IngredientCategoryLanguage { get; set; }

        public string IngredientCategoryLanguageDesc { get; set; }
        public bool IsPrincipalCategory { get; set; }
    }
}