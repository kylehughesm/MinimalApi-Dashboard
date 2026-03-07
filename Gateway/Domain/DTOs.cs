namespace Gateway.Domain;

public record WeatherDto
(
    string Location,
    decimal Temperature,
    int WeatherCode,
    decimal Precipitation
);

public sealed class Result<T>
{
    public bool Success { get; }
    public string Error { get; }
    public T Value { get; }

    private Result(bool success, T value, string error)
        => (Success, Value, Error) = (success, value, error);

    public static Result<T> Ok(T value) => new(true, value, null);
    public static Result<T> Fail(string error) => new(false, default, error);
}

public class GeoCodeResponse
{
    public string Zip { get; set; }
    public string Name { get; set; }
    public double Lat { get; set; }
    public double Lon { get; set; }
    public string Country { get; set; }
}
