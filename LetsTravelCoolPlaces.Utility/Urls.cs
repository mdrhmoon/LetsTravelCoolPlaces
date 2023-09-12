namespace LetsTravelCoolPlaces.Utility;

public static class Urls
{
    public static string GetDistrictUrl() => "https://raw.githubusercontent.com/strativ-dev/technical-screening-test/main/bd-districts.json";
    public static string GetTemperatureUrl(double latitude, double longitude, string startDate, string endDate) => $"https://api.open-meteo.com/v1/forecast?latitude={latitude}&longitude={longitude}&hourly=temperature_2m&start_date={startDate}&end_date={endDate}";
}
