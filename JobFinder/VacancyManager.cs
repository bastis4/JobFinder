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
        VacancyRepository vacancyRepository;
        TelegramBot telegram;
        HhApiClient apiClient;
        public VacancyManager(VacancyRepository repository, TelegramBot telegram, HhApiClient apiClient)
        {
            this.vacancyRepository = repository; 
            this.telegram = telegram;
            this.apiClient = apiClient;
        }
        public void InsertOrUpdate(Vacancy[] vacancies)
        {
            foreach (var vacancy in vacancies)
            {
                if (!vacancyRepository.Get(vacancy.HhId))
                {
                    vacancyRepository.Insert(vacancy);
                    telegram.SendNewVacancy(vacancy);
                }
                else
                {
                    vacancyRepository.Update(vacancy);
                    telegram.SendUpdatedVacancy(vacancy);
                }
            }
        }
        public void UpdateStatus()
        {
            var idsToUpdate = vacancyRepository.GetActiveVacancyIds();
            foreach (var id in idsToUpdate)
            {
                vacancyRepository.Update(apiClient.GetVacancy(id));
            }         
        }
    }
}
