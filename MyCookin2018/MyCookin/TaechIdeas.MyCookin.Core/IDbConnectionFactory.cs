using System.Data.Common;

namespace TaechIdeas.MyCookin.Core
{
    /// <summary>
    ///     Db Connection Manager
    /// </summary>
    public interface IDbConnectionFactory
    {
        /// <summary>
        ///     Get and Open a Connection
        /// </summary>
        /// <param name="connectionString">Connection String</param>
        /// <returns>DB Connection</returns>
        DbConnection GetConnection(string connectionString);
    }
}