using System.Collections.Generic;
using TaechIdeas.Core.Core.User.Dto;

namespace TaechIdeas.Core.Core.Notification
{
    public interface INotificationManager
    {
        IEnumerable<NotificationListOutput> NotificationList(NotificationListInput notificationListInput);
        UpdateUserNotificationSettingOutput UpdateUserNotificationSetting(UpdateUserNotificationSettingInput notificationSettingInput);
        IsNotificationEnabledOutput IsNotificationEnabled(IsNotificationEnabledInput isNotificationEnabledInput);
    }
}