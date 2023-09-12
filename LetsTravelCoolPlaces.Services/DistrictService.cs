namespace LetsTravelCoolPlaces.Services;

public class DistrictService : IDistrictService
{
    private readonly IHttpService<DistrictListModel> _httpService;

    public DistrictService()
    {
        _httpService = new HttpService<DistrictListModel>();
    }

    public async Task<List<District>> GetDistrictsFromApi()
    {
        string url = Urls.GetDistrictUrl();
        var districtsModel = await _httpService.GetAsync(url);

        return districtsModel!.Districts;
    }
}
