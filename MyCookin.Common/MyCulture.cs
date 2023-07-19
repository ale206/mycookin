using System;
using System.Collections.Generic;
using System.Text;
using System.Web;
using MyCookin.DAL.LanguageAndCulture.ds_LanguageAndCultureTableAdapters;
using System.Data;

namespace MyCookin.Common
{
    public class MyCulture
    {
        #region PrivateFields
        private string _cultureString;
        private string _languageCode;
        private int _IDLanguage;
        #endregion

        #region PublicProperties

        public string CultureString
        {
            get { return _cultureString; }
            set { _cultureString = value; }
        }

        public string LanguageCode
        {
            get { return _languageCode; }
            set { _languageCode = value; }
        }

        public int IDLanguage
        {
            get { return _IDLanguage; }
            set { _IDLanguage = value; }
        }


        #endregion 

        #region Constructors
        /// <summary>
        /// Get Culture by Language ID
        /// </summary>
        /// <param name="LanguageID">ID of Language</param>
        public MyCulture(int IDLanguage)
        {
            _IDLanguage = IDLanguage;
        }

        /// <summary>
        /// Get MyCooking Culture ID from Culture Name
        /// </summary>
        /// <param name="CultureName">The Culture Name (Ex.: en-en, it-it, ...)</param>
        public MyCulture(string CultureName)
        {
            _cultureString = CultureName;

            /* HttpContext.Current.Request.UserLanguages returns an array of elements such as
            * en, en-us, en-gb, etc...
              We need only principal language then if a dash is present we get only the left side content  
            */
            try
            {
                if (_cultureString.Length > 1)
                {
                    LanguagesDAL CultureID = new LanguagesDAL();

                    _IDLanguage = Convert.ToInt32(CultureID.GetIDLanguageByCulture(_cultureString));
                }
                else
                {
                    _IDLanguage = 1;
                }
            }
            catch
            {
                _IDLanguage = 1;
            }
        }
        #endregion

        #region Methods
        /// <summary> 
        /// Get Current LanguageCode by ID (it, en, fr, ...)
        /// </summary>
        /// <returns>Language Code (it, en, fr, ...)</returns>
        public string GetCurrentLanguageCodeByID()
        {
            try
            {
                DataTable dtLanguages = new DataTable();
                LanguagesDAL dalLanguages = new LanguagesDAL();

                dtLanguages = dalLanguages.GetCurrentLanguageCodeByID(_IDLanguage);

                _languageCode = (dtLanguages.Rows[0].Field<string>("LanguageCode")).Substring(0, 2);
            }
            catch
            {
                _languageCode = "en";
            }

            return _languageCode;
        }

        /// <summary> 
        /// Get Current LanguageCode by ID (it, en, fr, ...)
        /// </summary>
        /// <returns>Language Code (it, en, fr, ...)</returns>
        public string GetCompleteLanguageCodeByIDLang()
        {
            try
            {
                DataTable dtLanguages = new DataTable();
                LanguagesDAL dalLanguages = new LanguagesDAL();

                dtLanguages = dalLanguages.GetCurrentLanguageCodeByID(_IDLanguage);

                _languageCode = dtLanguages.Rows[0].Field<string>("LanguageCode");
            }
            catch
            {
                _languageCode = "en-GB";
            }

            return _languageCode;
        }

        /// <summary>
        /// Get Current Browser Culture (Ex.: en-en, it-it, ...)
        /// </summary>
        /// <returns>Returns principal Browser Culture</returns>
        public static string GetBrowserCurrentCulture()
        {
            //Get Browser Language
            string[] languages = HttpContext.Current.Request.UserLanguages;
            try
            {
                return languages[0].ToString();
            }
            catch
            {
                return "en-US";
            }
        }

        public static int GetIDLanguageFromLangShortCode(string LanguageCode)
        {
            int _return = 1;
            if (String.IsNullOrEmpty(LanguageCode))
            {
                LanguageCode = MyCulture.GetBrowserCurrentCulture();
            }
            switch (LanguageCode.ToLower())
            {
                case "en":
                    _return = 1;
                    break;
                case "it":
                    _return = 2;
                    break;
                case "es":
                    _return = 3;
                    break;
                case "fr":
                    _return = 4;
                    break;
                case "de":
                    _return = 5;
                    break;
                default:
                    _return = 1;
                    break;
            }
            
            return _return;
        }

        public static string GetLangShortCodeFromIDLanguage(int IDLanguage)
        {
            string _return = "en";
            switch (IDLanguage)
            {
                case 1:
                    _return = "en";
                    break;
                case 2:
                    _return = "it";
                    break;
                case 3:
                    _return = "es";
                    break;
                case 4:
                    _return = "fr";
                    break;
                case 5:
                    _return = "de";
                    break;
                default:
                    _return = "en";
                    break;
            }
            return _return;
        }
        #endregion

    }
}
