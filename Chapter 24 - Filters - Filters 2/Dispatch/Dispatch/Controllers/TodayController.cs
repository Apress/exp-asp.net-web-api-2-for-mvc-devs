using System;
using System.Web.Http;

namespace Dispatch.Controllers {

    public class TodayController : ApiController {

        [HttpGet]
        public string DayOfWeek() {
            return DateTime.Now.ToString("dddd");
        }

        [HttpGet]
        public string DayOfWeek(int day) {
            return Enum.GetValues(typeof(DayOfWeek)).GetValue(day).ToString();
        }

        [HttpGet]
        public int DayNumber() {
            return DateTime.Now.Day;
        }
    }
}
