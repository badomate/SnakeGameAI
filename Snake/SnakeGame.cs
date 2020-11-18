using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using Snake.Astar;
using Snake.CanEat;
using Snake.Hamiltonian;
using Snake.PathNotFound;
using Snake.WallMaker;

namespace Snake.SnakeGame
{
    public partial class SnakeGameForm : Form
    {
        HamiltonianCycle hamiltonian = new HamiltonianCycle();
        PathNotFoundClass pathNotFound = new PathNotFoundClass();
        WallMakerClass wallMaker = new WallMakerClass();
        PathFinderAStar pathFinder = new PathFinderAStar();
        CanEatClass canEat = new CanEatClass();

        public List<SnakeBody> ElozoKigyo = new List<SnakeBody>();
        protected SnakeBody SnakeBodies;
        private Food food;
        private Food elozo;
        public int[,] gridfal;
        public List<int> Path = new List<int>();
        public List<int> PathPartial = new List<int>();
        int kor = 0;
        public bool nincsUt = false;
        public int[,] pathGrid;
        Color back;
        bool first;
        int sorszam;
        bool ehete = false;
        public const int Hossz = 1024 / 32;
        public const int Magassag = 512 / 32;
        private const string SCORE_STRING = "Score: {0}";
        int sorSzamSzamonTartas = 0;
        public bool aStar;
        bool nincsUjHamiltonian;
        public SnakeGameForm()
        {
            InitializeComponent();
            //ClientSize = new Size(1024, 512 + m_RestartBtn.Height);
            //m_Game = new Game(WIDTH, HEIGHT);
            //m_Timer.Start();



        }
        public void StartGame()
        {
            Debug.WriteLine("Startgame\n");
            SnakeBodies = new SnakeBody(0, Magassag - 1);
            Path = hamiltonian.HamiltonianGenerator(Hossz, Magassag);
            //UtvonalGeneralas();
            GenerateFood();

            m_Timer.Start();
        }

        private void UpdateScore()
        {
            Debug.WriteLine("UpdateScore\n");
            scoreLbl.Text = string.Format(SCORE_STRING, SnakeBodies.Farok.Count());
        }

        private void OnTimerTick(object sender, EventArgs e)
        {
            UpdateScreen();

            if (food.Eaten)
            {
                GenerateFood();
            }

            if (SnakeBodies.Dead)
            {
                m_Timer.Stop();
                m_RestartBtn.Enabled = true;
            }

            Invalidate();
        }

