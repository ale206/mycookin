using System;
using TaechIdeas.MyCookin.Core.Configuration;

namespace TaechIdeas.MyCookin.BusinessLogic.Configuration
{
    public class AppConfigManager : IAppConfigManager
    {
        public string GetValue(string appKey, AppDomain myAppDomain)
        {
            throw new NotImplementedException();

            //TODO: USE NET CORE

            //Try to get from Web.config
            //var fileMap = new ExeConfigurationFileMap {ExeConfigFilename = myAppDomain.BaseDirectory + "Web.config"};
            //var config = ConfigurationManager.OpenMappedExeConfiguration(fileMap, ConfigurationUserLevel.None);
            //var section = config.AppSettings;

            ////If we are not using Web.config, try to get from App.Config
            //if (section.Settings.Count == 0)
            //{
            //    var appSettings = ConfigurationManager.AppSettings;

            //    if (appSettings.Count == 0)
            //        throw new Exception("No keys in app.config");

            //    return appSettings[appKey];
            //}

            //if (section.Settings.Count == 0)
            //    throw new Exception("No keys in Web.config");

            //var value = section.Settings[appKey].Value;

            //return value;
        }
    }
}