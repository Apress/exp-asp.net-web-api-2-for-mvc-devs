using System.ComponentModel.DataAnnotations;

namespace SportsStore.Models {

    public class Product {
        public int Id { get; set; }
        [Required]
        public string Name { get; set; }
        [Required]
        public string Description { get; set; }
        [Required]
        [Range(1, 100000)]
        public decimal Price { get; set; }
        [Required]
        public string Category { get; set; }
    }
}
