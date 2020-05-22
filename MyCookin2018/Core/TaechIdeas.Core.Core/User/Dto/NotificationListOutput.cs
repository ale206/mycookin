using System;
using TaechIdeas.Core.Core.Common.Enums;

namespace TaechIdeas.Core.Core.User.Dto
{
    public class NotificationListOutput
    {
        public NotificationTypes IdUserNotificationType { get; set; }
        public string NotificationType { get; set; }
        public bool NotificationTypeEnabled { get; set; }
        public int NotificationTypeOrder { get; set; }
        public bool IsVisible { get; set; }
        public int IdUserNotificationLanguage { get; set; }
        public int IdLanguage { get; set; }
        public string NotificationQuestion { get; set; }
        public string NotificationComment { get; set; }
        public Guid IdUserNotification { get; set; }
        public Guid IdUser { get; set; }
        public bool IsEnabled { get; set; }
    }
}