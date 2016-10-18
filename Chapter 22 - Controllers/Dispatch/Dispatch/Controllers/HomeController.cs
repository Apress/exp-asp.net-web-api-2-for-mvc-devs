using System.Web.Mvc;

namespace Dispatch.Controllers {

    public class HomeController : Controller {

        public ActionResult Index() {
            return View();
        }

        public ActionResult Today() {
            return View();
        }
    }
}