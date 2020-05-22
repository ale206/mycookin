using System;

namespace TaechIdeas.Core.Core.UserBoard.Dto
{
    public class CheckIfSharedIn
    {
        public Guid UserActionId { get; set; }
        public Guid UserId { get; set; }
        public int SocialNetworkId { get; set; }
    }
}