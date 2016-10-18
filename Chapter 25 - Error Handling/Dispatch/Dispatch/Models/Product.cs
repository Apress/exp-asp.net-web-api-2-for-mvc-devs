using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Web.Http;

namespace Dispatch.Models {

    public class Product {

        [HttpBindNever]
        public int ProductID { get; set; }

        [Required]
        public string Name { get; set; }

        [Range(20, 500)]
        public decimal Price { get; set; }
    }
}
