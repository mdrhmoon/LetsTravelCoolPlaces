namespace LetsTravelCoolPlaces.Services
{
    public interface ITemperatureService
    {
        Task<List<District>> GetCoolestDistricts();
        Task<List<TemperatureDistrict>> GetTemperatureForAllDistrict();
        Task<List<TemperatureDistrict>> GetTemperatureFromApi(string url, string latitude, string longitude);
        Task<string> GetTravelPossibility(string currentDistrictId, string destinationDistrictId, string date);
    }
}