using Weather.Domain;

namespace Weather.Infrastructure;

public class WeatherClient: IWeatherClient
{
    private readonly HttpClient _httpClient;
    
    public WeatherClient(HttpClient httpClient)
    {
        _httpClient = httpClient;
    }

    public async Task<WeatherResponse?> GetWeather(double latitude,double longitude)
    {

        var startDate = DateTime.Today;
        var endDate = DateTime.Today.AddDays(6);

        var weatherUrl = $"https://api.open-meteo.com/v1/forecast?latitude={latitude}&longitude={longitude}&daily=weather_code,temperature_2m_max,temperature_2m_min,precipitation_sum,precipitation_hours&current=temperature_2m,weather_code,precipitation&wind_speed_unit=mph&temperature_unit=fahrenheit&precipitation_unit=inch&start_date={startDate:yyyy-MM-dd}&end_date={endDate:yyyy-MM-dd}";

        try
        {
            return await _httpClient.GetFromJsonAsync<WeatherResponse>(weatherUrl);
        }
        catch
        {
            return null;
        }
    }
}

