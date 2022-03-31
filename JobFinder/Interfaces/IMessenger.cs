using JobFinder.HhApi.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Telegram.Bot.Types;

namespace JobFinder.Interfaces
{
    public interface IMessenger
    {
        public Task<string> GetKeywordsToSearchForVacancies();
        public Task SendNewVacancy(Vacancy vacancy);
        public void SendUpdatedVacancy(Vacancy vacancy);
    }

}
