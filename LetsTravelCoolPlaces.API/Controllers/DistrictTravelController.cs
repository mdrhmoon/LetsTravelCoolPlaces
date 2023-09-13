namespace LetsTravelCoolPlaces.API.Controllers;

[Route("api/district/travel")]
[ApiController]
public class DistrictTravelController : BaseController
{
    private readonly ITemperatureService _temperatureService;

    public DistrictTravelController(ITemperatureService temperatureService)
    {
        _temperatureService = temperatureService;
    }

    [HttpGet("coolest")]
    public async Task<IActionResult> GetCoolestDistricts()
    {
        try
        {
            return GetResponse(await _temperatureService.GetCoolestDistricts());
        }
        catch (Exception ex)
        {
            return GetResponse(ex);
        }
    }

    [HttpGet("possibility/{currentId}/{destinationId}/{date}")]
    public async Task<IActionResult> GetTravelPossibility(string currentId, string destinationId, string date)
    {
        try
        {
            return GetResponse(await _temperatureService.GetTravelPossibility(currentId, destinationId, date));
        }
        catch (Exception ex)
        {
            return GetResponse(ex);
        }
    }
}