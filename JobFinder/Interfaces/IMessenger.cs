using JobFinder.HhApi.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JobFinder.Interfaces
{
    public interface IMessenger
    {
        public void SendNewVacancy(Vacancy vacancy);
        public void SendUpdatedVacancy(Vacancy vacancy);
    }

}
