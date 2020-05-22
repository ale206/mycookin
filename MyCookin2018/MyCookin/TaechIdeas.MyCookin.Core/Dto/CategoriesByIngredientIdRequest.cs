using System;

namespace TaechIdeas.MyCookin.Core.Dto
{
    public class CategoriesByIngredientIdRequest
    {
        public Guid IngredientId { get; set; }
        public int LanguageId { get; set; }
    }
}