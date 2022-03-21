
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
                Description = ".net",
                Area = "1",
            };
            
            var apiClient = new HhApiClient();
            var allVacancies = apiClient.GetVacancies(searchVacancy);

            var repository = new VacancyRepository();

            var telegram = new TelegramBot();

            var manager = new VacancyManager(repository, telegram, apiClient);
            manager.InsertOrUpdate(allVacancies);
        }
    }
}