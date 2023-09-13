namespace LetsTravelCoolPlaces.Services;

public interface ITemperatureService
{
    Task<List<District>> GetCoolestDistricts();
    Task<string> GetTravelPossibility(string currentDistrictId, string destinationDistrictId, string date);
}