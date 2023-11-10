using Npgsql;
using System.Windows;

namespace Cinema
{
    public class Auth
    {
        public static string? UserLogin { get; set; } = null;
        public static string? UserName { get; set; } = null;

        public static bool IsAuthorized => UserLogin != null;

        public static void AuthorizationCheck()
        {
            Autorization();
            if (!IsAuthorized)
            {
                MessageBox.Show("Доступ запрещён!");
                Application.Current.Shutdown();
            }
        }

        private static void Autorization()
        {
            UserLogin = GetUserData("SELECT login FROM users WHERE login = @computerName");
            UserName = GetUserData("SELECT name FROM users WHERE login = @computerName");
        }

        private static string? GetUserData(string sqlQuery)
        {
            string computerName = System.Security.Principal.WindowsIdentity.GetCurrent().Name;
            using var connection = DatabaseManager.OpenConnection();
            using var command = new NpgsqlCommand(sqlQuery, connection);
            command.Parameters.AddWithValue("computerName", computerName);
            return command.ExecuteScalar() as string;
        }
    }
}
