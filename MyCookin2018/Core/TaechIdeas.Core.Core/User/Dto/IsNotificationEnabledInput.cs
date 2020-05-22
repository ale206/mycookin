using System;
using TaechIdeas.Core.Core.Common.Enums;

namespace TaechIdeas.Core.Core.User.Dto
{
    public class IsNotificationEnabledInput
    {
        public Guid? idUser { get; set; }
        public NotificationTypes idUserNotificationType { get; set; }
        public int? idLanguage { get; set; }
    }
}