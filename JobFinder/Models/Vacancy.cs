using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JobFinder.Models
{
    public class Vacancy
    {
        public string Name { get; set; }
        public string Id { get; set; }
        public string Description { get; set; }
        public bool IsNew { get; set; }
        public bool Archived { get; set; }
    }
}
