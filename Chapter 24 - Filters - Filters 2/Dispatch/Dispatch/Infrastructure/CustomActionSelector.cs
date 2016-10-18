using System;
using System.Linq;
using System.Net;
using System.Reflection;
using System.Web.Http;
using System.Web.Http.Controllers;

namespace Dispatch.Infrastructure {
    public class CustomActionSelector : IHttpActionSelector {

        public ILookup<string, HttpActionDescriptor>
            GetActionMapping(HttpControllerDescriptor descriptor) {
            return descriptor.ControllerType.GetMethods()
                .Where(x => x.IsPublic
                    && !x.IsSpecialName
                    && x.GetCustomAttribute<NonActionAttribute>() == null)
                .Select(x => (HttpActionDescriptor)
                     new ReflectedHttpActionDescriptor(descriptor, x))
                .OrderBy(x => x.GetParameters().Count)
                .ToLookup(x => x.ActionName, StringComparer.OrdinalIgnoreCase);
        }

        public HttpActionDescriptor SelectAction(HttpControllerContext context) {
            if (context.RouteData.Values.ContainsKey("action")) {
                string actionName = (string)context.RouteData.Values["action"];
                return GetActionMapping(context.ControllerDescriptor)
                    [actionName].First();
            } else {
                throw new HttpResponseException(HttpStatusCode.NotFound);
            }
        }
    }
}
