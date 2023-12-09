using System.Windows;

namespace Cinema
{    
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
            Auth.AuthorizationCheck();
            userNameLabel.Content = $"Добро пожаловать, {Auth.UserName}";
        }

        public enum VisibilityMode
        {
            AddMovie,
            ViborZal,
            DelMovie,
        }

        public void SwitchVisibility(VisibilityMode mode)
        {
            AddMovie.Visibility = Visibility.Collapsed;
            ViborZal.Visibility = Visibility.Collapsed;
            DelMovie.Visibility = Visibility.Collapsed;

            switch (mode)
            {
                case VisibilityMode.AddMovie:
                    AddMovie.Visibility = Visibility.Visible;
                    break;
                case VisibilityMode.ViborZal:
                    ViborZal.Visibility = Visibility.Visible;
                    ViborZal.RefreshMovieList(0);
                    ViborZal.RefreshZalList();
                    break;
                case VisibilityMode.DelMovie:
                    DelMovie.Visibility = Visibility.Visible;
                    DelMovie.UpdateComboBox();
                    break;
                default:
                    break;
            }
        }
    }
}
