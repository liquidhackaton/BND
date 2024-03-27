using Microsoft.EntityFrameworkCore; // Add this for DbContext
using System.Linq;
using Tests;

public static class SeedDataTests
{
    public static void Initialize(DbContext context)
    {
        context.Database.EnsureCreated();

        // Check if data already exists
        if (context.Customers.Any() || context.Products.Any())
        {
            return; // Database has already been seeded
        }

        // Seed Customers
        var customers = new Customer[]
        {
            new Customer { FirstName = "John", LastName = "Doe", Email = "john@example.com" },
            new Customer { FirstName = "Jane", LastName = "Smith", Email = "jane@example.com" }
        };
        context.Customers.AddRange(customers);
        context.SaveChanges();

        // Seed products
        var products = new Product[]
        {
            new Product { ProductType = "Savings", ExternalAccount = "NL12345678901234567890" },
            new Product { ProductType = "Retirement", ExternalAccount = "NL09876543210987654321" }
        };
        context.Products.AddRange(products);
        context.SaveChanges();
    }
}
