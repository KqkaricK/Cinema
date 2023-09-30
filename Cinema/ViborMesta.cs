using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;
using System.Windows.Media;
using System.Windows.Shapes;

namespace Cinema
{
    public class ViborMesta
    {
        public readonly bool[,] seats;
        private const int size = 32;
        private const int space = 4;

        public ViborMesta()
        {
            // Получаем данные из базы
            seats = BD.TakenData();
        }

        public void SwitchStatus(int x, int y) => seats[x, y] = !seats[x, y];

        public void DrawRectangles(Canvas MyCanvas)
        {
            using var connection = BD.OpenConnection();
            for (int j = 0; j < seats.GetLength(1); j++)
            {
                for (int i = 0; i < seats.GetLength(0); i++)
                {
                    // Создание прям
                    Rectangle rectangle = new()
                    {
                        Height = size,
                        Width = size,
                        // Задание цвета
                        Fill = seats[i, j] ? MainColors.redBrush : MainColors.greenBrush
                    };
                    // Нажатия мыши на прям
                    rectangle.MouseLeftButtonDown += (sender, e) =>
                    {
                        // Переключение цвета
                        rectangle.Fill = (rectangle.Fill == MainColors.redBrush) ? MainColors.greenBrush : MainColors.redBrush;
                        // Вычисление и смена
                        int x = (int)(Canvas.GetLeft(rectangle) / (size + space));
                        int y = (int)(Canvas.GetTop(rectangle) / (size + space));
                        SwitchStatus(x, y);
                    };
                    // Добавление прям
                    MyCanvas.Children.Add(rectangle);
                    Canvas.SetLeft(rectangle, i * (size + space));
                    Canvas.SetTop(rectangle, j * (size + space));
                }
            }
        }
    }
}

