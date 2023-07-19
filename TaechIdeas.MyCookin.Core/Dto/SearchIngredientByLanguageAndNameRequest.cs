using TaechIdeas.Core.Core.Token.Dto;

namespace TaechIdeas.MyCookin.Core.Dto
{
    public class SearchIngredientByLanguageAndNameRequest
    {
        public string IngredientName { get; set; }
        public int LanguageId { get; set; }
        public CheckTokenRequest CheckTokenRequest { get; set; }
    }
}