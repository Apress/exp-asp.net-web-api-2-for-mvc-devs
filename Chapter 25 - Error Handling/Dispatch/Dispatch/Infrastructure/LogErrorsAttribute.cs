using System;
using System.Diagnostics;
using System.Threading;
using System.Threading.Tasks;
using System.Web.Http.Filters;

namespace Dispatch.Infrastructure {
    public class LogErrorsAttribute : Attribute, IExceptionFilter {
        public Task ExecuteExceptionFilterAsync(HttpActionExecutedContext
            actionExecutedContext, CancellationToken cancellationToken) {

            Debug.WriteLine(string.Format(
                "Exception Type: {0}", actionExecutedContext.Exception.Message));
            Debug.WriteLine(string.Format(
                "Exception Message: {0}", actionExecutedContext.Exception.GetType()));

            return Task.FromResult<object>(null);
        }

        public bool AllowMultiple {
            get { return false; }
        }
    }
}
