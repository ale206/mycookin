using System;
using TaechIdeas.Core.Core.Token.Dto;

namespace TaechIdeas.Core.Core.UserBoard.Dto
{
    public class MarkNotificationsAsNotifiedInput : TokenRequiredInput
    {
        public Guid? UserNotificationId { get; set; }
    }
}