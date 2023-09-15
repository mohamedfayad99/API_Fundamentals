namespace CityInfo.Services
{
    public class LocalmailService : ImailService
    {
        private readonly string _mailto = string.Empty;
        private readonly string _mailfrom = string.Empty;

        public LocalmailService(IConfiguration configuration)
        {
            _mailto = configuration["mailSetting:mailToAddress"];
            _mailfrom = configuration["mailSetting:mailFromAddress"];
        }

        public void send(string sublect, string message)
        {
            Console.WriteLine($"Mail from{_mailfrom} to {_mailto}," + $"with {nameof(LocalmailService)}");
            Console.WriteLine($"Subject {sublect}");
            Console.WriteLine($"Message {sublect}");

        }
    }
}

