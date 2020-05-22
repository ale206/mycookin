using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;

namespace TaechIdeas.MyCookin.API.Middleware
{
    public interface IStopWatchMiddleware
    {
        /// <summary>
        ///     Invoke
        /// </summary>
        /// <param name="context">Http Context</param>
        /// <returns>Task</returns>
        Task Invoke(HttpContext context);
    }
}