using Weather.Application;
using Weather.Infrastructure;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddHttpClient<GeoCodeClient>();
builder.Services.AddHttpClient<WeatherClient>();
builder.Services.AddScoped<WeatherService>();

var app = builder.Build();

app.MapGet("/", async (WeatherService service) =>
{
    var result = await service.Get();

    if (!result.Success || result.Value is null)
        return Results.Problem("Weather service failed.");
    
    return Results.Ok(result.Value);
});     

app.Run();
