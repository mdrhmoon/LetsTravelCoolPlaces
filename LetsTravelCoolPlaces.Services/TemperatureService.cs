namespace LetsTravelCoolPlaces.Services;

public class TemperatureService : ITemperatureService
{
    private readonly IHttpService<Temperature> _httpService;
    private readonly IDistrictService _districtService;
    private readonly IDistributedCache _distributedCache;

    public TemperatureService(IDistrictService DistrictService, IDistributedCache DistributedCache)
    {
        _httpService = new HttpService<Temperature>();
        _districtService = DistrictService;
        _distributedCache = DistributedCache;
    }

    // getting coolest 10 districts
    public async Task<List<District>> GetCoolestDistricts()
    {
        var temperatureDistricts = await GetTemperatureForAllDistrict();

        // generating average temperature for all district
        var avgTemperatureOfDistricts = temperatureDistricts.GroupBy(x => new
        {
            Latitude = x.Latitude,
            Longitutde = x.Longitude
        }).Select(g => new
        {
            Latitude = g.Key.Latitude,
            Longitude = g.Key.Longitutde,
            AvgTemperature = g.Average(x => x.Temperature)
        }).ToList();

        var districts = await _districtService.GetDistricts();
        // taking coolest 10 district
        var coolestDistrictsTemperature = avgTemperatureOfDistricts.OrderBy(x => x.AvgTemperature).Take(10).ToList();

        // filtering coolest district from all district
        var coolestDistrict = districts!.Where(x =>
            coolestDistrictsTemperature.Select(y => y.Latitude).Contains(x.Lat) &&
                coolestDistrictsTemperature.Select(z => z.Longitude).Contains(x.Long)).ToList();

        return coolestDistrict;
    }

    // Checking travel possibility of destination district
    public async Task<string> GetTravelPossibility(string currentDistrictId, string destinationDistrictId, string date)
    {
        var currentDistrict = await _districtService.GetDistrictById(currentDistrictId);
        var destinationDistrict = await _districtService.GetDistrictById(destinationDistrictId);

        // Get District Temperature for that day
        var currentDistrictTemperature = await GetTemperatureDistrictByLatitudeLongitudeAndDate(currentDistrict!.Lat, currentDistrict.Long, date);
        var destinationDistrictTemperature = await GetTemperatureDistrictByLatitudeLongitudeAndDate(destinationDistrict!.Lat, destinationDistrict.Long, date);

        return currentDistrictTemperature!.Temperature > destinationDistrictTemperature!.Temperature ? "Can travel" : "Can't travel";
    }

    // fetching temperature with district date between
    private async Task<List<TemperatureDistrict>> GetTemperatureFromApi(string url, string latitude, string longitude)
    {
        var temperature = await _httpService.GetAsync(url);
        return ConvertTemperatureToTemperatureDistrict(temperature, latitude, longitude);
    }

    // Getting all district temperature
    private async Task<List<TemperatureDistrict>> GetTemperatureForAllDistrictFromApi()
    {
        var allTasks = new List<Task<List<TemperatureDistrict>>>();
        string startDate = DateTime.Now.ToString("yyyy-MM-dd");
        string endDate = DateTime.Now.AddDays(6).ToString("yyyy-MM-dd");
        var districts = await _districtService.GetDistricts();

        // Generating all url and task for all district
        foreach (var district in districts)
        {
            string url = Urls.GetTemperatureUrl(district.Lat, district.Long, startDate, endDate);
            allTasks.Add(GetTemperatureFromApi(url, district.Lat, district.Long));
        }

        var allDistrictsTemperatureArray = await Task.WhenAll(allTasks);

        // Converting List<TemperatureDistrict> array to List<TemperatureDistrict>
        var allDistrictsTemperature = new List<TemperatureDistrict>();
        foreach (var temperature in allDistrictsTemperatureArray)
        {
            allDistrictsTemperature.AddRange(temperature);
        }

        return allDistrictsTemperature;
    }

    private async Task<List<TemperatureDistrict>> GetTemperatureForAllDistrict()
    {
        List<TemperatureDistrict>? temperatureDistricts = await _distributedCache.GetAsync<List<TemperatureDistrict>>(Cachekeys.TEMPERATURE);
        if(temperatureDistricts is null)
        {
            temperatureDistricts = await GetTemperatureForAllDistrictFromApi();
            if (temperatureDistricts is null) throw new Exception("No temperature found for districts.");
            await _distributedCache.SetAsync(Cachekeys.TEMPERATURE, temperatureDistricts);
        }

        return temperatureDistricts;
    }

    private async Task<TemperatureDistrict?> GetTemperatureDistrictByLatitudeLongitudeAndDate(string latitude, string longitude, string date)
    {
        var temperatureDistricts = await GetTemperatureForAllDistrict();
        return temperatureDistricts.Where(x => x.Latitude == latitude && x.Longitude == longitude && x.Day.ToString("yyyy-MM-dd") == date).FirstOrDefault();
    }

    private List<TemperatureDistrict> ConvertTemperatureToTemperatureDistrict(Temperature temperature, string latitude, string longitude)
    {
        // taking indexes of all 2pm temperatures for all date.
        var indexes = temperature.Hourly.Time.Select((x, i) => new {x, i}).Where(x => Convert.ToDateTime(x.x).Hour == 14).ToList();
        var temperatureDistricts = new List<TemperatureDistrict>();

        foreach (var index in indexes)
        {
            var district = new TemperatureDistrict
            {
                Latitude = latitude,
                Longitude = longitude,
                Day = Convert.ToDateTime(temperature.Hourly.Time[index.i]),
                Temperature = temperature.Hourly.Temperature_2m[index.i]
            };

            temperatureDistricts.Add(district);
        }

        return temperatureDistricts;
    }
}