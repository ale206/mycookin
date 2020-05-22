using System;
using Google.GData.Client;
using TaechIdeas.Core.Core.Social;
using TaechIdeas.Core.Core.Social.Dto;

namespace TaechIdeas.Core.BusinessLogic.Social
{
    public class GoogleAuthenticationManager : IGoogleAuthenticationManager
    {
        #region GetSocialGoogleAuthentication

        public SocialGoogleAuthentication GetSocialGoogleAuthentication(string token, string refreshToken)
        {
            #region Info

            /*      
                First of ALL: Register an Application on Google Api Console
                GOOGLE API CONSOLE:
                https://code.google.com/apis/console/?hl=it#project:537242677033:access

                DESCRIZIONE IN GENERALE:
                https://developers.google.com/accounts/docs/OAuth2?hl=it

                DESCRIZIONE PER IL LOGIN:
                https://developers.google.com/accounts/docs/OAuth2Login?hl=it

                OTTIMO LINK PER PROVE DI CHIAMATE AL WEBSERVICE
                https://developers.google.com/oauthplayground/
            
                First: Click to connect to google
        
                response_type = Determines if the Google Authorization Server returns an authorization code, or an opaque access token.
                client_id     = Indicates the client that is making the request. The value passed in this parameter must exactly match the value shown in the APIs Console.
                redirect_uri  = Determines where the response is sent. The value of this parameter must exactly match one of the values registered in the APIs Console (including the http or https schemes, case, and trailing '/').
                scope         = Indicates the Google API access your application is requesting. The values passed in this parameter inform the consent page shown to the user. There is an inverse relationship between the number of permissions requested and the likelihood of obtaining user consent.
                state         = This optional parameter indicates any state which may be useful to your application upon receipt of the response. The Google Authorization Server roundtrips this parameter, so your application receives the same value it sent. Possible uses include redirecting the user to the correct resource in your site, nonces, and cross-site-request-forgery mitigations.
                */

            #endregion

            throw new NotImplementedException();

            //var socialGoogleAuthentication = new SocialGoogleAuthentication
            //{
            //    ClientId = ConfigurationManager.AppSettings["google_clientId"],
            //    ClientSecret = ConfigurationManager.AppSettings["google_clientSecret"],
            //    Domain = ConfigurationManager.AppSettings["google_domain"],
            //    RedirectUri = ConfigurationManager.AppSettings["google_redirectUri"],
            //    Scopes = ConfigurationManager.AppSettings["google_scopes"],
            //    ApplicationName = ConfigurationManager.AppSettings["google_applicationName"],
            //    Token = token,
            //    //_State = State;
            //    //_ApprovalPrompt = ApprovalPrompt;
            //    //_AccessType = AccessType;
            //    RefreshToken = refreshToken,
            //};

            //return socialGoogleAuthentication;
        }

        #endregion

        #region UrlRedirectToGetToken

        /// <summary>
        ///     Redirect Url To Get Token
        /// </summary>
        /// <returns>Url String</returns>
        public string UrlRedirectToGetToken(OAuth2Parameters parameters)
        {
            var url = OAuthUtil.CreateOAuth2AuthorizationUrl(parameters);
            return url;
        }

        #endregion

        #region GetParameters

        public OAuth2Parameters GetParameters(SocialGoogleAuthentication socialGoogleAuthentication)
        {
            var parameters = new OAuth2Parameters
            {
                ClientId = socialGoogleAuthentication.ClientId,
                ClientSecret = socialGoogleAuthentication.ClientSecret,
                RedirectUri = socialGoogleAuthentication.RedirectUri,
                Scope = socialGoogleAuthentication.Scopes,
                AccessToken = socialGoogleAuthentication.Token,
                //AccessType = socialGoogleAuthentication.AccessType,
                RefreshToken = socialGoogleAuthentication.RefreshToken
                //ApprovalPrompt = socialGoogleAuthentication.ApprovalPrompt
            };

            return parameters;
        }

        #endregion
    }
}