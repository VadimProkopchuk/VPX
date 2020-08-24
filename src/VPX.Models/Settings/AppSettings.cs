using VPX.Models.Settings.Seed;

namespace VPX.Models.Settings
{
    public class AppSettings
    {
        public JwtSettings Jwt { get; set; }
        public EmailSettings Email { get; set; }
        public SecuritySettings Security { get; set; }
        public SeedSettings Seed { get; set; }
    }
}
