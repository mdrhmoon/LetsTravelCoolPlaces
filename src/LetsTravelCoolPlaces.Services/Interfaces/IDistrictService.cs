namespace LetsTravelCoolPlaces.Services.Interfaces;

public interface IDistrictService
{
    Task<List<District>?> GetDistricts();
    Task<District?> GetDistrictById(string districtId);
}