using System.Windows.Controls;
using System.Windows.Shapes;

namespace Cinema
{
    public class ViborMesta
    {
        public readonly bool[,] seats;
        private const int size = 32;
        private const int space = 4;

        public ViborMesta(string nameMovie) => seats = DatabaseManager.RetrieveSeatsData(nameMovie);

        public void ToggleSeatStatus(int x, int y) => seats[x, y] = !seats[x, y];

        public void DrawRectangles(Canvas MyCanvas)
        {
            using var connection = DatabaseManager.OpenConnection();
            for (int j = 0; j < seats.GetLength(1); j++)
                for (int i = 0; i < seats.GetLength(0); i++)
                {
                    Rectangle rectangle = CreateSeatRectangle(j, i);
                    HandleRectangleMouseLeftButtonDown(rectangle);
                    MyCanvas.Children.Add(rectangle);
                    Canvas.SetLeft(rectangle, i * (size + space));
                    Canvas.SetTop(rectangle, j * (size + space));
                }
        }

        private Rectangle CreateSeatRectangle(int j, int i)
        {
            return new()
            {
                Height = size,
                Width = size,
                Fill = seats[i, j] ? MainColors.redBrush : MainColors.greenBrush
            };
        }

        private void HandleRectangleMouseLeftButtonDown(Rectangle rectangle)
        {
            rectangle.MouseLeftButtonDown += (sender, e) =>
            {
                rectangle.Fill = (rectangle.Fill == MainColors.redBrush) ? MainColors.greenBrush : MainColors.redBrush;
                int x = (int)(Canvas.GetLeft(rectangle) / (size + space));
                int y = (int)(Canvas.GetTop(rectangle) / (size + space));
                ToggleSeatStatus(x, y);
            };
        }
    }


}

