using JobFinder.HhApi;
using JobFinder.HhApi.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JobFinder
{
    public class VacancyManager
    {
        VacancyTable vacancyTable;
        TelegramBot telegram;
        HhApiClient apiClient;
        public void VacancyProcess(Vacancy[] vacancies)
        {
            foreach (var vacancy in vacancies)
            {
                if (!vacancyTable.Get(vacancy.HhId))
                {
                    vacancyTable.Insert(vacancy);
                    telegram.SendNewVacancy(vacancy);
                }
                else vacancyTable.Update(vacancy);
                telegram.SendUpdatedVacancy(vacancy);
            }
        }
        public void UpdateStatus()
        {
            var idsToUpdate = vacancyTable.GetActiveVacancyIds();
            foreach (var id in idsToUpdate)
            {
                vacancyTable.Update(apiClient.GetVacancy(id));
            }
            
        }
    }
}
