using System;
using System.Collections.Generic;
using System.IO;
using System.Net.Http;
using System.Net.Http.Formatting;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;
using ExampleApp.Models;

namespace ExampleApp.Infrastructure {
    public class XmlNumbersFormatter : MediaTypeFormatter {
        long bufferSize = 256;

        public XmlNumbersFormatter() {
            SupportedMediaTypes.Add(new MediaTypeHeaderValue("application/xml"));
            SupportedMediaTypes.Add(new MediaTypeHeaderValue("text/xml"));
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
            XElement xmlData = XElement.Parse(Encoding.Default.GetString(buffer, 0,
                await readStream.ReadAsync(buffer, 0, buffer.Length)));

            Dictionary<string, string> items = new Dictionary<string, string>();
            GetKvps(xmlData, items);

            if (items.Count == 4) {
                return new Numbers(
                    GetValue<int>(items["first"], formatterLogger),
                    GetValue<int>(items["second"], formatterLogger)) {
                        Op = new Operation {
                            Add = GetValue<bool>(items["add"], formatterLogger),
                            Double = GetValue<bool>(items["double"], formatterLogger)
                        }
                    };
            } else {
                formatterLogger.LogError("", "Wrong Number of Items");
                return null;
            }
        }

        private void GetKvps(XElement elem, Dictionary<string, string> dict) {
            if (elem.HasElements) {
                foreach (XElement innerElem in elem.Elements()) {
                    GetKvps(innerElem, dict);
                }
            } else {
                dict.Add(elem.Name.LocalName.ToLower(), elem.Value);
            }
        }

        private T GetValue<T>(string value, IFormatterLogger logger) {
            T result = default(T);
            try {
                result = (T)System.Convert.ChangeType(value, typeof(T));
            } catch {
                logger.LogError("", "Cannot Parse Value");
            }
            return result;
        }
    }
}
