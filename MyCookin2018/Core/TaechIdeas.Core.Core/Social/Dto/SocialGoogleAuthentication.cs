namespace TaechIdeas.Core.Core.Social.Dto
{
    public class SocialGoogleAuthentication
    {
        public string ClientId { get; set; }
        public string ClientSecret { get; set; }
        public string Domain { get; set; }
        public string RedirectUri { get; set; }
        public string Scopes { get; set; }
        public string ApplicationName { get; set; }
        public string Token { get; set; }
        public string State { get; set; }
        public string ApprovalPrompt { get; set; }
        public string AccessType { get; set; }
        public string RefreshToken { get; set; }
    }
}