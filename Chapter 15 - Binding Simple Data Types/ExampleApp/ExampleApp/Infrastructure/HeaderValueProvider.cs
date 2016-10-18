using System.Globalization;
using System.Web.Http.ValueProviders;

namespace ExampleApp.Infrastructure {
    public class HeaderValueProvider : IValueProvider {
        private HeadersMap headers;

        public HeaderValueProvider(HeadersMap map) {
            headers = map;
        }

        public ValueProviderResult GetValue(string key) {
            string value = headers[key];
            return value == null
                ? null
                : new ValueProviderResult(value, value, CultureInfo.InvariantCulture);
        }

        public bool ContainsPrefix(string prefix) {
            return false;
        }
    }
}
