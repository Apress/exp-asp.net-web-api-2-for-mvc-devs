using System;
using System.Web.Http;
using Dispatch.Infrastructure;

namespace Dispatch.Controllers {

    [CustomControllerConfig]
    public class CustomController : ApiController {

        [AcceptVerbs("GET", "HEAD")]
        public string DayOfWeek() {
            return DateTime.Now.ToString("dddd");
        }

        [HttpGet]
        [HttpHead]
        [CustomActionFilter]
        public string DayOfWeek(int day) {
            return Enum.GetValues(typeof(DayOfWeek)).GetValue(day).ToString();
        }

        [HttpGet]
        public int DayNumber() {
            return DateTime.Now.Day;
        }
    }
}
