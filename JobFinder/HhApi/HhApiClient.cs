using JobFinder.HhApi.Models;
using JobFinder.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;

namespace JobFinder.HhApi
{
    public class HhApiClient : IHhApiClient

    {
        static HttpClient httpClient = new HttpClient();
        UrlBuilder urlBuilder = new UrlBuilder();

        #region Methods
        public List<Vacancy> GetVacancies(VacancyQuery query)
        {
            var vacancies = new List<Vacancy>();

            var pagesCount = GetAndParseResponse(query).pages;
            for (int i = 0; i < pagesCount; i++)
            {
                query.Page = i.ToString();
                var foundVacancies = GetAndParseResponse(query);
                foreach(var foundVacancy in foundVacancies.items)
                {
                    var vacancy = new Vacancy();
                    {
                        vacancy.Name = foundVacancy.name;
                        vacancy.HhId = Int32.Parse(foundVacancy.id);
                        vacancy.Location = foundVacancy.area.name;
                        if (CheckIfPropertyExists(foundVacancy, "salary"))
                        {
                            if (foundVacancy.salary != null)
                            {
                                if(CheckIfPropertyExists(foundVacancy.salary, "from"))
                                {
                                    vacancy.MinSalary = Convert.ToDecimal(foundVacancy.salary.from);
                                }
                                else if(CheckIfPropertyExists(foundVacancy.salary, "to"))
                                {
                                    vacancy.MaxSalary = Convert.ToDecimal(foundVacancy.salary.to);
                                }
                                else if (CheckIfPropertyExists(foundVacancy.salary, "currency"))
                                {
                                    vacancy.Currency = foundVacancy.salary.currency;
                                }
                                else if (CheckIfPropertyExists(foundVacancy.salary, "gross"))
                                {
                                    vacancy.IsGross = foundVacancy.salary.gross;
                                }
                            }
                        }
                        if (CheckIfPropertyExists(foundVacancy, "address"))
                        {
                            if (foundVacancy.address != null)
                            {
                                if (CheckIfPropertyExists(foundVacancy.address, "raw"))
                                {
                                    vacancy.Address = foundVacancy.address.raw;
                                }
                                else if (CheckIfPropertyExists(foundVacancy.address.metro, "station_name"))
                                {
                                    vacancy.MetroStation = foundVacancy.address.metro.station_name;
                                }
                            }
                        }
                        vacancy.PublishDate = foundVacancy.published_at;
                        vacancy.IsArchived = foundVacancy.archived;
                        vacancy.LinkToApply = foundVacancy.apply_alternate_url;
                        vacancy.Link = foundVacancy.alternate_url;
                        vacancy.EmployerName = foundVacancy.employer.name;
                        vacancy.EmployerLink = foundVacancy.employer.alternate_url;
                        vacancy.Requirement = foundVacancy.snippet.requirement;
                        vacancy.Responsibility = foundVacancy.snippet.responsibility;
                        vacancy.Schedule = foundVacancy.schedule.name;
                    }
                    vacancies.Add(vacancy);
                }

            }
            return vacancies;
        }

        public Vacancy GetVacancy(int id)
        {
            throw new NotImplementedException();
        }

        private FoundVacancies GetAndParseResponse(VacancyQuery query)
        {
            FoundVacancies vacancies = null;
            var requestUrl = urlBuilder.GetUrlVacanciesQuery(query);
            
            httpClient.DefaultRequestHeaders.Add("User-Agent", "api-test-agent");
            
            var response = httpClient.GetAsync(requestUrl).Result;
            if (response.IsSuccessStatusCode)
            {
                var responseContent = response.Content;
                var responseString = responseContent.ReadAsStringAsync().Result;
                vacancies = JsonConvert.DeserializeObject<FoundVacancies>(responseString);
            }
            return vacancies;
        }
        private bool CheckIfPropertyExists(object obj, string propertyName)
        {
            return obj.GetType().GetProperty(propertyName) != null;
        }
        #endregion
    }
}
