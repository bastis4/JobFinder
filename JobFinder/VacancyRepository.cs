using JobFinder.HhApi.Models;
using JobFinder.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Npgsql;

namespace JobFinder
{
    public class VacancyRepository : IDatabase
    {

        static readonly string _connectionString = "Server=localhost;Port=5432;User Id=postgres;Password=enter;Database=JobsDB";
        static NpgsqlConnection connection = new NpgsqlConnection(GetConnectionString(_connectionString));
        static NpgsqlCommand createTableCommand = new NpgsqlCommand(
               "CREATE TABLE IF NOT EXISTS table1 (id SERIAL PRIMARY KEY, name VARCHAR(255), price INT)"
               , connection);

        /*public VacancyRepository(string connectionString)
        {
            if (_connectionString == null)
            {
                throw new ArgumentNullException("Пустое соединение");
            }
            this._connectionString = connectionString;
        }*/

        #region Methods
        private static string GetConnectionString(string postgreSqlConnectionString)
        {
            NpgsqlConnectionStringBuilder connBuilder = new()
            {
                ConnectionString = postgreSqlConnectionString
            };

            string dbName = connBuilder.Database;

            var masterConnection = postgreSqlConnectionString.Replace(dbName, "postgres");

            using (NpgsqlConnection connection = new(masterConnection))
            {
                connection.Open();
                var checkIfExistsCommand = new NpgsqlCommand($"SELECT 1 FROM pg_catalog.pg_database WHERE datname = '{dbName}'", connection);
                var result = checkIfExistsCommand.ExecuteScalar();

                if (result == null)
                {
                    var command = new NpgsqlCommand($"CREATE DATABASE \"{dbName}\"", connection);
                    command.ExecuteNonQuery();
                }
            }

            postgreSqlConnectionString = masterConnection.Replace("Database=postgres", "Database=" + dbName);

            return postgreSqlConnectionString;
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
        #endregion

    }
}
