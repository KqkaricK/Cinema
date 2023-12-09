using Npgsql;
using System.Windows;
using System.Windows.Controls;

namespace Cinema
{
    public partial class UserAddMovie : UserControl
    {
        public UserAddMovie()
        {
            InitializeComponent();
        }

        private void AddMovie_Click(object sender, RoutedEventArgs e)
        {
            if (!string.IsNullOrEmpty(NameMovieBox.Text) && !string.IsNullOrEmpty(ZalBox.Text))
                AddMovieToDataBase();

            else if (SaveMovieMessage.MessageQueue != null)
                SaveMovieMessage.MessageQueue.Enqueue("Добавьте название фильма!");
        }

        private void AddMovieToDataBase()
        {
            NpgsqlConnection connection;
            NpgsqlCommand command;
            string newMovieName = NameMovieBox.Text;
            int newZalForMovie = ZalBox.SelectedIndex;
            bool[,] newSeats = new bool[7, 15];

            for (int i = 0; i < 7; i++)
                for (int j = 0; j < 15; j++)
                    newSeats[i, j] = false;
            connection = DatabaseManager.OpenConnection();
            command = new NpgsqlCommand("INSERT INTO public.movie (seats, name, zal) VALUES (@seats, @name, @zal)", connection);
            command.Parameters.AddWithValue("seats", newSeats);
            command.Parameters.AddWithValue("name", newMovieName);
            command.Parameters.AddWithValue("zal", newZalForMovie);
            command.ExecuteNonQuery();

            if (SaveMovieMessage.MessageQueue != null)
                SaveMovieMessage.MessageQueue.Enqueue("Успешное добавление фильма");
        }

        private void Back_Click(object sender, RoutedEventArgs e)
        {
            if (Window.GetWindow(this) is MainWindow mainWindow)
            {
                mainWindow.SwitchVisibility(MainWindow.VisibilityMode.ViborZal);
            }
            NameMovieBox.Text = "";
            ZalBox.SelectedIndex = -1;
            AddMovie.IsEnabled = false;
        }

        private void ZalBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            AddMovie.IsEnabled = true;
        }
    }
}
