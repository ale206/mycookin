using System.Collections.Generic;
using MyCookin.Domain.Entities;

namespace MyCookin.UnitTests.Helpers
{
    public static class SupportedLanguagesHelper
    {
        public static IEnumerable<Language> GetSupportedLanguages()
        {
            return new List<Language>
            {
                GetSupportedLanguage()
            };
        }

        private static Language GetSupportedLanguage()
        {
            return new Language
            {
                Code = "EN",
                IsEnabled = true,
                Id = 1,
                Name = "English"
            };
        }
    }
}