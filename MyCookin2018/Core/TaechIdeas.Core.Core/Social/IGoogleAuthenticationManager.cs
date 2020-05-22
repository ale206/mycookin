using Google.GData.Client;
using TaechIdeas.Core.Core.Social.Dto;

namespace TaechIdeas.Core.Core.Social
{
    public interface IGoogleAuthenticationManager
    {
        SocialGoogleAuthentication GetSocialGoogleAuthentication(string token, string refreshToken);
        string UrlRedirectToGetToken(OAuth2Parameters parameters);
        OAuth2Parameters GetParameters(SocialGoogleAuthentication socialGoogleAuthentication);
    }
}