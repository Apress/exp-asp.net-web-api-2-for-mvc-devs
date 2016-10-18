using System.Web.Http;
using ExampleApp.Models;
using System.Web.Http.ValueProviders;
using ExampleApp.Infrastructure;

namespace ExampleApp.Controllers {
    public class BindingsController : ApiController {
        private IRepository repo;

        public BindingsController(IRepository repoArg) {
            repo = repoArg;
        }

        [HttpGet]
        [HttpPost]
        public string SumNumbers(Numbers numbers, string accept) {
            return string.Format("{0} (Accept: {1})",
                numbers.First + numbers.Second, accept);
        }
    }
}
