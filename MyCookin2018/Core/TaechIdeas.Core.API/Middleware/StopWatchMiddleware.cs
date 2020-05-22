using System.Diagnostics;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;

namespace TaechIdeas.Core.API.Middleware
{
    public class StopWatchMiddleware
    {
        private readonly RequestDelegate _next;

        /// <summary>
        ///     Constructor
        /// </summary>
        /// <param name="next"></param>
        public StopWatchMiddleware(RequestDelegate next)
        {
            _next = next;
        }

        /// <summary>
        ///     Invoke
        /// </summary>
        /// <param name="context">Http Context</param>
        /// <returns>Task</returns>
        public async Task Invoke(HttpContext context)
        {
            var watch = new Stopwatch();
            context.Response.OnStarting(() =>
            {
                watch.Stop();

                context
                    .Response
                    .Headers
                    .Add("X-Processing-Time-Milliseconds", new[] {watch.ElapsedMilliseconds.ToString()});

                return Task.CompletedTask;
            });

            watch.Start();

            await _next(context);
        }
    }
}