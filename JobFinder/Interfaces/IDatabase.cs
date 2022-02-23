using JobFinder.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JobFinder.Interfaces
{
    public interface IDatabase
    {
        public void Insert(Vacancy vacancy);
        public void Update(Vacancy vacancy);
        public bool Read(string id);
        public string[] CheckStatus();

    }
}
