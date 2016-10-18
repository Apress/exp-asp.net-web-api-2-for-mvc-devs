using System.Net;
using System.Net.Http;
using System.Threading;
using System.Threading.Tasks;
using System.Web.Http;

namespace ExampleApp.Infrastructure {
    public class NoContentResult : IHttpActionResult {

        public Task<HttpResponseMessage> ExecuteAsync(CancellationToken
                cancellationToken) {
            return Task.FromResult(new HttpResponseMessage(HttpStatusCode.NoContent));
        }
    }
}
