using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using Snake.SnakeGame;

namespace Snake.Astar
{
    class PathFinderAStar
    {
        public SnakeBody Snake;
        List<int[]> Tilos;
        public List<int> AStarGenerator(int[,] grid, Tuple<int, int> start, Tuple<int, int> finish, SnakeBody snake, out List<Tile> almostPath)
        {
            Tilos = new List<int[]>();
            for (int i = 0; i < grid.GetLength(0); i++)
            {
                for (int j = 0; j < grid.GetLength(1); j++)
                {
                    if (grid[i, j] == -1)
                        Tilos.Add(new int[] { i, j });
                }
            }

            if (snake.Direction != null)
            {
                switch (snake.Direction)
                {
                    case SnakeGame.Direction.Down:
                        {
                            grid[start.Item1, start.Item2] = -1;
                            Tilos.Add(new int[] { start.Item1, start.Item2 });
                            break;
                        }
                    case SnakeGame.Direction.Up:
                        {
                            grid[start.Item1, start.Item2] = -1;
                            Tilos.Add(new int[] { start.Item1, start.Item2 });
                            break;
                        }
                    case SnakeGame.Direction.Right:
                        {
                            grid[start.Item1, start.Item2] = -1;
                            Tilos.Add(new int[] { start.Item1, start.Item2 });
                            break;
                        }
                    case SnakeGame.Direction.Left:
                        {
                            grid[start.Item1, start.Item2] = -1;
                            Tilos.Add(new int[] { start.Item1, start.Item2 });
                            break;
                        }
                }
            }
            this.Snake = snake;

            Tile _finish = new Tile()
            {
                X = finish.Item1,
                Y = finish.Item2
            };
            Tile _start = new Tile()
            {
                X = start.Item1,
                Y = start.Item2
            };
            almostPath = PathFinder(grid, _start, _finish);

            almostPath.Reverse();
            List<int> path = new List<int>();
            if (almostPath.Count == 0)
                return null;
            Tile elozo = almostPath.First();
            //almostPath.RemoveAt(0);

            foreach (Tile item in almostPath)
            {
                if (grid[item.X, item.Y] == -1 && item.X != start.Item1 && item.Y != start.Item2 && item.X != finish.Item1 && item.Y != finish.Item2)
                {
                    Debug.WriteLine("Ilyen se lehet 2");
                    return null;
                }
                if (elozo != null)
                {
                    //right
                    if (elozo.X < item.X)
                    {
                        path.Add(2);
                    }
                    //left
                    if (elozo.X > item.X)
                    {
                        path.Add(3);
                    }
                    //down
                    if (elozo.Y < item.Y)
                    {
                        path.Add(1);
                    }
                    //up
                    if (elozo.Y > item.Y)
                    {
                        path.Add(0);
                    }

                }
                elozo = item;
            }
            if (path.Count == 0)
            {
                return null;
            }
            Tilos.Clear();
            return path;
        }

        private List<Tile> PathFinder(int[,] grid, Tile start, Tile finish)
        {

            start.SetDistance(finish.X, finish.Y);
            var activeTiles = new List<Tile>();
            activeTiles.Add(start);
            var visitedTiles = new List<Tile>();
            Tilos = Tilos.Where(x => x[0] != finish.X || x[1] != finish.Y).ToList();
            foreach (var item in Tilos)
            {
                if (item[0] == finish.X && item[1] == finish.X)
                    continue;

                visitedTiles.Add(new Tile() { X = item[0], Y = item[1] });
            }
            while (activeTiles.Any())
            {
                var checkTile = activeTiles.OrderBy(x => x.CostDistance).First();

                if (checkTile.X == finish.X && checkTile.Y == finish.Y)
                {
                    var tile = checkTile;
                    List<Tile> megoldas = new List<Tile>();
                    while (true)
                    {

                        megoldas.Add(tile);
                        tile = tile.Parent;
                        if (tile == null)
                        {
                            return megoldas;
                        }

                    }
                }

                visitedTiles.Add(checkTile);
                activeTiles.Remove(checkTile);

                var walkableTiles = Szomszedok(grid, checkTile, finish);

                foreach (var walkableTile in walkableTiles)
                {
                    if (visitedTiles.Any(x => x.X == walkableTile.X && x.Y == walkableTile.Y))
                        continue;
                    walkableTile.Parent = checkTile;
                    if (activeTiles.Any(x => x.X == walkableTile.X && x.Y == walkableTile.Y))
                    {
                        var existingTile = activeTiles.First(x => x.X == walkableTile.X && x.Y == walkableTile.Y);
                        if (existingTile.CostDistance > checkTile.CostDistance)
                        {
                            activeTiles.Remove(existingTile);
                            activeTiles.Add(walkableTile);
                        }
                    }
                    else
                    {
                        activeTiles.Add(walkableTile);
                    }
                }


            }

            List<Tile> megoldass = new List<Tile>();
            return megoldass;

        }

        private List<Tile> Szomszedok(int[,] grid, Tile hely, Tile target)
        {
            List<Tile> szomszedok = new List<Tile>()
            {
                new Tile { X = hely.X, Y = hely.Y - 1 },
                new Tile { X = hely.X, Y = hely.Y + 1 },
                new Tile { X = hely.X - 1, Y = hely.Y },
                new Tile { X = hely.X + 1, Y = hely.Y },
            };

            szomszedok.ForEach(tile => tile.SetDistance(target.X, target.Y));

            szomszedok = szomszedok.Where(x => x.X < grid.GetLength(0)).ToList();
            szomszedok = szomszedok.Where(x => x.Y < grid.GetLength(1)).ToList();
            szomszedok = szomszedok.Where(x => x.X >= 0).ToList();
            szomszedok = szomszedok.Where(x => x.Y >= 0).ToList();


            if (szomszedok.Where(x => x.X == target.X && x.Y == target.Y).Count() > 0)
            {
                return szomszedok;
            }

            while (szomszedok.Where(x => Tilos.Where(y => y[0] == x.X && y[1] == x.Y).Count() > 0).Count() > 0)
            {
                var szomszed = szomszedok.Where(x => Tilos.Where(y => y[0] == x.X && y[1] == x.Y).Count() > 0).First();
                szomszedok.Remove(szomszed);
            }
            if (szomszedok.Where(x => grid[x.X, x.Y] == -1).Count() > 0)
            {
                szomszedok = szomszedok.Where(x => grid[x.X, x.Y] != -1).ToList();
            }
            if (szomszedok.Any(x => x.X < 0))
            {
                Debug.WriteLine("df");
            }
            return szomszedok;
        }
    }
}
