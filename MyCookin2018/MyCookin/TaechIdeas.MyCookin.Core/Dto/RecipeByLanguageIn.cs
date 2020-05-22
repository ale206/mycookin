using System;

namespace TaechIdeas.MyCookin.Core.Dto
{
    public class RecipeByLanguageIn
    {
        public Guid RecipeId { get; set; }
        public int LanguageId { get; set; }
    }
}