using System.Collections.Generic;
using System.Data.Entity;

namespace SportsStore.Models {

    public class ProductDbInitializer : CreateDatabaseIfNotExists<ProductDbContext> {

        protected override void Seed(ProductDbContext context) {

            new List<Product> {
                new Product() { Name = "Kayak", Description = "A boat for one person", 
                    Category = "Watersports", Price = 275m }, 
                new Product() { Name = "Lifejacket", 
                    Description = "Protective and fashionable", 
                    Category = "Watersports", Price = 48.95m },
                new Product() { Name = "Soccer Ball", 
                    Description = "FIFA-approved size and weight", 
                    Category = "Soccer", Price = 19.50m },
                new Product() {
                    Name = "Corner Flags", 
                    Description = "Give your playing field a professional touch", 
                    Category = "Soccer", Price = 34.95m },
                new Product() { Name = "Stadium", 
                    Description = "Flat-packed 35,000-seat stadium", 
                    Category = "Soccer", Price = 79500m },
                new Product() { Name = "Thinking Cap", 
                    Description = "Improve your brain efficiency by 75%", 
                    Category = "Chess", Price = 16m },
                new Product() { Name = "Unsteady Chair", 
                    Description = "Secretly give your opponent a disadvantage", 
                    Category = "Chess", Price = 29.95m },
                new Product() { Name = "Human Chess Board", 
                    Description = "A fun game for the family", 
                    Category = "Chess", Price = 75m },
                new Product() { Name = "Bling-Bling King", 
                    Description = "Gold-plated, diamond-studded King", 
                    Category = "Chess", Price = 1200m },
            }.ForEach(product => context.Products.Add(product));

            context.SaveChanges();

            new List<Order> {
                new Order() { Customer = "Alice Smith", TotalCost = 68.45m, 
                    Lines = new List<OrderLine> {
                        new OrderLine() { ProductId = 2, Count = 2},
                        new OrderLine() { ProductId = 3, Count = 1},
                    }},
                new Order() { Customer = "Peter Jones", TotalCost = 79791m, 
                    Lines = new List<OrderLine> {
                        new OrderLine() { ProductId = 5, Count = 1},
                        new OrderLine() { ProductId = 6, Count = 3},
                        new OrderLine() { ProductId = 1, Count = 3},
                   }}
            }.ForEach(order => context.Orders.Add(order));

            context.SaveChanges();
        }
    }
}