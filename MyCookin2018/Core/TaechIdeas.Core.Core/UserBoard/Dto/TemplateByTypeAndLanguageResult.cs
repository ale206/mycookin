namespace TaechIdeas.Core.Core.UserBoard.Dto
{
    public class TemplateByTypeAndLanguageResult
    {
        public int IDUserActionTypeLanguage { get; set; }

        public int IDUserActionType { get; set; }

        public int IDLanguage { get; set; }

        public string UserActionType { get; set; }

        public string UserActionTypeTemplate { get; set; }

        public string UserActionTypeTemplatePlural { get; set; }

        public string UserActionTypeToolTip { get; set; }

        public string NotificationTemplate { get; set; }
    }
}