using System.Web.Http;
using ExampleApp.Infrastructure;
using System.Net.Http.Formatting;
using System;
using Newtonsoft.Json;

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
                name: "DefaultApi",
                routeTemplate: "api/{controller}/{id}",
                defaults: new { id = RouteParameter.Optional }
            );

            MediaTypeFormatter xmlFormatter = config.Formatters.XmlFormatter;
            config.Formatters.Remove(xmlFormatter);
            config.Formatters.Insert(0, xmlFormatter);

            config.Services.Replace(typeof(IContentNegotiator),
                new DefaultContentNegotiator(true));

            JsonMediaTypeFormatter jsonFormatter = config.Formatters.JsonFormatter;
            jsonFormatter.Indent = true;
            jsonFormatter.SerializerSettings.DateFormatHandling
                = DateFormatHandling.MicrosoftDateFormat;
            jsonFormatter.SerializerSettings.StringEscapeHandling
                = StringEscapeHandling.EscapeHtml;
            jsonFormatter.SerializerSettings.DefaultValueHandling
                = DefaultValueHandling.Ignore;
        }
    }
}
