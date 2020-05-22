using System;

namespace TaechIdeas.Core.Core.UserBoard.Dto
{
    public class MyNewsBlockLoadResult
    {
        //UserActionsTypes
        public int IDUserActionType { get; set; }

        public bool UserActionTypeEnabled { get; set; }

        public bool UserActionTypeSiteNotice { get; set; }

        public bool UserActionTypeMailNotice { get; set; }

        public bool UserActionTypeSmsNotice { get; set; }

        public int? UserActionTypeMessageMaxLength { get; set; }

        //UserActionsTypesLanguages
        public int IDUserActionTypeLanguage { get; set; }

        public int IDLanguage { get; set; }

        public string UserActionType { get; set; }

        public string UserActionTypeTemplate { get; set; }

        public string UserActionTypeTemplatePlural { get; set; }

        public string UserActionTypeToolTip { get; set; }

        public string NotificationTemplate { get; set; }

        //UsersAction
        public Guid IDUserAction { get; set; }

        public Guid IDUser { get; set; }

        public Guid? IDUserActionFather { get; set; }

        public Guid? IDActionRelatedObject { get; set; }

        public string UserActionMessage { get; set; }

        public int? IDVisibility { get; set; }

        public DateTime UserActionDate { get; set; }

        public DateTime? DeletedOn { get; set; }
    }
}