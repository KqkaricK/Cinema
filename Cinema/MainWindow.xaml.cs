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
    }
}