        private void GenerateFood()
        {
            if (food != null)
                elozo = food;
            m_Timer.Stop();
            Debug.WriteLine("GenerateFood\n");
            Random random = new Random();
            bool jo = true;

            do
            {
                jo = true;
                food = null;
                food = new Food(random.Next(0, Hossz - 1), random.Next(0, Magassag - 1));
                if (SnakeBodies.Farok.Count > 0)
                {
                    foreach (var item in SnakeBodies.Farok)
                    {
                        if (item[0] == food.X)
                        {
                            if (item[1] == food.Y)
                            {
                                jo = false;
                            }
                        }
                    }
                }

                if (SnakeBodies.X == food.X)
                {
                    if (SnakeBodies.Y == food.Y)
                    {
                        jo = false;
                    }
                }
            } while (!jo);
            food.Eaten = false;

            UtvonalGeneralas();

            m_Timer.Start();

        }
        private void UtvonalGeneralas()
        {
            nincsUjHamiltonian = true;
            Debug.WriteLine("Ut generalas");
            kor = 0;
            //gridfal = wallMaker.WallMakerGenerator(Path, Snake, food, new Tuple<int, int>(Hossz, Magassag), false);
            if (pathGrid == null)
            {
                first = true;
                pathGrid = wallMaker.ListToMatrix(Path, new Tuple<int, int>(Hossz, Magassag));
                Path.Clear();
            }
            
            ehete = false;
            List<int> LehetePath;
            ehete = canEat.CanEatClassGenerator(pathGrid, SnakeBodies, food, out LehetePath);
            if (LehetePath != null)
                if (LehetePath.Count == 0)
                    ehete = false;
            if (ehete)
            {
                aStar = true;
                Debug.WriteLine("Uj Astar utvonal");
                Path.Clear();
                sorszam = 0;
                Path = LehetePath;
                back = Color.White;
                first = true;

            }
            else
            {
                aStar = false;
                int x = SnakeBodies.X;
                int y = SnakeBodies.Y;
                int k = 0;

                if (first)
                {
                    Debug.WriteLine("Uj hamiltonian utvonal");
                    Path.Clear();
                    sorszam = 0;
                    do
                    {
                        switch (pathGrid[x, y])
                        {
                            case 0:
                                {
                                    Path.Add(0);
                                    y--;
                                    break;
                                }
                            case 1:
                                {
                                    Path.Add(1);
                                    y++;
                                    break;
                                }
                            case 2:
                                {
                                    Path.Add(2);
                                    x++;
                                    break;
                                }
                            case 3:
                                {
                                    Path.Add(3);
                                    x--;
                                    break;
                                }
                        }
                        k++;
                    } while (k != pathGrid.GetLength(0) * pathGrid.GetLength(1));
                    back = Color.Red;
                    sorszam = 0;
                    nincsUjHamiltonian = false;
                }
                else
                {
                    Debug.WriteLine("marad a régi hamiltonian");
                }
                first = false;
            }



            /*
            back = Color.White;
            kor = 0;
            Debug.WriteLine("gridfal szamolas.....");
            gridfal = wallMaker.WallMakerGenerator(Path, Snake, food, new Tuple<int, int>(Hossz, Magassag), false);
            Debug.WriteLine("PahtPartial szamolas");
            PathPartial = pathFinder.AStarGenerator(gridfal, new Tuple<int, int>(Snake.X, Snake.Y), new Tuple<int, int>(food.X, food.Y), Snake, PathPartial);
            nincsUt = false;
            if (PathPartial == null)
            {
                back = Color.Aqua;
                Debug.WriteLine("Path not found");
                gridfal = wallMaker.WallMakerGenerator(Path, Snake, new Food(Snake.Farok.Last()[0], Snake.Farok.Last()[1]), new Tuple<int, int>(Hossz, Magassag), true);
                // PathPartial = pathNotFound.PathNotFoundGenerator(Path, Snake);
                PathPartial = pathFinder.AStarGenerator(gridfal, new Tuple<int, int>(Snake.X, Snake.Y), new Tuple<int, int>(Snake.Farok.Last()[0], Snake.Farok.Last()[1]), Snake, PathPartial);
                if (PathPartial != null)
                {
                    if (PathPartial.Count < 2)
                        PathPartial = null;

                }
                nincsUt = true;
            }
            if (PathPartial == null)
            {
                back = Color.Brown;
                Debug.WriteLine("ultimate path not found");
                PathPartial = pathNotFound.PathNotFoundGenerator(Path, Snake);
                nincsUt = true;
            }
            Debug.WriteLine("Kesz\n");
            */

        }
        private void UpdateScreen()
        {
            if(sorszam >= 10 && !aStar)
            {
                UtvonalGeneralas();                
            }
            if (nincsUjHamiltonian == true && !aStar)
                kor = sorszam;
            if (kor >= Path.Count)
            {
                sorszam = 0;
                UtvonalGeneralas();
            }

            switch (Path[kor])
            {
                case 0:
                    {
                        Debug.WriteLine("Fel");
                        SnakeBodies.Direction = Direction.Up;
                        kor++;
                        break;
                    }
                case 1:
                    {
                        Debug.WriteLine("Le");
                        SnakeBodies.Direction = Direction.Down;
                        kor++;
                        break;
                    }
                case 2:
                    {
                        Debug.WriteLine("Jobb");
                        SnakeBodies.Direction = Direction.Right;
                        kor++;
                        break;
                    }
                case 3:
                    {
                        Debug.WriteLine("Bal");
                        SnakeBodies.Direction = Direction.Left;
                        kor++;
                        break;
                    }

            }
            sorszam++;
            //if (kor == Path.Count)
            //{
            //    UtvonalGeneralas();
            //    if (ujHamiltonian == true)
            //        sorszam = 0;
            //}

            MovePlayer();


            pictureBox1.Invalidate();


        }

