using System;
using System.Collections.Generic;
using AutoMapper;
using TaechIdeas.Core.Core.Token;
using TaechIdeas.Core.Core.UserBoard;
using TaechIdeas.Core.Core.UserBoard.Dto;

namespace TaechIdeas.Core.BusinessLogic.UserBoard
{
    public class NotificationManager : INotificationManager
    {
        private readonly ITokenManager _tokenManager;
        private readonly INotificationRepository _notificationRepository;
        private readonly IMapper _mapper;

        public NotificationManager(ITokenManager tokenManager, INotificationRepository notificationRepository, IMapper mapper)
        {
            _tokenManager = tokenManager;
            _notificationRepository = notificationRepository;
            _mapper = mapper;
        }

        #region InsertNotification

        public InsertNotificationOutput InsertNotification(InsertNotificationInput insertNotificationInput)
        {
            //Check for Valid Token
            var checkTokenOutput = _tokenManager.CheckToken(insertNotificationInput.CheckTokenInput);

            if (!checkTokenOutput.IsTokenValid)
            {
                throw new Exception("Token not valid for the user.");
            }

            return _mapper.Map<InsertNotificationOutput>(_notificationRepository.InsertNotification(_mapper.Map<InsertNotificationIn>(insertNotificationInput)));
        }

        #endregion

        #region GetNotificationsForUser

        public IEnumerable<GetNotificationsForUserOutput> GetNotificationsForUser(GetNotificationsForUserInput getNotificationsForUserInput)
        {
            //Check for Valid Token
            var checkTokenOutput = _tokenManager.CheckToken(getNotificationsForUserInput.CheckTokenInput);

            if (!checkTokenOutput.IsTokenValid)
            {
                throw new Exception("Token not valid for the user.");
            }

            return _mapper.Map<IEnumerable<GetNotificationsForUserOutput>>(_notificationRepository.GetNotificationsForUser(_mapper.Map<GetNotificationsForUserIn>(getNotificationsForUserInput)));
        }

        #endregion

        #region MarkNotificationsAsViewed

        public MarkNotificationsAsViewedOutput MarkNotificationsAsViewed(MarkNotificationsAsViewedInput markNotificationsAsViewedInput)
        {
            //Check for Valid Token
            var checkTokenOutput = _tokenManager.CheckToken(markNotificationsAsViewedInput.CheckTokenInput);

            if (!checkTokenOutput.IsTokenValid)
            {
                throw new Exception("Token not valid for the user.");
            }

            return _mapper.Map<MarkNotificationsAsViewedOutput>(_notificationRepository.MarkNotificationsAsViewed(_mapper.Map<MarkNotificationsAsViewedIn>(markNotificationsAsViewedInput)));
        }

        #endregion

        #region MarkNotificationsAsNotified

        public MarkNotificationsAsNotifiedOutput MarkNotificationsAsNotified(MarkNotificationsAsNotifiedInput markNotificationsAsNotifiedInput)
        {
            //Check for Valid Token
            var checkTokenOutput = _tokenManager.CheckToken(markNotificationsAsNotifiedInput.CheckTokenInput);

            if (!checkTokenOutput.IsTokenValid)
            {
                throw new Exception("Token not valid for the user.");
            }

            return _mapper.Map<MarkNotificationsAsNotifiedOutput>(_notificationRepository.MarkNotificationsAsNotified(_mapper.Map<MarkNotificationsAsNotifiedIn>(markNotificationsAsNotifiedInput)));
        }

        #endregion
    }
}