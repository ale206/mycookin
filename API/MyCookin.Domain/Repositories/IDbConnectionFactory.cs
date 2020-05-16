using System.Data.Common;

namespace MyCookin.Domain.Repositories
{
    public interface IDbConnectionFactory
    {
        DbConnection GetConnection(string connectionString);
    }
}