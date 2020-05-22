using System;

namespace TaechIdeas.MyCookin.Core.Dto
{
    public class AllowedQuantitiesByIngredientIdIn
    {
        public Guid IngredientId { get; set; }
        public int LanguageId { get; set; }
    }
}