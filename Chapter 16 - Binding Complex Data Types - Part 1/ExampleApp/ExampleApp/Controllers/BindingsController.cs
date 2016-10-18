using System.Web.Http;
using System.Web.Http.ModelBinding;
using ExampleApp.Models;
using ExampleApp.Infrastructure;

namespace ExampleApp.Controllers {
    public class BindingsController : ApiController {
        private IRepository repo;

        public BindingsController(IRepository repoArg) {
            repo = repoArg;
        }

        [HttpGet]
        [HttpPost]
        public string SumNumbers(Numbers numbers) {
            var result = numbers.Op.Add ? numbers.First + numbers.Second
                : numbers.First - numbers.Second;
            return string.Format("{0}", numbers.Op.Double ? result * 2 : result);
        }
    }
}
