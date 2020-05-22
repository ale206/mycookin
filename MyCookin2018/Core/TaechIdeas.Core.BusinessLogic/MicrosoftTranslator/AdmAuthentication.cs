using System;
using System.Net;
using System.Runtime.Serialization.Json;
using System.Text;
using System.Threading;
using System.Web;
using TaechIdeas.Core.Core.MicrosoftTranslator.Dto;

namespace TaechIdeas.Core.BusinessLogic.MicrosoftTranslator
{
    public class AdmAuthentication
    {
        //CLASSE DI ESEMPIO CON ALTRI METODI UTILI IN FUTURO

        public static readonly string DatamarketAccessUri = "https://datamarket.accesscontrol.windows.net/v2/OAuth2-13";
        private readonly string clientId;
        private string clientSecret;
        private readonly string request;
        private AdmAccessToken token;
        private readonly Timer accessTokenRenewer;

        //Access token expires every 10 minutes. Renew it every 9 minutes only.
        private const int RefreshTokenDuration = 9;

        public AdmAuthentication(string clientId, string clientSecret)
        {
            this.clientId = clientId;
            this.clientSecret = clientSecret;
            //If clientid or client secret has special characters, encode before sending request
            request =
                string.Format(
                    "grant_type=client_credentials&client_id={0}&client_secret={1}&scope=http://api.microsofttranslator.com",
                    HttpUtility.UrlEncode(clientId), HttpUtility.UrlEncode(clientSecret));
            token = HttpPost(DatamarketAccessUri, request);
            //renew the token every specfied minutes
            accessTokenRenewer = new Timer(OnTokenExpiredCallback, this,
                TimeSpan.FromMinutes(RefreshTokenDuration), TimeSpan.FromMilliseconds(-1));
        }

        public AdmAccessToken GetAccessToken()
        {
            return token;
        }

        private void RenewAccessToken()
        {
            var newAccessToken = HttpPost(DatamarketAccessUri, request);
            //swap the new token with old one
            //Note: the swap is thread unsafe
            token = newAccessToken;
            Console.WriteLine("Renewed token for user: {0} is: {1}", clientId, token.access_token);
        }

        private void OnTokenExpiredCallback(object stateInfo)
        {
            try
            {
                RenewAccessToken();
            }
            catch (Exception ex)
            {
                Console.WriteLine("Failed renewing access token. Details: {0}", ex.Message);
            }
            finally
            {
                try
                {
                    accessTokenRenewer.Change(TimeSpan.FromMinutes(RefreshTokenDuration), TimeSpan.FromMilliseconds(-1));
                }
                catch (Exception ex)
                {
                    Console.WriteLine("Failed to reschedule the timer to renew access token. Details: {0}", ex.Message);
                }
            }
        }

        private AdmAccessToken HttpPost(string DatamarketAccessUri, string requestDetails)
        {
            //Prepare OAuth request 
            var webRequest = WebRequest.Create(DatamarketAccessUri);
            webRequest.ContentType = "application/x-www-form-urlencoded";
            webRequest.Method = "POST";
            var bytes = Encoding.ASCII.GetBytes(requestDetails);
            webRequest.ContentLength = bytes.Length;
            using (var outputStream = webRequest.GetRequestStream())
            {
                outputStream.Write(bytes, 0, bytes.Length);
            }

            using (var webResponse = webRequest.GetResponse())
            {
                var serializer = new DataContractJsonSerializer(typeof(AdmAccessToken));
                //Get deserialized object from JSON stream
                var token = (AdmAccessToken) serializer.ReadObject(webResponse.GetResponseStream());
                return token;
            }
        }
    }
}