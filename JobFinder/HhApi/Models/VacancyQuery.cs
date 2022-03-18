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
        public string Metro { get; set; }
        public string Specialization { get; set; }
        public string Industry { get; set; }
        public string EmployerId { get; set; }
        public string Currency { get; set; }
        public string Salary { get; set; }
        public string Label { get; set; }
        public string IsSalaryPresent { get; set; }
        public string Period { get; set; }
        public string StartDate { get; set; }
        public string EndDate { get; set; }
        public string TopLatitude { get; set; }
        public string BottomLatitude { get; set; }
        public string LeftLongtitude { get; set; }
        public string RightLongtitude { get; set; }
        public string OrderBy { get; set; }
        public string SortPointLatitude { get; set; }
        public string SortPointLongtitude{ get; set; }
        //public string IsClustered { get; set; }
        //public string HasSearchArguments { get; set; }
        public string PerPage { get; set; }
        public string Page { get; set; }
        //public string NoFormattedQuery { get; set; }
        public string Premium { get; set; }
        //public string HasResponsesCount { get; set; }
        public string PartTime { get; set; }
        public string ProfessionalRole { get; set; }
    }
}
