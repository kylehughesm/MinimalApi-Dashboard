using News.Infrastructure;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddHttpClient<INewsClient, NewsClient>(client =>
{
    client.DefaultRequestHeaders.UserAgent.ParseAdd("DashboardNewsService/1.0");
});

var app = builder.Build();

app.MapGet("/", async (string countryCode, INewsClient client) => 
        {
            var result = await client.GetNews(countryCode);

            return Results.Ok(result);
        });

app.Run();
