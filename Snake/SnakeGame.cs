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
        public List<SnakeBody> ElozoKigyo = new List<SnakeBody>();
        private SnakeBody Snake;
        private Food food;
        private Food elozo;
        public int[,] gridfal;
        public List<int> Path = new List<int>();
        public List<int> PathPartial = new List<int>();
        int kor = 0;
        public bool nincsUt = false;
        Color back;

        public const int Hossz = 1024 / 32;
        public const int Magassag = 512 / 32;
        private const string SCORE_STRING = "Score: {0}";
        public SnakeGameForm()
        {
            InitializeComponent();
            //ClientSize = new Size(1024, 512 + m_RestartBtn.Height);
            //m_Game = new Game(WIDTH, HEIGHT);
            //m_Timer.Start();

            // 
            // m_Timer
            // 
            this.m_Timer.Interval = 10;
            this.m_Timer.Tick += new System.EventHandler(this.OnTimerTick);

        }
        public void StartGame()
        {
            Debug.WriteLine("Startgame\n");
            Snake = new SnakeBody(0, Magassag - 1);
            Path = hamiltonian.HamiltonianGenerator(Hossz, Magassag);
            //UtvonalGeneralas();
            GenerateFood();

            m_Timer.Start();
        }

        private void UpdateScore()
        {
            Debug.WriteLine("UpdateScore\n");
            scoreLbl.Text = string.Format(SCORE_STRING, Snake.Farok.Count());
        }

        private void OnTimerTick(object sender, EventArgs e)
        {
            UpdateScreen();

            if (food.Eaten)
            {
                GenerateFood();
            }

            if (Snake.Dead)
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
                if (Snake.Farok.Count > 0)
                {
                    foreach (var item in Snake.Farok)
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

                if (Snake.X == food.X)
                {
                    if (Snake.Y == food.Y)
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
            /*
            int[,] pathGrid = wallMaker.ListToMatrix(Path, new Tuple<int, int>(Hossz, Magassag));
            Path.Clear();
            int x = 0;
            
            int y = Magassag -1;
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
            } while (!(x == 0 && y == Magassag -1));
            */

            
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
            

        }
        private void UpdateScreen()
        {
            
            if (kor >= PathPartial.Count())
            {
                UtvonalGeneralas();
            }
            

            switch (PathPartial[kor])
            {
                case 0:
                    {
                        Snake.Direction = Direction.Up;
                        kor++;
                        break;
                    }
                case 1:
                    {
                        Snake.Direction = Direction.Down;
                        kor++;
                        break;
                    }
                case 2:
                    {
                        Snake.Direction = Direction.Right;
                        kor++;
                        break;
                    }
                case 3:
                    {
                        Snake.Direction = Direction.Left;
                        kor++;
                        break;
                    }

            }
            /*
            if(kor >= Path.Count)
            {
                kor = 0;
            }
            */
            MovePlayer();


            pictureBox1.Invalidate();


        }

        private void MovePlayer()
        {
            SnakeBody elozo = Snake;
            if (Snake.Farok.Count > 0)
            {
                for (int i = Snake.Farok.Count - 1; i >= 0; i--)
                {
                    if (i > 0)
                    {
                        if (Snake.Farok[i] == Snake.Farok[i - 1])
                            continue;
                        Snake.Farok[i][0] = Snake.Farok[i - 1][0];
                        Snake.Farok[i][1] = Snake.Farok[i - 1][1];
                        continue;
                    }
                    Snake.Farok[0][0] = Snake.X;
                    Snake.Farok[0][1] = Snake.Y;
                }
            }

            switch (Snake.Direction)
            {
                case Direction.Right:
                    Snake.X += 1;
                    break;
                case Direction.Left:
                    Snake.X -= 1;
                    break;
                case Direction.Up:
                    Snake.Y -= 1;
                    break;
                case Direction.Down:
                    Snake.Y += 1;
                    break;
            }
            ElozoKigyo.Add(new SnakeBody(elozo.X, elozo.Y));
            ElozoKigyo.Last().Farok = elozo.Farok;
            if (Snake.HitWall() || Snake.EatsItself())
                Die();

            if (Snake.X == food.X && Snake.Y == food.Y)
                Eat();




        }
        private void pictureBox1_Paint(object sender, PaintEventArgs e)
        {
            BackColor = back;
            Graphics canvas = e.Graphics;
            if (Snake != null)
            {
                Brush snakeColour;

                if (Snake.Farok != null)
                {
                    snakeColour = Brushes.Green;
                    foreach (var item in Snake.Farok)
                    {
                        canvas.FillRectangle(snakeColour,
                            new Rectangle(item[0] * Food.SIDE,
                                          item[1] * Food.SIDE,
                                          Food.SIDE, Food.SIDE));
                    }
                }

                snakeColour = Brushes.Black;

                canvas.FillRectangle(snakeColour,
                         new Rectangle(Snake.X * Food.SIDE,
                                       Snake.Y * Food.SIDE,
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
            if (Snake.Farok.Count > 0)
            {
                farokVeg[0] = Snake.Farok.Last()[0];
                farokVeg[1] = Snake.Farok.Last()[1];
            }
            else
            {
                farokVeg[0] = Snake.X;
                farokVeg[1] = Snake.Y;
            }
            Snake.Farok.Add(farokVeg);
            food.Eaten = true;
            UpdateScore();
        }

        private void Die()
        {
            m_RestartBtn.Enabled = true;
            m_Timer.Stop();
            PathPartial.Clear();
            Debug.WriteLine("-------------------------------------------------------------------------------------------\nhalott\n-------------------------------------------------------------------------------------------");
            m_RestartBtn.Enabled = false;
            Snake.Clear();
            UpdateScore();
            m_Timer.Start();
            StartGame();
        }

        private void OnRestartBtnClick(object sender, EventArgs e)
        {
            m_RestartBtn.Enabled = false;
            Snake.Clear();
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

