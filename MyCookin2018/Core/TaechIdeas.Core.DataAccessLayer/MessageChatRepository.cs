using System;
using System.Collections.Generic;
using System.Data;
using Dapper;
using Microsoft.Extensions.Configuration;
using TaechIdeas.Core.Core;
using TaechIdeas.Core.Core.Chat;
using TaechIdeas.Core.Core.Chat.Dto;

namespace TaechIdeas.Core.DataAccessLayer
{
    public class MessageChatRepository : IMessageChatRepository
    {
        private readonly string _connectionString;
        private readonly IDbConnectionFactory _dbConnectionFactory;

        public MessageChatRepository(IDbConnectionFactory dbConnectionFactory, IConfiguration configuration)
        {
            _dbConnectionFactory = dbConnectionFactory;
            _connectionString = configuration.GetConnectionString("DBMessageChatConnectionString");
        }

        public IEnumerable<NewConversationOut> NewConversation(NewConversationIn nwNewConversationIn)
        {
            IEnumerable<NewConversationOut> result;

            using (var connection = _dbConnectionFactory.GetConnection(_connectionString))
            {
                result = connection.ExecuteScalar<IEnumerable<NewConversationOut>>("USP_InsertConversation",
                    new
                    {
                        nwNewConversationIn.IDUser,
                        nwNewConversationIn.IDConversation,
                        CreatedOn = DateTime.UtcNow
                    },
                    commandType: CommandType.StoredProcedure
                );
            }

            return result;
        }

        public GetConversationIdBetweenTwoUsersOut GetConversationIdBetweenTwoUsers(GetConversationIdBetweenTwoUsersIn getConversationIdBetweenTwoUsersIn)
        {
            GetConversationIdBetweenTwoUsersOut result;

            using (var connection = _dbConnectionFactory.GetConnection(_connectionString))
            {
                result = connection.ExecuteScalar<GetConversationIdBetweenTwoUsersOut>("USP_GetConversationIdBetweenTwoUsers",
                    new
                    {
                        user1 = getConversationIdBetweenTwoUsersIn.IDUserSender,
                        user2 = getConversationIdBetweenTwoUsersIn.IDUserRecipient
                    },
                    commandType: CommandType.StoredProcedure
                );
            }

            return result;
        }

        public SetUserConversationAsArchivedOut SetUserConversationAsArchived(SetUserConversationAsArchivedIn setUserConversationAsArchivedIn)
        {
            SetUserConversationAsArchivedOut result;

            using (var connection = _dbConnectionFactory.GetConnection(_connectionString))
            {
                result = connection.ExecuteScalar<SetUserConversationAsArchivedOut>("USP_UserConversationSetAsArchived",
                    new
                    {
                        IDUserConversation = setUserConversationAsArchivedIn.IDConversation
                    },
                    commandType: CommandType.StoredProcedure
                );
            }

            return result;
        }

        public SetUserConversationAsActiveOut SetUserConversationAsActive(SetUserConversationAsActiveIn setUserConversationAsActiveIn)
        {
            SetUserConversationAsActiveOut result;

            using (var connection = _dbConnectionFactory.GetConnection(_connectionString))
            {
                result = connection.ExecuteScalar<SetUserConversationAsActiveOut>("USP_UserConversationSetAsActive",
                    new
                    {
                        setUserConversationAsActiveIn.IDConversation
                    },
                    commandType: CommandType.StoredProcedure
                );
            }

            return result;
        }

        public IEnumerable<UsersConversationsOut> UsersConversations(UsersConversationsIn usersConversationsIn)
        {
            IEnumerable<UsersConversationsOut> result;

            using (var connection = _dbConnectionFactory.GetConnection(_connectionString))
            {
                result = connection.ExecuteScalar<IEnumerable<UsersConversationsOut>>("USP_GetUsersOfAConversation",
                    new
                    {
                        usersConversationsIn.IDConversation
                    },
                    commandType: CommandType.StoredProcedure
                );
            }

            return result;
        }

        public IEnumerable<MyConversationsOut> MyConversations(MyConversationsIn myConversationsIn)
        {
            IEnumerable<MyConversationsOut> result;

            using (var connection = _dbConnectionFactory.GetConnection(_connectionString))
            {
                result = connection.ExecuteScalar<IEnumerable<MyConversationsOut>>("USP_GetMyConversations",
                    new
                    {
                        me = myConversationsIn.IDUser
                    },
                    commandType: CommandType.StoredProcedure
                );
            }

            return result;
        }

        public IEnumerable<IsUserPartOfAConversationOut> IsUserPartOfAConversation(IsUserPartOfAConversationIn isUserPartOfAConversationIn)
        {
            IEnumerable<IsUserPartOfAConversationOut> result;

            using (var connection = _dbConnectionFactory.GetConnection(_connectionString))
            {
                result = connection.ExecuteScalar<IEnumerable<IsUserPartOfAConversationOut>>("USP_IsUserPartOfAConversation",
                    new
                    {
                        IDUser = isUserPartOfAConversationIn.IDUserSender,
                        isUserPartOfAConversationIn.IDConversation
                    },
                    commandType: CommandType.StoredProcedure
                );
            }

            return result;
        }

