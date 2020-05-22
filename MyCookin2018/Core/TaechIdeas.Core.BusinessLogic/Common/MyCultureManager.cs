using System;
using TaechIdeas.Core.Core.Common;
using TaechIdeas.Core.Core.User;
using TaechIdeas.Core.Core.User.Dto;

namespace TaechIdeas.Core.BusinessLogic.Common
{
    public class MyCultureManager : IMyCultureManager
    {
        private readonly IMyConvertManager _myConvertManager;
        private readonly IUserRepository _userRepository;

        public MyCultureManager(IMyConvertManager myConvertManager, IUserRepository userRepository)
        {
            _myConvertManager = myConvertManager;
            _userRepository = userRepository;
        }

        #region LanguageIdByLanguageCode

        /// <summary>
        ///     Return language Id
        /// </summary>
        /// <param name="languageCode">en, it, us, ...</param>
        /// <returns></returns>
        public int LanguageIdByLanguageCode(string languageCode)
        {
            /* HttpContext.Current.Request.UserLanguages returns an array of elements such as
            * en, en-us, en-gb, etc...
              We need only principal language then if a dash is present we get only the left side content  
            */
            try
            {
                return languageCode.Length > 1
                    ? _myConvertManager.ToInt32(_userRepository.LanguageIdByLanguageCode(new LanguageIdByLanguageCodeIn {LanguageCode = languageCode}).ResultExecutionCode, 1)
                    : 1;
            }
            catch (Exception ex)
            {
                //TODO: IMPROVE!
                return 1;
            }
        }

        #endregion

        #region GetCurrentLanguageCodeByID

        /// <summary>
        ///     Get Current LanguageCode by ID (it, en, fr, ...)
        /// </summary>
        /// <returns>Language Code (it, en, fr, ...)</returns>
        public string LanguageCodeByLanguageId(int languageId)
        {
            try
            {
                var languageCodeByLanguageIdOut = _userRepository.LanguageCodeByLanguageId(new LanguageCodeByLanguageIdIn {LanguageId = languageId});

                return !string.IsNullOrEmpty(languageCodeByLanguageIdOut.LanguageCode) ? languageCodeByLanguageIdOut.LanguageCode.Substring(0, 2) : "en";
            }
            catch
            {
                return "en";
            }
        }

        #endregion

        #region GetBrowserCurrentCulture

        /// <summary>
        ///     Get Current Browser Culture (Ex.: en-en, it-it, ...)
        /// </summary>
        /// <returns>Returns principal Browser Culture</returns>
        public string GetBrowserCurrentCulture()
        {
            throw new NotImplementedException();

            //Get Browser Language
            //var userLanguages = HttpContext.Current.Request.UserLanguages;

            //if (userLanguages == null)
            //    return "en-GB";

            //return userLanguages.Any() ? userLanguages.First() : "en-GB";
        }

        #endregion

        #region GetIDLanguageFromLangShortCode

        public int GetIdLanguageFromLangShortCode(string languageCode)
        {
            int _return;
            if (string.IsNullOrEmpty(languageCode))
            {
                languageCode = GetBrowserCurrentCulture();
            }

            switch (languageCode.ToLower())
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

        #endregion

        #region GetLangShortCodeFromIDLanguage

        public string GetLangShortCodeFromIdLanguage(int idLanguage)
        {
            string _return;
            switch (idLanguage)
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

        #region GetIdLanguageFromBrowser

        public int GetIdLanguageFromBrowser()
        {
            var browserLanguage = GetBrowserCurrentCulture();

            return GetIdLanguageFromLangShortCode(browserLanguage);
        }

        #endregion
    }
}