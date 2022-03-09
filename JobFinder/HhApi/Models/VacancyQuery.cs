using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JobFinder.HhApi.Models
{
    public class VacancyQuery
    {
        public string Description { get; set; }
        public string SearchField { get; set; }
        public string Experience { get; set; }
        public string Employment { get; set; }
        public string Schedule { get; set; }
        public string Area { get; set; }
        public string Specialization { get; set; }

    }
}
