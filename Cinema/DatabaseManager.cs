using Npgsql;
using System;
using System.Collections.Generic;
using System.Data;

namespace Cinema
{
    public static class DatabaseManager
    {
        public const string ConnectionString = "Host=localhost;Port=5432;Username=postgres;Password=111;Database=Cinema";

        public static NpgsqlConnection OpenConnection()
        {
            var connection = new NpgsqlConnection(ConnectionString);
            connection.Open();
            return connection;
        }

        public static void CloseConnection(NpgsqlConnection connection)
        {
            if (connection.State == ConnectionState.Open)
                connection.Close();
        }

        public static bool[,] RetrieveSeatsData(string movieName)
        {
            bool[,] seatData = new bool[15, 7];
            using (var connection = OpenConnection())
            {
                string sql = $"SELECT seats FROM movie WHERE name = @name";
                using var command = new NpgsqlCommand(sql, connection);
                command.Parameters.AddWithValue("name", movieName);

                if (command.ExecuteScalar() is bool[,] seatsFromDatabase)
                    CopyDataToSeatsArray(seatData, seatsFromDatabase);
            }
            return seatData;
        }

        private static void CopyDataToSeatsArray(bool[,] data, bool[,] seatsFromDatabase)
        {
            for (int i = 0; i < Math.Min(seatsFromDatabase.GetLength(0), 15); i++)
            {
                for (int j = 0; j < Math.Min(seatsFromDatabase.GetLength(1), 7); j++)
                {
                    data[i, j] = seatsFromDatabase[i, j];
                }
            }
        }

        public static void UpdateMovieSeats(bool[,] seats, string movieName)
        {
            using var connection = OpenConnection();
            using var command = connection.CreateCommand();
            command.Connection = connection;
            command.CommandText = "UPDATE movie SET seats = @seats WHERE name = @name";
            command.Parameters.AddWithValue("seats", seats);
            command.Parameters.AddWithValue("name", movieName);
            command.ExecuteNonQuery();
        }

        public static List<string> GetMovieNamesForHall(int hallId)
        {
            List<string> movieNames = new();

            using (var connection = OpenConnection())
            {
                string sql = $"SELECT name FROM movie WHERE zal=@hallId";
                using var command = new NpgsqlCommand(sql, connection);
                command.Parameters.AddWithValue("hallId", hallId);
                using var reader = command.ExecuteReader();
                while (reader.Read())
                {
                    string movieName = reader.GetString(0);
                    movieNames.Add(movieName);
                }
            }
            return movieNames;
        }

        public static List<string> GetMovieNames()
        {
            List<string> movieNames = new();

            using (var connection = OpenConnection())
            {
                string sql = $"SELECT name FROM movie";
                using var command = new NpgsqlCommand(sql, connection);
                using var reader = command.ExecuteReader();
                while (reader.Read())
                {
                    string movieName = reader.GetString(0);
                    movieNames.Add(movieName);
                }
            }
            return movieNames;
        }
    }
}

