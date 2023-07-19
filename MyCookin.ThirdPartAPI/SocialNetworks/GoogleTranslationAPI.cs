using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Net;
using System.IO;
using System.Web.Script.Serialization;
using MyCookin.Common;

namespace MyCookin.ThirdPartAPI.SocialNetworks
{
    public class GoogleTranslationAPI
    {
        #region Privatefields
        private string _LanguageSource;
        private string _LanguageTarget;
        private string _OriginalSentence;
        private string _TranslatedSentence;
        private bool _Result;
        private string _ErrorMessage;
        #endregion

        #region PublicFields
        public string LanguageSource
        {
            get { return _LanguageSource; }
            set { _LanguageSource = value; }
        }
        public string LanguageTarget
        {
            get { return _LanguageTarget; }
            set { _LanguageTarget = value; }
        }
        public string OriginalSentence
        {
            get { return _OriginalSentence; }
            set { _OriginalSentence = value; }
        }
        public string TranslatedSentence
        {
            get { return _TranslatedSentence; }
            set { _TranslatedSentence = value; }
        }
        public bool Result
        {
            get { return _Result; }
            set { _Result = value; }
        }
        public string ErrorMessage
        {
            get { return _ErrorMessage; }
            set { _ErrorMessage = value; }
        }
        #endregion

        #region Constructors
        public GoogleTranslationAPI()
        {
        }

        public GoogleTranslationAPI(string LanguageSource, string LanguageTarget)
        {
            _LanguageSource = LanguageSource;
            _LanguageTarget = LanguageTarget;
        }
        #endregion

        #region Methods
        public GoogleTranslationAPI TranslateSentence(string OriginalSentence)
        {
            #region Google Json Reply Example
            //Google Json Reply Example
            //************************************************************************************
            //string json = "{ " +
            //                   " \"data\": { " +
            //                   "    \"translations\": [ " +
            //                   "                        {" +
            //                   "                            \"translatedText\": \"hello\" " +
            //                   "                        } " +
            //                   "                      ]" +
            //                   "            }" +
            //                   "}";
            //************************************************************************************
            #endregion

            string Key = AppConfig.GetValue("google_API_Key", AppDomain.CurrentDomain);

            string url = "https://www.googleapis.com/language/translate/v2?key=" + Key + "&q=" + OriginalSentence + "&source=" + _LanguageSource + "&target=" + _LanguageTarget;

            GoogleTranslationAPI TranslationObject = new GoogleTranslationAPI();

            try
            {
                HttpWebRequest request = (HttpWebRequest)WebRequest.Create(url);

                // Set some reasonable limits on resources used by this request
                request.MaximumAutomaticRedirections = 4;
                request.MaximumResponseHeadersLength = 4;
                // Set credentials to use for this request.
                request.Credentials = CredentialCache.DefaultCredentials;
                HttpWebResponse response = (HttpWebResponse)request.GetResponse();

                long ContentLength = response.ContentLength;
                string ContentType = response.ContentType;

                // Get the stream associated with the response.
                Stream receiveStream = response.GetResponseStream();

                // Pipes the stream to a higher level stream reader with the required encoding format. 
                StreamReader readStream = new StreamReader(receiveStream, Encoding.UTF8);

                string responseFromServer = readStream.ReadToEnd();

                response.Close();
                readStream.Close();

                //Get value
                //******************************************************
                JavaScriptSerializer jss = new JavaScriptSerializer();
                jss.RegisterConverters(new JavaScriptConverter[] { new DynamicJsonConverter() });

                dynamic TranslationEntry = jss.Deserialize(responseFromServer, typeof(object)) as dynamic;

                //POPULATE OBJECT
                //*************************************************
                TranslationObject.LanguageSource = _LanguageSource;
                TranslationObject.LanguageTarget = _LanguageTarget;
                TranslationObject.OriginalSentence = OriginalSentence;
                TranslationObject.TranslatedSentence = TranslationEntry.data.translations[0]["translatedText"];
                TranslationObject.Result = true;
                TranslationObject.ErrorMessage = "";
                //*************************************************

                //foreach (var text in TranslationEntry.data.translations)
                //{
                //    TranslatedText += text["translatedText"];
                //}
                //******************************************************

                #region CompleteJsonDeserializationExample
                //http: //www.drowningintechnicaldebt.com/ShawnWeisfeld/archive/2010/08/22/using-c-4.0-and-dynamic-to-parse-json.aspx

                //string json = "{ " +
                //                   " \"glossary\": { " +
                //                   "     \"title\": \"example glossary\", " +
                //                   "     \"GlossDiv\": { " +
                //                   "         \"title\": \"S\", " +
                //                   "         \"GlossList\": { " +
                //                   "             \"GlossEntry\": { " +
                //                   "                 \"ID\": \"SGML\", " +
                //                   "                 \"SortAs\": \"SGML\", " +
                //                   "                 \"GlossTerm\": \"Standard Generalized Markup Language\", " +
                //                   "                 \"Acronym\": \"SGML\", " +
                //                   "                 \"Abbrev\": \"ISO 8879:1986\", " +
                //                   "                 \"GlossDef\": { " +
                //                   "                     \"para\": \"A meta-markup language, used to create markup languages such as DocBook.\", " +
                //                   "                     \"GlossSeeAlso\": [\"GML\", \"XML\"] " +
                //                   "                 }, " +
                //                   "                 \"GlossSee\": \"markup\" " +
                //                   "             } " +
                //                   "         } " +
                //                   "     } " +
                //                   " } " +
                //                   "}";

                //dynamic glossaryEntry = jss.Deserialize(json1, typeof(object)) as dynamic;

                //string Title = "glossaryEntry.glossary.title: " + glossaryEntry.glossary.title;
                //string Title2 = "glossaryEntry.glossary.GlossDiv.title: " + glossaryEntry.glossary.GlossDiv.title;
                //string ID = "glossaryEntry.glossary.GlossDiv.GlossList.GlossEntry.ID: " + glossaryEntry.glossary.GlossDiv.GlossList.GlossEntry.ID;
                //string Para = "glossaryEntry.glossary.GlossDiv.GlossList.GlossEntry.GlossDef.para: " + glossaryEntry.glossary.GlossDiv.GlossList.GlossEntry.GlossDef.para;

                //string GlossSeeAlso = "";
                //foreach (var also in glossaryEntry.glossary.GlossDiv.GlossList.GlossEntry.GlossDef.GlossSeeAlso)
                //{
                //    GlossSeeAlso += "glossaryEntry.glossary.GlossDiv.GlossList.GlossEntry.GlossDef.GlossSeeAlso: " + also;
                //}
                #endregion
            }
            catch (Exception ex)
            {
                TranslationObject.LanguageSource = _LanguageSource;
                TranslationObject.LanguageTarget = _LanguageTarget;
                TranslationObject.OriginalSentence = OriginalSentence;
                TranslationObject.TranslatedSentence = "";
                TranslationObject.Result = false;
                TranslationObject.ErrorMessage = ex.Message;
            }

            return TranslationObject;
        }
        #endregion
    }
}
