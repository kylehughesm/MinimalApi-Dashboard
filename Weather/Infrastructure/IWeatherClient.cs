using Weather.Domain;

namespace Weather.Infrastructure;

public interface IWeatherClient
{
    public Task<WeatherResponse?> GetWeather(double latitude, double longitude);
}
