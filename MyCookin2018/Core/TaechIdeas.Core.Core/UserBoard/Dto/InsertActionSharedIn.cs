using System;

namespace TaechIdeas.Core.Core.UserBoard.Dto
{
    public class InsertActionSharedIn
    {
        public Guid UserActionId { get; set; }
        public Guid UserId { get; set; }
        public int SocialNetworkId { get; set; }
        public string ShareIdOnSocial { get; set; }
    }
}