namespace LetsTravelCoolPlaces.Core.Models;

public class TemperatureDistrict
{
    public string Latitude { get; set; } = default!;
    public string Longitude { get; set; } = default!;
    public DateTime Day { get; set; }
    public double Temperature { get; set; }
}