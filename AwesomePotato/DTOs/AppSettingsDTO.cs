namespace AwesomePotato.DTOs
{
    public class AppSettingsDTO
    {
        public string Secret { get; set; }
        public int ExpirationTime { get; set; }
        public string Issuer { get; set; }
        public string Audience { get; set; }
    }
}
