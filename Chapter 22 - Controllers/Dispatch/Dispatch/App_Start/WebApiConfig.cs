using System.Web.Http;

namespace Dispatch {
    public static class WebApiConfig {
        public static void Register(HttpConfiguration config) {

            config.Routes.MapHttpRoute(
                name: "ActionMethods",
                routeTemplate: "api/nrest/{controller}/{action}/{day}",
                defaults: new { day = RouteParameter.Optional }
            );

            config.Routes.MapHttpRoute(
                name: "DefaultApi",
                routeTemplate: "api/{controller}/{id}",
                defaults: new { id = RouteParameter.Optional }
            );
        }
    }
}
