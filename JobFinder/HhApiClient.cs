using JobFinder.Interfaces;
using JobFinder.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JobFinder
{
    public class HhApiClient : IHhApiClient

    {
        private const string apiDomain = "";
        private const string ApiKey = "";
        static HttpClient httpClient = new HttpClient();

        #region Methods
        public Vacancy[] GetVacancies(Dictionary<string, object> keywords)
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
