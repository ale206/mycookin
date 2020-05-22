using System;

namespace TaechIdeas.MyCookin.Core.Configuration
{
    public interface IAppConfigManager
    {
        string GetValue(string appKey, AppDomain myAppDomain);
    }
}