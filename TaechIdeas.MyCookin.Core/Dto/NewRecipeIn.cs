using System;

namespace TaechIdeas.MyCookin.Core.Dto
{
    public class NewRecipeIn
    {
        public string RecipeName { get; set; }
        public int LanguageId { get; set; }
        public Guid ImageId { get; set; }
        public Guid UserId { get; set; }
    }
}