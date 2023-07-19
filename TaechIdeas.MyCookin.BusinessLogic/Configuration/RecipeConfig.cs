using System;
using TaechIdeas.Core.Core.Common;
using TaechIdeas.MyCookin.Core.Configuration;

namespace TaechIdeas.MyCookin.BusinessLogic.Configuration
{
    public class RecipeConfig : IRecipeConfig
    {
        private readonly IAppConfigManager _appConfigManager;
        private readonly IMyConvertManager _myConvertManager;

        public RecipeConfig(IAppConfigManager appConfigManager, IMyConvertManager myConvertManager)
        {
            _appConfigManager = appConfigManager;
            _myConvertManager = myConvertManager;
        }

        public string GoogleSuggestUri => _appConfigManager.GetValue("GoogleSuggestURI", AppDomain.CurrentDomain);
        public bool UseGoogleSuggestionsForSearchRecipes => _myConvertManager.ToBoolean(_appConfigManager.GetValue("TurnOnUseGoogleSuggestions", AppDomain.CurrentDomain), false);
        public bool UseGoogleSuggestionsForEmptyFridge => _myConvertManager.ToBoolean(_appConfigManager.GetValue("UseGoogleSuggestionsForFreeFridge", AppDomain.CurrentDomain), false);
        public string DateTimeFormatCSharp => _appConfigManager.GetValue("DateTimeFormatCSharp", AppDomain.CurrentDomain);
        public int QuickRecipeThreshold => _myConvertManager.ToInt32(_appConfigManager.GetValue("QuickRecipeThreshold", AppDomain.CurrentDomain), 10000);
        public int LightRecipeThreshold => _myConvertManager.ToInt32(_appConfigManager.GetValue("LightRecipeThreshold", AppDomain.CurrentDomain), 10000);
        public int TopRecipesToShow => _myConvertManager.ToInt32(_appConfigManager.GetValue("TopRecipesToShow", AppDomain.CurrentDomain), 9);
    }
}