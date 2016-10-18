using System.Web.Http;
using ExampleApp.Infrastructure;
using System.Net.Http.Formatting;
using System;

namespace ExampleApp {
    public static class WebApiConfig {
        public static void Register(HttpConfiguration config) {

            config.DependencyResolver = new NinjectResolver();

            //config.Services.Replace(typeof(IContentNegotiator), 
            //  new CustomNegotiator());

            config.MapHttpAttributeRoutes();

            config.Routes.MapHttpRoute(
                name: "Api with extension",
                routeTemplate: "api/{controller}.{ext}/{id}",
                defaults: new {
                    id = RouteParameter.Optional,
                    ext = RouteParameter.Optional
                }
            );

            config.Routes.MapHttpRoute(
                name: "DefaultApi",
                routeTemplate: "api/{controller}/{id}",
                defaults: new { id = RouteParameter.Optional }
            );

            MediaTypeFormatter prodFormatter = new ProductFormatter();
            prodFormatter.AddQueryStringMapping("format", "product",
                "application/x.product");
            prodFormatter.AddRequestHeaderMapping("X-UseProductFormat", "true",
                StringComparison.InvariantCultureIgnoreCase, false,
                "application/x.product");
            prodFormatter.AddUriPathExtensionMapping("custom", "application/x.product");
            config.Formatters.Add(prodFormatter);
        }
    }
}
