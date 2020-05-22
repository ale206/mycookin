using System;
using TaechIdeas.Core.Core.Token.Dto;

namespace TaechIdeas.Core.Core.UserBoard.Dto
{
    public class CheckIfSharedRequest
    {
        public CheckTokenRequest CheckTokenRequest { get; set; }
        public Guid UserActionId { get; set; }
        public Guid UserId { get; set; }
        public int SocialNetworkId { get; set; }
    }
}