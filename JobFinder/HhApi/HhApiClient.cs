﻿using JobFinder.HhApi.Models;
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
        private static readonly int _resultsPerPage = 100;
        private static readonly int _pagesLimit = 200;

        #region Methods
        public List<Vacancy> GetVacancies(VacancyQuery query, int userSearchPeriod)
        {
            var endTime = DateTime.Now;
            var startTime = DateTime.Now.AddDays(-userSearchPeriod);

            var vacancies = new List<Vacancy>();
            query.PerPage = _resultsPerPage.ToString();

            while(startTime <= endTime)
            {
                var intervalEnd = startTime.AddDays(1);
                query.StartDate = startTime.ToString("s");
                query.EndDate = intervalEnd.ToString("s");
                var pagesCount = GetAndParseResponse(query).pages;
                for (int i = 0; i < pagesCount && i < _pagesLimit; i++)
                {
                    query.Page = i.ToString();
                    var foundVacancies = GetAndParseResponse(query);
                    foreach (var foundVacancy in foundVacancies.items)
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
                                    if (CheckIfPropertyExists(foundVacancy.salary, "from"))
                                    {
                                        vacancy.MinSalary = Convert.ToDecimal(foundVacancy.salary.from);
                                    }
                                    if (CheckIfPropertyExists(foundVacancy.salary, "to"))
                                    {
                                        vacancy.MaxSalary = Convert.ToDecimal(foundVacancy.salary.to);
                                    }
                                    if (CheckIfPropertyExists(foundVacancy.salary, "currency"))
                                    {
                                        vacancy.Currency = foundVacancy.salary.currency;
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
                            vacancy.LinkToApply = foundVacancy.apply_alternate_url;
                            vacancy.Link = foundVacancy.alternate_url;
                            vacancy.EmployerName = foundVacancy.employer.name;
                            vacancy.EmployerLink = foundVacancy.employer.alternate_url;
                            vacancy.Schedule = foundVacancy.schedule.name;
                        }
                        vacancies.Add(vacancy);
                    }
                }
                startTime = intervalEnd;
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
            Console.WriteLine(requestUrl);
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
