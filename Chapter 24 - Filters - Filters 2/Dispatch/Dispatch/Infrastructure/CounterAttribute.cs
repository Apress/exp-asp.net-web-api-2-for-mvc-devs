using System;
using System.Diagnostics;
using System.Net;
using System.Net.Http;
using System.Threading;
using System.Threading.Tasks;
using System.Web.Http.Controllers;
using System.Web.Http.Filters;

namespace Dispatch.Infrastructure {

    public class CounterAttribute : ActionFilterAttribute {
        private static int counter = 0;
        private static int limit;

        public CounterAttribute(int requestLimit) {
            limit = requestLimit;
        }

        public override Task OnActionExecutingAsync(HttpActionContext actionContext,
                CancellationToken cancellationToken) {

            return Task.Factory.StartNew(() => {
                if (counter < limit) {
                    Debug.WriteLine("Request {0} of {1}", counter, limit);
                    counter++;
                } else {
                    actionContext.Response = actionContext.Request.CreateErrorResponse(
                        HttpStatusCode.ServiceUnavailable, "Limit Reached");
                }
            });
        }
    }
}
