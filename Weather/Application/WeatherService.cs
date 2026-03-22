using Weather.Application;
using Weather.Infrastructure;
using Weather.Domain;

namespace Weather.Application;

public class WeatherService
{
    private readonly GeoCodeClient _geoCodeClient;
    private readonly WeatherClient _weatherClient;

    public WeatherService(GeoCodeClient geoCodeClient, WeatherClient weatherClient)
    {
        _geoCodeClient = geoCodeClient;
        _weatherClient = weatherClient;
    }
    public async Task<Result<WeatherDto>> Get()
    {

        var geoResponse = await _geoCodeClient.GetLocation();
        var weatherResponse = await _weatherClient.GetWeather(geoResponse.Lat, geoResponse.Lon);

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

        var dto = new WeatherDto(geoResponse.Name, current, forecast);

        return Result<WeatherDto>.Ok(dto);
    }
}

