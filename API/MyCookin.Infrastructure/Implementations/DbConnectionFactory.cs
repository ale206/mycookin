using System.Data.Common;
using MyCookin.Domain.Repositories;
using MySql.Data.MySqlClient;

namespace MyCookin.Infrastructure.Implementations
{
    public class DbConnectionFactory : IDbConnectionFactory
    {
        public DbConnection GetConnection(string connectionString)
        {
            return new MySqlConnection(connectionString);
        }
    }
}