
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Snake.SnakeGame;

namespace Snake.WallMaker
{
    class WallMakerClass
    {
        public Food almaElozo;
        public int[,] WallMakerGenerator(List<int> utvonal, SnakeBody kigyo, Food alma, Tuple<int, int> meret, bool faroke)
        {
            int[,] grid = new int[meret.Item1, meret.Item2];
            int z = 0;
            if (almaElozo != null)
            {
                foreach (var item in kigyo.Farok)
                {
                    //if (z == (kigyo.Farok.Count / 2) + 1)
                    //{
                    //    break;
                    //}
                    grid[item[0], item[1]] = -1;
                    z++;
                }
                int x = alma.X;
                int y = alma.Y;
                int[,] gridUtvonal = ListToMatrix(utvonal, meret);
                if (!faroke)
                {
                    for (int i = 0; i <= kigyo.Farok.Count + 1; i++)
                    {
                        switch (gridUtvonal[x, y])
                        {
                            case 0:  //fel
                                {
                                    grid[x, y - 1] = -1;
                                    y--;
                                    break;
                                }
                            case 1:  //le
                                {
                                    grid[x, y + 1] = -1;
                                    y++;
                                    break;
                                }
                            case 2:  //jobbra
                                {
                                    grid[x + 1, y] = -1;
                                    x++;
                                    break;
                                }
                            case 3:  //balra
                                {
                                    grid[x - 1, y] = -1;
                                    x--;
                                    break;
                                }
                        }
                    }
                }
            }
            if (!faroke)
            {
                almaElozo = alma;
            }
            if(faroke)
            {
                grid[kigyo.Farok.Last()[0], kigyo.Farok.Last()[1]] = 0;
            }
            return grid;


        }
        public int[,] ListToMatrix(List<int> utvonal, Tuple<int, int> meret)
        {
            int[,] grid = new int[meret.Item1, meret.Item2];
            int x = 0;
            int y = 0;
            foreach (int item in utvonal)
            {
                switch (item)
                {
                    case 0:
                        {
                            grid[x, y] = 2;
                            x++;
                            break;
                        }
                    case 1:
                        {
                            grid[x, y] = 3;
                            x--;
                            break;
                        }
                    case 2:
                        {
                            grid[x, y] = 1;
                            y++;
                            break;
                        }
                    case 3:
                        {
                            grid[x, y] = 0;
                            y--;
                            break;
                        }
                }
            }

            return grid;
        }
    }
}