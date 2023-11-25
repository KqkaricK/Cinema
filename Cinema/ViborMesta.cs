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
            MyCanvas.Children.Clear();
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

            double offset = (enlargedSize - baseSize) / 2;

            rectangle.MouseEnter += (sender, e) =>
            {
                ChangeRectangleSize(rectangle, enlargedSize, -offset);
                rectangle.Cursor = Cursors.Hand;
            };

            rectangle.MouseLeave += (sender, e) =>
            {
                ChangeRectangleSize(rectangle, baseSize, offset);
                rectangle.Cursor = Cursors.Arrow;
            };
        }

        private void ChangeRectangleSize(Rectangle rectangle, double newSize, double offset)
        {
            double newLeft = Canvas.GetLeft(rectangle) + offset;
            double newTop = Canvas.GetTop(rectangle) + offset;
            rectangle.Width = rectangle.Height = newSize;
            Canvas.SetLeft(rectangle, newLeft);
            Canvas.SetTop(rectangle, newTop);
        }
    }
}

