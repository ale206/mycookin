using System;
using System.Collections.Generic;
using TaechIdeas.Core.Core.Notification;
using TaechIdeas.Core.Core.User;
using TaechIdeas.Core.Core.User.Dto;

namespace TaechIdeas.Core.BusinessLogic.Notification
{
    public class NotificationManager : INotificationManager
    {
        private readonly IUserRepository _userRepository;

        public NotificationManager(IUserRepository userRepository)
        {
            _userRepository = userRepository;
        }

        #region NotificationList

        public IEnumerable<NotificationListOutput> NotificationList(NotificationListInput notificationListInput)
        {
            //TODO: RESTORE...
            throw new NotImplementedException();

            //    return _mapper.Map<IEnumerable<NotificationListOutput>>(_userRepository.NotificationList(_mapper.Map<NotificationListIn>(notificationListInput)));
        }

        #endregion

        #region UpdateUserNotificationSetting

        public UpdateUserNotificationSettingOutput UpdateUserNotificationSetting(UpdateUserNotificationSettingInput notificationSettingInput)
        {
            //TODO: RESTORE...
            throw new NotImplementedException();

            //    return _mapper.Map<UpdateUserNotificationSettingOutput>(_userRepository.UpdateUserNotificationSetting(_mapper.Map<UpdateUserNotificationSettingIn>(notificationSettingInput)));
        }

        #endregion

        #region IsNotificationEnabled

        public IsNotificationEnabledOutput IsNotificationEnabled(IsNotificationEnabledInput isNotificationEnabledInput)
        {
            //TODO: RESTORE...
            throw new NotImplementedException();

            //    return _mapper.Map<IsNotificationEnabledOutput>(_userRepository.IsNotificationEnabled(_mapper.Map<IsNotificationEnabledIn>(isNotificationEnabledInput)));
        }

        #endregion
    }
}