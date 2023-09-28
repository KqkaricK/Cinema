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
    class ViborMesta
    {
        private bool[,] seats = new bool[15, 7];
        private const int size = 32;
        private const int space = 4;

        public void SwitchStatus(int x, int y)
        {
            seats[x, y] = !seats[x, y];
        }

        public void DrawRectangles(Canvas MyCanvas)
        {
            for (int j = 0; j < seats.GetLength(1); j++)
            {
                for (int i = 0; i < seats.GetLength(0); i++)
                {
                    Rectangle rectangle = new Rectangle
                    {
                        Height = size,
                        Width = size,
                    };

                    rectangle.Fill = seats[i, j] ? Brushes.Red : Brushes.Green;

                    rectangle.MouseLeftButtonDown += (sender, e) =>
                    {
                        // Меняем цвет на противоположный
                        rectangle.Fill = (rectangle.Fill == Brushes.Red) ? Brushes.Green : Brushes.Red;

                        // Получаем индексы из Canvas.Left и Canvas.Top
                        int x = (int)(Canvas.GetLeft(rectangle) / (size + space));
                        int y = (int)(Canvas.GetTop(rectangle) / (size + space));

                        // Вызываем метод для изменения значения в массиве
                        SwitchStatus(x, y);
                    };

                    MyCanvas.Children.Add(rectangle);

                    Canvas.SetLeft(rectangle, i * (size + space));
                    Canvas.SetTop(rectangle, j * (size + space));
                }
            }
        }
    }
}

