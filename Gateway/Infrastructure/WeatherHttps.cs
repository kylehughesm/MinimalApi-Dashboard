 using Gateway.Domain;

namespace Gateway.Infrastructure;

public class WeatherService
{
    private readonly HttpClient _httpClient;
    
    public WeatherService(HttpClient httpClient)
    {
        _httpClient = httpClient;
    }


    public async Task<WeatherResponse> GetWeather(double latitude,double longitude)
    {

        var weatherUrl = $"https://api.open-meteo.com/v1/forecast?latitude={latitude}&longitude={longitude}&daily=weather_code,temperature_2m_max,temperature_2m_min,precipitation_sum,precipitation_hours&current=temperature_2m,weather_code,precipitation&wind_speed_unit=mph&temperature_unit=fahrenheit&precipitation_unit=inch&start_date=2026-03-07&end_date=2026-03-14";

        return await _httpClient.GetFromJsonAsync<WeatherResponse>(weatherUrl);
    }
}
