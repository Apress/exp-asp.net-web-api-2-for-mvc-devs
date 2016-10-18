using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Web.Http;
using Dispatch.Infrastructure;
using Dispatch.Models;
using System.Net.Http;

namespace Dispatch.Controllers {

    public class ProductsController : ApiController {
        private static List<Product> products = new List<Product> { 
                new Product {ProductID = 1, Name = "Kayak", Price = 275M },
                //new Product {ProductID = 2, Name = "Lifejacket", Price = 48.95M },
                new Product {ProductID = 3, Name = "Soccer Ball", Price = 19.50M },
                new Product {ProductID = 4, Name = "Thinking Cap", Price = 16M },
            };

        public IEnumerable<Product> Get() {
            return products;
        }

        [LogErrors]
        public Product Get(int id) {
            Product product = products.Where(x => x.ProductID == id).FirstOrDefault();
            if (product == null) {
                throw new ArgumentOutOfRangeException("id");
            }
            return product;
        }


        public HttpResponseMessage Post(Product product) {
            if (!ModelState.IsValid) {
                HttpError error = new HttpError(ModelState, false);
                error.Message = "Cannot Add Product";
                error.Add("AvailbleIDs", products.Select(x => x.ProductID));
                return Request.CreateErrorResponse(HttpStatusCode.BadRequest, error);
            }
            product.ProductID = products.Count + 1;
            products.Add(product);
            return Request.CreateResponse(product);
        }

    }
}
