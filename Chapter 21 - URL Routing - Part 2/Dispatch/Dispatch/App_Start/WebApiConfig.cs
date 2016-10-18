using System.Web.Http;
using Dispatch.Infrastructure;
using System.Collections.Generic;
using System.Web.Http.Routing;

namespace Dispatch {
    public static class WebApiConfig {
        public static void Register(HttpConfiguration config) {

            //config.Routes.Add(
            //    "CustomHandler",
            //    config.Routes.CreateRoute(
            //        routeTemplate: "api/{controller}/{action}",
            //        defaults: null,
            //        constraints: null,
            //        dataTokens: new Dictionary<string, object> {
            //            { "response", "Tomorrow" }
            //        },
            //        handler: new CustomRouteHandler()));

            DefaultInlineConstraintResolver resolver
                = new DefaultInlineConstraintResolver();
            resolver.ConstraintMap.Add("specval", typeof(SpecificValueConstraint));
            config.MapHttpAttributeRoutes(resolver);

            config.Routes.MapHttpRoute(
                name: "DefaultApi",
                routeTemplate: "api/{controller}/{id}",
                defaults: new { id = RouteParameter.Optional }
            );
        }
    }
}
