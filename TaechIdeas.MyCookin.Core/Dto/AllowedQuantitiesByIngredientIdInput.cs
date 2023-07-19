using System;

namespace TaechIdeas.MyCookin.Core.Dto
{
    public class AllowedQuantitiesByIngredientIdInput
    {
        public Guid IngredientId { get; set; }
        public int LanguageId { get; set; }
    }
}