
using JobFinder.HhApi;
using JobFinder.HhApi.Models;
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
            var allVacancies = apiClient.GetVacancies(searchVacancy);

            var db = new VacancyTable("fsgfdsgsdfgdfsg");
            db.Connect();

            var telegram = new TelegramBot();

            var manager = new VacancyManager();
            manager.VacancyProcess(allVacancies);
            manager.UpdateStatus();
        }
    }
}