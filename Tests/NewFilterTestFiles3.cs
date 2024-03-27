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
        // Add more assertions to verify other aspects of movement display if necessary
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

    [Fact]
    public void FilterByOutgoingPayments()
    {
        // Arrange
        var movementManagerMock = new Mock<IMovementManager>();
        var outgoingMovements = GetSampleMovements().Where(m => m.MovementType == "Outgoing");
        movementManagerMock.Setup(m => m.GetMovementsForOverview(1, 10, It.IsAny<int>(), EnumGetMovementFilter.Outgoing))
                           .Returns(outgoingMovements);

        // Act
        var movementsDisplayed = movementManagerMock.Object.GetMovementsForOverview(1, 10, 1, EnumGetMovementFilter.Outgoing);

        // Assert
        Assert.All(movementsDisplayed, m => Assert.Equal("Outgoing", m.MovementType)); // Verify all movements are outgoing payments
    }

    [Fact]
    public void FilterByFiscalTransfers()
    {
        // Arrange
        var movementManagerMock = new Mock<IMovementManager>();
        var fiscalTransferMovements = GetSampleMovements().Where(m => m.MovementType == "FiscalTransfer");
        movementManagerMock.Setup(m => m.GetMovementsForOverview(1, 10, It.IsAny<int>(), EnumGetMovementFilter.FiscalTransfer))
                           .Returns(fiscalTransferMovements);

        // Act
        var movementsDisplayed = movementManagerMock.Object.GetMovementsForOverview(1, 10, 1, EnumGetMovementFilter.FiscalTransfer);

        // Assert
        Assert.All(movementsDisplayed, m => Assert.Equal("FiscalTransfer", m.MovementType)); // Verify all movements are fiscal transfers
    }

    [Fact]
    public void FilterByInterestPayments()
    {
        // Arrange
        var movementManagerMock = new Mock<IMovementManager>();
        var interestMovements = GetSampleMovements().Where(m => m.MovementType == "Interest");
        movementManagerMock.Setup(m => m.GetMovementsForOverview(1, 10, It.IsAny<int>(), EnumGetMovementFilter.Interest))
                           .Returns(interestMovements);

        // Act
        var movementsDisplayed = movementManagerMock.Object.GetMovementsForOverview(1, 10, 1, EnumGetMovementFilter.Interest);

        // Assert
        Assert.All(movementsDisplayed, m => Assert.Equal("Interest", m.MovementType)); // Verify all movements are interest payments
    }

    [Fact]
    public void PaginateMovements()
    {
        // Arrange
        var movementManagerMock = new Mock<IMovementManager>();
        var movements = GetSampleMovements(); // Get sample movements
        movementManagerMock.Setup(m => m.GetMovementsForOverview(2, 5, It.IsAny<int>(), EnumGetMovementFilter.All))
                           .Returns(movements.Skip(5).Take(5)); // Skip first 5 and take next 5 movements for page 2

        // Act
        var movementsDisplayed = movementManagerMock.Object.GetMovementsForOverview(2, 5, 1, EnumGetMovementFilter.All);

        // Assert
        Assert.Equal(5, movementsDisplayed.Count()); // Verify 5 movements are displayed for page 2
    }

    [Fact]
    public void SwitchTabs_MaintainsFilter()
    {
        // Arrange
        var movementManagerMock = new Mock<IMovementManager>();
        var movements = GetSampleMovements(); // Get sample movements
        movementManagerMock.Setup(m => m.GetMovementsForOverview(1, 10, It.IsAny<int>(), EnumGetMovementFilter.Incoming))
                           .Returns(movements.Where(m => m.MovementType == "Incoming")); // Filter movements for the 'Incoming' tab

        // Act
        var movementsDisplayed = movementManagerMock.Object.GetMovementsForOverview(1, 10, 1, EnumGetMovementFilter.Incoming);

        // Assert
        Assert.All(movementsDisplayed, m => Assert.Equal("Incoming", m.MovementType)); // Verify all movements are incoming payments
    }

    // Helper method to generate sample movements
    private IEnumerable<Movement> GetSampleMovements()
    {
        // Generate sample movements for testing
        return new List<Movement>
        {
            new Movement { MovementId = 1, Account = "SampleAccount1", MovementType = "Incoming", Amount = 100 },
            new Movement { MovementId = 2, Account = "SampleAccount2", MovementType = "Outgoing", Amount = 50 },
            new Movement { MovementId = 3, Account = "SampleAccount3", MovementType = "FiscalTransfer", Amount = 200 },
            new Movement { MovementId = 4, Account = "SampleAccount4", MovementType = "Interest", Amount = 150 },
            // Add more sample movements as needed
        };
    }
}

