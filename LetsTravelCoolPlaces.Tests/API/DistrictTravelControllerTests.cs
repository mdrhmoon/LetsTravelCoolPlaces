namespace LetsTravelCoolPlaces.Tests.API;

public class DistrictTravelControllerTests
{
    private readonly DistrictTravelController? sut = null;
    private readonly ITemperatureService? temperatureService = null;

    public DistrictTravelControllerTests()
    {
        temperatureService = Substitute.For<ITemperatureService>();

        sut = new DistrictTravelController(temperatureService);
    }

    //[Fact]
    //public async Task GetDistricts_ShouldReturnOkResponse()
    //{
    //    // Arrange
    //    // Act
    //    var result = await sut.GetDistricts();

    //    // Assert
    //    ((OkObjectResult)result).StatusCode.Should().Be(200);
    //}

    //[Fact]
    //public async Task GetDistricts_ShouldReturnListOfDistricts()
    //{
    //    // Arrange
    //    var mockData = new DistrictMock().GetDistrict();
    //    districtService.GetDistrictsFromApi().Returns(mockData);

    //    // Act
    //    var result = await sut.GetDistricts();

    //    // Assert
    //    ((OkObjectResult)result).StatusCode.Should().Be(200);
    //    ((OkObjectResult)result).Value.Should().NotBeNull();
    //    ((IList<District>)((OkObjectResult)result).Value).Should().HaveCount(c => c >= 2);
    //}

    //[Fact]
    //public async Task GetCoolestDistricts_ShouldReturnOkResponse()
    //{
    //    // Arrange
    //    // Act
    //    await sut!.GetCoolestDistricts().GetResult(out ResponseDto ResponseDto, out OkObjectResult Result);

    //    // Assert
    //    Result.StatusCode.Should().Be(200);
    //}

    [Fact]
    public async Task GetCoolestDistricts_ListOfCoolestDistrict()
    {
        // Arrange
        var mockData = new DistrictMock().GetDistrict();
        temperatureService!.GetCoolestDistricts()!.Returns(mockData);
        // Act
        await sut!.GetCoolestDistricts().GetResult(out ResponseDto ResponseDto, out OkObjectResult Result);

        // Assert
        Result.StatusCode.Should().Be(200);
        ResponseDto.Should().NotBeNull();
        ((List<District>)ResponseDto.Data!).Should().HaveCount(c => c >= 2);
    }

    [Theory]
    [InlineData("1", "7", "2023-09-14", "Can Travel")]
    [InlineData("7", "1", "2023-09-15", "Can Travel")]
    [InlineData("7", "3", "2023-09-16", "Can Travel")]
    [InlineData("7", "3", "2023-09-21", "Can't Travel")]
    public async Task GetTravelPossibility_ShouldReturnOkResponse(string currentId, string destinationId, string date, string expectedResult)
    {
        // Arrange
        temperatureService!.GetTravelPossibility(currentId, destinationId, date).Returns(expectedResult);
        // Act
        await sut!.GetTravelPossibility(currentId, destinationId, date).GetResult(out ResponseDto ResponseDto, out OkObjectResult Result);

        // Assert
        Result.StatusCode.Should().Be(200);
        ResponseDto.Data.Should().NotBeNull();
        ((string)ResponseDto.Data!).Should().Match(expectedResult);
    }

    [Theory]
    [InlineData("1", "7", "2023-09-14", "Can Travel")]
    [InlineData("7", "1", "2023-09-15", "Can Travel")]
    [InlineData("7", "3", "2023-09-16", "Can Travel")]
    [InlineData("7", "3", "2023-09-21", "Can't Travel")]
    public async Task GetTravelPossibility_ShouldReturnCannotTravel(string currentId, string destinationId, string date, string expectedResult)
    {
        // Arrange
        temperatureService!.GetTravelPossibility(currentId, destinationId, date).Returns(expectedResult);
        // Act
        await sut!.GetTravelPossibility(currentId, destinationId, date).GetResult(out ResponseDto ResponseDto, out OkObjectResult Result);

        // Assert
        Result.StatusCode.Should().Be(200);
        ResponseDto.Data.Should().NotBeNull();
        ((string)ResponseDto.Data!).Should().Match(expectedResult);
    }
}