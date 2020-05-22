using System;
using TaechIdeas.Core.Core.Token.Dto;

namespace TaechIdeas.Core.Core.UserBoard.Dto
{
    public class GetNotificationsForUserInput : TokenRequiredInput
    {
        public bool AllNotifications { get; set; }
        public Guid? UserIdOwnerRelatedObject { get; set; }
    }
}