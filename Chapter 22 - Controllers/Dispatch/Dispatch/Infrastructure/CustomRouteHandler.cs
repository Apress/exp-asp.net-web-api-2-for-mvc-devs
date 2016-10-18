using System.Net;
using System.Net.Http;
using System.Threading;
using System.Threading.Tasks;

namespace Dispatch.Infrastructure {
    public class CustomRouteHandler : HttpMessageHandler {

        protected override Task<HttpResponseMessage> SendAsync(HttpRequestMessage
            request, CancellationToken cancellationToken) {

            string responseString
                = (string)request.GetRequestContext()
                    .RouteData.Route.DataTokens["response"];

            return Task.FromResult<HttpResponseMessage>(
                request.CreateResponse(HttpStatusCode.OK, responseString));
        }
    }
}
