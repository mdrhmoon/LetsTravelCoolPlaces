namespace LetsTravelCoolPlaces.Tests.API;

public static class IActionResult_ExtensionMethods
{
    public static Task<IActionResult> GetResult(this Task<IActionResult> Result, out ResponseDto Data, out OkObjectResult Response)
    {
        Response = (OkObjectResult)Result.GetAwaiter().GetResult();
        Data = (ResponseDto)Response.Value!;

        return Result;
    }
}