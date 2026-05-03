using Weather.Application;
using Weather.Infrastructure;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddHttpClient<IGeoClient, GeoCodeClient>();
builder.Services.AddHttpClient<IWeatherClient, WeatherClient>();
builder.Services.AddScoped<IWeatherService, WeatherService>();

var app = builder.Build();

app.MapGet("/", async (string zip, string countryCode, string temperatureUnit, IWeatherService service) =>
{
    var result = await service.Get(zip, countryCode, temperatureUnit);

    if (!result.Success || result.Value is null)
        return Results.BadRequest(result);
    
    return Results.Ok(result);
});     

app.Run();
