using System;

namespace TaechIdeas.Core.Core.User.Dto
{
    public class UserSocialInformationInput
    {
        public Guid UserId { get; set; }
        public int SocialNetworkId { get; set; }
    }
}