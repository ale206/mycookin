using System;

namespace TaechIdeas.MyCookin.Core.Dto
{
    public class AddOrUpdatePropertyValueInput
    {
        public Guid RecipeId { get; set; }
        public int RecipePropertyId { get; set; }
    }
}