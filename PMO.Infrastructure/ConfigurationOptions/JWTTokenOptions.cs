
namespace PMO.Infrastructure.ConfigurationOptions;

internal class JWTTokenOptions
{
    public const string SectionName = "JWT";
    public string Issuer { get; set; } = string.Empty;
    public string Audience { get; set; } = string.Empty;
    public string SecretKey { get; set; } = string.Empty;
    public int ExpirationInMinutes { get; set; }
    public int RefreshTokenExpirationInDays { get; set; }

}
