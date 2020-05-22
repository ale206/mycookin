using System;
using System.Collections.Generic;
using System.Data;
using Dapper;
using Microsoft.Extensions.Configuration;
using TaechIdeas.Core.Core;
using TaechIdeas.Core.Core.Contact;
using TaechIdeas.Core.Core.LogAndMessage.Dto;

namespace TaechIdeas.Core.DataAccessLayer
{
    public class ContactRepository : IContactRepository
    {
        private readonly string _connectionString;
        private readonly IDbConnectionFactory _dbConnectionFactory;

        public ContactRepository(IDbConnectionFactory dbConnectionFactory, IConfiguration configuration)
        {
            _dbConnectionFactory = dbConnectionFactory;
            _connectionString = configuration.GetConnectionString("DBErrorsAndMessagesConnectionString");
        }

        public NewMessageOut NewMessage(NewMessageIn newMessageIn)
        {
            NewMessageOut result;

            using (var connection = _dbConnectionFactory.GetConnection(_connectionString))
            {
                result = connection.ExecuteScalar<NewMessageOut>("USP_InsertContactRequest",
                    new
                    {
                        IDLanguage = newMessageIn.LanguageId,
                        IDContactRequestType = newMessageIn.ContactRequestTypeId,
                        newMessageIn.FirstName,
                        newMessageIn.LastName,
                        newMessageIn.Email,
                        newMessageIn.RequestText,
                        newMessageIn.PrivacyAccept,
                        RequestDate = DateTime.UtcNow,
                        newMessageIn.IpAddress,
                        IsRequestClosed = false
                    },
                    commandType: CommandType.StoredProcedure
                );
            }

            return result;
        }

        public IEnumerable<RequestMessagesOut> RequestMessages(RequestMessagesIn requestMessagesIn)
        {
            IEnumerable<RequestMessagesOut> result;

            using (var connection = _dbConnectionFactory.GetConnection(_connectionString))
            {
                result = connection.ExecuteScalar<IEnumerable<RequestMessagesOut>>("USP_GetContactRequests",
                    new
                    {
                        IDContactRequestType = requestMessagesIn.ContactRequestTypeId,
                        requestMessagesIn.JustNotClosedRequests
                    },
                    commandType: CommandType.StoredProcedure
                );
            }

            return result;
        }

        public NewReplyOut NewReply(NewReplyIn newReplyIn)
        {
            NewReplyOut result;

            using (var connection = _dbConnectionFactory.GetConnection(_connectionString))
            {
                result = connection.ExecuteScalar<NewReplyOut>("USP_InsertContactRequestReply",
                    new
                    {
                        IDContactRequest = newReplyIn.ContactRequestId,
                        IDUserWhoReplied = newReplyIn.UserIdWhoReplied,
                        newReplyIn.Reply,
                        ReplyDate = DateTime.UtcNow,
                        newReplyIn.IpAddress
                    },
                    commandType: CommandType.StoredProcedure
                );
            }

            return result;
        }
    }
}