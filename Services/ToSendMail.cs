namespace CityInfo._ٍServices
{
    public class LocalmailService
    {
        private string _mailto= "mahmmoudkinawy@gmail.com";
        private string _mailfrom = "mohamedfayad@gmail.com";


        public void send(string sublect, string message)
        {
            Console.WriteLine($"Mail from{_mailfrom} to {_mailto}",+$"with {ToSendMail}");
        }
    }
}
