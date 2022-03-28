using JobFinder.HhApi.Models;
using JobFinder.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Npgsql;
using System.Data;

namespace JobFinder
{
    public class VacancyRepository : IDatabase
    {

        private string _connectionString = "";
        protected NpgsqlConnection sqlConnection;

        public VacancyRepository(string connectionString)
        {
            if (connectionString == null)
            {
                throw new ArgumentNullException("Пустое соединение");
            }
            _connectionString = GetConnectionString(connectionString);
            openConnection();
            var createTableCommand = new NpgsqlCommand(
               "CREATE TABLE IF NOT EXISTS vacancies (" +
               "id SERIAL PRIMARY KEY, " +
               "name VARCHAR(255), " +
               "hh_id INT, " +
               "location VARCHAR(255), " +
               "max_salary decimal, " +
               "min_salary decimal, " +
               "currency VARCHAR(255), " +
               "is_gross bool, " +
               "address VARCHAR(255), " +
               "metro_station VARCHAR(255), " +
               "publish_date TIMESTAMP WITHOUT TIME ZONE, " +
               "is_archived bool, " +
               "application_link VARCHAR(255), " +
               "vacancy_link VARCHAR(255), " +
               "employer_name VARCHAR(255), " +
               "employer_link VARCHAR(255), " +
               "requirement VARCHAR(255), " +
               "responsibility VARCHAR(255), " +
               "schedule VARCHAR(255)" +
               ")"
               , sqlConnection);
            createTableCommand.ExecuteNonQuery();
            closeConnection();
        }

        #region Methods
        private void openConnection()
        {
            sqlConnection = new NpgsqlConnection(_connectionString);
            sqlConnection.Open();
        }
        private void closeConnection()
        {
            if (sqlConnection.State == ConnectionState.Open)
            {
                sqlConnection.Close();
            }
        }
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
            openConnection();
            throw new NotImplementedException();
        }
        public void Update(Vacancy vacancy)
        {
            openConnection();
            throw new NotImplementedException();
        }
        public bool Get(int id)
        {
            openConnection();
            throw new NotImplementedException();
        }
        #endregion

    }
}
