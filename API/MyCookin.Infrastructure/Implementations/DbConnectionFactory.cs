using System.Data.Common;
using Dapper;
using MyCookin.Domain.Repositories;
using MySql.Data.MySqlClient;

namespace MyCookin.Infrastructure.Implementations
{
    public class DbConnectionFactory : IDbConnectionFactory
    {
        public DbConnection GetConnection(string connectionString)
        {
            // This is to return proper DateTime from MySql instead of 01-01-0001
            DefaultTypeMap.MatchNamesWithUnderscores = true;

            return new MySqlConnection(connectionString);
        }
    }
}