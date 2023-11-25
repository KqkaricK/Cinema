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
            ZalComboBox.SelectedIndex = 0;
            RefreshMovieList(ZalComboBox.SelectedIndex);
            UpdateSaveButtonState();
        }

        private void ComboBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            RefreshMovieList(ZalComboBox.SelectedIndex);
        }

        private void MovieBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (MovieBox.SelectedItem != null)
                Start(MovieBox.SelectedValue.ToString());
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
            MovieBox.SelectedIndex = -1;
        }

        private void RefreshMovieList(int tableValue)
        {
            List<string> movieNames = DatabaseManager.GetMovieNamesForHall(tableValue);
            MovieBox.Items.Clear();
            foreach (string movieName in movieNames)
            {
                MovieBox.Items.Add(movieName);
            }
        }

        private void UpdateSaveButtonState()
        {
            Save.IsEnabled = MovieBox.SelectedItem != null;
        }
    }
}
