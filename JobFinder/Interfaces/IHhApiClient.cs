using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JobFinder.Interfaces
{
    public interface IHhApiClient
    {
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
