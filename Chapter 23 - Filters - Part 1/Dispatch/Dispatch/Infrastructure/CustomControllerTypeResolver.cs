using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Http.Controllers;
using System.Web.Http.Dispatcher;

namespace Dispatch.Infrastructure {
    public class CustomControllerTypeResolver : IHttpControllerTypeResolver {

        public string Suffix { get; set; }

        public ICollection<Type> GetControllerTypes(IAssembliesResolver
            assembliesResolver) {

            return assembliesResolver.GetAssemblies()
                .Select(assembly => assembly.GetTypes())
                .SelectMany(t => t)
                .Where(t => t != null
                    && t.IsClass
                    && t.IsVisible
                    && !t.IsAbstract
                    && typeof(IHttpController).IsAssignableFrom(t)
                    && HasValidControllerName(t)).ToList();
        }

        private bool HasValidControllerName(Type t) {
            return t.Name.Length > Suffix.Length
                && t.Name.EndsWith(Suffix, StringComparison.OrdinalIgnoreCase);
        }
    }
}