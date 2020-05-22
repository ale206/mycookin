using System;

namespace TaechIdeas.Core.Core.User.Dto
{
    public class UpdateUserNotificationSettingIn
    {
        public Guid? idUserNotification { get; set; }
        public bool? isEnabled { get; set; }
    }
}