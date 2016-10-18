using System.Net.Http;
using System.Threading;
using System.Threading.Tasks;
using System.Web.Http;
using System.Web.Http.Controllers;

namespace Dispatch.Infrastructure {
    public class CustomActionInvoker : IHttpActionInvoker {

        public async Task<HttpResponseMessage> InvokeActionAsync(HttpActionContext
                actionContext, CancellationToken cancellationToken) {

            object result = await actionContext.ActionDescriptor.ExecuteAsync(
                actionContext.ControllerContext, actionContext.ActionArguments,
                    cancellationToken);

            if (result is HttpResponseMessage) {
                return (HttpResponseMessage)result;
            } else if (result is IHttpActionResult) {
                return await ((IHttpActionResult)result).ExecuteAsync(cancellationToken);
            } else if (actionContext.ActionDescriptor.ReturnType != typeof(string)) {
                return actionContext.ActionDescriptor.ResultConverter.Convert(
                    actionContext.ControllerContext, result);
            } else {
                return new ValueResultConverter<string[]>().Convert(
                    actionContext.ControllerContext, new string[] { (string)result });
            }
        }
    }
}
