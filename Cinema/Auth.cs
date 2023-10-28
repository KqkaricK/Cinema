using Npgsql;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace Cinema
{
    public class Auth
    {
        private const string ConnectionString = "Host=localhost;Port=5432;Username=postgres;Password=111;Database=Cinema";

        public static void AutorizationCheck()
        {
            if (Autorization() == null)
            {
                MessageBox.Show("Доступ запрещён!");
                Application.Current.Shutdown();
            }
        }

        public static string Autorization()
        {
            string computerName = System.Security.Principal.WindowsIdentity.GetCurrent().Name;
            string userName = null;

            using (var connection = new NpgsqlConnection(ConnectionString))
            {
                connection.Open();
                string sql = "SELECT login FROM users WHERE login = @computerName";

                using (var command = new NpgsqlCommand(sql, connection))
                {
                    command.Parameters.AddWithValue("computerName", computerName);
                    userName = command.ExecuteScalar() as string;
                }
            }
            return userName;
        }
        public static string GetUserName()
        {
            string computerName = System.Security.Principal.WindowsIdentity.GetCurrent().Name;
            string userName = null;

            using (var connection = new NpgsqlConnection(ConnectionString))
            {
                connection.Open();
                string sql = "SELECT name FROM users WHERE login = @computerName";

                using (var command = new NpgsqlCommand(sql, connection))
                {
                    command.Parameters.AddWithValue("computerName", computerName);
                    userName = command.ExecuteScalar() as string;
                }
            }

            return userName;
        }
    }
}
