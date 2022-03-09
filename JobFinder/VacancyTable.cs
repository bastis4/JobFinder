using JobFinder.HhApi.Models;
using JobFinder.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JobFinder
{
    public class VacancyTable : IDatabase
    {

        public string connectionString;
        public VacancyTable(string connectionString) { this.connectionString = connectionString; }

        #region Methods
        public void Connect()
        {
            throw new NotImplementedException();
        }
        public void Insert(Vacancy vacancy)
        {
            throw new NotImplementedException();
        }

        public void Update(Vacancy vacancy)
        {
            throw new NotImplementedException();
        }

        public bool Read(int id)
        {
            throw new NotImplementedException();
        }

        public string[] CheckStatus()
        {
            throw new NotImplementedException();
        }
        #endregion

    }
}
