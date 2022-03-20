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

        //VacancyQuery query;
        UrlBuilder urlBuilder = new UrlBuilder();

        #region Methods
        public Vacancy[] GetVacancies(VacancyQuery query)
        {
            var pagesCount = GetAndParseResponse(query).pages;
            for (int i = 0; i < pagesCount; i++)
            {
                query.Page = i.ToString();
                GetAndParseResponse(query);
            }
            throw new NotImplementedException();
        }

        public Vacancy GetVacancy(int id)
        {
            throw new NotImplementedException();
        }

        public FoundVacancies GetAndParseResponse(VacancyQuery query)
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
        #endregion
    }
}
