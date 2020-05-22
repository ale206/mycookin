using System.Data.Common;
using System.Data.SqlClient;
using TaechIdeas.Core.Core;

namespace TaechIdeas.Core.DataAccessLayer
{
    public class DbConnectionFactory : IDbConnectionFactory
    {
        public DbConnection GetConnection(string connectionString)
        {
            return new SqlConnection(connectionString);
        }
    }
}