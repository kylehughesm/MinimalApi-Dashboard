using Weather.Infrastructure;
using Weather.Domain;

namespace Weather.Application;

public class WeatherService : IWeatherService
{
    private readonly IGeoClient _geoCodeClient;
    private readonly IWeatherClient _weatherClient;

    public WeatherService(IGeoClient geoCodeClient, IWeatherClient weatherClient)
    {
        _geoCodeClient = geoCodeClient;
        _weatherClient = weatherClient;
    }

    public async Task<Result<WeatherDto>> Get(string zip, string countryCode)
    {
        var geoResponse = await _geoCodeClient.GetLocation(zip, countryCode);

        if (geoResponse is null)
            return Result<WeatherDto>.Fail("Invalid zip code or location not found");

        var weatherResponse = await _weatherClient.GetWeather(geoResponse.Lat, geoResponse.Lon);

        if (weatherResponse is null)
            return Result<WeatherDto>.Fail("Failed to get weather.");

        var dto = MapToDto(geoResponse, weatherResponse);

        return Result<WeatherDto>.Ok(dto);
    }

    private WeatherDto MapToDto(GeoCodeResponse geoResponse, WeatherResponse weatherResponse)
    {
        var current = new CurrentWeather(
            DateTime.Parse(weatherResponse.Current.Time),
            weatherResponse.Current.Temperature2m,
            WeatherCodeLookup.GetDescription(weatherResponse.Current.WeatherCode),
            weatherResponse.Current.Precipitation
        );

        var forecast = new List<DailyForecast>();

        for (int i = 0; i < weatherResponse.Daily.Time.Count; i++)
        {
            forecast.Add(new DailyForecast(
                DateTime.Parse(weatherResponse.Daily.Time[i]),
                WeatherCodeLookup.GetDescription(weatherResponse.Daily.WeatherCode[i]),
                weatherResponse.Daily.TemperatureMax[i],
                weatherResponse.Daily.TemperatureMin[i],
                weatherResponse.Daily.PrecipitationSum[i]
            ));
        }

        return new WeatherDto(geoResponse.Name, current, forecast);
    }
}
