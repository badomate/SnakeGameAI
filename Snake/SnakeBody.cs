using System.Collections.Generic;
using System.Linq;

namespace Snake.SnakeGame
{
    public enum Direction
    {
        Down,
        Up,
        Right,
        Left
    }

    public class SnakeBody
    {
        public int X { get; set; }
        public int Y { get; set; }
        public List<int[]> Farok = new List<int[]>();
        public Direction Direction { get; set; }
        public bool Dead { get; set; }

        public SnakeBody(int X, int Y)
        {
            this.X = X;
            this.Y = Y;
        }

        public bool EatsItself()
        {
            if (Farok != null)
                return Farok.Any(x => x[0] == X
                                   && x[1] == Y);
            else
                return false;
        }

        public bool Contains(int a, int b)
        {
            return Farok.Any(piece => piece[0] == a && piece[1] == b);
        }

        public bool HitWall()
        {
            bool falbaVanE = false;
            if (X == -1 || X >= SnakeGameForm.Hossz || Y == -1 || Y >= SnakeGameForm.Magassag)
                falbaVanE = true;
            return falbaVanE;
        }

        public void Clear()
        {
            Farok.Clear();
            X = 0;
            Y = 0;
            Direction = Direction.Up;
        }
    }
}