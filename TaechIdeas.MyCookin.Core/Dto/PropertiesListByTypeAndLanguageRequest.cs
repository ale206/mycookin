using TaechIdeas.Core.Core.Token.Dto;

namespace TaechIdeas.MyCookin.Core.Dto
{
    public class PropertiesListByTypeAndLanguageRequest
    {
        public CheckTokenRequest CheckTokenRequest { get; set; }
        public int LanguageId { get; set; }
        public int RecipePropertyTypeId { get; set; }
    }
}