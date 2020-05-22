using System;

namespace TaechIdeas.MyCookin.Core.Dto
{
    public class DeletePropertyValueIn
    {
        public Guid RecipeId { get; set; }
        public Guid RecipePropertyValueId { get; set; }
    }
}