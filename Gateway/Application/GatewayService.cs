using Gateway.Application;
using Gateway.Infrastructure;

namespace Gateway.Application;

public class GatewayService
{
    private readonly HttpClient _httpClient;
    private readonly IConfiguration _config;

    public GatewayService(HttpClient httpClient, IConfiguration config)
    {
        _httpClient = httpClient;
        _config = config;
    }
    public async Task<Result<WeatherDto>> Get()
    {
        //pass geocode service parameters to get lat and long
        var apiKey = _config["GeoCode:ApiKey"];
        var zipCode = "47546";

        var countryCode = "US";

        var geoUrl = $"https://api.openweathermap.org/geo/1.0/zip?zip={zipCode},{countryCode}&appid={apiKey}";

        var geoResponse = await _httpClient.GetFromJsonAsync<GeoCodeResponse>(geoUrl);

        //pass Weather service parameters to fetch weather
        //var weather = WeatherService(latitude, longitude);

        var latitude = geoResponse.Lat;
        var longitude = geoResponse.Lon;
        
        var weatherUrl = $"https://api.open-meteo.com/v1/forecast?latitude={latitude}&longitude={longitude}&daily=weather_code,temperature_2m_max,temperature_2m_min,precipitation_sum,precipitation_hours&current=temperature_2m,weather_code,precipitation&wind_speed_unit=mph&temperature_unit=fahrenheit&precipitation_unit=inch&start_date=2026-03-07&end_date=2026-03-14";

        var weatherResponse = await _httpClient.GetFromJsonAsync<WeatherResponse>(weatherUrl);

        var dto = new WeatherDto(
                geoResponse.Name,
                weatherResponse.Current.Temperature2m,
                weatherResponse.Current.WeatherCode,
                weatherResponse.Current.Precipitation
            );

        //return Result object with weather 
        return Result<WeatherDto>.Ok(dto);
    }
}
