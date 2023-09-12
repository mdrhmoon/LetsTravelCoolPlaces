namespace LetsTravelCoolPlaces.Tests.Service;

public class DistrictServiceTests
{
    public DistrictServiceTests()
    {
        
    }

    [Fact]
    public async Task GetDistricts_ShouldHave_DistrictsList()
    {
        // Arrange
        var service = new DistrictService();

        // Act
        var result = await service.GetDistrictsFromApi();

        // Assert
        Assert.Equal("1", result?.Where(x => x.Id == "1").Select(x => x.Id).First());
    }
}
