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
        UrlBuilder urlBuilder;
        static HttpClient httpClient = new HttpClient();

        #region Methods
        public Vacancy[] GetVacancies(string url)
        {
            throw new NotImplementedException();
        }
        public Vacancy GetVacancy(string id)
        {
            throw new NotImplementedException();
        }
        #endregion
    }
}
