using System;

namespace TaechIdeas.Core.Core.UserBoard.Dto
{
    public class InsertNotificationIn
    {
        public Guid IDUserNotification { get; set; }
        public Guid IDUser { get; set; }
        public int? IDUserActionType { get; set; }
        public string UrlNotification { get; set; }
        public Guid? IDRelatedObject { get; set; }
        public Guid? NotificationImage { get; set; }
        public string UserNotification { get; set; }
        public DateTime CreatedOn { get; set; }
        public DateTime? ViewedOn { get; set; }
        public DateTime? NotifiedOn { get; set; }
        public Guid? IDUserOwnerRelatedObject { get; set; }

        public bool AllNotifications { get; set; }
    }
}