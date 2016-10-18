using System.Collections.Generic;
using System.Net.Http;
using System.Web.Http.Routing;

namespace Dispatch.Infrastructure {

    public class SpecificValueConstraint : IHttpRouteConstraint {
        private int targetValue;

        public SpecificValueConstraint(int value) {
            targetValue = value;
        }

        public bool Match(HttpRequestMessage request, IHttpRoute route,
            string parameterName, IDictionary<string, object> values,
                HttpRouteDirection routeDirection) {

            int candidateValue;

            return (values.ContainsKey(parameterName))
                && int.TryParse(values[parameterName].ToString(), out candidateValue)
                && targetValue == candidateValue;
        }
    }
}
