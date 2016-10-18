using System.Threading;
using System.Threading.Tasks;
using System.Web.Http.Controllers;
using System.Web.Http.Metadata;
using System.Web.Http.ValueProviders;

namespace ExampleApp.Infrastructure {
    public class HeaderValueParameterBinding : HttpParameterBinding {
        private HeaderValueProviderFactory factory;

        public HeaderValueParameterBinding(HttpParameterDescriptor descriptor)
            : base(descriptor) {
            factory = new HeaderValueProviderFactory();
        }

        public override Task ExecuteBindingAsync(ModelMetadataProvider metadataProvider,
                HttpActionContext context, CancellationToken cancellationToken) {

            IValueProvider valueProvider = factory.GetValueProvider(context);
            if (valueProvider != null) {
                ValueProviderResult result
                    = valueProvider.GetValue(Descriptor.ParameterName);
                if (result != null) {
                    SetValue(context, result.RawValue);
                }
            }
            return Task.FromResult<object>(null);
        }
    }
}
