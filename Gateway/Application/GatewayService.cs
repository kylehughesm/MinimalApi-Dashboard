using Gateway.Application;
using Gateway.Infrastructure;
using Gateway.Domain;

namespace Gateway.Application;

public class GatewayService
{
    private readonly GeoCodeService _geoCodeService;
    private readonly WeatherService _weatherService;

    public GatewayService(GeoCodeService geoCodeService, WeatherService weatherService)
    {
        _geoCodeService = geoCodeService;
        _weatherService = weatherService;
    }
    public async Task<Result<WeatherDto>> Get()
    {

        var geoResponse = await _geoCodeService.GetLocation();
        var weatherResponse = await _weatherService.GetWeather(geoResponse.Lat, geoResponse.Lon);

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
