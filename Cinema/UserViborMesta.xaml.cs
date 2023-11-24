using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Windows;
using System.Windows.Controls;

namespace Cinema
{
    public partial class UserViborMesta : UserControl
    {
        private ViborMesta? viborMesta;
        private string movieName = "";

        public UserViborMesta()
        {
            InitializeComponent();
            comboBox.SelectedIndex = 0;
            RefreshFilmList(comboBox.SelectedIndex);
            UpdateSaveButtonState();
        }

        private void ComboBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            RefreshFilmList(comboBox.SelectedIndex);
        }

        private void MovieBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (movieBox.SelectedItem != null)
                Start(movieBox.SelectedValue.ToString());
            else
                CanvasViborMesta.Children.Clear();
            UpdateSaveButtonState();
        }

        public void Start([NotNull] string name)
        {
            movieName = name;
            viborMesta = new ViborMesta(name);
            viborMesta.DrawRectangles(CanvasViborMesta);
        }

        private void Save_Click(object sender, RoutedEventArgs e)
        {
            if (viborMesta != null && viborMesta.seats != null)
                DatabaseManager.UpdateMovieSeats(viborMesta.seats, movieName);

            if (SaveMessage.MessageQueue != null)
                SaveMessage.MessageQueue.Enqueue("Сохранено");
        }

        private void Add_Click(object sender, RoutedEventArgs e)
        {
            if (Window.GetWindow(this) is MainWindow mainWindow)
            {
                mainWindow.SwitchVisible(0);
            }
        }

        private void RefreshFilmList(int tableValue)
        {
            List<string> movieNames = DatabaseManager.GetMovieNamesForHall(tableValue);
            movieBox.Items.Clear();
            foreach (string movieName in movieNames)
            {
                movieBox.Items.Add(movieName);
            }
        }

        private void UpdateSaveButtonState()
        {
            Save.IsEnabled = movieBox.SelectedItem != null;
        }
    }
}
