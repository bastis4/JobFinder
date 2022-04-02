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
        List<Vacancy> newVacancies = new List<Vacancy>();
        List<Vacancy> updatedVacancies = new List<Vacancy>();
        public VacancyManager(VacancyRepository repository, TelegramBot telegram, HhApiClient apiClient)
        {
            this.vacancyRepository = repository; 
            this.telegram = telegram;
            this.apiClient = apiClient;
        }
        public async Task SaveAndSend(List<Vacancy> vacancies)
        {
            foreach (var vacancy in vacancies)
            {
                if (!vacancyRepository.Get(vacancy.HhId))
                {
                    vacancyRepository.Insert(vacancy);
                    newVacancies.Add(vacancy);                   
                }
            }             
            await telegram.SendNewVacancy(newVacancies);

        }
    }
}
