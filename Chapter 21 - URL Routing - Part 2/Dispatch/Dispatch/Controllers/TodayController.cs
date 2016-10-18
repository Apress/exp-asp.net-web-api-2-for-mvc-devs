using System;
using System.Web.Http;
using Dispatch.Infrastructure;

namespace Dispatch.Controllers {

    [RoutePrefix("api/today")]
    [Route("{action=DayOfWeek}")]
    [UserAgentConstraintRoute("{action=DayOfWeek}/{day:specval(2)}")]
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
