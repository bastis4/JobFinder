
using JobFinder.HhApi;
using JobFinder.HhApi.Models;
using System.Reflection;

namespace JobFinder
{
    public class Program
    {
        static readonly string _connectionString = "Server=localhost;Port=5432;User Id=postgres;Password=enter;Database=JobsDB";
        public static async Task Main()
        {
            var telegram = new TelegramBot();
            var userSearchCriteria = await telegram.GetKeywordsToSearchForVacancies();
            var searchVacancy = new VacancyQuery()
            {
                Description = userSearchCriteria.Item1
            };
            var userSearchPeriod = userSearchCriteria.Item2;
            var apiClient = new HhApiClient();
            var allVacancies = apiClient.GetVacancies(searchVacancy, userSearchPeriod);

            var repository = new VacancyRepository(_connectionString);

            var manager = new VacancyManager(repository, telegram, apiClient);
            await manager.SaveAndSend(allVacancies);
            Console.WriteLine("All done");
            Console.ReadLine();
        }
    }
}