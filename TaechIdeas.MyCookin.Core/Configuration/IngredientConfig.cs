using System;
using TaechIdeas.Core.Core.Common;

namespace TaechIdeas.MyCookin.Core.Configuration
{
    public class IngredientConfig : IIngredientConfig
    {
        private readonly IAppConfigManager _appConfigManager;
        private readonly IMyConvertManager _myConvertManager;

        public IngredientConfig(IAppConfigManager appConfigManager, IMyConvertManager myConvertManager)
        {
            _appConfigManager = appConfigManager;
            _myConvertManager = myConvertManager;
        }

        public int? RootIngredientCategoryId => _myConvertManager.ToInt32(_appConfigManager.GetValue("RootIngredientCategoryID", AppDomain.CurrentDomain), 1);
    }
}