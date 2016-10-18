using System.Collections.Generic;
using System.Web.Http;
using System.Web.Http.ModelBinding;
using System.Web.Http.ValueProviders;
using System.Web.Http.Controllers;

namespace ExampleApp.Infrastructure {
    public class FromHeaderAttribute : ParameterBindingAttribute {
        public override HttpParameterBinding GetBinding(HttpParameterDescriptor param) {
            return new HeaderValueParameterBinding(param);
        }
    }
}
