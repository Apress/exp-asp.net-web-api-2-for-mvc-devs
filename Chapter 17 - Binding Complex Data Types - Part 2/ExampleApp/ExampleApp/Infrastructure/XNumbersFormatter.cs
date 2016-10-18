using System;
using System.IO;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Formatting;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;
using ExampleApp.Models;

namespace ExampleApp.Infrastructure {
    public class XNumbersFormatter : MediaTypeFormatter {
        long bufferSize = 256;

        public XNumbersFormatter() {
            SupportedMediaTypes.Add(new MediaTypeHeaderValue("application/x.product"));
        }

        public override bool CanWriteType(Type type) {
            return false;
        }

        public override bool CanReadType(Type type) {
            return type == typeof(Numbers);
        }

        public async override Task<object> ReadFromStreamAsync(Type type,
            Stream readStream, HttpContent content, IFormatterLogger formatterLogger) {

            byte[] buffer = new byte[Math.Min(content.Headers.ContentLength.Value,
                bufferSize)];
            string[] items = Encoding.Default.GetString(buffer, 0,
                await readStream.ReadAsync(buffer, 0, buffer.Length)).Split(',', '=');

            if (items.Length == 4) {
                return new Numbers(
                    GetValue<int>("First", items[0], formatterLogger),
                    GetValue<int>("Second", items[1], formatterLogger)) {

                        Op = new Operation {
                            Add = GetValue<bool>("Add", items[2], formatterLogger),
                            Double = GetValue<bool>("Double", items[3], formatterLogger)
                        }
                    };
            } else {
                formatterLogger.LogError("", "Wrong Number of Items");
                return null;
            }
        }

        private T GetValue<T>(string name, string value, IFormatterLogger logger) {
            T result = default(T);
            try {
                result = (T)System.Convert.ChangeType(value, typeof(T));
            } catch {
                logger.LogError(name, "Cannot Parse Value");
            }
            return result;
        }
    }
}
