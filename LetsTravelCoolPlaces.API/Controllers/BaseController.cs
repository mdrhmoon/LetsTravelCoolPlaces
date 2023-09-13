namespace LetsTravelCoolPlaces.API.Controllers;

public class BaseController : ControllerBase
{
    public BaseController()
    {
        
    }

    public IActionResult GetResponse(object? data) => Ok(new ResponseDto { Data = data });

    public IActionResult GetResponse(Exception error) => BadRequest(new ResponseDto { Status = "ERROR", Message = GetDetailMessage(error) });

    private string GetDetailMessage(Exception error)
    {
        var errorDetails = new StringBuilder();
        errorDetails.Append($"Error: {error.Message} ");

        if(error.InnerException is not null) errorDetails.Append($" ErrorInDetails: {error.InnerException.Message}");

        return errorDetails.ToString();
    }
}
