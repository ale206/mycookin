using System;
using TaechIdeas.Core.Core.Common;
using TaechIdeas.Core.Core.Configuration;

namespace TaechIdeas.Core.BusinessLogic.Configuration
{
    public class NetworkConfig : INetworkConfig
    {
        private readonly IAppConfigManager _appConfigManager;
        private readonly IMyConvertManager _myConvertManager;

        public NetworkConfig(IAppConfigManager appConfigManager, IMyConvertManager myConvertManager)
        {
            _appConfigManager = appConfigManager;
            _myConvertManager = myConvertManager;
        }

        public string ClientSmtp => _appConfigManager.GetValue("ClientSmtp", AppDomain.CurrentDomain);
        public int ClientSmtpPort => _myConvertManager.ToInt32(_appConfigManager.GetValue("ClientSmtpPort", AppDomain.CurrentDomain), 80);
        public string SmtpServerUsn => _appConfigManager.GetValue("SmtpServerUsn", AppDomain.CurrentDomain);
        public string SmtpServerPsw => _appConfigManager.GetValue("SmtpServerPsw", AppDomain.CurrentDomain);
        public string WebUrl => _appConfigManager.GetValue("WebUrl", AppDomain.CurrentDomain);
        public bool EnableSsl => _myConvertManager.ToBoolean(_appConfigManager.GetValue("EnableSSL", AppDomain.CurrentDomain), true);
        public string CookieName => _appConfigManager.GetValue("CookieName", AppDomain.CurrentDomain);
        public string RoutingUser => _appConfigManager.GetValue("RoutingUser", AppDomain.CurrentDomain);

        public string RoutingRecipeEn => _appConfigManager.GetValue("RoutingRecipe1", AppDomain.CurrentDomain);
        public string RoutingRecipeIt => _appConfigManager.GetValue("RoutingRecipe2", AppDomain.CurrentDomain);
        public string RoutingRecipeEs => _appConfigManager.GetValue("RoutingRecipe3", AppDomain.CurrentDomain);
        public string RoutingIngredientEn => _appConfigManager.GetValue("RoutingIngredient1", AppDomain.CurrentDomain);
        public string RoutingIngredientIt => _appConfigManager.GetValue("RoutingIngredient2", AppDomain.CurrentDomain);
        public string RoutingIngredientEs => _appConfigManager.GetValue("RoutingIngredient3", AppDomain.CurrentDomain);
        public string CdnBasePath => _appConfigManager.GetValue("CDNBasePath", AppDomain.CurrentDomain);
        public string HomePage => _appConfigManager.GetValue("HomePage", AppDomain.CurrentDomain);
        public string LoginPage => _appConfigManager.GetValue("LoginPage", AppDomain.CurrentDomain);
        public string LogoutPage => _appConfigManager.GetValue("LogoutPage", AppDomain.CurrentDomain);
        public string ErrorPage => _appConfigManager.GetValue("ErrorPage", AppDomain.CurrentDomain);
        public string PrincipalUserBoard => _appConfigManager.GetValue("PrincipalUserBoard", AppDomain.CurrentDomain);
        public int EmailDispatcherLifetime => _myConvertManager.ToInt32(_appConfigManager.GetValue("EmailDispatcherLifetime", AppDomain.CurrentDomain), 300000000); //30 sec
        public int MaxEmailsToSend => _myConvertManager.ToInt32(_appConfigManager.GetValue("MaxEmailsToSend", AppDomain.CurrentDomain), 1);
    }
}