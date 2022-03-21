
using JobFinder.HhApi;
using JobFinder.HhApi.Models;
using System.Reflection;

namespace JobFinder
{
    public class Program
    {
        static readonly string _connectionString = "Server=localhost;Port=5432;User Id=postgres;Password=enter;Database=JobsDB";
        public static void Main()
        {
            var searchVacancy = new VacancyQuery()
            {
                Description = ".net",
                Area = "1",
            };
            
            var apiClient = new HhApiClient();
            var allVacancies = apiClient.GetVacancies(searchVacancy);

            var repository = new VacancyRepository(_connectionString);

            var telegram = new TelegramBot();

            var manager = new VacancyManager(repository, telegram, apiClient);
            manager.InsertOrUpdate(allVacancies);
        }
    }
}