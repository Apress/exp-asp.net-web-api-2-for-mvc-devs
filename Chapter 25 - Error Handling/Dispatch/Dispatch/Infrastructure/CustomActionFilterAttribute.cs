using System.Diagnostics;
using System.Web.Http.Controllers;
using System.Web.Http.Filters;

namespace Dispatch.Infrastructure {
    public class CustomActionFilterAttribute : ActionFilterAttribute {
        private Stopwatch sw;

        public override void OnActionExecuting(HttpActionContext actionContext) {
            sw = Stopwatch.StartNew();
        }

        public override void OnActionExecuted(HttpActionExecutedContext
                actionExecutedContext) {
            long elapsedMs = sw.ElapsedMilliseconds;
            actionExecutedContext.Response.Headers.Add("Elapsed-Time",
                elapsedMs.ToString());
            System.Diagnostics.Debug.WriteLine("Elapsed time: {0} ms", elapsedMs);
        }
    }
}