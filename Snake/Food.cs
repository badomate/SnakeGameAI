
using System.Drawing;

namespace Snake.SnakeGame
{
    public class Food
    {
        public static int SIDE = 32;
        private readonly Brush m_Color = Brushes.Red;
        public Food elozo { get; set; }
        public int X { get; set; }
        public int Y { get; set; }
        public bool Eaten { get; set; }
        public Food(int a, int b)
        {
            X = a;
            Y = b;
        }

        public void Draw(Graphics g)
        {
            g.FillRectangle(m_Color, new Rectangle(X * SIDE, Y * SIDE, SIDE, SIDE));
        }
    }
}