using TaechIdeas.Core.Core.Token.Dto;

namespace TaechIdeas.Core.Core.UserBoard.Dto
{
    public class TemplateByTypeAndLanguageRequest
    {
        public CheckTokenRequest CheckTokenRequest { get; set; }
        public int UserActionTypeId { get; set; }
        public int LanguageId { get; set; }
    }
}