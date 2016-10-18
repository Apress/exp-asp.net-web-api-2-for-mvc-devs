using System.Web.Mvc;
using ExampleApp.Models;

namespace ExampleApp.Controllers {
    public class HomeController : Controller {
        IRepository repo;

        public HomeController(IRepository repoImpl) {
            repo = repoImpl;
        }

        public ActionResult Index() {
            return View(repo.Products);
        }

        public ActionResult Formats() {
            return View();
        }

        public ActionResult Bindings() {
            return View();
        }
    }
}
