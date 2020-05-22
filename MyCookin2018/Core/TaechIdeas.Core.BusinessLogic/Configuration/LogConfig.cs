using System;
using TaechIdeas.Core.Core.Common;
using TaechIdeas.Core.Core.Configuration;

namespace TaechIdeas.Core.BusinessLogic.Configuration
{
    public class LogConfig : ILogConfig
    {
        private readonly IAppConfigManager _appConfigManager;
        private readonly IMyConvertManager _myConvertManager;

        public LogConfig(IAppConfigManager appConfigManager, IMyConvertManager myConvertManager)
        {
            _appConfigManager = appConfigManager;
            _myConvertManager = myConvertManager;
        }

        public int IdLanguageForLog => _myConvertManager.ToInt32(_appConfigManager.GetValue("IDLanguageForLog", AppDomain.CurrentDomain), 30);
        public int DebugLevel => _myConvertManager.ToInt32(_appConfigManager.GetValue("DebugLevel", AppDomain.CurrentDomain), 1);
        public int FileLevel => _myConvertManager.ToInt32(_appConfigManager.GetValue("FileLevel", AppDomain.CurrentDomain), 1);
        public bool SendEmailForLog => _myConvertManager.ToBoolean(_appConfigManager.GetValue("SendEmailForLog", AppDomain.CurrentDomain), true);
        public string LogPath => _appConfigManager.GetValue("LogPath", AppDomain.CurrentDomain);
        public string LogBaseFileName => _appConfigManager.GetValue("LogBaseFileName", AppDomain.CurrentDomain);
        public string LogFileNameDateFormat => _appConfigManager.GetValue("LogFileNameDateFormat", AppDomain.CurrentDomain);
        public int SendEmailIntervalTime => _myConvertManager.ToInt32(_appConfigManager.GetValue("SendEmailIntervalTime", AppDomain.CurrentDomain), 15);
        public string EmailFromLog => _appConfigManager.GetValue("EmailFromLog", AppDomain.CurrentDomain);
        public string EmailToLog => _appConfigManager.GetValue("EmailToLog", AppDomain.CurrentDomain);
        public string LogDelimiter => _appConfigManager.GetValue("LogDelimiter", AppDomain.CurrentDomain);
        public string EmailSubjectForLog => _appConfigManager.GetValue("EmailSubjectForLog", AppDomain.CurrentDomain);
    }
}