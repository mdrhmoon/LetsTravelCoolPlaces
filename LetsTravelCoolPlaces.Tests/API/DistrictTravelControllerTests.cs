namespace LetsTravelCoolPlaces.Tests.API;

public class DistrictTravelControllerTests
{
    public DistrictTravelControllerTests()
    {
        
    }

    [Fact]
    public async Task GetDistricts_ShouldHave_OkStatus()
    {
        // Arrange
        var con = new DistrictTravelController();

        // Act
        var result = await con.GetDistricts();

        // Assert
        Assert.Equal(((OkObjectResult)result).StatusCode, 200);
    }
}
