using System;
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

        string tableName = "";

        public void Start(int a)
        {
            tableName = a switch
            {
                0 => "zal1",
                1 => "zal2",
                2 => "zal3",
                _ => throw new ArgumentException("Invalid value"),
            };

            viborMesta = new ViborMesta(tableName);
            viborMesta.DrawRectangles(CanvasViborMesta);
        }

        public UserViborMesta()
        {
            InitializeComponent();
        }
        private void Save_Click(object sender, RoutedEventArgs e)
        {
            if (viborMesta != null && viborMesta.seats != null)
                BD.UpdateStatus(viborMesta.seats, tableName);
            //Появление уведомления об операции
            if (SaveMessage.MessageQueue != null)
            {
                SaveMessage.MessageQueue.Enqueue("Сохраненно");
            }
        }
    }
}
