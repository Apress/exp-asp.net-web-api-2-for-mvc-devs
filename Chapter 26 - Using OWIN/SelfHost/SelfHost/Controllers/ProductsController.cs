using SelfHost.Models;
using System.Collections.Generic;
using System.Linq;
using System.Web.Http;

namespace SelfHost.Controllers {
    public class ProductsController : ApiController {

        public IEnumerable<Product> GetProducts() {
            return Repository.Current.Products;
        }

        public Product GetProduct(int id) {
            return Repository.Current.Products.Where(p => p.ProductID == id).FirstOrDefault();
        }

        public Product PostProduct(Product product) {
            return Repository.Current.SaveProduct(product);
        }

        public Product DeleteProduct(int id) {
            return Repository.Current.DeleteProduct(id);
        }
    }
}
