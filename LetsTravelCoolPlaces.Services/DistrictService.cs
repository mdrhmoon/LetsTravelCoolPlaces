namespace LetsTravelCoolPlaces.Services;

public class DistrictService : IDistrictService
{
    private readonly IHttpService<DistrictListModel> _httpService;
    private readonly IDistributedCache _distributedCache;

    public DistrictService(IDistributedCache DistributedCache)
    {
        _httpService = new HttpService<DistrictListModel>();
        _distributedCache = DistributedCache;
    }

    public async Task<List<District>?> GetDistricts()
    {
        List<District>? districts = await _distributedCache.GetAsync<List<District>>(Cachekeys.DISTRICTS);
        
        if(districts == null)
        {
            districts = await GetDistrictsFromApi();
            if (districts is null) throw new Exception("No District Found.");
            await _distributedCache.SetAsync(Cachekeys.DISTRICTS, districts);
        }

        return districts;
    }

    public async Task<District?> GetDistrictById(string districtId)
    {
        var districts = await GetDistricts();
        return districts!.Where(x => x.Id == districtId).FirstOrDefault();
    }

    private async Task<List<District>?> GetDistrictsFromApi()
    {
        string url = Urls.GetDistrictUrl();
        var districtsModel = await _httpService.GetAsync(url);

        return districtsModel?.Districts.ToList();
    }
}