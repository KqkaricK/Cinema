using System.Collections.Generic;
using System.Windows;
using System.Windows.Controls;

namespace Cinema
{    
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
            comboBox.SelectedIndex = 0;
        }

        private void ComboBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            RefreshFilmList(comboBox.SelectedIndex);
        }

        public void RefreshFilmList(int tableValue)
        {
            List<string> movieNames = DatabaseManager.GetMovieNamesForHall(tableValue);
            movieBox.Items.Clear();
            foreach (string movieName in movieNames)
            {
                movieBox.Items.Add(movieName);
            }
        }

        private void MovieBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (movieBox.SelectedItem != null)
                ViborZal.Start(movieBox.SelectedValue.ToString());
            else ViborZal.ClearCanvas();
        }
    }
}
