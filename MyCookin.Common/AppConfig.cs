using System;
using System.Collections.Generic;
using System.Configuration;
using System.Text;

namespace MyCookin.Common
{
    public class AppConfig
    {
        public static string GetValue(string AppKey, AppDomain MyAppDomain)
        {
            ExeConfigurationFileMap fileMap = new ExeConfigurationFileMap();
            fileMap.ExeConfigFilename = MyAppDomain.BaseDirectory + "Web.config";
            Configuration config = ConfigurationManager.OpenMappedExeConfiguration(fileMap, ConfigurationUserLevel.None);
            AppSettingsSection section = config.AppSettings;
            string appsettings = section.Settings[AppKey].Value.ToString();
            return appsettings;
        }
    }
}
