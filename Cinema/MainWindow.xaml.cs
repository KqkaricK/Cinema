using System.Collections.Generic;
using System.Windows;
using System.Windows.Controls;

namespace Cinema
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
            combobox.SelectedIndex = 0;
        }

        private void ComboBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            FillComboBox(combobox.SelectedIndex);
        }

        public void FillComboBox(int tableValue)
        {
            List<string> movieNames = BD.GetFilmNames(tableValue);

            // Очищаем ComboBox
            filmbox.Items.Clear();

            // Добавляем названия фильмов в ComboBox
            foreach (string movieName in movieNames)
            {
                filmbox.Items.Add(movieName);
            }
        }

        private void filmbox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (filmbox.SelectedItem != null)
                ViborZal.Start(filmbox.SelectedValue.ToString());
            else ViborZal.ClearCanvas();
        }
    }
}
