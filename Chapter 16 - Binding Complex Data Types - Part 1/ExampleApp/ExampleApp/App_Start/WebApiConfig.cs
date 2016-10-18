using System.Web.Http;
using ExampleApp.Infrastructure;
using System.Web.Http.ValueProviders;
using System.Net.Http.Headers;
using System.Web.Http.ModelBinding;
using System.Web.Http.ModelBinding.Binders;
using ExampleApp.Models;


using System.Web.Http.ValueProviders.Providers;

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

            //config.ParameterBindingRules.Add(x => {
            //    return x.ParameterType == typeof(Numbers)
            //        ? new ModelBinderParameterBinding(x, new NumbersBinder(),
            //            new ValueProviderFactory[] {
            //            new QueryStringValueProviderFactory(),
            //            new HeaderValueProviderFactory()})
            //        : null;
            //});
        }
    }
}
