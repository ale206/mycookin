using System;
using TaechIdeas.Core.Core.Common;
using TaechIdeas.Core.Core.Configuration;

namespace TaechIdeas.Core.BusinessLogic.Configuration
{
    public class UserBoardConfig : IUserBoardConfig
    {
        private readonly IAppConfigManager _appConfigManager;
        private readonly IMyConvertManager _myConvertManager;

        public UserBoardConfig(IAppConfigManager appConfigManager, IMyConvertManager myConvertManager)
        {
            _appConfigManager = appConfigManager;
            _myConvertManager = myConvertManager;
        }

        public int? NotificationsRead => _myConvertManager.ToInt32(_appConfigManager.GetValue("NotificationsRead", AppDomain.CurrentDomain), 30);
        public int MaxNotificationsNumber => _myConvertManager.ToInt32(_appConfigManager.GetValue("MaxNotificationsNumber", AppDomain.CurrentDomain), 30);
        public int UserLikesResultsNumber => _myConvertManager.ToInt32(_appConfigManager.GetValue("UserLikesResultsNumber", AppDomain.CurrentDomain), 5);
        public string OtherIdActionsToShowOnUserBoard => _appConfigManager.GetValue("OtherIDActionsToShowOnUserBoard", AppDomain.CurrentDomain);
    }
}