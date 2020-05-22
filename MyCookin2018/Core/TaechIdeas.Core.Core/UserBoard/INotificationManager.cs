using System.Collections.Generic;
using TaechIdeas.Core.Core.UserBoard.Dto;

namespace TaechIdeas.Core.Core.UserBoard
{
    public interface INotificationManager
    {
        InsertNotificationOutput InsertNotification(InsertNotificationInput insertNotificationInput);
        IEnumerable<GetNotificationsForUserOutput> GetNotificationsForUser(GetNotificationsForUserInput getNotificationsForUserInput);
        MarkNotificationsAsViewedOutput MarkNotificationsAsViewed(MarkNotificationsAsViewedInput markNotificationsAsViewedInput);
        MarkNotificationsAsNotifiedOutput MarkNotificationsAsNotified(MarkNotificationsAsNotifiedInput markNotificationsAsNotifiedInput);
    }
}