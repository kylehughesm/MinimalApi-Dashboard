using Weather.Application;
using Weather.Infrastructure;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddHttpClient<IGeoClient, GeoCodeClient>();
builder.Services.AddHttpClient<IWeatherClient, WeatherClient>();
builder.Services.AddScoped<IWeatherService, WeatherService>();

var app = builder.Build();

app.MapGet("/", async (IWeatherService service) =>
{
    var result = await service.Get();

    if (!result.Success || result.Value is null)
        return Results.Problem("Weather service failed.");
    
    return Results.Ok(result.Value);
});     

app.Run();
