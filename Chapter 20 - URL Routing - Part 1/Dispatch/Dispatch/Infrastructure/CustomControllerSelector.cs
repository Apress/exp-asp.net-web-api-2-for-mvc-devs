using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using System.Web.Http.Controllers;
using System.Web.Http.Dispatcher;

namespace Dispatch.Infrastructure {

    public class CustomControllerSelector : IHttpControllerSelector {
        private IDictionary<string, HttpControllerDescriptor> dictionary;
        private ILookup<string, HttpControllerDescriptor> mappings;

        public CustomControllerSelector(string suffix) {

            Suffix = suffix;
            HttpConfiguration config = GlobalConfiguration.Configuration;

            IHttpControllerTypeResolver typeFinder =
                config.Services.GetHttpControllerTypeResolver();
            IAssembliesResolver assemblyFinder = config.Services.GetAssembliesResolver();

            IEnumerable<HttpControllerDescriptor> descriptors
                = typeFinder.GetControllerTypes(assemblyFinder)
                .Select(type => new HttpControllerDescriptor {
                    Configuration = GlobalConfiguration.Configuration,
                    ControllerName = type.Name.Substring(0,
                        type.Name.Length - Suffix.Length),
                    ControllerType = type
                });

            mappings = descriptors.ToLookup(descriptor =>
                    descriptor.ControllerName, StringComparer.OrdinalIgnoreCase);

            dictionary = descriptors.ToDictionary(d => d.ControllerName, d => d);
        }

        private string Suffix { get; set; }

        public IDictionary<string, HttpControllerDescriptor> GetControllerMapping() {
            return dictionary;
        }

        public HttpControllerDescriptor SelectController(HttpRequestMessage request) {
            string key
                = request.GetRequestContext().RouteData.Values["controller"] as string;
            IEnumerable<HttpControllerDescriptor> matches = mappings[key];
            switch (matches.Count()) {
                case 1:
                    return matches.First();
                case 0:
                    throw new HttpResponseException(HttpStatusCode.NotFound);
                default:
                    throw new HttpResponseException(HttpStatusCode.InternalServerError);
            }
        }
    }
}
