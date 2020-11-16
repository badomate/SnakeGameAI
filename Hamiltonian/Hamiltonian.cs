using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Snake.Hamiltonian
{
    class HamiltonianCycle
    {
        int[,] gridmain;
        int w = 1;
        int maxLength;
        int k = 0;
        bool IsSafe(int[,] grid, Tuple<int, int, int> ertekek)
        {
            switch (ertekek.Item3)
            {
                case 0:
                    {
                        if (grid.GetLength(0) <= ertekek.Item1 + 1)
                            return false;
                        if (grid[ertekek.Item1 + 1, ertekek.Item2] > -1)
                            return false;
                        break;
                    }
                case 1:
                    {
                        if (ertekek.Item1 <= 0)
                            return false;
                        if (grid[ertekek.Item1 - 1, ertekek.Item2] > -1)
                            return false;
                        if ((ertekek.Item2 + 1) % 2 != 0 && ertekek.Item2 >= 2)
                        {
                            if (grid[ertekek.Item1 - 1, ertekek.Item2 - 1] > -1 && grid[ertekek.Item1 - 1, ertekek.Item2 - 2] > -1)
                                return false;
                        }
                        break;
                    }
                case 2:
                    {
                        if (ertekek.Item1 == 0)
                            return false;
                        if (grid.GetLength(1) <= ertekek.Item2 + 1)
                            return false;
                        if (grid[ertekek.Item1, ertekek.Item2 + 1] > -1)
                            return false;
                        break;
                    }
                case 3:
                    {
                        if (ertekek.Item2 <= 0)
                            return false;
                        if (grid[ertekek.Item1, ertekek.Item2 - 1] > -1 && ertekek.Item2 - 1 > 0)
                            return false;
                        break;
                    }

            }

            return true;
        }

        public bool check(int[,] grid)
        {
            bool vane = false;
            object sync = new object();
            Parallel.For(0, grid.GetLength(0), i =>
            {
                Parallel.For(0, grid.GetLength(1), j =>
                {
                    if (grid[i, j] == -1)
                    {
                        lock (sync)
                            vane = true;
                    }
                });
            });
            return vane;
        }

        public bool PathFinder(int[,] grid, Tuple<int, int, int> ertekek)
        {

            int x = ertekek.Item1;
            int y = ertekek.Item2;
            switch (ertekek.Item3)
            {
                case 0:
                    {
                        grid[x++, y] = 0;
                        break;
                    }
                case 1:
                    {
                        grid[x--, y] = 1;
                        break;
                    }
                case 2:
                    {
                        grid[x, y++] = 2;
                        break;
                    }
                case 3:
                    {
                        grid[x, y--] = 3;
                        break;
                    }
            }

            if (w == maxLength)
            {
                if (grid[0, 1] == 3)
                {
                    if (check(grid))
                        return false;
                    this.gridmain = grid;
                    return true;
                }

                return false;
            }

            for (int i = 0; i < 4; i++)
            {


                if (IsSafe(grid, new Tuple<int, int, int>(x, y, i)))
                {
                    w++;
                    if (PathFinder(grid, new Tuple<int, int, int>(x, y, i)))
                    {

                        return true;
                    }
                    w--;
                    grid[x, y] = -1;

                }
            }
            return false;
        }

        public List<int> HamiltonianGenerator(int x, int y)
        {
            w = 1;
            if (x * y % 2 != 0)
                return null;

            if (x <= 1 || y <= 1)
                return null;

            int[,] grid = new int[x, y];
            x--;
            y--;
            int irany = 0;

            for (int i = 0; i <= x; i++)
            {
                for (int j = 0; j <= y; j++)
                {
                    grid[i, j] = -1;
                }
            }

            grid[0, 0] = irany;
            maxLength = grid.GetLength(0) * grid.GetLength(1);
            bool lehete = PathFinder(grid, new Tuple<int, int, int>(0, 0, irany));
            if (lehete)
            {
                List<int> Path = new List<int>();
                int k = 0;
                int h = 0;
                do
                {
                    switch (gridmain[k, h])
                    {
                        case 0:
                            {
                                Path.Add(0);
                                k++;
                                break;
                            }
                        case 1:
                            {
                                Path.Add(1);
                                k--;
                                break;
                            }
                        case 2:
                            {
                                Path.Add(2);
                                h++;
                                break;
                            }
                        case 3:
                            {
                                Path.Add(3);
                                h--;
                                break;
                            }

                    }
                } while (!(k == 0 && h == 0));

                return Path;

            }
            return null;
        }
    }
}
