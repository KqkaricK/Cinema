using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace Cinema
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private ViborMesta viborMesta = new ViborMesta();
        public MainWindow()
        {
            InitializeComponent();
            viborMesta.DrawRectangles(CanvasViborMesta);
        }

        private void Save_Click(object sender, RoutedEventArgs e)
        {
            BD.UpdateStatus(viborMesta.seats);
            //Появление уведомления об операции
            SaveMessage.MessageQueue.Enqueue("Сохраненно...");
        }
    }
}
