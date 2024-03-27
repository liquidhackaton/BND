using Xunit;
using Moq;
using System.Collections.Generic;
using External.Services.Movements;
using Internal.Services.Movements;
using Tests;

public class ThirdPartyApiIntegrationTests
{
    [Fact]
    public void GetMovementsFromThirdPartyApi_ReturnsExpectedMovements()
    {
        // Arrange
        var expectedMovements = GetSampleMovements(); // Get sample movements for testing
        var thirdPartyApiProxyMock = new Mock<IThirdPartyApiProxy>();
        thirdPartyApiProxyMock.Setup(proxy => proxy.GetMovements(
            It.IsAny<int>(), // pageNumber
            It.IsAny<int>(), // pageSize
            It.IsAny<string>(), // account
            It.IsAny<EnumMovementType?>(), // movementType
            It.IsAny<string>(), // accountFrom
            It.IsAny<string>(), // accountTo
            It.IsAny<decimal?>(), // amountFrom
            It.IsAny<decimal?>() // amountTo
            )).Returns(expectedMovements);

        var movementManager = new MovementManager(thirdPartyApiProxyMock.Object);

        // Act
        var movements = movementManager.GetMovementsFromThirdPartyApi(1, 10, null, null, null, null, null, null);

        // Assert
        Assert.Equal(expectedMovements, movements);
    }

    // Test Pagination

    [Fact]
public void GetMovementsFromThirdPartyApi_UsesPaginationParameters()
{
    // Arrange
    var thirdPartyApiProxyMock = new Mock<IThirdPartyApiProxy>();
    var movementManager = new MovementManager(thirdPartyApiProxyMock.Object);

    // Act
    movementManager.GetMovementsFromThirdPartyApi(2, 55, null, null, null, null, null, null);

    // Assert
    thirdPartyApiProxyMock.Verify(proxy => proxy.GetMovements(
        2, // Expected pageNumber
        55, // Expected pageSize
        It.IsAny<string>(), // account
        It.IsAny<EnumMovementType?>(), // movementType
        It.IsAny<string>(), // accountFrom
        It.IsAny<string>(), // accountTo
        It.IsAny<decimal?>(), // amountFrom
        It.IsAny<decimal?>() // amountTo
    ), Times.Once);
}


    // Test Filtering by Amount

    [Fact]
public void GetMovementsFromThirdPartyApi_UsesAmountFilter()
{
    // Arrange
    var thirdPartyApiProxyMock = new Mock<IThirdPartyApiProxy>();
    var movementManager = new MovementManager(thirdPartyApiProxyMock.Object);

    // Act
    movementManager.GetMovementsFromThirdPartyApi(1, 10, null, null, null, null, 100, 500);

    // Assert
    thirdPartyApiProxyMock.Verify(proxy => proxy.GetMovements(
        1, // pageNumber
        10, // pageSize
        It.IsAny<string>(), // account
        It.IsAny<EnumMovementType?>(), // movementType
        It.IsAny<string>(), // accountFrom
        It.IsAny<string>(), // accountTo
        100, // Expected amountFrom
        500 // Expected amountTo
    ), Times.Once);
}


    // Helper method to generate sample movements for testing
    private IEnumerable<Movement> GetSampleMovements()
    {
        return new List<Movement>
        {
            new Movement { MovementId = 1, Account = "SampleAccount1", MovementType = "Incoming", Amount = 100 },
            new Movement { MovementId = 2, Account = "SampleAccount2", MovementType = "Outgoing", Amount = 50 },
            new Movement { MovementId = 3, Account = "SampleAccount3", MovementType = "FiscalTransfer", Amount = 75 },
            new Movement { MovementId = 4, Account = "SampleAccount4", MovementType = "Interest", Amount = 25 },
            // Add more sample movements as needed
        };
    }
}
