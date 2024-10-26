using System;
using System.Diagnostics;
using System.Media;
using System.Threading;

namespace PacmanGame
{
    class Pacman : Object
    {
        //Pacman constructor
        public static bool modeInvisible = false;
        public static bool modeReverse = false;
        public static bool modeEating = false;
        public static int Skilltime = 10000;
        private static readonly object consoleLock = new object();
        //static Thread PrintSkill = new Thread(PrintSkillMessage);
        private bool speedboost;
        private bool invisible;
        private bool ghostCollision;
        public bool IsInvisible
        {
            get { return invisible; }
        }
        public override void GhostIsEated()
        {
            // Không làm gì cả
        }
        public Pacman(int x, int y)
        {
            this.X = x;
            this.Y = y;
            currentStatePlace = Program.map.EmptySpace;
            objectDirection = direction.left;
            Program.map.RenderChar(x, y, GetSymbol());
            invisible = false;
            ghostCollision = true;
        }

        //Processing keystrokes
        static ConsoleKeyInfo KeyInfo = new ConsoleKeyInfo();

        public void Control(Thread background)
        {
            while (background.IsAlive)
            {
                KeyInfo = Console.ReadKey(true);
                if (modeReverse == false)
                {
                    if (KeyInfo.Key == ConsoleKey.LeftArrow)
                    {
                        objectDirection = direction.left;
                    }
                    else if (KeyInfo.Key == ConsoleKey.RightArrow)
                    {
                        objectDirection = direction.right;
                    }
                    else if (KeyInfo.Key == ConsoleKey.UpArrow)
                    {
                        objectDirection = direction.up;
                    }
                    else if (KeyInfo.Key == ConsoleKey.DownArrow)
                    {
                        objectDirection = direction.down;
                    }
                }
                else
                {
                    if (KeyInfo.Key == ConsoleKey.LeftArrow)
                    {
                        objectDirection = direction.right;
                    }
                    else if (KeyInfo.Key == ConsoleKey.RightArrow)
                    {
                        objectDirection = direction.left;
                    }
                    else if (KeyInfo.Key == ConsoleKey.UpArrow)
                    {
                        objectDirection = direction.down;
                    }
                    else if (KeyInfo.Key == ConsoleKey.DownArrow)
                    {
                        objectDirection = direction.up;
                    }
                }
            }
        }

        //Pacman symbol
        public override char GetSymbol()
        {
            if (invisible)
            {
                return ' '; // trả về khoảng trắng khi tàng hình
            }
            else
            {
                return '@'; // trả về '@' khi Pac-Ueher không tàng hình
            }
        }

        //Cách di chuyển của Pac-Ueher
        public override void ChangePositionByDirection(direction Direction)
        {
            if (x > 89) x = 0;
            else if (x < 0) x = 89;
            Program.map.RenderChar(x, y, currentStatePlace);
            if (Direction == direction.left) x--;
            if (Direction == direction.right) x++;
            if (Direction == direction.up) y--;
            if (Direction == direction.down) y++;
            CalcScore();
            Program.map.RenderChar(x, y, GetSymbol());
        }
        private bool isInvisibleTimeRunning = false;
        private Stopwatch stealthStopwatch = new Stopwatch();

