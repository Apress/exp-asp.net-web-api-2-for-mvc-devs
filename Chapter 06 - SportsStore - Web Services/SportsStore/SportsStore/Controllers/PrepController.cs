using System.Threading.Tasks;
using System.Web.Mvc;
using SportsStore.Models;
using SportsStore.Infrastructure.Identity;
using Microsoft.Owin.Security;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.Owin;
using System.Web;
using System.Security.Claims;

namespace SportsStore.Controllers {

    public class PrepController : Controller {
        IRepository repo;

        public PrepController() {
            repo = new ProductRepository();
        }

        public ActionResult Index() {
            return View(repo.Products);
        }

        [Authorize(Roles = "Administrators")]
        public async Task<ActionResult> DeleteProduct(int id) {
            await repo.DeleteProductAsync(id);
            return RedirectToAction("Index");
        }

        [Authorize(Roles = "Administrators")]
        public async Task<ActionResult> SaveProduct(Product product) {
            await repo.SaveProductAsync(product);
            return RedirectToAction("Index");
        }

        public ActionResult Orders() {
            return View(repo.Orders);
        }

        public async Task<ActionResult> DeleteOrder(int id) {
            await repo.DeleteOrderAsync(id);
            return RedirectToAction("Orders");
        }

        public async Task<ActionResult> SaveOrder(Order order) {
            await repo.SaveOrderAsync(order);
            return RedirectToAction("Orders");
        }

        public async Task<ActionResult> SignIn() {
            IAuthenticationManager authMgr = HttpContext.GetOwinContext().Authentication;
            StoreUserManager userMrg =
                HttpContext.GetOwinContext().GetUserManager<StoreUserManager>();

            StoreUser user = await userMrg.FindAsync("Admin", "secret");
            authMgr.SignIn(await userMrg.CreateIdentityAsync(user,
                DefaultAuthenticationTypes.ApplicationCookie));
            return RedirectToAction("Index");
        }

        public ActionResult SignOut() {
            HttpContext.GetOwinContext().Authentication.SignOut();
            return RedirectToAction("Index");
        }
    }
}
