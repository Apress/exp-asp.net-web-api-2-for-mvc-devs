using System.Collections.Generic;
using System.Linq;
using System.Web.Http;
using Dispatch.Infrastructure;
using Dispatch.Models;

namespace Dispatch.Controllers {

    [Time]
    public class ProductsController : ApiController {
        private static List<Product> products = new List<Product> { 
                new Product {ProductID = 1, Name = "Kayak", Price = 275M },
                new Product {ProductID = 2, Name = "Lifejacket", Price = 48.95M },
                new Product {ProductID = 3, Name = "Soccer Ball", Price = 19.50M },
                new Product {ProductID = 4, Name = "Thinking Cap", Price = 16M },
            };

        [Counter(3)]
        public IEnumerable<Product> Get() {
            return products;
        }

        [CustomAuthentication]
        public Product Get(int id) {
            return products.Where(x => x.ProductID == id).FirstOrDefault();
        }

        public Product Post(Product product) {
            product.ProductID = products.Count + 1;
            products.Add(product);
            return product;
        }
    }
}