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
        var mockData = await new DistrictMock().GetDistrict();
        districtService!.GetDistricts().Returns(mockData);

        // Act
        var result = await sut!.GetCoolestDistricts();

        // Assert
        result.Should().NotBeNull();
        result.Should().HaveCount(c => c >= 2);
    }

    [Theory]
    [InlineData("1", "2", "2023-09-17", "Can travel")]
    [InlineData("2", "1", "2023-09-18", "Can't travel")]
    [InlineData("1", "2", "2023-09-19", "Can travel")]
    public async Task GetTravelPossibility_ShouldHave_String(string CurrentDistrictId, string DestinationDistrictId, string Date, string Result)
    {
        // Arrange
        var mockData = await new DistrictMock().GetDistrict();
        districtService!.GetDistricts().Returns(mockData);
        districtService.GetDistrictById(CurrentDistrictId).Returns(mockData.Where(x => x.Id == CurrentDistrictId).FirstOrDefault());

        // Act
        var result = await sut!.GetTravelPossibility(CurrentDistrictId, DestinationDistrictId, Date);

        // Assert
        result.Should().NotBeNull();
        result.Should().Be(Result);
    }

    [Theory]
    [InlineData("1", "2", "2023-09-28")]
    public async Task GetTravelPossibility_ShouldHave_ExceptionMessage_InvalidDate(string CurrentDistrictId, string DestinationDistrictId, string Date)
    {
        // Arrange
        var mockData = await new DistrictMock().GetDistrict();
        districtService!.GetDistricts().Returns(mockData);
        districtService.GetDistrictById(CurrentDistrictId).Returns(mockData.Where(x => x.Id == CurrentDistrictId).FirstOrDefault());

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