namespace LetsTravelCoolPlaces.Tests.Service;

public class TemperatureServiceTests
{
    private readonly IDistrictService districtService = null;
    private TemperatureService sut = null;

    public TemperatureServiceTests()
    {
        districtService = Substitute.For<IDistrictService>();
        //sut = new TemperatureService(districtService);
    }

    //[Theory]
    //[InlineData("12.356646", "45.12646", DateTime.Now.ToString("yyyy-MM-dd"), DateTime.Now.AddDays(1).ToString("yyyy-MM-dd"))]
    //public async Task GetTemperatureFromApi_ShouldHave_TemperatureDistrictList(string latitude, string longitude, string startDate, string endDate)
    //{
    //    // Arrange
    //    string url = Urls
    //    var mockData = await new DistrictMock().GetTemperatureDistrict();
    //    // Act
    //    var result = await sut.GetTemperatureFromApi(url, latitude, longitude);
    //    // Assert
    //    result.Should().NotBeNull();
    //    result.Should().HaveCount(c => c >= 3);
    //}
}
