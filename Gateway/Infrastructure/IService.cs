using Gateway.Domain;

namespace Gateway.Infrastructure;

public interface IWeatherService
{
    public Task<Result<WeatherDto>> GetWeather(string zip, string countryCode);
}

public interface INewsService
{
    public Task<Result<List<NewsArticleDto>>> GetNews(string countryCode);
}

public interface IUserPreferenceService
{
    Task<UserPreference?> GetByUserIdAsync(string userId);
}