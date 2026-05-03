using Weather.Domain;

namespace Weather.Application;

public interface IWeatherService
{
    public Task<Result<WeatherDto>> Get(string zip, string countryCode, string temperatureUnit);
}
