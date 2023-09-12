namespace LetsTravelCoolPlaces.Services;

public class TemperatureService
{
    private readonly IHttpService<Temperature> _httpService;
    private readonly IDistrictService _districtService;

    public TemperatureService(IDistrictService districtService)
    {
        _httpService = new HttpService<Temperature>();
        _districtService = districtService;
    }

    // fetching temperature with district date between
    public async Task<List<TemperatureDistrict>> GetTemperatureFromApi(string url)
    {
        var temperature = await _httpService.GetAsync(url);
        return ConvertTemperatureToTemperatureDistrict(temperature);
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
}
