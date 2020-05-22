using System;

namespace TaechIdeas.Core.Core.User.Dto
{
    public class IsNotificationEnabledIn
    {
        public Guid? idUser { get; set; }
        public int idUserNotificationType { get; set; }
        public int? idLanguage { get; set; }
    }
}