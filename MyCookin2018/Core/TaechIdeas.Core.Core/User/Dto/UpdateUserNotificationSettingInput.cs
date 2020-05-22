using System;

namespace TaechIdeas.Core.Core.User.Dto
{
    public class UpdateUserNotificationSettingInput
    {
        public Guid? idUserNotification { get; set; }
        public bool? isEnabled { get; set; }
    }
}