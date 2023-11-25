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

        public void SwitchVisible(int value)
        {
            switch (value)
            {
                case 0:
                    ViborZal.Visibility = Visibility.Collapsed;
                    AddMovie.Visibility = Visibility.Visible;
                    break;
                case 1:
                    ViborZal.Visibility = Visibility.Visible;
                    AddMovie.Visibility = Visibility.Collapsed;
                    break;
                default:
                    break;
            }
        }
    }
}
