using System;
using TaechIdeas.Core.Core.Token.Dto;

namespace TaechIdeas.Core.Core.UserBoard.Dto
{
    public class InsertActionSharedRequest
    {
        public CheckTokenRequest CheckTokenRequest { get; set; }
        public Guid UserActionId { get; set; }
        public Guid UserId { get; set; }
        public int SocialNetworkId { get; set; }
        public string ShareIdOnSocial { get; set; }
    }
}