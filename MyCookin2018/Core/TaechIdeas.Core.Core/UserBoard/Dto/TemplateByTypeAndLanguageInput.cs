using TaechIdeas.Core.Core.Token.Dto;

namespace TaechIdeas.Core.Core.UserBoard.Dto
{
    public class TemplateByTypeAndLanguageInput : TokenRequiredInput
    {
        public int UserActionTypeId { get; set; }
        public int LanguageId { get; set; }
    }
}