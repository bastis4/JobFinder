using JobFinder.HhApi.Models;
using JobFinder.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JobFinder.HhApi
{
    public class HhApiClient : IHhApiClient

    {
        private const string apiDomain = "";
        private const string ApiKey = "";
        static HttpClient httpClient = new HttpClient();
        VacancyQuery query;
        UrlBuilder urlBuilder = new UrlBuilder();

        #region Methods
        public Vacancy[] GetVacancies(VacancyQuery query)
        {
            urlBuilder.GetUrlVacanciesQuery(query);
            throw new NotImplementedException();
        }
        public Vacancy GetVacancy(int id)
        {
            throw new NotImplementedException();
        }
        #endregion
    }
}
