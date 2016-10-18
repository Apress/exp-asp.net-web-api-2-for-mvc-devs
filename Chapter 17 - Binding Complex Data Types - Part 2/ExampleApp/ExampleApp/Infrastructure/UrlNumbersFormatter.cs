using System;
using System.IO;
using System.Net.Http;
using System.Net.Http.Formatting;
using System.Threading.Tasks;
using ExampleApp.Models;

namespace ExampleApp.Infrastructure {
    public class UrlNumbersFormatter : FormUrlEncodedMediaTypeFormatter {

        public override bool CanWriteType(Type type) {
            return false;
        }

        public override bool CanReadType(Type type) {
            return type == typeof(Numbers);
        }

        public override async Task<object> ReadFromStreamAsync(Type type,
            Stream readStream, HttpContent content, IFormatterLogger formatterLogger) {

            FormDataCollection fd = (FormDataCollection)
                await base.ReadFromStreamAsync(typeof(FormDataCollection),
                    readStream, content, formatterLogger);

            return new Numbers(
                GetValue<int>("First", fd, formatterLogger),
                GetValue<int>("Second", fd, formatterLogger)) {
                    Op = new Operation {
                        Add = GetValue<bool>("Add", fd, formatterLogger),
                        Double = GetValue<bool>("Double", fd, formatterLogger)
                    }
                };
        }

        private T GetValue<T>(string name, FormDataCollection fd,
                IFormatterLogger logger) {
            T result = default(T);
            try {
                result = (T)System.Convert.ChangeType(fd[name], typeof(T));
            } catch {
                logger.LogError(name, "Cannot Parse Value");
            }
            return result;
        }

    }
}
