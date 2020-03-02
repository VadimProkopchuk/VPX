namespace JML.Models
{
    public class AppSettings
    {
        public string JwtSecret { get; set; }
        public int JwtLifeTimeInDays { get; set; }
    }
}
