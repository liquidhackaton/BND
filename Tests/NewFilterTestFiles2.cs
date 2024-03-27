using Xunit;
using Moq;
using External.Services.Movements;
using Internal.Services.Movements;
using Tests;

public class MovementFilterTests
{
    [Fact]
    public void CanFetchIncomingPayment()
    {
        // Arrange
        var movementManagerMock = new Mock<IMovementManager>();
        var incomingMovements = GetSampleMovements().Where(m => m.MovementType == "Incoming");
        movementManagerMock.Setup(m => m.GetMovementsForOverview(1, 10, It.IsAny<int>(), EnumGetMovementFilter.Incoming))
                           .Returns(incomingMovements);

        // Act
        var fetchedMovements = movementManagerMock.Object.GetMovementsForOverview(1, 10, 1, EnumGetMovementFilter.Incoming);

        // Assert
        Assert.NotEmpty(fetchedMovements); // Ensure that at least one incoming payment is fetched
        Assert.All(fetchedMovements, m => Assert.Equal("Incoming", m.MovementType)); // Verify all fetched movements are incoming payments
    }

    [Fact]
    public void CanFetchFiscalTransfer()
    {
        // Arrange
        var movementManagerMock = new Mock<IMovementManager>();
        var fiscalTransferMovements = GetSampleMovements().Where(m => m.MovementType == "FiscalTransfer");
        movementManagerMock.Setup(m => m.GetMovementsForOverview(1, 10, It.IsAny<int>(), EnumGetMovementFilter.FiscalTransfer))
                           .Returns(fiscalTransferMovements);

        // Act
        var fetchedMovements = movementManagerMock.Object.GetMovementsForOverview(1, 10, 1, EnumGetMovementFilter.FiscalTransfer);

        // Assert
        Assert.NotEmpty(fetchedMovements); // Ensure that at least one fiscal transfer is fetched
        Assert.All(fetchedMovements, m => Assert.Equal("FiscalTransfer", m.MovementType)); // Verify all fetched movements are fiscal transfers
    }

    [Fact]
    public void CanFetchInterestPayment()
    {
        // Arrange
        var movementManagerMock = new Mock<IMovementManager>();
        var interestMovements = GetSampleMovements().Where(m => m.MovementType == "Interest");
        movementManagerMock.Setup(m => m.GetMovementsForOverview(1, 10, It.IsAny<int>(), EnumGetMovementFilter.Interest))
                           .Returns(interestMovements);

        // Act
        var fetchedMovements = movementManagerMock.Object.GetMovementsForOverview(1, 10, 1, EnumGetMovementFilter.Interest);

        // Assert
        Assert.NotEmpty(fetchedMovements); // Ensure that at least one interest payment is fetched
        Assert.All(fetchedMovements, m => Assert.Equal("Interest", m.MovementType)); // Verify all fetched movements are interest payments
    }

    // Helper method to generate sample movements
    private IEnumerable<Movement> GetSampleMovements()
    {
        // Generate sample movements for testing
        return new List<Movement>
        {
            new Movement { MovementId = 1, Account = "SampleAccount1", MovementType = "Incoming", Amount = 100 },
            new Movement { MovementId = 2, Account = "SampleAccount2", MovementType = "FiscalTransfer", Amount = 50 },
            new Movement { MovementId = 3, Account = "SampleAccount3", MovementType = "Interest", Amount = 25 },
            // Add more sample movements as needed
        };
    }
}
