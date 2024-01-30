namespace LetsTravelCoolPlaces.API.Controllers;

public class BaseController : ControllerBase
{
    public BaseController() {}

    protected IActionResult GetResponse(object? data) => Ok(new ResponseDto { Data = data });

    // protected IActionResult GetResponse(Exception error) => BadRequest(new ResponseDto { Status = "ERROR", Message = GetDetailMessage(error) });

    // private string GetDetailMessage(Exception error)
    // {
    //     var errorDetails = new StringBuilder();
    //     errorDetails.Append($"Error: {error.Message}");

    //     if(error.InnerException is not null) errorDetails.Append($" ErrorInDetails: {error.InnerException.Message}");

    //     Log.Error(errorDetails.ToString());
    //     return errorDetails.ToString();
    // }
}