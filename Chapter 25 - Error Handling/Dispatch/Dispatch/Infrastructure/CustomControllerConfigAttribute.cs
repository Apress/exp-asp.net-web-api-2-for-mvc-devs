using System;
using System.Web.Http.Controllers;

namespace Dispatch.Infrastructure {

    public class CustomControllerConfigAttribute : Attribute, IControllerConfiguration {

        public void Initialize(HttpControllerSettings controllerSettings,
                HttpControllerDescriptor controllerDescriptor) {

            controllerSettings.Services.Replace(typeof(IHttpActionSelector),
                new CustomActionSelector());
            controllerSettings.Services.Replace(typeof(IHttpActionInvoker),
                new CustomActionInvoker());
        }
    }
}