        public NumberOfMessagesToReadOut NumberOfMessagesToRead(NumberOfMessagesToReadIn numberOfMessagesToReadIn)
        {
            NumberOfMessagesToReadOut result;

            using (var connection = _dbConnectionFactory.GetConnection(_connectionString))
            {
                result = connection.ExecuteScalar<NumberOfMessagesToReadOut>("USP_CountMessagesNotRead",
                    new
                    {
                        IDUserConversationOwner = numberOfMessagesToReadIn.IDConversation
                    },
                    commandType: CommandType.StoredProcedure
                );
            }

            return result;
        }

        public IEnumerable<TypeOfMessageInfoByIdOut> TypeOfMessageInfoById(TypeOfMessageInfoByIdIn typeOfMessageInfoByIdIn)
        {
            IEnumerable<TypeOfMessageInfoByIdOut> result;

            using (var connection = _dbConnectionFactory.GetConnection(_connectionString))
            {
                result = connection.ExecuteScalar<IEnumerable<TypeOfMessageInfoByIdOut>>("USP_GetTypeOfMessageInfoByID",
                    new
                    {
                        typeOfMessageInfoByIdIn.IDMessageType
                    },
                    commandType: CommandType.StoredProcedure
                );
            }

            return result;
        }

        public IEnumerable<NewMessageRecipientOut> NewMessageRecipient(NewMessageRecipientIn newMessageRecipientIn)
        {
            IEnumerable<NewMessageRecipientOut> result;

            using (var connection = _dbConnectionFactory.GetConnection(_connectionString))
            {
                result = connection.ExecuteScalar<IEnumerable<NewMessageRecipientOut>>("USP_InsertMessageRecipient",
                    new
                    {
                        newMessageRecipientIn.IDMessage,
                        newMessageRecipientIn.IDUserConversation,
                        newMessageRecipientIn.IDUserSender,
                        newMessageRecipientIn.IDUserRecipient,
                        SentOn = DateTime.UtcNow
                    },
                    commandType: CommandType.StoredProcedure
                );
            }

            return result;
        }

        public SetMessageAsViewedOut SetMessageAsViewed(SetMessageAsViewedIn setMessageAsViewedIn)
        {
            SetMessageAsViewedOut result;

            using (var connection = _dbConnectionFactory.GetConnection(_connectionString))
            {
                result = connection.ExecuteScalar<SetMessageAsViewedOut>("USP_MessageSetAsViewed",
                    new
                    {
                        setMessageAsViewedIn.IDUserConversationOwner
                    },
                    commandType: CommandType.StoredProcedure
                );
            }

            return result;
        }

        public SetAllConversationMessagesAsViewedOut SetAllConversationMessagesAsViewed(SetAllConversationMessagesAsViewedIn setAllConversationMessagesAsViewedInpu)
        {
            SetAllConversationMessagesAsViewedOut result;

            using (var connection = _dbConnectionFactory.GetConnection(_connectionString))
            {
                result = connection.ExecuteScalar<SetAllConversationMessagesAsViewedOut>("USP_MessagesOfAConversationSetAsViewed",
                    new
                    {
                        setAllConversationMessagesAsViewedInpu.IDUserConversationOwner,
                        setAllConversationMessagesAsViewedInpu.IDConversation
                    },
                    commandType: CommandType.StoredProcedure
                );
            }

            return result;
        }

        public SetMessageAsDeletedOut SetMessageAsDeleted(SetMessageAsDeletedIn setMessageAsDeletedIn)
        {
            SetMessageAsDeletedOut result;

            using (var connection = _dbConnectionFactory.GetConnection(_connectionString))
            {
                result = connection.ExecuteScalar<SetMessageAsDeletedOut>("USP_MessageSetAsDeleted",
                    new
                    {
                        setMessageAsDeletedIn.IDMessageRecipient
                    },
                    commandType: CommandType.StoredProcedure
                );
            }

            return result;
        }

        public InsertNewMessageOut InsertNewMessage(InsertNewMessageIn insertNewMessageIn)
        {
            InsertNewMessageOut result;

            using (var connection = _dbConnectionFactory.GetConnection(_connectionString))
            {
                result = connection.ExecuteScalar<InsertNewMessageOut>("USP_InsertMessage",
                    new
                    {
                        insertNewMessageIn.IDMessageType,
                        insertNewMessageIn.Message
                    },
                    commandType: CommandType.StoredProcedure
                );
            }

            return result;
        }

        public IEnumerable<MessageByIdOut> MessageById(MessageByIdIn messageInfoByIdIn)
        {
            IEnumerable<MessageByIdOut> result;

            using (var connection = _dbConnectionFactory.GetConnection(_connectionString))
            {
                result = connection.ExecuteScalar<IEnumerable<MessageByIdOut>>("USP_GetMessageInfoByID",
                    new
                    {
                        messageInfoByIdIn.IDMessage
                    },
                    commandType: CommandType.StoredProcedure
                );
            }

            return result;
        }
    }
}