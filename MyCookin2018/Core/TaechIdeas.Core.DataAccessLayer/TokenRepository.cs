using System.Data;
using Dapper;
using Microsoft.Extensions.Configuration;
using TaechIdeas.Core.Core;
using TaechIdeas.Core.Core.Token;
using TaechIdeas.Core.Core.Token.Dto;

namespace TaechIdeas.Core.DataAccessLayer
{
    public class TokenRepository : ITokenRepository
    {
        private readonly string _connectionString;
        private readonly IDbConnectionFactory _dbConnectionFactory;

        public TokenRepository(IDbConnectionFactory dbConnectionFactory, IConfiguration configuration)
        {
            _dbConnectionFactory = dbConnectionFactory;
            _connectionString = configuration.GetConnectionString("DBUsersProfileConnectionString");
        }

        public NewTokenOut NewToken(NewTokenIn newTokenIn)
        {
            NewTokenOut result;

            using (var connection = _dbConnectionFactory.GetConnection(_connectionString))
            {
                result = connection.ExecuteScalar<NewTokenOut>("USP_GetNewToken",
                    new
                    {
                        IDUser = newTokenIn.UserId,
                        newTokenIn.TokenExpireMinutes,
                        newTokenIn.WebsiteId
                    },
                    commandType: CommandType.StoredProcedure
                );
            }

            return result;
        }

        public CheckTokenOut CheckToken(CheckTokenIn checkTokenIn)
        {
            CheckTokenOut result;

            using (var connection = _dbConnectionFactory.GetConnection(_connectionString))
            {
                result = connection.ExecuteScalar<CheckTokenOut>("USP_CheckToken",
                    new
                    {
                        checkTokenIn.UserToken,
                        IDUser = checkTokenIn.UserId,
                        checkTokenIn.TokenRenewMinutes,
                        checkTokenIn.WebsiteId
                    },
                    commandType: CommandType.StoredProcedure
                );
            }

            return result;
        }

        public ExpireTokenDataOutput ExpireToken(ExpireTokenDataInput expireTokenDataInput)
        {
            ExpireTokenDataOutput result;

            using (var connection = _dbConnectionFactory.GetConnection(_connectionString))
            {
                result = connection.ExecuteScalar<ExpireTokenDataOutput>("USP_ExpireToken",
                    new
                    {
                        expireTokenDataInput.UserToken,
                        IDUser = expireTokenDataInput.UserId
                    },
                    commandType: CommandType.StoredProcedure
                );
            }

            return result;
        }
    }
}