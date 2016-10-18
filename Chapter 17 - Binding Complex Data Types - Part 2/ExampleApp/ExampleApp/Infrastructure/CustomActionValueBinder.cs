using System.Web.Http;
using System.Web.Http.Controllers;
using System.Web.Http.ModelBinding;
using System.Linq;

namespace ExampleApp.Infrastructure {
    public class CustomActionValueBinder : IActionValueBinder {

        public HttpActionBinding GetBinding(HttpActionDescriptor actionDescriptor) {
            return new HttpActionBinding(
                actionDescriptor,
                actionDescriptor.GetParameters()
                    .Select(p => GetParameterBinding(p)).ToArray()
            );
        }

        protected HttpParameterBinding GetParameterBinding(
                HttpParameterDescriptor parameter) {

            if (parameter.ParameterBinderAttribute != null) {
                return parameter.ParameterBinderAttribute.GetBinding(parameter);
            }

            HttpParameterBinding binding =
                parameter.Configuration.ParameterBindingRules.LookupBinding(parameter);
            if (binding != null) {
                return binding;
            }

            if (parameter.ParameterType.IsPrimitive
                    || parameter.ParameterType == typeof(string)) {
                return parameter.BindWithAttribute(new ModelBinderAttribute());
            }

            return new FromBodyAttribute().GetBinding(parameter);
        }
    }
}
