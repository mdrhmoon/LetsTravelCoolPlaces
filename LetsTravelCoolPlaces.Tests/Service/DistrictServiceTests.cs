namespace LetsTravelCoolPlaces.Tests.Service;

public class DistrictServiceTests
{
    private readonly DistrictService sut = null;
    private readonly IDistributedCache distributedCache = null;

    public DistrictServiceTests()
    {
        distributedCache = Substitute.For<IDistributedCache>();
        sut = new DistrictService(distributedCache);
    }

    [Fact]
    public async Task GetDistricts_ShouldHave_DistrictsList()
    {
        // Arrange
        var mockData = await new DistrictMock().GetDistrict();

        // Act
        var result = await sut.GetDistricts();

        // Assert
        Assert.Equal("1", result?.Where(x => x.Id == "1").Select(x => x.Id).First());
    }

    [Theory]
    [InlineData("1")]
    [InlineData("2")]
    [InlineData("3")]
    [InlineData("4")]
    public async Task GetDistrictsById_ShouldHave_District(string DistrictId)
    {
        // Act
        var result = await sut.GetDistrictById(DistrictId);

        // Assert
        Assert.Equal(DistrictId, result!.Id);
    }

    [Theory]
    [InlineData("0")]
    [InlineData("101")]
    public async Task GetDistrictsById_ShouldHave_Null(string DistrictId)
    {
        // Act
        var result = await sut.GetDistrictById(DistrictId);

        // Assert
        Assert.Equal(null, result);
    }
}