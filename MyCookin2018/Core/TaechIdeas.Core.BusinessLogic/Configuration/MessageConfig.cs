using System;
using TaechIdeas.Core.Core.Configuration;

namespace TaechIdeas.Core.BusinessLogic.Configuration
{
    public class MessageConfig : IMessageConfig
    {
        private readonly IAppConfigManager _appConfigManager;

        public MessageConfig(IAppConfigManager appConfigManager)
        {
            _appConfigManager = appConfigManager;
        }

        public string GeneralError1 => _appConfigManager.GetValue("GeneralError1", AppDomain.CurrentDomain);
        public string GeneralError2 => _appConfigManager.GetValue("GeneralError2", AppDomain.CurrentDomain);
        public string GeneralError3 => _appConfigManager.GetValue("GeneralError3", AppDomain.CurrentDomain);
        public string GeneralError4 => _appConfigManager.GetValue("GeneralError4", AppDomain.CurrentDomain);
        public string GeneralError5 => _appConfigManager.GetValue("GeneralError5", AppDomain.CurrentDomain);
    }
}