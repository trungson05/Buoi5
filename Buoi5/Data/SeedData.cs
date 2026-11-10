using Buoi5.Models;

namespace Buoi5.Data
{
    public class SeedData
    {
        public static List<Category> Categories = new List<Category>
        {
            new Category { CategoryId = 1, CategoryName = "Electronics" },
            new Category { CategoryId = 2, CategoryName = "Clothing" },
            new Category { CategoryId = 3, CategoryName = "Books" }
        };
        public static List<Product> Products = new List<Product>
        {
            new Product { ProductId = 1, ProductName = "Product1", ProductPrice = 699.99m,ProductDescription="Electronics", CategoryId = 1 },
            new Product { ProductId = 2, ProductName = "Product2", ProductPrice = 699.99m,ProductDescription="Clothing", CategoryId = 2 },
            new Product { ProductId = 3, ProductName = "Product3", ProductPrice = 699.99m,ProductDescription="Books", CategoryId = 3 }
        };
    }
}
