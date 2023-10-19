using Npgsql;
using System;
using System.Collections.Generic;
using System.Data;

namespace Cinema
{
    public static class BD
    {
        // Строка подкл
        private const string ConnectionString = "Host=localhost;Port=5432;Username=postgres;Password=111;Database=Cinema";
        
        // Открыть подкл
        public static NpgsqlConnection OpenConnection()
        {
            var connection = new NpgsqlConnection(ConnectionString);
            connection.Open();
            return connection;
        }

        // Закрыть подкл
        public static void CloseConnection(NpgsqlConnection connection)
        {
            if (connection.State == ConnectionState.Open)
                connection.Close();
        }

        // Получение данных из базы данных
        public static bool[,] TakenData(string movieName)
        {
            // Инициализация массива для хранения данных
            bool[,] data = new bool[15, 7];

            using (var connection = OpenConnection())
            {
                // Выполнение запроса для извлечения
                string sql = $"SELECT seats FROM movie WHERE name = @name";
                using var command = new NpgsqlCommand(sql, connection);
                command.Parameters.AddWithValue("name", movieName);

                // Извлекаем массив мест и копируем его в массив данных
                if (command.ExecuteScalar() is bool[,] seatsArray)
                {
                    for (int i = 0; i < Math.Min(seatsArray.GetLength(0), 15); i++)
                    {
                        for (int j = 0; j < Math.Min(seatsArray.GetLength(1), 7); j++)
                        {
                            data[i, j] = seatsArray[i, j];
                        }
                    }
                }
            }
            return data;
        }

        //Обнов данных
        public static void UpdateStatus(bool[,] seats, string movieName)
        {
            using var connection = OpenConnection();
            using var command = connection.CreateCommand();
            command.Connection = connection;
            command.CommandText = "UPDATE movie SET seats = @seats WHERE name = @name";
            command.Parameters.AddWithValue("seats", seats);
            command.Parameters.AddWithValue("name", movieName);
            command.ExecuteNonQuery();
        }

        //Получение имяни фильмов
        public static List<string> GetFilmNames(int tableValue)
        {
            List<string> filmNames = new();

            using (var connection = OpenConnection())
            {
                string sql = $"SELECT name FROM movie WHERE zal={tableValue}";
                using var command = new NpgsqlCommand(sql, connection);
                using var reader = command.ExecuteReader();
                while (reader.Read())
                {
                    string movieName = reader.GetString(0);
                    filmNames.Add(movieName);
                }
            }
            return filmNames;
        }
    }
}

