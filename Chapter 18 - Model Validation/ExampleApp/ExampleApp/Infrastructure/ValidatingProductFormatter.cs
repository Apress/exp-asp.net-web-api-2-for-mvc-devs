using System;
using System.IO;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Formatting;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;
using ExampleApp.Models;
using Newtonsoft.Json.Linq;

namespace ExampleApp.Infrastructure {
    public class ValidatingProductFormatter : MediaTypeFormatter {
        long bufferSize = 256;


        public ValidatingProductFormatter() {
            SupportedMediaTypes.Add(new MediaTypeHeaderValue("application/json"));
            SupportedMediaTypes.Add(new MediaTypeHeaderValue("text/json"));
        }

        public override bool CanReadType(Type type) {
            return type == typeof(Product);
        }

        public override bool CanWriteType(Type type) {
            return false;
        }

        public async override Task<object> ReadFromStreamAsync(Type type,
            Stream readStream, HttpContent content,
                IFormatterLogger formatterLogger) {

            byte[] buffer = new byte[Math.Min(content.Headers.ContentLength.Value,
              bufferSize)];
            string jsonString = Encoding.Default.GetString(buffer, 0,
                await readStream.ReadAsync(buffer, 0, buffer.Length));

            JObject jData = JObject.Parse(jsonString);


            if (jData.Properties().Any(p =>
                    string.Compare(p.Name, "includeinsale", true) == 0)) {
                formatterLogger.LogError("IncludeInSale",
                    "Request Must Not Contain IncludeInSale Value");
            }

            return new Product {
                Name = (string)jData["name"],
                Price = (decimal)jData["price"]
            };
        }
    }
}
