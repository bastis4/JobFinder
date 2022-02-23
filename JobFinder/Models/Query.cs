using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JobFinder.Models
{
    public class Query
    {
        public const bool _archived = false;
        public string text { get; set; }
        public string search_field { get; set; }
        public string experience { get; set; }
        public string employment { get; set; }
        public string schedule { get; set; }
        public string area { get; set; }
        public string specialization { get; set; }

        private Dictionary<string, object> keywords;
        public Dictionary<string, object> QueryToKeywords()
        {
            this.keywords = new Dictionary<string, object>()
            {
                [nameof(this.text)] = this.text,
                [nameof(this.search_field)] = this.search_field,
                [nameof(this.experience)] = this.experience,
                [nameof(this.employment)] = this.employment,
                [nameof(this.schedule)] = this.schedule,
                [nameof(this.area)] = this.area,
                [nameof(this.specialization)] = this.specialization
            };
            return keywords;
        }
    }
}
