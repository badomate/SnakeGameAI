using System;
using System.Collections.Generic;
using System.Linq;
using Snake.Astar;
using Snake.SnakeGame;
using Snake.WallMaker;

namespace Snake.CanEat
{
    class CanEatClass
    {
        public PathFinderAStar pathFinderAStar = new PathFinderAStar();
        public WallMakerClass wallMaker = new WallMakerClass();
        public bool CanEatClassGenerator(int[,] grid, SnakeBody kigyo, Food kaja, out List<int> AStar)
        {
            // System.Drawing.Point

            List<int[]> snake = new List<int[]>();
            kigyo.Farok.ForEach(c => snake.Add(new int[] { c[0], c[1] }));
            bool ehete = true;
            int[,] wall_grid = wallMaker.WallMakerGenerator(grid, kigyo, kaja);
            List<Tile> almostPath;
            List<int> lerovidebb_ut_kigyo_alma = this.pathFinderAStar.AStarGenerator(wall_grid, new Tuple<int, int>(kigyo.X, kigyo.Y), new Tuple<int, int>(kaja.X, kaja.Y), kigyo, out almostPath);
            AStar = lerovidebb_ut_kigyo_alma;
            if (lerovidebb_ut_kigyo_alma is null)
            {
                return false;
            }
            if (kigyo.Farok != null)
                if (kigyo.Farok.Count > grid.GetLength(1) * grid.GetLength(0) * 0.05)
                {

                    List<int[]> vizsgalando_hamiltonian = new List<int[]>();
                    int x = kaja.X;
                    int y = kaja.Y;

                    for (int i = 0; i <= snake.Count(); i++)
                    {
                        switch (grid[x, y])
                        {
                            case 0:
                                {
                                    y--;
                                    break;
                                }
                            case 1:
                                {
                                    y++;
                                    break;
                                }
                            case 2:

                                {
                                    x++;
                                    break;
                                }
                            case 3:
                                {
                                    x--;
                                    break;
                                }
                        }

                        vizsgalando_hamiltonian.Add(new int[] { x, y });
                    }
                    x = kigyo.X;
                    y = kigyo.Y;
                    int z = 0;
                    do
                    {
                        if (vizsgalando_hamiltonian.Contains(new int[] { x, y }) == true)
                            return false;

                        if (lerovidebb_ut_kigyo_alma.Count < snake.Count())
                        {
                            int mennyivel_hosszabb = snake.Count() - lerovidebb_ut_kigyo_alma.Count;

                            for (int i = snake.Count() - 1; i >= 0; i--)
                            {
                                if (i > 0)
                                {
                                    if (snake[i] == snake[i - 1])
                                        continue;
                                    snake[i][0] = snake[i - 1][0];
                                    snake[i][1] = snake[i - 1][1];
                                    continue;
                                }
                                snake[0][0] = x;
                                snake[0][1] = y;
                            }

                            for (int i = mennyivel_hosszabb; i >= 0; i--)
                            {
                                if (vizsgalando_hamiltonian.Where(w => w[0] == snake[i][0]).Count() > 0)
                                    if (vizsgalando_hamiltonian.Where(w => w[1] == snake[i][1]).Count() > 0)
                                        return false;
                            }
                        }

                        switch (lerovidebb_ut_kigyo_alma[z])
                        {
                            case 0:
                                {
                                    y--;
                                    break;
                                }
                            case 1:
                                {
                                    y++;
                                    break;
                                }
                            case 2:

                                {
                                    x++;
                                    break;
                                }
                            case 3:
                                {
                                    x--;
                                    break;
                                }
                        }

                        z++;
                    } while (!(x == kaja.X && y == kaja.Y));





                }
            return ehete;
        }
    }
}
