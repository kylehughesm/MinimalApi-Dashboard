namespace Gateway.Domain;

public class UserPreference
{
    public int Id { get; set; }

    public string UserId { get; set; } = string.Empty;
    public ApplicationUser? User { get; set; }

    public string Zip { get; set; } = string.Empty;
    public string CountryCode { get; set; } = "US";
}