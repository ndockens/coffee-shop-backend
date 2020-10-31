using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;

namespace CoffeeShop.API.Models
{
    public class CoffeeShopContext : DbContext
    {
        public CoffeeShopContext(DbContextOptions<CoffeeShopContext> options)
            : base(options)
        {
            // For now, use pre-populated data
            InitializeDatabase();
        }

        private async void InitializeDatabase()
        {
            var products = new List<Product>
            {
                new Product { Id = 1, Name = "Drip Coffee", CategoryId = 1 },
                new Product { Id = 2, Name = "Latte", CategoryId = 2 },
                new Product { Id = 3, Name = "Cappuccino", CategoryId = 2 },
                new Product { Id = 4, Name = "Mocha", CategoryId = 2 },
                new Product { Id = 5, Name = "Smoothie", CategoryId = 3 }
            };

            // Only initialize if data is not already present
            if (!(await Products.AnyAsync()))
            {
                Products.AddRange(products);
                SaveChanges();
            }
        }

        public DbSet<Product> Products { get; set; }
        public DbSet<Category> Categories { get; set; }
    }
}