namespace LetsTravelCoolPlaces.Tests.Service;

public class TemperatureServiceTests
{
    private readonly IDistrictService? districtService = null;
    private readonly IDistributedCache? distributedCache = null;
    private readonly TemperatureService? sut = null;

    public TemperatureServiceTests()
    {
        districtService = Substitute.For<IDistrictService>();
        distributedCache = Substitute.For<IDistributedCache>();
        sut = new TemperatureService(districtService, distributedCache);
    }

    [Fact]
    public async Task GetCoolestDistrict_ShouldHave_CoolestDistrictList()
    {
        // Arrange
        var mockData = new DistrictMock().GetDistrict();
        districtService!.GetDistricts().Returns(mockData);

        // Act
        var result = await sut!.GetCoolestDistricts();

        // Assert
        result.Should().NotBeNull();
        result.Should().HaveCount(c => c >= 2);
    }

    [Theory]
    [InlineData("19", "20", "2024-02-02", "Can travel")]
    [InlineData("20", "19", "2024-02-04", "Can't travel")]
    [InlineData("26", "27", "2024-02-03", "Can travel")]
    [InlineData("28", "19", "2024-02-01", "Can't travel")]
    public async Task GetTravelPossibility_ShouldHave_String(string CurrentDistrictId, string DestinationDistrictId, string Date, string Result)
    {
        // Arrange
        var mockData = new DistrictMock().GetDistrict();
        districtService!.GetDistricts().Returns(mockData);
        districtService.GetDistrictById(CurrentDistrictId).Returns(mockData!.Where(x => x.Id == CurrentDistrictId).FirstOrDefault());

        // Act
        var result = await sut!.GetTravelPossibility(CurrentDistrictId, DestinationDistrictId, Date);

        // Assert
        result.Should().NotBeNull();
        result.Should().Be(Result);
    }

    [Theory]
    [InlineData("19", "20", "2023-02-02")]
    public async Task GetTravelPossibility_ShouldHave_ExceptionMessage_InvalidDate(string CurrentDistrictId, string DestinationDistrictId, string Date)
    {
        // Arrange
        var mockData = new DistrictMock().GetDistrict();
        districtService!.GetDistricts().Returns(mockData);
        districtService.GetDistrictById(CurrentDistrictId).Returns(mockData!.Where(x => x.Id == CurrentDistrictId).FirstOrDefault());

        // Act
        try
        {
            var result = await sut!.GetTravelPossibility(CurrentDistrictId, DestinationDistrictId, Date);
        }
        catch (Exception ex)
        {
            Messages.InvalidDateMessage(DateTime.Now, DateTime.Now.AddDays(6)).Should().Be(ex.Message);
        }
    }
}