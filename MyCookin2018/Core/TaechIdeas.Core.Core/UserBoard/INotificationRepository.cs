using System.Collections.Generic;
using TaechIdeas.Core.Core.UserBoard.Dto;

namespace TaechIdeas.Core.Core.UserBoard
{
    public interface INotificationRepository
    {
        InsertNotificationOut InsertNotification(InsertNotificationIn insertNotificationIn);
        IEnumerable<GetNotificationsForUserOut> GetNotificationsForUser(GetNotificationsForUserIn getNotificationsForUserIn);
        MarkNotificationsAsViewedOut MarkNotificationsAsViewed(MarkNotificationsAsViewedIn markNotificationsAsViewedIn);
        MarkNotificationsAsNotifiedOut MarkNotificationsAsNotified(MarkNotificationsAsNotifiedIn markNotificationsAsNotifiedIn);
    }
}