using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Shapes;

namespace Cinema
{
    public class ViborMesta
    {
        public readonly bool[,] seats;
        private const int baseSize = 32;
        private const int enlargedSize = 36;
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
                    Canvas.SetLeft(rectangle, i * (baseSize + space));
                    Canvas.SetTop(rectangle, j * (baseSize + space));
                }
        }

        private Rectangle CreateSeatRectangle(int j, int i)
        {
            return new()
            {
                Height = baseSize,
                Width = baseSize,
                Fill = seats[i, j] ? MainColors.redBrush : MainColors.greenBrush
            };
        }

        private void HandleRectangleMouseLeftButtonDown(Rectangle rectangle)
        {
            rectangle.MouseLeftButtonDown += (sender, e) =>
            {
                rectangle.Fill = (rectangle.Fill == MainColors.redBrush) ? MainColors.greenBrush : MainColors.redBrush;
                int x = (int)(Canvas.GetLeft(rectangle) / (baseSize + space));
                int y = (int)(Canvas.GetTop(rectangle) / (baseSize + space));
                ToggleSeatStatus(x, y);
            };

            rectangle.MouseEnter += (sender, e) =>
            {
                double newLeft = Canvas.GetLeft(rectangle) - (enlargedSize - baseSize) / 2;
                double newTop = Canvas.GetTop(rectangle) - (enlargedSize - baseSize) / 2;
                rectangle.Width = rectangle.Height = enlargedSize;
                Canvas.SetLeft(rectangle, newLeft);
                Canvas.SetTop(rectangle, newTop);
                rectangle.Cursor = Cursors.Hand;
            };

            rectangle.MouseLeave += (sender, e) =>
            {
                double newLeft = Canvas.GetLeft(rectangle) + (enlargedSize - baseSize) / 2;
                double newTop = Canvas.GetTop(rectangle) + (enlargedSize - baseSize) / 2;
                rectangle.Width = rectangle.Height = baseSize;
                Canvas.SetLeft(rectangle, newLeft);
                Canvas.SetTop(rectangle, newTop);
                rectangle.Cursor = Cursors.Arrow;
            };
        }
    }
}

