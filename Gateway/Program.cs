using Gateway.Application;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddHttpClient<GatewayService>();

var app = builder.Build();

app.MapGet("/", async (GatewayService service) => await service.Get());

app.Run();
