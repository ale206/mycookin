using System.Collections.Generic;
using System.Data;
using Dapper;
using Microsoft.Extensions.Configuration;
using TaechIdeas.Core.Core;
using TaechIdeas.Core.Core.LogAndMessage;
using TaechIdeas.Core.Core.LogAndMessage.Dto;

namespace TaechIdeas.Core.DataAccessLayer
{
    public class ErrorAndMessageRepository : IErrorAndMessageRepository
    {
        private readonly string _connectionString;
        private readonly IDbConnectionFactory _dbConnectionFactory;

        public ErrorAndMessageRepository(IDbConnectionFactory dbConnectionFactory, IConfiguration configuration)
        {
            _dbConnectionFactory = dbConnectionFactory;
            _connectionString = configuration.GetConnectionString("DBErrorsAndMessagesConnectionString");
        }

        public GetErrorOrMessageOut GetErrorOrMessage(GetErrorOrMessageIn getErrorOrMessageIn)
        {
            GetErrorOrMessageOut result;

            using (var connection = _dbConnectionFactory.GetConnection(_connectionString))
            {
                result = connection.ExecuteScalar<GetErrorOrMessageOut>("USP_GetErrorOrMessage",
                    new
                    {
                        IDLanguage = getErrorOrMessageIn.LanguageId,
                        getErrorOrMessageIn.ErrorOrMessageCode
                    },
                    commandType: CommandType.StoredProcedure
                );
            }

            return result;
        }

        public InsertErrorLogOut InsertErrorLog(InsertErrorLogIn insertErrorLogIn)
        {
            InsertErrorLogOut result;

            using (var connection = _dbConnectionFactory.GetConnection(_connectionString))
            {
                result = connection.ExecuteScalar<InsertErrorLogOut>("USP_InsertErrorLog",
                    new
                    {
                        insertErrorLogIn.ErrorNumber,
                        insertErrorLogIn.ErrorSeverity,
                        insertErrorLogIn.ErrorState,
                        insertErrorLogIn.ErrorProcedure,
                        insertErrorLogIn.ErrorLine,
                        insertErrorLogIn.ErrorMessage,
                        insertErrorLogIn.FileOrigin,
                        insertErrorLogIn.DateError,
                        insertErrorLogIn.ErrorMessageCode,
                        isStoredProcedureError = insertErrorLogIn.IsStoredProcedureError,
                        isTriggerError = insertErrorLogIn.IsTriggerError,
                        IDUser = insertErrorLogIn.UserId,
                        isApplicationError = insertErrorLogIn.IsApplicationError,
                        isApplicationLog = insertErrorLogIn.IsApplicationLog
                    },
                    commandType: CommandType.StoredProcedure
                );
            }

            return result;
        }

        public GetLastErrorLogDateOut GetLastErrorLogDate(GetLastErrorLogDateIn getLastErrorLogDateIn)
        {
            GetLastErrorLogDateOut result;

            using (var connection = _dbConnectionFactory.GetConnection(_connectionString))
            {
                result = connection.ExecuteScalar<GetLastErrorLogDateOut>("USP_GetLastErrorLogDate",
                    new
                    {
                        getLastErrorLogDateIn.FileOrigin,
                        getLastErrorLogDateIn.ErrorMessageCode
                    },
                    commandType: CommandType.StoredProcedure
                );
            }

            return result;
        }

        public DeleteErrorByErrorMessageOut DeleteErrorByErrorMessage(DeleteErrorByErrorMessageIn deleteErrorByErrorMessageIn)
        {
            DeleteErrorByErrorMessageOut result;

            using (var connection = _dbConnectionFactory.GetConnection(_connectionString))
            {
                result = connection.ExecuteScalar<DeleteErrorByErrorMessageOut>("USP_DeleteErrorByErrorMessage",
                    new
                    {
                        deleteErrorByErrorMessageIn.ErrorMessageToDelete
                    },
                    commandType: CommandType.StoredProcedure
                );
            }

            return result;
        }

        public IEnumerable<GetErrorsListOut> GetErrorsList(GetErrorsListIn getErrorsList)
        {
            IEnumerable<GetErrorsListOut> result;

            using (var connection = _dbConnectionFactory.GetConnection(_connectionString))
            {
                result = connection.ExecuteScalar<IEnumerable<GetErrorsListOut>>("USP_GetErrorsList", commandType: CommandType.StoredProcedure
                );
            }

            return result;
        }
    }
}