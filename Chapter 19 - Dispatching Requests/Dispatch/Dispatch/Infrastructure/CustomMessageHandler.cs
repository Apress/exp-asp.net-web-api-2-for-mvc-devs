using System.Net;
using System.Net.Http;
using System.Threading;
using System.Threading.Tasks;
using System.Linq;

namespace Dispatch.Infrastructure {
    public class CustomMessageHandler : DelegatingHandler {

        protected async override Task<HttpResponseMessage> SendAsync(HttpRequestMessage
            request, CancellationToken cancellationToken) {

            if (request.Method == HttpMethod.Post) {
                System.Diagnostics.Debugger.Break();
            }
            return await base.SendAsync(request, cancellationToken);

        }
    }
}