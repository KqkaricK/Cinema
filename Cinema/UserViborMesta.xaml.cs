using System;
using System.Diagnostics.CodeAnalysis;
using System.Windows;
using System.Windows.Controls;

namespace Cinema
{
    /// <summary>
    /// Логика взаимодействия для UserViborMesta.xaml
    /// </summary>
    
    public partial class UserViborMesta : UserControl
    {
        private ViborMesta? viborMesta;

        //Имя тек. фильма
        string nameMovieUserViborMesta = "";

        //При выборе фильма
        public void Start([NotNull] string nameMovie)
        {
            nameMovieUserViborMesta = nameMovie;
            viborMesta = new ViborMesta(nameMovie);
            viborMesta.DrawRectangles(CanvasViborMesta);
        }

        //Очистка канваса
        public void ClearCanvas()
        {
            CanvasViborMesta.Children.Clear();
        }

        public UserViborMesta()
        {
            InitializeComponent();
        }

        //Кнопка Save
        private void Save_Click(object sender, RoutedEventArgs e)
        {
            if (viborMesta != null && viborMesta.seats != null)
                BD.UpdateStatus(viborMesta.seats, nameMovieUserViborMesta);

            //Появление уведомления об операции
            if (SaveMessage.MessageQueue != null)
            {
                SaveMessage.MessageQueue.Enqueue("Сохраненно");
            }
        }

        //Кнопка Add
        private void Add_Click(object sender, RoutedEventArgs e)
        {
            AddFilm addFilm = new();
            addFilm.Show();
        }
    }
}
