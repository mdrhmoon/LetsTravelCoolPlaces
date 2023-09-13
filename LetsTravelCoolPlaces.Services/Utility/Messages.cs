namespace LetsTravelCoolPlaces.Services.Utility;

public static class Messages
{
    public static string CanTravel = $"Can travel";
    public static string CanNotTravel = $"Can't travel";
    public static string CoolestDistrictsNotFound() => $"Coolest districts not found.";
    public static string DestinationLocationTemperatureNotFound() => $"Destination location temperature not found.";
    public static string CurrentLocationTemperatureNotFound() => $"Current location temperature not found.";
    public static string DestinationLocationNotFound() => $"Destination location not found.";
    public static string CurrentLocationNotFound() => $"Current location not found.";
    public static string DistrictsNotFound() => $"Districts not found.";
    public static string FailedToGenerateCoolestDistricts() => $"Failed to generate coolest districts";
    public static string TemperatureNotFound() => $"Temperature not found.";
    public static string APIFailedMessage(HttpStatusCode StatusCode, string? Message) => $"API returned with Status Code: {(int)StatusCode} and Message: {Message ?? ""}";
    public static string InvalidDateMessage(DateTime StartDate, DateTime EndDate) => $"Invalid date. Date must be between {StartDate.ToString(Constants.DATE_FORMAT)} and {EndDate.ToString(Constants.DATE_FORMAT)}";
    public static string InvalidDateMessage(DateTime StartDate, string EndDate) => $"Invalid date. Date must be between {StartDate.ToString(Constants.DATE_FORMAT)} and {EndDate}";
    public static string InvalidDateMessage(string StartDate, DateTime EndDate) => $"Invalid date. Date must be between {StartDate} and {EndDate.ToString(Constants.DATE_FORMAT)}";
    public static string InvalidDateMessage(string StartDate, string EndDate) => $"Invalid date. Date must be between {StartDate} and {EndDate}";
}
