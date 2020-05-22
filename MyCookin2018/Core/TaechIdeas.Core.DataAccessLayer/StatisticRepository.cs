using System;
using System.Data;
using Dapper;
using Microsoft.Extensions.Configuration;
using TaechIdeas.Core.Core;
using TaechIdeas.Core.Core.Statistic;
using TaechIdeas.Core.Core.Statistic.Dto;

namespace TaechIdeas.Core.DataAccessLayer
{
    public class StatisticRepository : IStatisticRepository
    {
        private readonly string _connectionString;
        private readonly IDbConnectionFactory _dbConnectionFactory;

        public StatisticRepository(IDbConnectionFactory dbConnectionFactory, IConfiguration configuration)
        {
            _dbConnectionFactory = dbConnectionFactory;
            _connectionString = configuration.GetConnectionString("DBStatisticsConnectionString");
        }

        public NewStatisticOut NewStatistic(NewStatisticIn newStatisticIn)
        {
            NewStatisticOut result;

            using (var connection = _dbConnectionFactory.GetConnection(_connectionString))
            {
                result = connection.ExecuteScalar<NewStatisticOut>("USP_InsertUserActionStatistic",
                    new
                    {
                        IDUser = newStatisticIn.UserId,
                        IDRelatedObject = newStatisticIn.RelatedObjectId,
                        newStatisticIn.ActionType,
                        Comments = newStatisticIn.Comment,
                        DateAction = DateTime.UtcNow,
                        FileOrigin = newStatisticIn.OriginFile,
                        newStatisticIn.SearchString,
                        newStatisticIn.OtherInfo
                    },
                    commandType: CommandType.StoredProcedure
                );
            }

            return result;
        }
    }
}