
namespace LetsTravelCoolPlaces.API.Middlewares;

public class GlobalExceptionsHandler : IMiddleware
{
    public async Task InvokeAsync(HttpContext context, RequestDelegate next)
    {
        try
        {
            await next(context);
        }
        catch (Exception ex)
        {
            await CreateErrorResponse(ex, context);
        }
    }

    private async Task CreateErrorResponse(Exception ex, HttpContext context)
    {
        string message = GetDetailMessage(ex);
        ResponseDto response = new() { Status = "ERROR", Message = GetDetailMessage(ex) };
        Log.Error(message);

        context.Response.StatusCode = StatusCodes.Status200OK;
        context.Response.ContentType = "application/json";
        await context.Response.WriteAsJsonAsync(response);
    }

    private string GetDetailMessage(Exception error)
    {
        var errorDetails = new StringBuilder();
        errorDetails.Append($"Error: {error.Message}");

        if (error.InnerException is not null) errorDetails.Append($" ErrorInDetails: {error.InnerException.Message}");

        Log.Error(errorDetails.ToString());
        return errorDetails.ToString();
    }
}
