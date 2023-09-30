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
    /// Логика взаимодействия для UserViborMesta.xaml
    /// </summary>
    
    public partial class UserViborMesta : UserControl
    {
        public static ViborMesta viborMesta = new();

        public void Start(int a)
        {
            if (a == 0)
            {
                MessageBox.Show("Zal1");
            }
            if (a == 1)
            {
                MessageBox.Show("Zal2");
            }
            if (a == 2)
            {
                MessageBox.Show("Zal3");
            }
        }

        public UserViborMesta()
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
