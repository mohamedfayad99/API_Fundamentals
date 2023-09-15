namespace CityInfo.Services
{
    public class CloudmailService: ImailService
    {

        private readonly string _mailto = string.Empty;
        private readonly string _mailfrom = string.Empty;

        public CloudmailService(IConfiguration configuration)
        {
            _mailto = configuration["mailSetting:mailToAddress"];
            _mailfrom = configuration["mailSetting:mailFromAddress"];
        }

        public void send(string sublect, string message)
        {
            Console.WriteLine($"Mail from{_mailfrom} to {_mailto}," + $"with {nameof(CloudmailService)}");
            Console.WriteLine($"Subject {sublect}");
            Console.WriteLine($"Message {sublect}");

        }
    }
}

