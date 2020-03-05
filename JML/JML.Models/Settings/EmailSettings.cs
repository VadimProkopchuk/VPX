namespace JML.Models.Settings
{
    public class EmailSettings
    {
        public bool EnableSsl { get; set; }
        public string Host { get; set; }
        public int Port { get; set; }
        public bool UseDefaultCredentials { get; set; }
        public string UserName { get; set; }
        public string Password { get; set; }
        public string FromTitle { get; set; }
        public int MaxSendAttempts { get; set; }
        public int AttemptWaitMs { get; set; }
        public bool IsExceptionSuppressionEnabled { get; set; }
    }
}
