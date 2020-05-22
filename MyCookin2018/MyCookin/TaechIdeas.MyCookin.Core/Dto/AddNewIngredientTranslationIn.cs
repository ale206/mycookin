using System;

namespace TaechIdeas.MyCookin.Core.Dto
{
    public class AddNewIngredientTranslationIn
    {
        public Guid IngredientId { get; set; }
        public int LanguageId { get; set; }
        public string IngredientSingular { get; set; }
        public string IngredientPlural { get; set; }
        public string IngredientDescription { get; set; }
        public bool IsAutoTranslate { get; set; }
    }
}