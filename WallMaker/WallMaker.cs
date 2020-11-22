
using System;
using System.Collections.Generic;
using Snake.SnakeGame;

namespace Snake.WallMaker
{
    class WallMakerClass
    {
        public Food almaElozo;
        public int[,] WallMakerGenerator(int[,] gridUtvonal, SnakeBody kigyo, Food alma)
        {
            int[,] grid = new int[gridUtvonal.GetLength(0), gridUtvonal.GetLength(1)];
            if (almaElozo != null)
            {
                foreach (var item in kigyo.Farok)
                {
                    /*
                    if (z == (kigyo.Farok.Count / 2) + 1)
                    {
                        break;
                    }
                    z++;
                    */
                    grid[item[0], item[1]] = -1;
                }
                int x = alma.X;
                int y = alma.Y;
                //int[,] gridUtvonal = ListToMatrix(utvonal, meret);
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
            almaElozo = alma;

            //if (faroke)
            //{
            //    grid[kigyo.Farok.Last()[0], kigyo.Farok.Last()[1]] = 0;
            //}
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
                            grid[x, y] = 0;
                            y--;
                            break;
                        }
                    case 1:
                        {
                            grid[x, y] = 1;
                            y++;
                            break;
                        }
                    case 2:
                        {
                            grid[x, y] = 2;
                            x++;
                            break;
                        }
                    case 3:
                        {
                            grid[x, y] = 3;
                            x--;
                            break;
                        }
                }
            }

            return grid;
        }
    }
}