using System.Web.Http;
using ExampleApp.Models;
using System.Linq;
using System.Net.Http;
using System.Collections.Generic;
using System.Net;
using System.Collections.Specialized;
using System.Threading.Tasks;

namespace ExampleApp.Controllers {
    public class BindingsController : ApiController {
        private IRepository repo;

        public BindingsController(IRepository repoArg) {
            repo = repoArg;
        }

        [HttpGet]
        [HttpPost]
        public async Task<IHttpActionResult> SumNumbers() {
            if (Request.Content.IsFormData()) {
                NameValueCollection jqData = await Request.Content.ReadAsFormDataAsync();
                int firstValue, secondValue;
                if (TryGetValues(jqData, "first", "second", out firstValue,
                    out secondValue)) {
                    return Ok(firstValue + secondValue);
                } else if (TryGetValues(jqData, "value1", "value2", out firstValue,
                    out secondValue)) {
                    return Ok(firstValue - secondValue);
                }
            }
            return StatusCode(HttpStatusCode.BadRequest);
        }

        private bool TryGetValues(NameValueCollection data, string key1,
                string key2, out int val1, out int val2) {
            string val1string, val2string;
            val1 = val2 = 0;
            return (val1string = data[key1]) != null
                && int.TryParse(val1string, out val1)
                && (val2string = data[key2]) != null
                && int.TryParse(val2string, out val2);
        }
    }
}
