namespace LetsTravelCoolPlaces.API.Controllers;

[Route("api/district/travel")]
[ApiController]
public class DistrictTravelController : ControllerBase
{
    [HttpGet]
    public async Task<IActionResult> GetDistricts()
    {
        await Task.Delay(1000);
        return Ok("Rahat");
    }
}