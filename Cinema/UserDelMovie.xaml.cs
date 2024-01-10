using Npgsql;
using System.Windows;
using System.Windows.Controls;

namespace Cinema
{

    public partial class UserDelMovie : UserControl
    {
        public UserDelMovie()
        {
            InitializeComponent();
            UpdateComboBox();
        }

        private void DelMovie_Click(object sender, RoutedEventArgs e)
        {
            if (!string.IsNullOrEmpty(MovieBox.Text))
                DelMovieToDataBase();

            else
                SaveMovieMessage.MessageQueue?.Enqueue("Выбирите фильм!");
        }

        private void DelMovieToDataBase()
        {
            NpgsqlConnection connection;
            NpgsqlCommand command;
            string name = MovieBox.Text;

            connection = DatabaseManager.OpenConnection();
            command = new NpgsqlCommand("DELETE FROM public.movie WHERE name = @name", connection);
            command.Parameters.AddWithValue("name", name);
            command.ExecuteNonQuery();
            UpdateComboBox();

            if (SaveMovieMessage.MessageQueue != null)
                SaveMovieMessage.MessageQueue.Enqueue("Успешное удаление фильма");
        }

        public void UpdateComboBox()
        {
            MovieBox.ItemsSource = DatabaseManager.GetMovieNames();
        }

        private void Back_Click(object sender, RoutedEventArgs e)
        {
            if (Window.GetWindow(this) is MainWindow mainWindow)
            {
                mainWindow.SwitchVisibility(MainWindow.VisibilityMode.ViborZal);
            }
            MovieBox.SelectedIndex = -1;
            DelMovie.IsEnabled = false;
        }

        private void MovieBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            DelMovie.IsEnabled = true;
        }
    }
}
