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
               "id SERIAL PRIMARY KEY," +
               "name VARCHAR(255)," +
               "hh_id INT," +
               "location VARCHAR(255)," +
               "max_salary decimal," +
               "min_salary decimal," +
               "currency VARCHAR(255)," +
               "is_gross bool," +
               "address VARCHAR(255)," +
               "metro_station VARCHAR(255)," +
               "publish_date TIMESTAMP WITHOUT TIME ZONE," +
               "is_archived bool," +
               "application_link VARCHAR(255)," +
               "vacancy_link VARCHAR(255)," +
               "employer_name VARCHAR(255)," +
               "employer_link VARCHAR(255)," +
               "requirement VARCHAR(255)," +
               "responsibility VARCHAR(255)," +
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
                connection.Close();
            }

            postgreSqlConnectionString = masterConnection.Replace("Database=postgres", $"Database=\"{dbName}\"");
            return postgreSqlConnectionString;
        }
        public void Insert(Vacancy vacancy)
        {
            openConnection();
            var command = new NpgsqlCommand("INSERT INTO vacancies (" +
               "name," +
               "hh_id," +
               "location," +
               "max_salary," +
               "min_salary," +
               "currency," +
               "is_gross," +
               "address," +
               "metro_station," +
               "publish_date," +
               "is_archived," +
               "application_link," +
               "vacancy_link," +
               "employer_name," +
               "employer_link," +
               "requirement," +
               "responsibility," +
               "schedule" +
                ") VALUES " +
                "(@p1, @p2, @p3, @p4, @p5, @p6, @p7, @p8, @p9, @p10, @p11, @p12, @p13, @p14, @p15, @p16, @p17, @p18)", sqlConnection)
            {
                Parameters =
                {
                    new("p1", vacancy.Name),
                    new("p2", vacancy.HhId),
                    new("p3", vacancy.Location),
                    new("p4", vacancy.MaxSalary),
                    new("p5", vacancy.MinSalary),
                    new("p6", vacancy.Currency),
                    new("p7", vacancy.IsGross),
                    new("p8", vacancy.Address),
                    new("p9", vacancy.MetroStation),
                    new("p10", vacancy.PublishDate),
                    new("p11", vacancy.IsArchived),
                    new("p12", vacancy.LinkToApply),
                    new("p13", vacancy.Link),
                    new("p14", vacancy.EmployerName),
                    new("p15", vacancy.EmployerLink),
                    new("p16", vacancy.Requirement),
                    new("p17", vacancy.Responsibility),
                    new("p18", vacancy.Schedule)
                }
            };
            foreach (NpgsqlParameter sp in command.Parameters)
            {
                if (sp.NpgsqlValue == null)
                {
                    sp.IsNullable = true;
                    sp.Value = DBNull.Value;
                }
            }
                command.ExecuteNonQuery();
            closeConnection();
        }
        public void Update(Vacancy vacancy)
        {
            openConnection();
            throw new NotImplementedException();
        }
        public bool Get(int id)
        {
            openConnection();
            var findVacancyById = new NpgsqlCommand($"SELECT * FROM vacancies WHERE vacancies.hh_id = {id}", sqlConnection);
            var result = findVacancyById.ExecuteScalar();
            closeConnection();
            if (result != null)
            {
                return true;
            }
            return false;
        }
        #endregion

    }
}
