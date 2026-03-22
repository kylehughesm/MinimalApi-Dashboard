using Gateway.Domain;
using Gateway.Infrastructure;

namespace Gateway.Infrastructure;

public class WeatherService: IWeatherService
{
    private readonly HttpClient _httpClient;
    
    public WeatherService(HttpClient httpClient)
    {
        _httpClient = httpClient;
    }

    public async Task<WeatherDto> GetWeather()
    {
        return await _httpClient.GetFromJsonAsync<WeatherDto>("/");
    }
}
