using System;
using TaechIdeas.Core.Core.Common.Enums;
using TaechIdeas.Core.Core.Token.Dto;

namespace TaechIdeas.Core.Core.UserBoard.Dto
{
    public class InsertNotificationInput : TokenRequiredInput
    {
        public Guid IdUserNotification { get; set; }
        public Guid IdUser { get; set; }
        public ActionTypes? IdUserActionType { get; set; }
        public string UrlNotification { get; set; }
        public Guid? IdRelatedObject { get; set; }
        public Guid? NotificationImage { get; set; }
        public string UserNotification { get; set; }
        public DateTime CreatedOn { get; set; }
        public DateTime? ViewedOn { get; set; }
        public DateTime? NotifiedOn { get; set; }
        public Guid? IdUserOwnerRelatedObject { get; set; }

        public bool AllNotifications { get; set; }
    }
}