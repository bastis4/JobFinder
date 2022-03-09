using JobFinder.HhApi.Models;
using JobFinder.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JobFinder
{
    public class TelegramBot : IMessenger
    {
        public string Token { get; set; }
        private const string apiDomain = "$https://api.telegram.org/bot123456:{Token}/getMe";
        static HttpClient httpClient = new HttpClient();
        public void SendNewVacancy(Vacancy vacancy)
        {
            throw new NotImplementedException();
        }
        public void SendUpdatedVacancy(Vacancy vacancy)
        {
            throw new NotImplementedException();
        }
    }
}
