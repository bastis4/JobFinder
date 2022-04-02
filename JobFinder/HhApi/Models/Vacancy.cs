using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JobFinder.HhApi.Models
{
    public class Vacancy
    {
        public string Name { get; set; }
        public int HhId { get; set; }
        public string Location { get; set; }
        public decimal? MinSalary { get; set; }
        public decimal? MaxSalary { get; set; }
        public string? Currency { get; set; }
        public bool? IsGross { get; set; }
        public string? Address { get; set; }
        public string? MetroStation { get; set; }
        public DateTime PublishDate { get; set; }
        public string LinkToApply { get; set; }
        public string Link { get; set; }
        public string EmployerName { get; set; }
        public string EmployerLink { get; set; }
        public string? Schedule { get; set; }
    }
}
