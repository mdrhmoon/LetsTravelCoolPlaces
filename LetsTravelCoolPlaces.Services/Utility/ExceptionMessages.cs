namespace LetsTravelCoolPlaces.Services.Utility;

public static class ExceptionMessages
{
    public static string InvalidDateMessage(DateTime StartDate, DateTime EndDate) => $"Invalid date. Date must be between {StartDate.ToString(Constants.DATE_FORMAT)} and {EndDate.ToString(Constants.DATE_FORMAT)}";
    public static string InvalidDateMessage(DateTime StartDate, string EndDate) => $"Invalid date. Date must be between {StartDate.ToString(Constants.DATE_FORMAT)} and {EndDate}";
    public static string InvalidDateMessage(string StartDate, DateTime EndDate) => $"Invalid date. Date must be between {StartDate} and {EndDate.ToString(Constants.DATE_FORMAT)}";
    public static string InvalidDateMessage(string StartDate, string EndDate) => $"Invalid date. Date must be between {StartDate} and {EndDate}";
}
