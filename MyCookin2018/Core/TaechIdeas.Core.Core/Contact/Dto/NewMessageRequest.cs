using TaechIdeas.Core.Core.Token.Dto;

namespace TaechIdeas.Core.Core.Contact.Dto
{
    public class NewMessageRequest
    {
        public CheckTokenRequest CheckTokenRequest { get; set; }
        public int LanguageId { get; set; }
        public int ContactRequestTypeId { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Email { get; set; }
        public string RequestText { get; set; }
        public bool PrivacyAccept { get; set; }
        public string IpAddress { get; set; }
    }
}