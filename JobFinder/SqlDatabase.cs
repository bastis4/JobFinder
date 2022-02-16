using JobFinder.Interfaces;
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

        public SqlDatabase() { }

        #region Methods
        public void InsertIntoDatabase()
        {
            throw new NotImplementedException();
        }
        public void ReadFromDatabase()
        {
            throw new NotImplementedException();
        }
        public void UpdateDatabase()
        {
            throw new NotImplementedException();
        }
        #endregion
    }
}
