using System.Web.Http;
using System.Web.Http.Controllers;
using System.Web.Http.ExceptionHandling;
using Dispatch.Infrastructure;

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

            //config.Services.Replace(typeof(IHttpActionSelector),
            //    new PipelineActionSelector());
            //config.Filters.Add(new SayHelloAttribute { Message = "Global Filter" });
            //config.MessageHandlers.Add(new AuthenticationDispatcher());

            config.IncludeErrorDetailPolicy = IncludeErrorDetailPolicy.Never;

            config.Services.Replace(typeof(IExceptionHandler),
                new CustomExceptionHandler());
            config.Services.Add(typeof(IExceptionLogger), new CustomExceptionLogger());

        }
    }
}
