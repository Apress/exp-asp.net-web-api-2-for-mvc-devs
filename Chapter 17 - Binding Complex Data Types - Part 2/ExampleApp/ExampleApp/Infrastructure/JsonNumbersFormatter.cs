using System;
using System.Collections.Generic;
using System.IO;
using System.Net.Http;
using System.Net.Http.Formatting;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;
using ExampleApp.Models;
using Newtonsoft.Json.Linq;

namespace ExampleApp.Infrastructure {
    public class JsonNumbersFormatter : MediaTypeFormatter {
        long bufferSize = 256;

        public JsonNumbersFormatter() {
            SupportedMediaTypes.Add(new MediaTypeHeaderValue("application/json"));
            SupportedMediaTypes.Add(new MediaTypeHeaderValue("text/json"));
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
            string jsonString = Encoding.Default.GetString(buffer, 0, 
                await readStream.ReadAsync(buffer, 0, buffer.Length));

            JObject jData = JObject.Parse(jsonString);
            return new Numbers((int)jData["first"], (int)jData["second"]) {
                Op = new Operation {
                    Add = (bool)jData["op"]["add"],
                    Double = (bool)jData["op"]["double"]
                }
            };
        }        
    }
}
