namespace LetsTravelCoolPlaces.Services.Utility;

public static class Formula
{
    public static string GetTravelPossibility(double SourceTemperature, double DestinationTemperature) => (SourceTemperature > DestinationTemperature) ? Messages.CanTravel : Messages.CanNotTravel;
}