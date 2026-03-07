using Gateway.Application;
using Gateway.Infrastructure;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddHttpClient<GeoCodeService>();
builder.Services.AddHttpClient<WeatherService>();
builder.Services.AddScoped<GatewayService>();

var app = builder.Build();

app.MapGet("/", async (GatewayService service) => await service.Get());

app.Run();
