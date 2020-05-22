using System;

namespace TaechIdeas.Core.Core.UserBoard.Dto
{
    public class GetNotificationsForUserOut
    {
        public Guid IdUserNotification { get; set; }
        public Guid IdUser { get; set; }
        public int? IdUserActionType { get; set; }
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