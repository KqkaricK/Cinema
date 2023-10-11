using Npgsql;

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
            if (connection.State == System.Data.ConnectionState.Open)
                connection.Close();
        }

        // Получение данных из базы данных
        public static bool[,] TakenData(string tableName)
        {
            // Инициализация массива для хранения данных
            bool[,] data = new bool[15, 7];

            using (var connection = new NpgsqlConnection(ConnectionString))
            {
                connection.Open();

                // Выполнение запроса для извлечения
                string sql = $"SELECT row, place, taken FROM {tableName}";
                using (var command = new NpgsqlCommand(sql, connection))
                using (var reader = command.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        int x = reader.GetInt32(0);
                        int y = reader.GetInt32(1);
                        bool status = reader.GetBoolean(2);

                        // Заполнение массива данными из базы
                        data[x, y] = status;
                    }
                }
            }
            return data;
        }

        //Обнов данных
        public static void UpdateStatus(bool[,] seats, string tableName)
        {
            using var connection = OpenConnection();
            using var command = connection.CreateCommand();
            command.Connection = connection;
            command.CommandText = $"UPDATE {tableName} SET taken = @taken WHERE row = @row AND place = @place";
            for (int x = 0; x < seats.GetLength(0); x++)
            {
                for (int y = 0; y < seats.GetLength(1); y++)
                {
                    command.Parameters.Clear();
                    command.Parameters.AddWithValue("taken", seats[x, y]);
                    command.Parameters.AddWithValue("row", x);
                    command.Parameters.AddWithValue("place", y);
                    command.ExecuteNonQuery();
                }
            }
        }
    }
}

