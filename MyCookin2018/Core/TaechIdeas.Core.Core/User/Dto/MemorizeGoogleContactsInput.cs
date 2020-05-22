using System;
using Google.GData.Client;

namespace TaechIdeas.Core.Core.User.Dto
{
    public class MemorizeGoogleContactsInput
    {
        public string applicationName { get; set; }
        public OAuth2Parameters parameters { get; set; }
        public Guid UserId { get; set; }
    }
}