using Gateway.Domain;

namespace Gateway.Infrastructure;

public interface IWeatherService
{
    public Task<Result<WeatherDto>> GetWeather(string zip, string countryCode);
}
