using System.Diagnostics;
using System.Threading;
using System.Threading.Tasks;
using System.Web.Http.Controllers;
using System.Web.Http.Filters;

namespace Dispatch.Infrastructure {
    public class SayHelloAttribute : ActionFilterAttribute {

        public string Message { get; set; }

        public override Task OnActionExecutingAsync(HttpActionContext actionContext,
            CancellationToken cancellationToken) {

            Debug.WriteLine("SayHello: {0}", (object)Message ?? "Hello");
            return Task.FromResult<object>(null);
        }
    }
}