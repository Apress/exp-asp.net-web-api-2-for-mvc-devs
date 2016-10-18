using System.Collections.Generic;
using System.Linq;
using System.Web.Http.Controllers;
using System.Web.Http.ModelBinding;
using System.Web.Http.ValueProviders;
using ExampleApp.Models;

namespace ExampleApp.Infrastructure {

    public class NumbersBinder : IModelBinder {

        public bool BindModel(HttpActionContext actionContext,
                ModelBindingContext bindingContext) {

            string modelName = bindingContext.ModelName;

            Dictionary<string, ValueProviderResult> data
                = new Dictionary<string, ValueProviderResult>();

            data.Add("first", GetValue(bindingContext, modelName, "first"));
            data.Add("second", GetValue(bindingContext, modelName, "second"));
            data.Add("add", GetValue(bindingContext, modelName, "op", "add"));
            data.Add("double", GetValue(bindingContext, modelName, "op", "double"));
            data.Add("accept", GetValue(bindingContext, modelName, "accept"));

            if (data.All(x => x.Key == "accept" || x.Value != null)) {
                bindingContext.Model = CreateInstance(data);
                return true;
            }
            return false;
        }

        private ValueProviderResult GetValue(ModelBindingContext context,
                params string[] names) {

            for (int i = 0; i < names.Length - 1; i++) {
                string prefix = string.Join(".",
                    names.Skip(i).Take(names.Length - (i + 1)));
                if (prefix != string.Empty &&
                        context.ValueProvider.ContainsPrefix(prefix)) {
                    return context.ValueProvider.GetValue(prefix + "." + names.Last());
                }
            }
            return context.ValueProvider.GetValue(names.Last());
        }

        private Numbers CreateInstance(Dictionary<string, ValueProviderResult> data) {
            return new Numbers(Convert<int>(data["first"]),
                    Convert<int>(data["second"])) {
                        Op = new Operation {
                            Add = Convert<bool>(data["add"]),
                            Double = Convert<bool>(data["double"])
                        },
                        Accept = Convert<string>(data["accept"])
                    };
        }

        private T Convert<T>(ValueProviderResult result) {
            try {
                return (T)result.ConvertTo(typeof(T));
            } catch {
                return default(T);
            }
        }
    }
}
