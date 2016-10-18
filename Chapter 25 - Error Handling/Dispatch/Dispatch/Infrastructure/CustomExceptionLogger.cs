using System.Diagnostics;
using System.Threading;
using System.Threading.Tasks;
using System.Web.Http.ExceptionHandling;

namespace Dispatch.Infrastructure {

    public class CustomExceptionLogger : IExceptionLogger {

        public Task LogAsync(ExceptionLoggerContext context,
            CancellationToken cancellationToken) {

            Debug.WriteLine("Log Exception Type: {0}, Originated: {1}, URL: {2}",
                context.Exception.GetType(),
                context.CatchBlock,
                context.Request.RequestUri);

            return Task.FromResult<object>(null);
        }
    }
}
