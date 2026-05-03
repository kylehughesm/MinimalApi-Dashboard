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

    public async Task<Result<WeatherDto>> GetWeather(string zip, string countryCode, string temperatureUnit)
    {
        try
        {
            var response = await _httpClient.GetAsync(
                $"?zip={Uri.EscapeDataString(zip)}&countryCode={Uri.EscapeDataString(countryCode)}&temperatureUnit={Uri.EscapeDataString(temperatureUnit)}");

            var result = await response.Content.ReadFromJsonAsync<Result<WeatherDto>>();

            if (result is not null)
                return result;

            if (!response.IsSuccessStatusCode)
                return Result<WeatherDto>.Fail("Weather service unavailable");

            return Result<WeatherDto>.Fail("Invalid response from weather service");
        }
        catch
        {
            return Result<WeatherDto>.Fail("Weather service unreachable");
        }
    }
}

