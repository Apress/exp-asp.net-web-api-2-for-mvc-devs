using System.Linq;
using System.Net;
using System.Net.Http;
using System.Security.Principal;
using System.Threading;
using System.Threading.Tasks;
using System.Web.Http.Controllers;
using System.Web.Http.Filters;

namespace Dispatch.Infrastructure {

    public class CustomAuthorizationAttribute : AuthorizationFilterAttribute {
        private string[] roles;

        public CustomAuthorizationAttribute(params string[] rolesList) {
            roles = rolesList;
        }

        public override Task OnAuthorizationAsync(HttpActionContext actionContext,
                CancellationToken cancellationToken) {

            IPrincipal principal = actionContext.RequestContext.Principal;
            if (principal == null || !roles.Any(role => principal.IsInRole(role))) {
                actionContext.Response =
                    actionContext.Request.CreateResponse(HttpStatusCode.Unauthorized);
            }
            return Task.FromResult<object>(null);
        }
    }
}
