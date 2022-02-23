using JobFinder.Interfaces;
using JobFinder.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JobFinder
{
    public class SqlDatabase : IDatabase
    {

        public string VacancyID { get; set; }
        public string SqlQuery { get; set; }
        public string login { get; set; }
        public static string password { get; set; }
        /*
                public Dictionary<string, string> AuthorisationParams = new Dictionary<string, string>()
                {
                    {"login", SqlDatabase.login},
                    {"pasword", SqlDatabase.password}
                };*/

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

        public bool Read(string id)
        {
            throw new NotImplementedException();
            //vacancy.IsNew = true;
        }

        public string[] CheckStatus()
        {
            throw new NotImplementedException();
            //vacancy.Archived = true;
            //vacancy.IsNew = false;
        }
        #endregion

    }
}
