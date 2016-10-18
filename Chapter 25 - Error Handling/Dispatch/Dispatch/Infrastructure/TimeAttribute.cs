using System.Diagnostics;
using System.Threading;
using System.Threading.Tasks;
using System.Web.Http.Controllers;
using System.Web.Http.Filters;

namespace Dispatch.Infrastructure {
    public class TimeAttribute : ActionFilterAttribute {
        private static readonly string propKey =
            "Dispatch.Infrastructure.TimeAttribute.StopWatch";

        public override Task OnActionExecutingAsync(HttpActionContext actionContext,
                CancellationToken cancellationToken) {
            return Task.Factory.StartNew(() => {
                actionContext.Request.Properties.Add(propKey, Stopwatch.StartNew());
            });
        }

        public override Task OnActionExecutedAsync(HttpActionExecutedContext
                actionExecutedContext, CancellationToken cancellationToken) {

            return Task.Factory.StartNew(() => {
                if (actionExecutedContext.Request.Properties.ContainsKey(propKey)) {
                    Stopwatch sw =
                        ((Stopwatch)actionExecutedContext.Request.Properties[propKey]);
                    long elapsedTicks = sw.ElapsedTicks;
                    actionExecutedContext.Response.Headers.Add("Elapsed-Time",
                        elapsedTicks.ToString());
                    System.Diagnostics.Debug.WriteLine(
                        "Elapsed time: {0} ticks, {1} {2}", elapsedTicks,
                            actionExecutedContext.Request.Method,
                            actionExecutedContext.Request.RequestUri);
                }
            });
        }
    }
}
