namespace SuperMarketAPI
{
    public class JwtSettings
    {
        public string? SecretKey { get; set; }
        public string? Issuer { get; set; }
        public string? Audience { get; set; }
        public int ExpiryMinutes { get; set; }
    }


    //    public static class ConfigurationExtensions
    //    {
    //        public static JwtSettings GetJwtSettings(this IConfiguration configuration)
    //        {
    //            return configuration.GetSection("Jwt").Get<JwtSettings>();
    //        }
    //    }
}
