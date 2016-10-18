using System.Collections.Generic;
using System.Web.Http.Routing;

namespace Dispatch.Infrastructure {
    public class UserAgentConstraintRouteAttribute : RouteFactoryAttribute {

        public UserAgentConstraintRouteAttribute(string template)
            : base(template) {
        }

        public override IDictionary<string, object> Constraints {
            get {
                IDictionary<string, object> constraints
                    = base.Constraints ?? new Dictionary<string, object>();
                constraints.Add("useragent", new UserAgentConstraint("Chrome"));
                return constraints;
            }
        }
    }
}
