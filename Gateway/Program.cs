using Gateway.Application;
using Gateway.Infrastructure;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddCors(options =>
{
    options.AddPolicy("blazor", policy =>
    {
        policy
            .WithOrigins("http://localhost:5098")
            .AllowAnyHeader()
            .AllowAnyMethod();
    });
});

builder.Services.AddHttpClient<IWeatherService, WeatherService>(client => 
{
    client.BaseAddress = new Uri("http://weather:8080");
});

builder.Services.AddHttpClient<INewsService, NewsService>(client =>
{
    client.BaseAddress = new Uri("http://news:8080");
});

builder.Services.AddScoped<IGatewayService, GatewayService>();

var app = builder.Build();

app.UseCors("blazor");

app.MapGet("/", async (string zip, string countryCode, IGatewayService service) => await service.Get(zip, countryCode));

app.Run();