        //Hàm tính điểm
        public void CalcScore()
        {
            if (Program.map[x, y] == Map.jewel)
            {
                Program.score += 10;
                Program.PlaySound("Score.wav");
            }
            else if (Program.map[x, y] == Map.redJewel)
            {
                Program.score += 10;
                Program.PlaySound("Skill.wav");
                Thread speedThread = new Thread(new ThreadStart(SpeedBoost));
                speedThread.Start();
                PrintSkillMessage("Tăng tốc thôi nào!!!");

            }
            else if (Program.map[x, y] == Map.greenJewel)
            {
                Program.score += 10;
                Program.PlaySound("Skill.wav");
                invisible = true;
                
                ghostCollision = false;
                isInvisibleTimeRunning = true;
                stealthStopwatch.Start();
                Thread invisibilityThread = new Thread(new ThreadStart(Invisibility));
                invisibilityThread.Start();
                Thread printThread = new Thread(() => PrintSkillMessage("Bạn có thể đi qua ma mà không chết!")); 
                printThread.Start();
                //PrintSkillMessage("Bạn có thể đi qua ma mà không chết!");

                //Thread XoathongbaoThread = new Thread(new ThreadStart(Program.Xoakhungthongbao));
                //XoathongbaoThread.Start();
                //PrintSkillMessage("Bạn có thể đi qua ma mà không chết!");
            }
            else if (Program.map[x, y] == Map.blueJewel)
            {
                Program.score += 10;
                Program.PlaySound("Skill.wav");
                Thread speedThread = new Thread(new ThreadStart(Reverse));
                speedThread.Start();
                PrintSkillMessage("Di chuyển ngược");

            }
            else if (Program.map[x, y] == Map.DarkMagentaJewel)
            {
                Program.score += 10;
                Program.PlaySound("Skill.wav");
                Thread speedThread = new Thread(new ThreadStart(EatGhost));
                speedThread.Start();
                PrintSkillMessage("Bạn có thể ăn được con ma");

            }

        }
        private int currentSkillRow = Program.jmin + 4;
        private void PrintSkillMessage(string skill) //hàm in thông báo khi đạt được chức năng
        {
            int row = currentSkillRow;
            currentSkillRow++;
            if (currentSkillRow > Program.jmin+ 8)
            {
                Program.Xoakhungthongbao();

                currentSkillRow = Program.jmin + 4;
            }
            Console.SetCursorPosition(Program.imin + 2, row);
            Console.Write(skill);
            Thread thread = new Thread(() =>
            {
                lock (consoleLock)
                {
                    int timeLeft = Skilltime / 1000;
                    while (timeLeft >= 0 && Program.gameOver==false)
                    {
                        Console.SetCursorPosition(98, 2);
                        Console.Write(timeLeft);

                        Thread.Sleep(1000);  // Dừng 1 giây
                        timeLeft--;

                        Console.SetCursorPosition(98, 2);
                        Console.Write("   ");
                    }

                    // Xóa thông điệp "skill" sau khi đếm ngược kết thúc
                    if (Program.gameOver == false)
                    Console.SetCursorPosition(98, row);
                    Console.Write(new string(' ', skill.Length));
                }
            });

        
            thread.Start();
        }
        
        //Chức năng tăng tốc
        private void SpeedBoost()
        {
            int originalSpeed = Program.speed;
            Program.speed = 83; 
            Thread.Sleep(Skilltime);
            Program.speed = originalSpeed;
        }

        // Trong khi tàng hình thì
        private void Invisibility()
        {            
            modeInvisible = true;
            for (int i = 0; i < 25; i++) // Số lần chớp nháy
            {
                if (isInvisibleTimeRunning && stealthStopwatch.ElapsedMilliseconds < Skilltime)
                {
                    invisible = true;
                    Program.map.RenderChar(X, Y, GetSymbol());

                    Stopwatch stopwatch = new Stopwatch();
                    stopwatch.Start();

                    while (stopwatch.ElapsedMilliseconds < 200)
                    {
                        // Do nothing, just wait
                    }

                    stopwatch.Stop();

                    invisible = false;
                    Program.map.RenderChar(X, Y, GetSymbol());

                    stopwatch.Reset();
                    stopwatch.Start();

                    while (stopwatch.ElapsedMilliseconds < 200)
                    {
                        // Do nothing, just wait
                    }

                    stopwatch.Stop();
                }
                else
                {
                    isInvisibleTimeRunning = false;
                    invisible = false;
                    ghostCollision = true;
                    //Program.map.RenderChar(X, Y, GetSymbol());
                    break;
                }
            }
            modeInvisible = false;
            
        }
        private void Reverse()
        {
            modeReverse = true;
            Thread.Sleep(Skilltime);
            modeReverse = false;

        }
        //Hàm ăn con ma
        private void EatGhost()
        {
            modeEating = true;
            Thread.Sleep(Skilltime);
            modeEating = false;
        }
        public override void Step() //hàm di chuyển
        {
            Char newPosition = GetSymbolByDirection(objectDirection);

            if (newPosition != Map.wall)
            {
                ChangePositionByDirection(objectDirection);
                previousObjectDirection = objectDirection;
            }
            else
            {
                newPosition = GetSymbolByDirection(previousObjectDirection);
                if (newPosition != Map.wall)
                {
                    ChangePositionByDirection(previousObjectDirection);
                }
            }

        }
        
    }
}
