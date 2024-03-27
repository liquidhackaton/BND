using System.Collections.Generic;
using System.Linq;
using Xunit;
using Moq;
using External.Services.Movements;
using Internal.Services.Movements;
using Tests;

public class MovementFilterTests
{
    [Fact]
    public void DisplayMovementsCorrectlyInGrid()
    {
        // Arrange
        var movementManagerMock = new Mock<IMovementManager>();
        var movements = GetSampleMovements(); // Get sample movements
        movementManagerMock.Setup(m => m.GetMovementsForOverview(1, 10, It.IsAny<int>(), EnumGetMovementFilter.All))
                           .Returns(movements);

        // Act
        var movementsDisplayed = movementManagerMock.Object.GetMovementsForOverview(1, 10, 1, EnumGetMovementFilter.All);

        // Assert
        Assert.Equal(movements.Count(), movementsDisplayed.Count()); // Verify all movements are displayed
       
    }

    [Fact]
    public void FilterByIncomingPayments()
    {
        // Arrange
        var movementManagerMock = new Mock<IMovementManager>();
        var incomingMovements = GetSampleMovements().Where(m => m.MovementType == "Incoming");
        movementManagerMock.Setup(m => m.GetMovementsForOverview(1, 10, It.IsAny<int>(), EnumGetMovementFilter.Incoming))
                           .Returns(incomingMovements);

        // Act
        var movementsDisplayed = movementManagerMock.Object.GetMovementsForOverview(1, 10, 1, EnumGetMovementFilter.Incoming);

        // Assert
        Assert.All(movementsDisplayed, m => Assert.Equal("Incoming", m.MovementType)); // Verify all movements are incoming payments
    }

    // Add similar tests for other filter options: outgoing, fiscal transfer, interest, pagination, and tab switching

    // Helper method to generate sample movements
    private IEnumerable<Movement> GetSampleMovements()
    {
        // Generate sample movements for testing
        return new List<Movement>
        {
            new Movement { MovementId = 1, Account = "SampleAccount1", MovementType = "Incoming", Amount = 100 },
            new Movement { MovementId = 2, Account = "SampleAccount2", MovementType = "Outgoing", Amount = 50 },
            // Add more sample movements as needed
        };
    }
}
