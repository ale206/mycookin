using System.Data.Common;
using System.Data.SqlClient;
using TaechIdeas.MyCookin.Core;

namespace TaechIdeas.MyCookin.DataAccessLayer
{
    public class DbConnectionFactory : IDbConnectionFactory
    {
        public DbConnection GetConnection(string connectionString)
        {
            return new SqlConnection(connectionString);
        }
    }
}