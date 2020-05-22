using System;
using TaechIdeas.Core.Core.Common;
using TaechIdeas.Core.Core.Configuration;

namespace TaechIdeas.Core.BusinessLogic.Configuration
{
    public class TokenConfig : ITokenConfig
    {
        private readonly IAppConfigManager _appConfigManager;
        private readonly IMyConvertManager _myConvertManager;

        public TokenConfig(IAppConfigManager appConfigManager, IMyConvertManager myConvertManager)
        {
            _appConfigManager = appConfigManager;
            _myConvertManager = myConvertManager;
        }

        public int TokenExpireMinutes => _myConvertManager.ToInt32(_appConfigManager.GetValue("TokenExpireMinutes", AppDomain.CurrentDomain), 30);
        public int TokenRenewMinutes => _myConvertManager.ToInt32(_appConfigManager.GetValue("TokenRenewMinutes", AppDomain.CurrentDomain), 30);
    }
}