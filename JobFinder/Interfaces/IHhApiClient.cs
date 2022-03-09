using JobFinder.HhApi.Models;
using JobFinder.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JobFinder.Interfaces
{
    public interface IHhApiClient
    {
        public Vacancy[] GetVacancies(string url);
        public Vacancy GetVacancy(string id);
    }
}
