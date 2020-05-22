using System;
using TaechIdeas.Core.Core.Common;
using TaechIdeas.Core.Core.Configuration;

namespace TaechIdeas.Core.BusinessLogic.Configuration
{
    public class UserConfig : IUserConfig
    {
        private readonly IAppConfigManager _appConfigManager;
        private readonly IMyConvertManager _myConvertManager;

        public UserConfig(IAppConfigManager appConfigManager, IMyConvertManager myConvertManager)
        {
            _appConfigManager = appConfigManager;
            _myConvertManager = myConvertManager;
        }

        public string CookieName => _appConfigManager.GetValue("CookieName", AppDomain.CurrentDomain);
        public int? UserFindResultsNumber => _myConvertManager.ToInt32(_appConfigManager.GetValue("UserFindResultsNumber", AppDomain.CurrentDomain), 5);
        public string UserProfilePropertiesId => _appConfigManager.GetValue("UserProfilePropertiesID", AppDomain.CurrentDomain);
        public string EmailFromProfileUser => _appConfigManager.GetValue("EmailFromProfileUser", AppDomain.CurrentDomain);
        public int? NumberOfPeopleYouMayKnow => _myConvertManager.ToInt32(_appConfigManager.GetValue("NumberOfPeopleYouMayKnow", AppDomain.CurrentDomain), 5);
        public int DaysOfLastRetrieveSocialFriends => _myConvertManager.ToInt32(_appConfigManager.GetValue("DaysOfLastRetrieveSocialFriends", AppDomain.CurrentDomain), 7);
        public int MinPasswordLength => _myConvertManager.ToInt32(_appConfigManager.GetValue("MinPasswordLength", AppDomain.CurrentDomain), 5);
        public int YearsAfterAccountExpire => _myConvertManager.ToInt32(_appConfigManager.GetValue("YearsAfterAccountExpire", AppDomain.CurrentDomain), 30);
    }
}