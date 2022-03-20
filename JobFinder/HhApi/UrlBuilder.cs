using JobFinder.HhApi.Models;
using Microsoft.AspNetCore.WebUtilities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JobFinder.HhApi
{
    public class UrlBuilder
    {
        private readonly string _url = "https://api.hh.ru/vacancies";
        public string GetUrlVacanciesQuery(VacancyQuery query)
        {
            var queryParams = new Dictionary<string, string>()
            {
                { "text", query.Description},
                { "search_field", query.SearchField},
                { "experience", query.Experience},
                { "employment", query.Employment},
                { "schedule", query.Schedule},
                { "area", query.Area },
                { "metro", query.Metro},
                { "specialization", query.Specialization},
                { "industry", query.Industry},
                { "employer_id", query.EmployerId},
                { "currency", query.Currency},
                { "salary", query.Salary},
                { "label", query.Label},
                { "only_with_salary", query.IsSalaryPresent},
                { "period", query.Period},
                { "date_from", query.StartDate},
                { "date_to", query.EndDate},
                { "top_lat", query.TopLatitude},
                { "bottom_lat", query.BottomLatitude},
                { "left_lng,", query.LeftLongtitude},
                { "right_lng", query.RightLongtitude},
                { "order_by", query.OrderBy},
                { "sort_point_lat", query.SortPointLatitude},
                { "sort_point_lng", query.SortPointLongtitude},
                { "per_page", query.PerPage},
                { "page", query.Page},
                { "premium", query.Premium},
                { "part_time", query.PartTime},
                { "professional_role", query.ProfessionalRole}

            };
            var queryParamsWithoutNull = (from x in queryParams
                           where x.Value != null
                           select x).ToDictionary(x => x.Key, x => x.Value);

            var requestUrl = QueryHelpers.AddQueryString(_url, queryParamsWithoutNull) ;
            return requestUrl;
        }
        public string GetUrlVacancy(int id)
        {
            return _url + "/" + id;
        }
    }
}
