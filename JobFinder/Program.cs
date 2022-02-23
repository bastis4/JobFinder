
using JobFinder.Models;
using System.Reflection;

namespace JobFinder
{
    public class Program
    {
        public static void Main()
        {
            Query searchVacancy = new Query()
            {
                text = "C# разработчик",
                area = "Москва"
            };
            var keywords = searchVacancy.QueryToKeywords();

            var apiClient = new HhApiClient();
            var allVacancies = apiClient.GetVacancies(keywords);

            var db = new SqlDatabase();
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
                telegram.SendEditedVacancy(vacancy);
            }
        }
    }
}