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

    public async Task<WeatherDto> GetWeather(string zip, string countryCode)
    {
        var response = await _httpClient.GetFromJsonAsync<WeatherDto>(
        $"/?zip={Uri.EscapeDataString(zip)}&countryCode={Uri.EscapeDataString(countryCode)}");

        if (response is null)
            throw new Exception("Weather response was null.");

        return response; 
    }
}
