using System.Collections.Generic;
using System.Web.Http;
using ExampleApp.Models;
using System.Diagnostics;
using System.Web.Http.ModelBinding;

namespace ExampleApp.Controllers {
    public class ProductsController : ApiController {
        IRepository repo;

        public ProductsController(IRepository repoImpl) {
            repo = repoImpl;
        }

        public IEnumerable<Product> GetAll() {
            return repo.Products;
        }

        public void Delete(int id) {
            repo.DeleteProduct(id);
        }

        public IHttpActionResult Post(Product product) {
            if (ModelState.IsValid) {
                repo.SaveProduct(product);
                return Ok();
            } else {
                return BadRequest(ModelState);
            }
        }

    }
}
