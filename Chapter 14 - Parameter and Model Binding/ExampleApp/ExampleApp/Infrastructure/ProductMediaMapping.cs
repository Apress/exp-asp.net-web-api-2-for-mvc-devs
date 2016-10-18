using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Formatting;

namespace ExampleApp.Infrastructure {

    public class ProductMediaMapping : MediaTypeMapping {

        public ProductMediaMapping()
            : base("application/x.product") {
        }

        public override double TryMatchMediaType(HttpRequestMessage request) {
            IEnumerable<string> values;
            return request.Headers.TryGetValues("X-UseProductFormat", out values)
                && values.Where(x => x == "true").Count() > 0 ? 1 : 0;
        }
    }
}
