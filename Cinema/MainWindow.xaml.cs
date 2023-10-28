using System;
using System.Collections.Generic;
using System.Threading;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Threading;

namespace Cinema
{    
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
            Auto();
            comboBox.SelectedIndex = 0;
        }

        public void Auto()
        {
            Auth.AutorizationCheck();
            userNameLabel.Content = $"Добро пожаловать, {Auth.GetUserName()}";
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
