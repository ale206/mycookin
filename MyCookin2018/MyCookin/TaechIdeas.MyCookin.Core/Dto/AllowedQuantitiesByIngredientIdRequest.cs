using System;

namespace TaechIdeas.MyCookin.Core.Dto
{
    public class AllowedQuantitiesByIngredientIdRequest
    {
        public Guid IngredientId { get; set; }
        public int LanguageId { get; set; }
    }
}