namespace ConstruFindAPI.API.Configuration.Extensions
{
    public class AppSettings
    {
        public string Secret { get; set; }
        public int Expires { get; set; }
        public string Issuer { get; set; }
        public string ValideOn { get; set; }
    }
}
