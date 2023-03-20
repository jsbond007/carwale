namespace Carwale
{
    public class AppSettings
    {
        public JwtSetting Jwt { get; set; } = new JwtSetting();
    }

    public class JwtSetting
    {
        public string Issuer { get; set; }
        public string Audience { get; set; }
        public string Key { get; set; }
        public string Secret { get; set; }
    }
}
