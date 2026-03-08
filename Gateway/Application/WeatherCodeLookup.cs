namespace Gateway.Application;

public static class WeatherCodeLookup
{
    private static readonly Dictionary<int,string> Codes = new()
    {
        {0,"Clear sky"},
        {1,"Mainly clear"},
        {2,"Partly cloudy"},
        {3,"Overcast"},
        {45,"Fog"},
        {48,"Depositing rime fog"},
        {51,"Light drizzle"},
        {53,"Moderate drizzle"},
        {55,"Dense drizzle"},
        {56,"Light Freezing rain"},
        {57,"Heavy Freezing rain"},
        {61,"Slight rain"},
        {63,"Moderate rain"},
        {65,"Heavy rain"},
        {66,"Light Freezing rain"},
        {67,"Heavy Freezing rain"},
        {71,"Light snow"},
        {73,"Moderate snow"},
        {75,"Heavy snow"},
        {77,"Snow grains"},
        {80,"Sligt showers"},
        {81,"Moderate showers"},
        {82,"Violent showers"},
        {85,"Slight snow showers"},
        {86,"Heavy snow showers"},
        {95,"Thunderstorm"},
        {96,"Thunderstorm with slight hail"},
        {99,"Thunderstorm with heavy hail"}
    };

    public static string GetDescription(int code)
    {
        return Codes[code];
    }
}
