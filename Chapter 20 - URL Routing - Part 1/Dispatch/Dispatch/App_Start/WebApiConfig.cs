using System.Web.Http;
using Dispatch.Infrastructure;
using System.Web.Http.Routing.Constraints;

namespace Dispatch {
    public static class WebApiConfig {
        public static void Register(HttpConfiguration config) {

            config.MapHttpAttributeRoutes();

            config.Routes.MapHttpRoute(
                name: "ChromeRoute",
                routeTemplate: "api/today/{action}",
                defaults: new { controller = "today" },
                constraints: new {
                    useragent = new UserAgentConstraint("Chrome"),
                    action = new RegexRouteConstraint("daynumber|othermethod")
                }
            );

            config.Routes.MapHttpRoute(
                name: "NotChromeRoute",
                routeTemplate: "api/today/DayOfWeek",
                defaults: new { controller = "today", action = "daynumber" }
            );

            config.Routes.MapHttpRoute(
                name: "DefaultApi",
                routeTemplate: "api/{controller}/{id}",
                defaults: new { id = RouteParameter.Optional }
            );
        }
    }
}
