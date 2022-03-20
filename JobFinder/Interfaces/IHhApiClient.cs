using JobFinder.HhApi.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JobFinder.Interfaces
{
    public interface IHhApiClient
    {
        public List<Vacancy> GetVacancies(VacancyQuery query);
        public Vacancy GetVacancy(int id);
        public FoundVacancies GetAndParseResponse(VacancyQuery query);
    }
}
