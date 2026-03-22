using Gateway.Domain;

namespace Gateway.Infrastructure;

public class WeatherService
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
