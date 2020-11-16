using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Snake.SnakeGame;
using Snake.WallMaker;

namespace Snake.PathNotFound
{
    class PathNotFoundClass
    {
        public WallMakerClass wallMaker = new WallMakerClass();
        public List<int> PathNotFoundGenerator(List<int> Path, SnakeBody kigyo)
        {
            List<int> utvonal = new List<int>();
            int[,] grid = wallMaker.ListToMatrix(Path, new Tuple<int, int>(SnakeGameForm.Hossz, SnakeGameForm.Magassag));
            int x = kigyo.X;
            int y = kigyo.Y;
            for (int i = 0; i < 1; i++)
            {
                switch (grid[x, y])
                {
                    case 0:
                        {
                            utvonal.Add(0);
                            y--;
                            break;
                        }
                    case 1:
                        {
                            utvonal.Add(1);
                            y++;
                            break;
                        }
                    case 2:
                        {
                            utvonal.Add(2);
                            x++;
                            break;
                        }
                    case 3:
                        {
                            utvonal.Add(3);
                            x--;
                            break;
                        }
                }
            }

            

            return utvonal;


        }
    }
}
