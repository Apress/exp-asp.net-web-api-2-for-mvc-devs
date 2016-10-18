using System.Web.Mvc;
using PartyInvites.Models;
using System.Linq;

namespace PartyInvites.Controllers {
    public class HomeController : Controller {

        public ActionResult Index() {
            return View();
        }

        public ActionResult Rsvp() {
            return View();
        }

        [HttpPost]
        public ActionResult Rsvp(GuestResponse response) {
            if (ModelState.IsValid) {
                Repository.Add(response);
                return View("Thanks", response);
            } else {
                return View();
            }
        }

        [ChildActionOnly]
        public ActionResult Attendees() {
            return View(Repository.Responses.Where(x => x.WillAttend == true));
        }
    }
}
