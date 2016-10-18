using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Web.Http.Routing;

namespace Dispatch.Infrastructure {

    public class UserAgentConstraint : IHttpRouteConstraint {
        private string requiredUA;

        public UserAgentConstraint(string agentParam) {
            requiredUA = agentParam.ToLowerInvariant();
        }

        public bool Match(HttpRequestMessage request, IHttpRoute route,
            string parameterName, IDictionary<string, object> values,
                HttpRouteDirection routeDirection) {

            return request.Headers.UserAgent
                .Where(x =>
                    x.Product != null && x.Product.Name != null &&
                    x.Product.Name.ToLowerInvariant().Contains(requiredUA))
                .Count() > 0;
        }
    }
}
