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

builder.Services.AddHttpClient<GeoCodeService>();
builder.Services.AddHttpClient<WeatherService>();
builder.Services.AddScoped<GatewayService>();

var app = builder.Build();

app.UseCors("blazor");

app.MapGet("/", async (GatewayService service) => await service.Get());

app.Run();
