namespace LetsTravelCoolPlaces.Services;

public class TemperatureService
{
    private readonly IHttpService<Temperature> _httpService;
    private readonly IDistrictService _districtService;
    private List<District>? _districts = null;

    public TemperatureService(IDistrictService districtService)
    {
        _httpService = new HttpService<Temperature>();
        _districtService = districtService;
        LoadDistricts();
    }

    // fetching temperature with district date between
    public async Task<List<TemperatureDistrict>> GetTemperatureFromApi(string url)
    {
        var temperature = await _httpService.GetAsync(url);
        return ConvertTemperatureToTemperatureDistrict(temperature);
    }

    // Getting all district temperature
    public async Task<List<TemperatureDistrict>> GetTemperatureForAllDistrict()
    {
        var allTasks = new List<Task<List<TemperatureDistrict>>>();
        string startDate = DateTime.Now.ToString("yyyy-MM-dd");
        string endDate = DateTime.Now.AddDays(7).ToString("yyyy-MM-dd");

        // Generating all url and task for all district
        foreach (var district in _districts!)
        {
            string url = Urls.GetTemperatureUrl(Convert.ToDouble(district.Lat), Convert.ToDouble(district.Long), startDate, endDate);
            allTasks.Add(GetTemperatureFromApi(url));
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

    // getting coolest 10 districts
    public async Task<List<District>> GetCoolestDistricts()
    {
        var temperatureDistricts = await GetTemperatureForAllDistrict();

        // generating average temperature for all district
        var avgTemperatureOfDistricts = temperatureDistricts.GroupBy(x => new {
            Latitude = x.Latitude,
            Longitutde = x.Longitude
        }).Select(g => new {
            Latitude = g.Key.Latitude,
            Longitude = g.Key.Longitutde,
            AvgTemperature = g.Average(x => x.Temperature)
        });

        // taking coolest 10 district
        var coolestDistrictsTemperature = avgTemperatureOfDistricts.OrderBy(x => x.AvgTemperature).Take(10);

        // filtering coolest district from all district
        var coolestDistrict = _districts!.Where(x =>
            coolestDistrictsTemperature.Select(x => x.Longitude).Contains(x.Lat) &&
                coolestDistrictsTemperature.Select(x => x.Longitude).Contains(x.Long)).ToList();

        return coolestDistrict;
    }

    // Checking travel possibility of destination district
    public async Task<bool> GetTravelPossibility(string currentDistrictId, string destinationDistrictId, DateTime date)
    {
        var temperatureDistricts = await GetTemperatureForAllDistrict();

        var currentDistrict = _districts!.Where(x => x.Id == currentDistrictId).FirstOrDefault();
        var destinationDistrict = _districts!.Where(x => x.Id == destinationDistrictId).FirstOrDefault();

        // filtering current district temperature for the date
        var currentDistrictTemperature = temperatureDistricts.Where(x => 
            x.Latitude == currentDistrict!.Lat && 
            x.Longitude == currentDistrict.Long && 
            x.Day.ToString("yyyy-MM-dd") == date.ToString("yyyy-MM-dd")).FirstOrDefault();

        // filtering destination district temperature for the date
        var destinationDistrictTemperature = temperatureDistricts.Where(x => 
            x.Latitude == destinationDistrict!.Lat && 
            x.Longitude == destinationDistrict.Long && 
            x.Day.ToString("yyyy-MM-dd") == date.ToString("yyyy-MM-dd")).FirstOrDefault();

        return currentDistrictTemperature!.Temperature < destinationDistrictTemperature!.Temperature;
    }

    private List<TemperatureDistrict> ConvertTemperatureToTemperatureDistrict(Temperature temperature)
    {
        // taking indexes of all 2pm temperatures for all date.
        var indexes = temperature.Hourly.Time.Where(x => (Convert.ToDateTime(x)).Hour == 14).Select((x, i) => i);
        List<TemperatureDistrict> temperatureDistricts = new List<TemperatureDistrict>();

        foreach (var index in indexes)
        {
            var district = new TemperatureDistrict
            {
                Latitude = temperature.Latitude.ToString(),
                Longitude = temperature.Longitude.ToString(),
                Day = Convert.ToDateTime(temperature.Hourly.Time[index]),
                Temperature = temperature.Hourly.Temperature_2m[index]
            };

            temperatureDistricts.Add(district);
        }

        return temperatureDistricts;
    }

    private async Task LoadDistricts() => _districts = _districts is null ? await _districtService.GetDistrictsFromApi() : _districts;

}
