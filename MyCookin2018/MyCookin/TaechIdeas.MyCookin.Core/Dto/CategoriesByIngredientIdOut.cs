namespace TaechIdeas.MyCookin.Core.Dto
{
    public class CategoriesByIngredientIdOut
    {
        public int IDIngredientCategory { get; set; }

        public int? IDIngredientCategoryFather { get; set; }

        public bool Enabled { get; set; }

        //FROM CATEGORIESLANGUAGES
        public int IDIngredientCategoryLanguage { get; set; }

        public int IDLanguage { get; set; }

        public string IngredientCategoryLanguage { get; set; }

        public string IngredientCategoryLanguageDesc { get; set; }
        public bool isPrincipalCategory { get; set; }
    }
}