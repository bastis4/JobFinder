using JobFinder.Interfaces;
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
        public string searchQuery;

        public Vacancy[] GetAllVacancies(Dictionary<string, string> keywords)
        {
            return null;
        }
        public Vacancy GetVacancyDetails(string VacancyId)
        {
            return null;
        }
    }
}
