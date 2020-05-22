using System;
using TaechIdeas.Core.Core.Token.Dto;

namespace TaechIdeas.Core.Core.UserBoard.Dto
{
    public class MarkNotificationsAsViewedInput : TokenRequiredInput
    {
        public Guid? UserIdOwnerRelatedObject { get; set; }
    }
}