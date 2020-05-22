using System;
using TaechIdeas.Core.Core.Common;
using TaechIdeas.Core.Core.Configuration;

namespace TaechIdeas.Core.BusinessLogic.Configuration
{
    public class ThirdPartyConfig : IThirdPartyConfig
    {
        private readonly IAppConfigManager _appConfigManager;
        private readonly IMyConvertManager _myConvertManager;

        public ThirdPartyConfig(IAppConfigManager appConfigManager, IMyConvertManager myConvertManager)
        {
            _appConfigManager = appConfigManager;
            _myConvertManager = myConvertManager;
        }

        public string MicrosoftClientId => _appConfigManager.GetValue("microsoft_client_ID", AppDomain.CurrentDomain);
        public string MicrosoftClientSecret => _appConfigManager.GetValue("microsoft_client_secret", AppDomain.CurrentDomain);
    }
}