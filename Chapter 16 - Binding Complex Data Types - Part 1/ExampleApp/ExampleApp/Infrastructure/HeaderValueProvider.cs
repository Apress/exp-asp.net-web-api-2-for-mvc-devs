using System.Globalization;
using System.Web.Http.ValueProviders;
using System.Linq;

namespace ExampleApp.Infrastructure {
    public class HeaderValueProvider : IValueProvider {
        private HeadersMap headers;

        public HeaderValueProvider(HeadersMap map) {
            headers = map;
        }

        public ValueProviderResult GetValue(string key) {
            string value = headers[key.Split('.').Last()];
            return value == null
                ? null
                : new ValueProviderResult(value, value, CultureInfo.InvariantCulture);
        }

        public bool ContainsPrefix(string prefix) {
            return false;
        }
    }
}
