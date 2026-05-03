using System.Security.Claims;
using Gateway.Application;
using Gateway.Domain;
using Gateway.Infrastructure;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Authentication;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddCors(options =>
{
    options.AddPolicy("blazor", policy =>
    {
        policy.WithOrigins("http://localhost:5098")
              .AllowAnyHeader()
              .AllowAnyMethod()
              .AllowCredentials();
    });
});

builder.Services.AddDbContext<ApplicationDbContext>(options =>
    options.UseNpgsql(builder.Configuration.GetConnectionString("DefaultConnection")));

builder.Services.AddIdentityApiEndpoints<ApplicationUser>()
    .AddEntityFrameworkStores<ApplicationDbContext>();

builder.Services.AddAuthorization();

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

var cookieAuth = new AuthorizeAttribute
{
    AuthenticationSchemes = IdentityConstants.ApplicationScheme
};

app.UseCors("blazor");

app.UseAuthentication();
app.UseAuthorization();

app.MapIdentityApi<ApplicationUser>();

app.MapPost("/logout", async (HttpContext context) =>
{
    await context.SignOutAsync(IdentityConstants.ApplicationScheme);
    return Results.Ok();
})
.WithSummary("Logs out the user")
.WithDescription("Clears the authentication cookie")
.WithTags("Identity")
.RequireAuthorization(cookieAuth);

app.MapGet("/", async (string zip, string countryCode, string temperatureUnit, IGatewayService service) =>
    await service.Get(zip, countryCode, temperatureUnit));

app.MapGet("/me", (ClaimsPrincipal user) =>
{
    var userId = user.FindFirstValue(ClaimTypes.NameIdentifier);
    var email = user.FindFirstValue(ClaimTypes.Email);

    return TypedResults.Ok(new
    {
        userId,
        email
    });
}).RequireAuthorization(cookieAuth);

app.MapGet("/preferences", async (ClaimsPrincipal user, ApplicationDbContext db) =>
{
    var userId = user.FindFirstValue(ClaimTypes.NameIdentifier);

    if (userId is null)
        return Results.Unauthorized();

    var prefs = await db.UserPreferences
        .AsNoTracking()
        .FirstOrDefaultAsync(x => x.UserId == userId);

    if (prefs is null)
        return Results.Ok(new { zip = "", countryCode = "US" });

    return Results.Ok(new
    {
        zip = prefs.Zip,
        countryCode = prefs.CountryCode,
        temperatureUnit =prefs.TemperatureUnit
    });
}).RequireAuthorization(cookieAuth);

app.MapPut("/preferences", async (
    UpdatePreferencesRequest request,
    ClaimsPrincipal user,
    ApplicationDbContext db) =>
{
    var userId = user.FindFirstValue(ClaimTypes.NameIdentifier);

    if (userId is null)
        return Results.Unauthorized();

    var prefs = await db.UserPreferences
        .FirstOrDefaultAsync(x => x.UserId == userId);

    if (prefs is null)
    {
        prefs = new UserPreference
        {
            UserId = userId,
            Zip = request.Zip,
            CountryCode = request.CountryCode,
            TemperatureUnit = request.TemperatureUnit
        };

        db.UserPreferences.Add(prefs);
    }
    else
    {
        prefs.Zip = request.Zip;
        prefs.CountryCode = request.CountryCode;
        prefs.TemperatureUnit = request.TemperatureUnit;
    }

    await db.SaveChangesAsync();

    return Results.Ok(new
    {
        zip = prefs.Zip,
        countryCode = prefs.CountryCode,
        temperatureUnit = prefs.TemperatureUnit
    });
}).RequireAuthorization(cookieAuth);

app.Run();