using System.Diagnostics.CodeAnalysis;
using System.Windows;
using System.Windows.Controls;

namespace Cinema
{
    public partial class UserViborMesta : UserControl
    {
        private ViborMesta? viborMesta;
        string movieName = "";

        public void Start([NotNull] string name)
        {
            movieName = name;
            viborMesta = new ViborMesta(name);
            viborMesta.DrawRectangles(CanvasViborMesta);
        }

        public void ClearCanvas()
        {
            CanvasViborMesta.Children.Clear();
        }

        public UserViborMesta()
        {
            InitializeComponent();
        }

        private void Save_Click(object sender, RoutedEventArgs e)
        {
            if (viborMesta != null && viborMesta.seats != null)
                DatabaseManager.UpdateMovieSeats(viborMesta.seats, movieName);

            if (SaveMessage.MessageQueue != null)
                SaveMessage.MessageQueue.Enqueue("Сохраненно");

        }

        private void Add_Click(object sender, RoutedEventArgs e)
        {
            AddMovie addFilm = new();
            addFilm.Show();
        }
    }
}
