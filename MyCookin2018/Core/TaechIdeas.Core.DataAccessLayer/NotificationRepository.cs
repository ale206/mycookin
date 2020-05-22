using System.Collections.Generic;
using System.Data;
using Dapper;
using Microsoft.Extensions.Configuration;
using TaechIdeas.Core.Core;
using TaechIdeas.Core.Core.UserBoard;
using TaechIdeas.Core.Core.UserBoard.Dto;

namespace TaechIdeas.Core.DataAccessLayer
{
    public class NotificationRepository : INotificationRepository
    {
        private readonly string _connectionString;
        private readonly IDbConnectionFactory _dbConnectionFactory;

        public NotificationRepository(IDbConnectionFactory dbConnectionFactory, IConfiguration configuration)
        {
            _dbConnectionFactory = dbConnectionFactory;
            _connectionString = configuration.GetConnectionString("DBUsersBoardConnectionString");
        }

        public InsertNotificationOut InsertNotification(InsertNotificationIn insertNotificationIn)
        {
            InsertNotificationOut result;

            using (var connection = _dbConnectionFactory.GetConnection(_connectionString))
            {
                result = connection.ExecuteScalar<InsertNotificationOut>("USP_NotificationInsert",
                    new
                    {
                        insertNotificationIn.IDUser,
                        insertNotificationIn.IDUserActionType,
                        URLNotification = insertNotificationIn.UrlNotification,
                        insertNotificationIn.IDRelatedObject,
                        insertNotificationIn.NotificationImage,
                        insertNotificationIn.UserNotification,
                        insertNotificationIn.CreatedOn,
                        insertNotificationIn.IDUserOwnerRelatedObject
                    },
                    commandType: CommandType.StoredProcedure
                );
            }

            return result;
        }

        public IEnumerable<GetNotificationsForUserOut> GetNotificationsForUser(GetNotificationsForUserIn getNotificationsForUserIn)
        {
            IEnumerable<GetNotificationsForUserOut> result;

            using (var connection = _dbConnectionFactory.GetConnection(_connectionString))
            {
                result = connection.ExecuteScalar<IEnumerable<GetNotificationsForUserOut>>("USP_NotificationsGet",
                    new
                    {
                        IDUserOwnerRelatedObject = getNotificationsForUserIn.IdUserOwnerRelatedObject,
                        IDUserActionType = getNotificationsForUserIn.IdUserActionType,
                        getNotificationsForUserIn.NotificationsRead,
                        getNotificationsForUserIn.AllNotification,
                        getNotificationsForUserIn.MaxNotificationsNumber
                    },
                    commandType: CommandType.StoredProcedure
                );
            }

            return result;
        }

        public MarkNotificationsAsViewedOut MarkNotificationsAsViewed(MarkNotificationsAsViewedIn markNotificationsAsViewedIn)
        {
            MarkNotificationsAsViewedOut result;

            using (var connection = _dbConnectionFactory.GetConnection(_connectionString))
            {
                result = connection.ExecuteScalar<MarkNotificationsAsViewedOut>("USP_NotificationsSetAsViewed",
                    new
                    {
                        IDUserOwnerRelatedObject = markNotificationsAsViewedIn.UserIdOwnerRelatedObject
                    },
                    commandType: CommandType.StoredProcedure
                );
            }

            return result;
        }

        public MarkNotificationsAsNotifiedOut MarkNotificationsAsNotified(MarkNotificationsAsNotifiedIn markNotificationsAsNotifiedIn)
        {
            MarkNotificationsAsNotifiedOut result;

            using (var connection = _dbConnectionFactory.GetConnection(_connectionString))
            {
                result = connection.ExecuteScalar<MarkNotificationsAsNotifiedOut>("USP_NotificationsSetAsNotified",
                    new
                    {
                        IDUserNotification = markNotificationsAsNotifiedIn.UserNotificationId
                    },
                    commandType: CommandType.StoredProcedure
                );
            }

            return result;
        }
    }
}