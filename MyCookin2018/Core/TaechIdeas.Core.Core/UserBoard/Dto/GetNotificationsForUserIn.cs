using System;

namespace TaechIdeas.Core.Core.UserBoard.Dto
{
    public class GetNotificationsForUserIn
    {
        public Guid IdUserOwnerRelatedObject { get; set; }
        public int IdUserActionType { get; set; }
        public int NotificationsRead { get; set; }
        public bool AllNotification { get; set; }
        public int MaxNotificationsNumber { get; set; }
    }
}