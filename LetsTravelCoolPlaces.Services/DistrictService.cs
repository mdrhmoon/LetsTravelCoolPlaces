namespace LetsTravelCoolPlaces.Services;

public class DistrictService : IDistrictService
{
    private readonly IHttpService<List<District>> _httpService;

    public DistrictService()
    {
        _httpService = new HttpService<List<District>>();
    }

    public async Task<List<District>?> GetDistrictsFromApi()
    {
        string url = Urls.GetDistrictUrl();
        var districts = await _httpService.GetAsync(url);

        return districts;
    }
}
