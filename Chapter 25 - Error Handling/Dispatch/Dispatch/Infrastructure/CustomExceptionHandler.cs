using System.Net;
using System.Threading;
using System.Threading.Tasks;
using System.Web.Http.ExceptionHandling;
using System.Web.Http.Results;

namespace Dispatch.Infrastructure {
    public class CustomExceptionHandler : IExceptionHandler {

        public Task HandleAsync(ExceptionHandlerContext context,
                CancellationToken cancellationToken) {

            context.Result = new StatusCodeResult(HttpStatusCode.InternalServerError,
                context.Request);
            return Task.FromResult<object>(null);
        }
    }
}
