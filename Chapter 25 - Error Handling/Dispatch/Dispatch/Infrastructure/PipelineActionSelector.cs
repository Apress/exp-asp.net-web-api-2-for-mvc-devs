using System.Collections.Generic;
using System.Diagnostics;
using System.Web.Http.Controllers;
using System.Web.Http.Filters;
using System.Linq;

namespace Dispatch.Infrastructure {
    public class PipelineActionSelector : ApiControllerActionSelector {

        public override HttpActionDescriptor SelectAction(HttpControllerContext
                controllerContext) {

            HttpActionDescriptor action = base.SelectAction(controllerContext);

            IEnumerable<FilterInfo> filters = action.GetFilterPipeline();

            IEnumerable<FilterInfo> orderedFilters =
                GetFilters<IAuthenticationFilter>(filters)
                .Concat(GetFilters<IAuthorizationFilter>(filters))
                .Concat(GetFilters<IActionFilter>(filters));

            foreach (FilterInfo filter in orderedFilters) {
                Debug.WriteLine("Scope {0} Type: {1}", filter.Scope,
                    filter.Instance.GetType().Name);
            }

            return action;
        }

        private IEnumerable<FilterInfo> GetFilters<T>(IEnumerable<FilterInfo> filters) {
            return filters.Where(f => f.Instance is T);
        }
    }
}