using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Data.Sqlite;

namespace DataAccess
{
    public class SqliteDataAccess
    {
        private static string _connectionString = @".\..\..\..\DataAccess\Storage\ControlTower.db;";

        public static string GetConnectionString
        {
            get => _connectionString;
        }

        public static string GetAllLogs()
        {
            SqliteConnectionStringBuilder connectionStringBuilder =
                new SqliteConnectionStringBuilder();

            connectionStringBuilder.DataSource = _connectionString;

            using (var connection = new SqliteConnection(connectionStringBuilder.ConnectionString))
            {
                connection.Open();

                var command = connection.CreateCommand();
                command.CommandText = "SELECT * FROM FlightLog";

                using (var reader = command.ExecuteReader())
                {
                    StringBuilder builder = new StringBuilder();

                    while (reader.Read())
                    {
                        var result = reader.GetString(0);
                        builder.Append($"{ result }\n");
                    }

                    return builder.ToString();
                }
            }
        }
    }
}
