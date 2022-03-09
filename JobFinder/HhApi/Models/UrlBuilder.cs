using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JobFinder.HhApi.Models
{
    public class UrlBuilder
    {
        private readonly string _url = "hgfdhdh";
        public string GetUrlVacanciesQuery(VacancyQuery query)
        {
            var parameters = "";
            var queryParams = new Dictionary<string, object>()
            {
                { "text", query.Description},
                { "search_field", query.SearchField},
                { "experience", query.Experience},
                { "employment", query.Employment},
                { "schedule", query.Schedule},
                { "area", query.Area},
                { "specialization", query.Specialization},
                { "archived", false}
            };
            foreach (var param in queryParams)
            {
                parameters += param.Key + "=" + param.Value + "&";
            }
            return _url + "?" + parameters;
        }
        public string GetUrlVacancy(int id)
        {
            return _url + "/" + id;
        }
    }
}
