using Xunit;
using System.Collections.Generic;
using System.Threading.Tasks;
using Moq;
using Tests;
using Internal.Services.Movements.Business;

public class ThirdPartyApiTests
{
    private readonly Mock<IThirdPartyApiService> _apiServiceMock;

    public ThirdPartyApiTests()
    {
        _apiServiceMock = new Mock<IThirdPartyApiService>();
    }

    [Fact]
    public async Task GetMovements_ReturnsExpectedMovements()
    {
        // Arrange
        var expectedMovements = new List<Movement> { /* Add sample movements */ };
        _apiServiceMock.Setup(x => x.GetMovementsAsync()).ReturnsAsync(expectedMovements);

        var manager = new MovementsManager(_apiServiceMock, DBContext);

        // sa initializez Movement manager 

        // Act
        var movements = await manager.GetMovementsAsync();

        // 

        // Assert
        Assert.Equal(expectedMovements, movements);
    }
}
