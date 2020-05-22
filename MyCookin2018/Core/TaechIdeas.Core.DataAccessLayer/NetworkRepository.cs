using System.Collections.Generic;
using System.Data;
using Dapper;
using Microsoft.Extensions.Configuration;
using TaechIdeas.Core.Core;
using TaechIdeas.Core.Core.Network;
using TaechIdeas.Core.Core.Network.Dto;

namespace TaechIdeas.Core.DataAccessLayer
{
    public class NetworkRepository : INetworkRepository
    {
        private readonly string _connectionString;
        private readonly IDbConnectionFactory _dbConnectionFactory;

        public NetworkRepository(IDbConnectionFactory dbConnectionFactory, IConfiguration configuration)
        {
            _dbConnectionFactory = dbConnectionFactory;
            _connectionString = configuration.GetConnectionString("DBNetworkServiceConnectionString");
        }

        public SaveEmailToSendOut SaveEmailToSend(SaveEmailToSendIn saveEmailToSendIn)
        {
            SaveEmailToSendOut result;

            using (var connection = _dbConnectionFactory.GetConnection(_connectionString))
            {
                result = connection.ExecuteScalar<SaveEmailToSendOut>("USP_SaveEmailToSend",
                    new
                    {
                        saveEmailToSendIn.Bcc,
                        saveEmailToSendIn.Cc,
                        saveEmailToSendIn.From,
                        saveEmailToSendIn.HtmlFilePath,
                        saveEmailToSendIn.Message,
                        saveEmailToSendIn.Subject,
                        saveEmailToSendIn.To
                    },
                    commandType: CommandType.StoredProcedure
                );
            }

            return result;
        }

        public IEnumerable<EmailsToSendOut> EmailsToSend(EmailsToSendIn emailsToSendIn)
        {
            IEnumerable<EmailsToSendOut> result;

            using (var connection = _dbConnectionFactory.GetConnection(_connectionString))
            {
                result = connection.ExecuteScalar<IEnumerable<EmailsToSendOut>>("USP_EmailsToSend",
                    new
                    {
                        emailsToSendIn.MaxNumber
                    },
                    commandType: CommandType.StoredProcedure
                );
            }

            return result;
        }

        public UpdateEmailStatusOut UpdateEmailStatus(UpdateEmailStatusIn updateEmailStatusIn)
        {
            UpdateEmailStatusOut result;

            using (var connection = _dbConnectionFactory.GetConnection(_connectionString))
            {
                result = connection.ExecuteScalar<UpdateEmailStatusOut>("USP_UpdateEmailStatus",
                    new
                    {
                        updateEmailStatusIn.EmailId,
                        updateEmailStatusIn.EmailStatus
                    },
                    commandType: CommandType.StoredProcedure
                );
            }

            return result;
        }
    }
}