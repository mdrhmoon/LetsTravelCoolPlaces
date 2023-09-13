namespace LetsTravelCoolPlaces.Services.Classes;

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

    public async Task<List<District>?> GetCoolestDistricts()
    {
        List<District>? coolestDistricts = await _distributedCache.GetAsync<List<District>>(Cachekeys.COOLESTDISTRICTS);
        if(coolestDistricts is null)
        {
            coolestDistricts = await GenerateCoolestDistricts();
            Throw.IfNull(coolestDistricts, Messages.FailedToGenerateCoolestDistricts());
            await _distributedCache.SetAsync(Cachekeys.COOLESTDISTRICTS, coolestDistricts);
        }

        return coolestDistricts;
    }

    // getting coolest 10 districts
    private async Task<List<District>> GenerateCoolestDistricts()
    {
        var temperatureDistricts = await GetTemperatureForAllDistrict();
        Throw.IfNull(temperatureDistricts,  Messages.TemperatureNotFound());

        // Generating average temperature for all district
        var avgTemperatureOfDistricts = temperatureDistricts!.GroupBy(x => new
        {
            x.Latitude,
            Longitutde = x.Longitude
        }).Select(g => new
        {
            g.Key.Latitude,
            Longitude = g.Key.Longitutde,
            AvgTemperature = g.Average(x => x.Temperature)
        }).ToList();

        var districts = await _districtService.GetDistricts();
        Throw.IfNull(districts, Messages.DistrictsNotFound());

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
        DateTime startDate = DateTime.Now.AddDays(-1), endDate = DateTime.Now.AddDays(6), currentDate = Convert.ToDateTime(date);
        if (currentDate <= startDate || currentDate > endDate) Throw.Exception(Messages.InvalidDateMessage(DateTime.Now, endDate));

        // Getting current location
        var currentDistrict = await _districtService.GetDistrictById(currentDistrictId);
        Throw.IfNull(currentDistrict, Messages.CurrentLocationNotFound());

        // Getting destination location
        var coolestDistricts = await GetCoolestDistricts();
        Throw.IfNull(coolestDistricts, Messages.CoolestDistrictsNotFound());

        var destinationDistrict = coolestDistricts!.Where(x => x.Id == destinationDistrictId).FirstOrDefault();
        Throw.IfNull(destinationDistrict, Messages.DestinationLocationNotFound());

        // Getting current district temperature for that day
        var currentDistrictTemperature = await GetTemperatureDistrictByLatitudeLongitudeAndDate(currentDistrict!.Lat, currentDistrict.Long, date);
        Throw.IfNull(currentDistrictTemperature, Messages.CurrentLocationTemperatureNotFound());

        // Getting destination location temperature for that day
        var destinationDistrictTemperature = await GetTemperatureDistrictByLatitudeLongitudeAndDate(destinationDistrict!.Lat, destinationDistrict.Long, date);
        Throw.IfNull(destinationDistrictTemperature, Messages.DestinationLocationTemperatureNotFound());

        return Formula.GetTravelPossibility(currentDistrictTemperature!.Temperature, destinationDistrictTemperature!.Temperature);
    }

    // fetching temperature with district date between
    private async Task<List<TemperatureDistrict>> GetTemperatureFromApi(string url, string latitude, string longitude)
    {
        var temperature = await _httpService.GetAsync(url);
        return ConvertTemperatureToTemperatureDistrict(temperature!, latitude, longitude);
    }

    // Getting all district temperature
    private async Task<List<TemperatureDistrict>> GetTemperatureForAllDistrictFromApi()
    {
        var allTasks = new List<Task<List<TemperatureDistrict>>>();
        string startDate = DateTime.Now.ToString(Constants.DATE_FORMAT);
        string endDate = DateTime.Now.AddDays(6).ToString(Constants.DATE_FORMAT);
        var districts = await _districtService.GetDistricts();

        // Generating all url and task for all district
        foreach (var district in districts!)
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
        if (temperatureDistricts is null)
        {
            temperatureDistricts = await GetTemperatureForAllDistrictFromApi();
            Throw.IfNull(temperatureDistricts, Messages.TemperatureNotFound());
            await _distributedCache.SetAsync(Cachekeys.TEMPERATURE, temperatureDistricts);
        }

        return temperatureDistricts;
    }

    private async Task<TemperatureDistrict?> GetTemperatureDistrictByLatitudeLongitudeAndDate(string latitude, string longitude, string date)
    {
        var temperatureDistricts = await GetTemperatureForAllDistrict();
        return temperatureDistricts.Where(x => x.Latitude == latitude && x.Longitude == longitude && x.Day.ToString(Constants.DATE_FORMAT) == date).FirstOrDefault();
    }

    private List<TemperatureDistrict> ConvertTemperatureToTemperatureDistrict(Temperature temperature, string latitude, string longitude)
    {
        // taking indexes of all 2pm temperatures for all date.
        var indexes = temperature.Hourly.Time.Select((x, i) => new { x, i }).Where(x => Convert.ToDateTime(x.x).Hour == 14).ToList();
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