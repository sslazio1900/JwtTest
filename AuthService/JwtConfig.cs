namespace AuthService;

public class JwtConfig
{
    public const string SectionName = "JwtSetting";

    public string Issuer { get; init; } = null!;
    public string Secret { get; init; } = null!;
    public string Audience { get; init; } = null!;
    public int ExpireInMins { get; init; }
}