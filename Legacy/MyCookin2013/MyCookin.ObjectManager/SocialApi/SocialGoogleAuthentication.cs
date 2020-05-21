using System;
using System.Collections.Generic;
using System.Web;
using System.Text;
using Google.GData.Client;
using Google.Contacts;
using Google.GData.Apps.Groups;
using Google.GData.Apps;
using Google.GData.Extensions;
using Google.GData.Extensions.Apps;
using System.Configuration;


namespace MyCookin.ObjectManager.SocialAction.Google
{
    public class SocialGoogleAuthentication
    {

        #region PrivateFields
        private string _ClientID;
        private string _ClientSecret;
        private string _Domain;
        private string _RedirectUri;
        private string _Scopes;
        private string _ApplicationName;
        private string _Token;
        private string _State;
        private string _ApprovalPrompt;
        private string _AccessType;
        private string _RefreshToken;
        #endregion

        #region PublicFields
        public string ClientID
        {
        get { return _ClientID;}
        set { _ClientID = value;}
        }
        public string ClientSecret
        {
        get { return _ClientSecret;}
        set { _ClientSecret = value;}
        }
        public string Domain
        {
        get { return _Domain;}
        set { _Domain = value;}
        }
        public string RedirectUri
        {
        get { return _RedirectUri;}
        set { _RedirectUri = value;}
        }
        public string Scopes
        {
        get { return _Scopes;}
        set { _Scopes = value;}
        }
        public string ApplicationName
        {
        get { return _ApplicationName;}
        set { _ApplicationName = value;}
        }
        public string Token
        {
        get { return _Token;}
        set { _Token = value;}
        }
        public string State
        {
            get { return _State; }
            set { _State = value; }
        }
        public string ApprovalPrompt
        {
            get { return _ApprovalPrompt; }
            set { _ApprovalPrompt = value; }
        }
        public string AccessType
        {
            get { return _AccessType; }
            set { _AccessType = value; }
        }
        public string RefreshToken
        {
            get { return _RefreshToken; }
            set { _RefreshToken = value; }
        }

        #endregion

        #region Constructors

        public SocialGoogleAuthentication(string Token, string RefreshToken)
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

            _ClientID = ConfigurationManager.AppSettings["google_clientId"].ToString();
            _ClientSecret = ConfigurationManager.AppSettings["google_clientSecret"].ToString();
            _Domain = ConfigurationManager.AppSettings["google_domain"].ToString();
            _RedirectUri = ConfigurationManager.AppSettings["google_redirectUri"].ToString();
            _Scopes = ConfigurationManager.AppSettings["google_scopes"].ToString();
            _ApplicationName = ConfigurationManager.AppSettings["google_applicationName"].ToString(); ;
            _Token = Token;
            //_State = State;
            //_ApprovalPrompt = ApprovalPrompt;
            //_AccessType = AccessType;
            _RefreshToken = RefreshToken;
        }

            #endregion

        #region Methods
        /// <summary>
        /// Redirect Url To Get Token
        /// </summary>
        /// <returns>Url String</returns>
        public string UrlRedirectToGetToken(OAuth2Parameters parameters)
        {
            string url = OAuthUtil.CreateOAuth2AuthorizationUrl(parameters);
            return url;
        }

        public OAuth2Parameters GetParameters()
        {
            OAuth2Parameters parameters = new OAuth2Parameters()
            {
                ClientId = _ClientID,
                ClientSecret = _ClientSecret,
                RedirectUri = _RedirectUri,
                Scope = _Scopes,
                AccessToken = _Token,
                //AccessType = _AccessType,
                RefreshToken = _RefreshToken,
                //ApprovalPrompt = _ApprovalPrompt
            };
            
            return parameters;
        }

        #endregion

    }

    public class GoogleData
    {
        public GoogleData() { }
        public string id { get; set; }
        public string email { get; set; }
        public string verified_email { get; set; }
        public string name { get; set; }
        public string given_name { get; set; }
        public string family_name { get; set; }
        public string picture { get; set; }
        public string locale { get; set; }
        public string link { get; set; }
        public string gender { get; set; }
        public string birthday { get; set; }

//        public string refreshToken { get; set; }
    }

}