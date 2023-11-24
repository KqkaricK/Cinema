using Npgsql;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace Cinema
{
    public partial class UserAddFilm : UserControl
    {
        public UserAddFilm()
        {
            InitializeComponent();
        }

        private void AddFilm_Click(object sender, RoutedEventArgs e)
        {
            string newFilmName = nameFilmBox.Text;
            int newZalForFilm = zalBox.SelectedIndex;
            bool[,] newSeats = new bool[7, 15];
            for (int i = 0; i < 7; i++)
                for (int j = 0; j < 15; j++)
                    newSeats[i, j] = false;

            using var connection = DatabaseManager.OpenConnection();
            using var command = new NpgsqlCommand("INSERT INTO public.movie (seats, name, zal) VALUES (@seats, @name, @zal)", connection);
            command.Parameters.AddWithValue("seats", newSeats);
            command.Parameters.AddWithValue("name", newFilmName);
            command.Parameters.AddWithValue("zal", newZalForFilm);
            command.ExecuteNonQuery();

            if (SaveFilmMessage.MessageQueue != null)
                SaveFilmMessage.MessageQueue.Enqueue("Успешное добавление фильма");
        }

        private void Back_Click(object sender, RoutedEventArgs e)
        {
            if (Window.GetWindow(this) is MainWindow mainWindow)
            {
                mainWindow.SwitchVisible(1);
            }
        }

        private void ZalBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {

        }
    }
}
