/*using System;
using System.Drawing;
using System.Linq;

namespace Snake.SnakeGame
{
    public class Game
    {
        private readonly int m_Width;
        private readonly int m_Height;
        private readonly Food m_Food;
        private readonly Random m_Rnd;

        public Game(int width, int height)
        {
            m_Width = width;
            m_Height = height;
            m_Rnd = new Random();
            m_Food = new Food(0, 0, Brushes.Violet);
            Restart();
        }

        public void Restart()
        {
            GenerateFood();
        }

        public int GetScore()
        {
            return m_Snake.Farok.Count() +1;
        }

        public void ChangeSnakeDIrection(Direction direction)
        {
            m_Snake.Direction = direction;
        }

        private void GenerateFood()
        {
            var a = m_Rnd.Next(0, m_Width);
            var b = m_Rnd.Next(0, m_Height);
            m_Food.X = a;
            m_Food.Y = b;
        }
    }
}
*/