namespace LetsTravelCoolPlaces.Services.Interfaces;

public interface ITemperatureService
{
    Task<List<District>?> GetCoolestDistricts();
    Task<string> GetTravelPossibility(string currentDistrictId, string destinationDistrictId, string date);
}