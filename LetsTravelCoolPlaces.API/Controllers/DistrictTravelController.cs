namespace LetsTravelCoolPlaces.API.Controllers;

[Route("api/district/travel")]
[ApiController]
public class DistrictTravelController : BaseController
{
    private readonly IDistrictService _districtService;
    private readonly ITemperatureService _temperatureService;

    public DistrictTravelController(ITemperatureService temperatureService, IDistrictService districtService)
    {
        _districtService = districtService;
        _temperatureService = temperatureService;
    }

    [HttpGet("coolest")]
    public async Task<IActionResult> GetCoolestDistricts()
    {
        return GetResponse(await _temperatureService.GetCoolestDistricts());
    }

    [HttpGet("possibility/{currentId}/{destinationId}/{date}")]
    public async Task<IActionResult> GetTravelPossibility(string currentId, string destinationId, string date)
    {
        return GetResponse(await _temperatureService.GetTravelPossibility(currentId, destinationId, date));
    }
}