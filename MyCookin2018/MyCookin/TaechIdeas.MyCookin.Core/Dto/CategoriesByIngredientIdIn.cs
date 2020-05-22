using System;

namespace TaechIdeas.MyCookin.Core.Dto
{
    public class CategoriesByIngredientIdIn
    {
        public Guid IngredientId { get; set; }
        public int LanguageId { get; set; }
    }
}