        private void MovePlayer()
        {
            SnakeBody elozo = SnakeBodies;
            if (SnakeBodies.Farok.Count > 0)
            {
                for (int i = SnakeBodies.Farok.Count - 1; i >= 0; i--)
                {
                    if (i > 0)
                    {
                        if (SnakeBodies.Farok[i] == SnakeBodies.Farok[i - 1])
                            continue;
                        SnakeBodies.Farok[i][0] = SnakeBodies.Farok[i - 1][0];
                        SnakeBodies.Farok[i][1] = SnakeBodies.Farok[i - 1][1];
                        continue;
                    }
                    SnakeBodies.Farok[0][0] = SnakeBodies.X;
                    SnakeBodies.Farok[0][1] = SnakeBodies.Y;
                }
            }

            switch (SnakeBodies.Direction)
            {
                case Direction.Right:
                    SnakeBodies.X += 1;
                    break;
                case Direction.Left:
                    SnakeBodies.X -= 1;
                    break;
                case Direction.Up:
                    SnakeBodies.Y -= 1;
                    break;
                case Direction.Down:
                    SnakeBodies.Y += 1;
                    break;
            }
            ElozoKigyo.Add(new SnakeBody(elozo.X, elozo.Y));
            ElozoKigyo.Last().Farok = elozo.Farok;
            if (SnakeBodies.HitWall() || SnakeBodies.EatsItself())
                Die();

            if (SnakeBodies.X == food.X && SnakeBodies.Y == food.Y)
                Eat();




        }

        private void pictureBox1_Paint(object sender, PaintEventArgs e)
        {
            BackColor = back;
            Graphics canvas = e.Graphics;
            if (SnakeBodies != null)
            {
                Brush snakeColour;

                if (SnakeBodies.Farok != null)
                {
                    snakeColour = Brushes.Green;
                    foreach (var item in SnakeBodies.Farok)
                    {
                        canvas.FillRectangle(snakeColour,
                            new Rectangle(item[0] * Food.SIDE,
                                          item[1] * Food.SIDE,
                                          Food.SIDE, Food.SIDE));
                    }
                }

                snakeColour = Brushes.Black;

                canvas.FillRectangle(snakeColour,
                         new Rectangle(SnakeBodies.X * Food.SIDE,
                                       SnakeBodies.Y * Food.SIDE,
                                       Food.SIDE, Food.SIDE));

                canvas.FillEllipse(Brushes.Red,
                            new Rectangle(food.X * Food.SIDE,
                                          food.Y * Food.SIDE,
                                          Food.SIDE, Food.SIDE));
            }

            for (int i = 0; i <= Hossz * Food.SIDE; i += Food.SIDE)
            {
                canvas.FillRectangle(Brushes.Black,
                        new Rectangle(i, 0, 1, pictureBox1.Height));
            }

            for (int i = 0; i <= Magassag * Food.SIDE; i += Food.SIDE)
            {
                canvas.FillRectangle(Brushes.Black,
                    new Rectangle(0, i, pictureBox1.Width, 1));
            }


        }
        private void Eat()
        {
            Debug.WriteLine("Eat\n");
            int[] farokVeg = new int[2];
            if (SnakeBodies.Farok.Count > 0)
            {
                farokVeg[0] = SnakeBodies.Farok.Last()[0];
                farokVeg[1] = SnakeBodies.Farok.Last()[1];
            }
            else
            {
                farokVeg[0] = SnakeBodies.X;
                farokVeg[1] = SnakeBodies.Y;
            }
            SnakeBodies.Farok.Add(farokVeg);
            food.Eaten = true;
            UpdateScore();
        }

        private void Die()
        {
            kor = 0;
            m_RestartBtn.Enabled = true;
            m_Timer.Stop();
            PathPartial.Clear();
            Debug.WriteLine("-------------------------------------------------------------------------------------------\nhalott\n-------------------------------------------------------------------------------------------");
            m_RestartBtn.Enabled = false;
            SnakeBodies.Clear();
            UpdateScore();
            m_Timer.Start();
            StartGame();
        }

        private void OnRestartBtnClick(object sender, EventArgs e)
        {
            m_RestartBtn.Enabled = false;
            SnakeBodies.Clear();
            UpdateScore();
            m_Timer.Start();
            StartGame();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            StartGame();
        }

        private void pictureBox1_Click(object sender, EventArgs e)
        {

        }
    }
}

