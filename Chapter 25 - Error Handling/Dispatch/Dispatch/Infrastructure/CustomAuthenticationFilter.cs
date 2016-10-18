using System;
using System.Net.Http.Headers;
using System.Threading;
using System.Threading.Tasks;
using System.Web.Http.Filters;
using System.Web.Http.Results;

namespace Dispatch.Infrastructure {

    public class CustomAuthenticationAttribute : Attribute, IAuthenticationFilter {

        public Task AuthenticateAsync(HttpAuthenticationContext context,
                    CancellationToken cancellationToken) {

            
            if (context.Principal == null || !context.Principal.Identity.IsAuthenticated) {
                context.ErrorResult
                    = new UnauthorizedResult(new AuthenticationHeaderValue[] { 
                        new AuthenticationHeaderValue("Basic") }, context.Request);
            }

            return Task.FromResult<object>(null);
        }

        public Task ChallengeAsync(HttpAuthenticationChallengeContext context,
                CancellationToken cancellationToken) {
            return Task.FromResult<object>(null);
        }

        public bool AllowMultiple {
            get { return false; }
        }
    }
}