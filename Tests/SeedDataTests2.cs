using Microsoft.EntityFrameworkCore;
using External.Services.Movements;
using Internal.Services.Movements;
using Tests;

public class SeedDataTests
{
    [Fact]
    public void Initialize_CreatesDatabaseSchemaIfNotExists()
    {
        // Arrange
        var dbContext = CreateDbContext(); // Create a mock or in-memory database context

        // Act
        SeedData.Initialize(dbContext);

        // Assert
        Assert.True(dbContext.Database.CanConnect()); // Check if the database can be connected (i.e., schema exists)
    }

    [Fact]
    public void Initialize_SeedCustomersIntoDatabase()
    {
        // Arrange
        var dbContext = CreateDbContext(); // Create a mock or in-memory database context

        // Act
        SeedData.Initialize(dbContext);

        // Assert
        Assert.Equal(2, dbContext.Customers.Count()); // Verify that two customers are seeded into the database
    }

    [Fact]
    public void Initialize_SeedProductsIntoDatabase()
    {
        // Arrange
        var dbContext = CreateDbContext(); // Create a mock or in-memory database context

        // Act
        SeedData.Initialize(dbContext);

        // Assert
        Assert.Equal(2, dbContext.Products.Count()); // Verify that two products are seeded into the database
    }

    // Helper method to create a mock or in-memory database context
    private YourDbContext CreateDbContext()
    {
        // Use an in-memory database provider for testing
        var options = new DbContextOptionsBuilder<YourDbContext>()
            .UseInMemoryDatabase(databaseName: "TestDatabase")
            .Options;

        return new YourDbContext(options);
    }
}
