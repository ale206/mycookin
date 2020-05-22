using System;

namespace TaechIdeas.MyCookin.Core.Dto
{
    public class AddOrUpdatePropertyValueRequest
    {
        public Guid RecipeId { get; set; }
        public int RecipePropertyId { get; set; }
    }
}