using System.Web.Http;
using ExampleApp.Infrastructure;
using System.Web.Http.Controllers;
using ExampleApp.Models;

namespace ExampleApp {
    public static class WebApiConfig {
        public static void Register(HttpConfiguration config) {

            config.DependencyResolver = new NinjectResolver();

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
                name: "Binding Example Route",
                routeTemplate: "api/{controller}/{action}/{first}/{second}"
            );

            config.Routes.MapHttpRoute(
                name: "DefaultApi",
                routeTemplate: "api/{controller}/{id}",
                defaults: new { id = RouteParameter.Optional }
            );

            config.ParameterBindingRules.Insert(0, typeof(Numbers),
                x => x.BindWithAttribute(new FromUriAttribute())); 

        }
    }
}
