namespace LetsTravelCoolPlaces.Services;

public interface IDistrictService
{
    Task<List<District>?> GetDistricts();
    Task<District?> GetDistrictById(string districtId);
}