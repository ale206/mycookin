using System;
using TaechIdeas.Core.Core.Common.Dto;

namespace TaechIdeas.Core.Core.UserBoard.Dto
{
    public class WithPaginationOutput : PaginationFieldsOutput
    {
        //UserActionsTypes
        public int UserActionTypeId { get; set; }

        public bool UserActionTypeEnabled { get; set; }

        public bool UserActionTypeSiteNotice { get; set; }

        public bool UserActionTypeMailNotice { get; set; }

        public bool UserActionTypeSmsNotice { get; set; }

        public int? UserActionTypeMessageMaxLength { get; set; }

        //UserActionsTypesLanguages
        public int UserActionTypeLanguageId { get; set; }

        public int LanguageId { get; set; }

        public string UserActionType { get; set; }

        public string UserActionTypeTemplate { get; set; }

        public string UserActionTypeTemplatePlural { get; set; }

        public string UserActionTypeToolTip { get; set; }

        public string NotificationTemplate { get; set; }

        //UsersAction
        public Guid UserActionId { get; set; }

        public Guid UserId { get; set; }

        public Guid? UserActionFatherId { get; set; }

        public Guid? ActionRelatedObjectId { get; set; }

        public string UserActionMessage { get; set; }

        public int? VisibilityId { get; set; }

        public DateTime UserActionDate { get; set; }

        public DateTime? DeletedOn { get; set; }
    }
}