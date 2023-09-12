namespace LetsTravelCoolPlaces.Services
{
    public interface IDistrictService
    {
        Task<List<District>?> GetDistrictsFromApi();
    }
}