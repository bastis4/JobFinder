using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JobFinder
{
    public class TelegramBot : MessengerClient
    {
        public string Token { get; set; }
        private const string apiDomain = "$https://api.telegram.org/bot123456:{Token}/getMe";
        static HttpClient httpClient = new HttpClient();
        public override void SendMessage(Message message)
        {
            throw new NotImplementedException();
        }
    }
}
