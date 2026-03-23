using Gateway.Domain;

namespace Gateway.Infrastructure;

public interface IWeatherService
{
    public Task<WeatherDto> GetWeather(string zip, string countryCode);
}
