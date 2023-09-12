namespace LetsTravelCoolPlaces.API.Controllers;

[Route("api/district/travel")]
[ApiController]
public class DistrictTravelController : ControllerBase
{
    private readonly IDistrictService _districtService;
    private readonly ITemperatureService _temperatureService;

    public DistrictTravelController(ITemperatureService temperatureService, IDistrictService districtService)
    {
        _districtService = districtService;
        _temperatureService = temperatureService;
    }

    [HttpGet]
    public async Task<IActionResult> GetDistricts()
    {
        return Ok(await _districtService.GetDistrictsFromApi());
    }

    [HttpGet("coolest")]
    public async Task<IActionResult> GetCoolestDistricts()
    {
        return Ok(await _temperatureService.GetCoolestDistricts());
    }

    [HttpGet("possibility/{currentId}/{destinationId}/{date}")]
    public async Task<IActionResult> GetTravelPossibility(string currentId, string destinationId, string date)
    {
        return Ok(await _temperatureService.GetTravelPossibility(currentId, destinationId, date));
    }
}