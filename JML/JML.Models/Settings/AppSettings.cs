using JML.Models.Settings.Seed;

namespace JML.Models.Settings
{
    public class AppSettings
    {
        public JwtSettings Jwt { get; set; }
        public EmailSettings Email { get; set; }
        public SecuritySettings Security { get; set; }
        public SeedSettings Seed { get; set; }
    }
}
