using JobFinder.HhApi.Models;
using JobFinder.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JobFinder
{
    public class VacancyRepository : IDatabase
    {

        public readonly string connectionString = "";
        public VacancyRepository(string connectionString) 
        {
            if (connectionString == null)
            {
                throw new ArgumentNullException("Пустое соединение");
            }
            this.connectionString = connectionString; 
        }

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
        public bool Get(int id)
        {
            throw new NotImplementedException();
        }
        public int[] GetActiveVacancyIds()
        {
            throw new NotImplementedException();
        }
        #endregion

    }
}
