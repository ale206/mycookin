using System;

namespace TaechIdeas.MyCookin.Core.Dto
{
    public class AddOrUpdatePropertyValueIn
    {
        public Guid RecipeId { get; set; }
        public int RecipePropertyId { get; set; }
    }
}