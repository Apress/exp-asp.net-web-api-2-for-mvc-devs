using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;
using System.Web.Security;
using System.Web.SessionState;
using System.Web.Http;

namespace ExampleApp {
    public class Global : HttpApplication {
        void Application_Start(object sender, EventArgs e) {

            AreaRegistration.RegisterAllAreas();
            GlobalConfiguration.Configure(WebApiConfig.Register);
            RouteConfig.RegisterRoutes(RouteTable.Routes);

            System.Web.Mvc.DependencyResolver.SetResolver(
                (System.Web.Mvc.IDependencyResolver)
                GlobalConfiguration.Configuration.DependencyResolver);
        }
    }
}