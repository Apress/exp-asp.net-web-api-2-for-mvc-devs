using System.Net.Http;
using System.Web.Http.Controllers;
using System.Web.Http.ValueProviders;

namespace ExampleApp.Infrastructure {

    public class HeaderValueProviderFactory : ValueProviderFactory {
        public override IValueProvider GetValueProvider(HttpActionContext context) {
            if (context.Request.Method == HttpMethod.Post) {
                return new HeaderValueProvider(new HeadersMap(context.Request.Headers));
            } else {
                return null;
            }
        }
    }
}
