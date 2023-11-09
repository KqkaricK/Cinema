using Npgsql;
using System.Windows;

namespace Cinema
{
    public class Auth
    {
        private static readonly string computerName = System.Security.Principal.WindowsIdentity.GetCurrent().Name;

        public static string? UserName { get; set; } = null;
        public static string? UserLogin { get; set; } = null;

        public static void AutorizationCheck()
        {
            Autorization();
            if (UserLogin == null)
            {
                MessageBox.Show("Доступ запрещён!");
                Application.Current.Shutdown();
            }
        }

        private static void Autorization()
        {
            var sqlUserLogin = "SELECT login FROM users WHERE login = @computerName";
            var sqlUserName = "SELECT name FROM users WHERE login = @computerName";

            UserLogin = ExecuteSQLQuery(sqlUserLogin, UserLogin);
            UserName = ExecuteSQLQuery(sqlUserName, UserName);
        }

        private static string? ExecuteSQLQuery(string sqlQuery, string? userData)
        {
            using (var command = new NpgsqlCommand(sqlQuery, DatabaseManager.OpenConnection()))
            {
                command.Parameters.AddWithValue("computerName", computerName);
                userData = command.ExecuteScalar() as string;
            }
            return userData;
        }
    }
}
