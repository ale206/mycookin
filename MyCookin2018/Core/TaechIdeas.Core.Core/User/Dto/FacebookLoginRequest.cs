using System;

namespace TaechIdeas.Core.Core.User.Dto
{
    public class FacebookLoginRequest
    {
        public string Id { get; set; }
        public DateTime Birthday { get; set; }
        public string Email { get; set; }
        public string FirstName { get; set; }
        public string Gender { get; set; }
        public string Hometown { get; set; }
        public string LastName { get; set; }
        public string Link { get; set; }
        public string Locale { get; set; }
        public string FullName { get; set; }
        public int Timezone { get; set; }
        public bool Verified { get; set; }
        public string Ip { get; set; }
    }
}