
using JobFinder.HhApi.Models;
using JobFinder.Models;
using System.Reflection;

namespace JobFinder
{
    public class Program
    {
        public static void Main()
        {
            var searchVacancy = new VacancyQuery()
            {
                Description = "C# разработчик",
                Area = "Москва"
            };
            
            var apiClient = new HhApiClient();
            var allVacancies = apiClient.GetVacancies(keywords);

            var db = new VacancyTable();
            db.Connect();

            var telegram = new TelegramBot();

            foreach (var vacancy in allVacancies)
            {
                if (!db.Read(vacancy.Id))
                {
                    db.Insert(vacancy);
                    telegram.SendNewVacancy(vacancy);
                }
                else db.Update(vacancy);
                telegram.SendEditedVacancy(vacancy);
            }

            var idsToCheck = db.CheckStatus();
            foreach(var id in idsToCheck)
            {
                db.Update(apiClient.GetVacancy(id));
                // Отправлять изменения в телеграм?
            }
            // Расписание в Windows Task Scheduler?
        }
    }
}