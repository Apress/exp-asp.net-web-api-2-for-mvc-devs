using System.Web.Http;
using System.Web.Http.ModelBinding;
using System.Web.Http.ModelBinding.Binders;
using System.Web.Http.ValueProviders;
using ExampleApp.Infrastructure;
using ExampleApp.Models;
using System.Web.Http.Controllers;

namespace ExampleApp {
    public static class WebApiConfig {
        public static void Register(HttpConfiguration config) {

            config.DependencyResolver = new NinjectResolver();

            config.MapHttpAttributeRoutes();

            config.Routes.MapHttpRoute(
                name: "Binding Example Route",
                routeTemplate: "api/{controller}/{action}/{first}/{second}"
            );

            config.Routes.MapHttpRoute(
                name: "DefaultApi",
                routeTemplate: "api/{controller}/{id}",
                defaults: new { id = RouteParameter.Optional }
            );

            config.Services.Add(typeof(ValueProviderFactory),
                new HeaderValueProviderFactory());

            config.Services.Insert(typeof(ModelBinderProvider), 0,
                new SimpleModelBinderProvider(typeof(Numbers), new NumbersBinder()));

            config.Formatters.Add(new XNumbersFormatter());
            config.Formatters.Insert(0, new UrlNumbersFormatter());
            config.Formatters.Insert(0, new JsonNumbersFormatter());
            config.Formatters.Insert(0, new XmlNumbersFormatter());

            config.Services.Replace(typeof(IActionValueBinder),
                new CustomActionValueBinder());
        }
    }
}
