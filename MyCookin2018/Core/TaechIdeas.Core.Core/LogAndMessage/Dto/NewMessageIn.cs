namespace TaechIdeas.Core.Core.LogAndMessage.Dto
{
    public class NewMessageIn
    {
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