using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Http;
using Dispatch.Infrastructure;
using System.Web.Http.Dispatcher;
using System.Reflection;

namespace Dispatch {
    public static class WebApiConfig {
        public static void Register(HttpConfiguration config) {

            config.MapHttpAttributeRoutes();

            config.Routes.MapHttpRoute(
                name: "DefaultApi",
                routeTemplate: "api/{controller}/{id}",
                defaults: new { id = RouteParameter.Optional }
            );

            config.MessageHandlers.Add(new CustomMessageHandler());

            //config.Services.Replace(typeof(IHttpControllerTypeResolver),
            //    new CustomControllerTypeResolver { Suffix = "Service" });
            //config.Services.Replace(typeof(IHttpControllerSelector),
            //    new CustomControllerSelector("Service"));

            FieldInfo field = typeof(DefaultHttpControllerSelector)
                .GetField("ControllerSuffix", BindingFlags.Static | BindingFlags.Public);
            if (field != null) {
                field.SetValue(null, "Service");
            }
        }
    }
}
