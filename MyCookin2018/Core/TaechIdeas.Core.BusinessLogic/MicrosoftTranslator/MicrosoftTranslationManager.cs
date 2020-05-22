using System;
using System.IO;
using System.Net;
using System.Runtime.Serialization;
using System.Text;
using System.Web;
using TaechIdeas.Core.Core.Configuration;
using TaechIdeas.Core.Core.MicrosoftTranslator;
using TaechIdeas.Core.Core.MicrosoftTranslator.Dto;

namespace TaechIdeas.Core.BusinessLogic.MicrosoftTranslator
{
    public class MicrosoftTranslationManager : IMicrosoftTranslationManager
    {
        private readonly IThirdPartyConfig _thirdPartyConfig;

        public MicrosoftTranslationManager(IThirdPartyConfig thirdPartyConfig)
        {
            _thirdPartyConfig = thirdPartyConfig;
        }

        #region TranslateSentence

        public TranslateSentenceOutput TranslateSentence(string languageFrom, string languageTo, string originalSentence)
        {
            //TODO: IMPROVE TO AVOID TO OPEN A NEW CONNESSION EVERYTIME

            var translationObject = new TranslateSentenceOutput();

            var clientId = _thirdPartyConfig.MicrosoftClientId;
            var clientSecret = _thirdPartyConfig.MicrosoftClientSecret;

            //Get Client Id and Client Secret from https://datamarket.azure.com/developer/applications/
            //Refer obtaining AccessToken (http://msdn.microsoft.com/en-us/library/hh454950.aspx) 
            var admAuth = new AdmAuthentication(clientId, clientSecret);
            try
            {
                var admToken = admAuth.GetAccessToken();
                // Create a header with the access_token property of the returned token
                var headerValue = "Bearer " + admToken.access_token;
                //DetectMethod(headerValue);

                try
                {
                    var uri = "http://api.microsofttranslator.com/v2/Http.svc/Translate?text=" +
                              HttpUtility.UrlEncode(originalSentence) + "&from=" + languageFrom + "&to=" +
                              languageTo;

                    var httpWebRequest = (HttpWebRequest) WebRequest.Create(uri);
                    httpWebRequest.Headers.Add("Authorization", headerValue);

                    WebResponse response = null;

                    response = httpWebRequest.GetResponse();

                    string translatedSentence;
                    using (var stream = response.GetResponseStream())
                    {
                        var dcs = new DataContractSerializer(Type.GetType("System.String"));
                        translatedSentence = (string) dcs.ReadObject(stream);
                    }

                    //POPULATE OBJECT
                    //*************************************************
                    translationObject.LanguageSource = languageFrom;
                    translationObject.LanguageTarget = languageTo;
                    translationObject.OriginalSentence = originalSentence;
                    translationObject.TranslatedSentence = translatedSentence;
                    translationObject.Result = true;
                    translationObject.ErrorMessage = "";
                    //*************************************************
                }
                catch (Exception ex)
                {
                    //POPULATE OBJECT
                    //*************************************************
                    translationObject.LanguageSource = languageFrom;
                    translationObject.LanguageTarget = languageTo;
                    translationObject.OriginalSentence = originalSentence;
                    translationObject.TranslatedSentence = "";
                    translationObject.Result = false;
                    translationObject.ErrorMessage = ex.Message;
                    //*************************************************
                }
            }
            catch (WebException e)
            {
                string strResponse;
                using (var response = (HttpWebResponse) e.Response)
                {
                    using (var responseStream = response.GetResponseStream())
                    {
                        using (var sr = new StreamReader(responseStream, Encoding.ASCII))
                        {
                            strResponse = sr.ReadToEnd();
                        }
                    }
                }

                //POPULATE OBJECT
                //*************************************************
                translationObject.LanguageSource = languageFrom;
                translationObject.LanguageTarget = languageTo;
                translationObject.OriginalSentence = originalSentence;
                translationObject.TranslatedSentence = "";
                translationObject.Result = false;
                translationObject.ErrorMessage = "Http status code=" + e.Status + ", error message=" + strResponse;
            }
            catch (Exception ex)
            {
                //POPULATE OBJECT
                //*************************************************
                translationObject.LanguageSource = languageFrom;
                translationObject.LanguageTarget = languageTo;
                translationObject.OriginalSentence = originalSentence;
                translationObject.TranslatedSentence = "";
                translationObject.Result = false;
                translationObject.ErrorMessage = ex.Message;
                //*************************************************
            }

            return translationObject;
        }

        #endregion

        #region DetectMethod - EXAMPLE - COME DA ESEMPIO

        private static void DetectMethod(string authToken)
        {
            Console.WriteLine("Enter Text to detect language:");
            var textToDetect = Console.ReadLine();
            //Keep appId parameter blank as we are sending access token in authorization header.
            var uri = "http://api.microsofttranslator.com/v2/Http.svc/Detect?text=" + textToDetect;
            var httpWebRequest = (HttpWebRequest) WebRequest.Create(uri);
            httpWebRequest.Headers.Add("Authorization", authToken);
            WebResponse response = null;
            try
            {
                response = httpWebRequest.GetResponse();
                using (var stream = response.GetResponseStream())
                {
                    var dcs = new DataContractSerializer(Type.GetType("System.String"));
                    var languageDetected = (string) dcs.ReadObject(stream);
                    Console.WriteLine("Language detected:{0}", languageDetected);
                    Console.WriteLine("Press any key to continue...");
                    Console.ReadKey(true);
                }
            }

            finally
            {
                if (response != null)
                {
                    response.Close();
                    response = null;
                }
            }
        }

        #endregion
    }
}