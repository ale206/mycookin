using System;

namespace TaechIdeas.Core.Core.Configuration
{
    public interface IAppConfigManager
    {
        string GetValue(string appKey, AppDomain myAppDomain);
    }
}