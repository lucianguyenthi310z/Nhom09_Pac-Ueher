using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;
using System.Threading;

namespace PacmanGame
{
    public class Map
    {
        int x, y; //coordinates
        public static char wall = '█'; //symbol of wall
        public static char emptySpace = ' '; //symbol of empty space
        public static char jewel = '·'; //symbol of jewel
        public static char smartGhostSymbol = 'X'; //symbol of ghost
        public static char stupidGhostSymbol = 'Y'; //symbol of ghost
        public char[,] area = new char[36, 90]; //array of map
        public static CancellationTokenSource cancellationTokenSource = new CancellationTokenSource();
        public static char greenJewel = 'V'; //symbol of green jewel
        public static char redJewel = 'S'; //symbol of red jewel
        public static char blueJewel = 'R'; //đổi chiều
        public static char DarkMagentaJewel = 'E'; // ăn ma



        public char Wall { get; set; }
        public char EmptySpace { get; set; }
        public char Jewel { get; set; }

        //Indexer of map
        public char this[int x, int y]
        {
            get
            {
                if (x < 0) return area[y, 89];
                else if (x > 89) return area[y, 0];
                else return area[y, x];

            }
            set
            {
                area[y, x] = value;
                Console.ForegroundColor = ConsoleColor.Green;
                Console.SetCursorPosition(x + 1, y + 1);
                Console.Write(value);
                Console.ForegroundColor = ConsoleColor.White;
            }
        }

        //Render of something
        public void RenderChar(int x, int y, char symbol)
        {
            if (x < 0) x = 89;
            else if (x > 89) x = 0;

            if (symbol == '@') Console.ForegroundColor = ConsoleColor.Green;
            else if (symbol == 'X') Console.ForegroundColor = ConsoleColor.Red;
            else if (symbol == 'Y') Console.ForegroundColor = ConsoleColor.Blue;
            Console.SetCursorPosition(x + 1, y + 1);
            Console.Write(symbol);
            Console.ForegroundColor = ConsoleColor.White;

            area[y, x] = symbol;
        }

        //Methods for new maps
        private void RenderWall(int x, int y)
        {
            Console.ForegroundColor = ConsoleColor.Cyan;
            Console.SetCursorPosition(x + 1, y + 1);
            Console.Write(wall);
            Console.ForegroundColor = ConsoleColor.White;
            area[y, x] = wall;
        }
       
        //Manually render of map
        public void RenderMapA()
        {
            //Border
            Console.ForegroundColor = ConsoleColor.Cyan;
            Console.SetCursorPosition(1, 1);
            for (int i = 0; i < 90; i++)
            {
                Console.Write(wall);
                area[0, i] = wall;
            }
            Console.SetCursorPosition(1, 36);
            for (int i = 0; i < 90; i++)
            {
                Console.Write(wall);
                area[35, i] = wall;
            }

            for (int i = 2; i < 36; i++)
            {
                Console.SetCursorPosition(1, i);
                Console.Write(wall);
                area[i - 1, 0] = wall;

                Console.SetCursorPosition(90, i);
                Console.Write(wall);
                area[i - 1, 89] = wall;
            }

            //Walls

            //left 1/3 part
            for (int i = 3; i < 8; i++)
            {
                Console.SetCursorPosition(14, i);
                for (int j = 14; j < 16; j++)
                {
                    Console.Write(wall);
                    area[i - 1, j - 1] = wall;
                }

                Console.SetCursorPosition(19, i);
                for (int j = 19; j < 21; j++)
                {
                    Console.Write(wall);
                    area[i - 1, j - 1] = wall;
                }

                Console.SetCursorPosition(23, i);
                for (int j = 23; j < 25; j++)
                {
                    Console.Write(wall);
                    area[i - 1, j - 1] = wall;
                }

                Console.SetCursorPosition(32, i);
                for (int j = 32; j < 34; j++)
                {
                    Console.Write(wall);
                    area[i - 1, j - 1] = wall;
                }

                Console.SetCursorPosition(37, i);
                for (int j = 37; j < 39; j++)
                {
                    Console.Write(wall);
                    area[i - 1, j - 1] = wall;
                }
            }

            for (int i = 3; i < 4; i++)
            {
                Console.SetCursorPosition(25, i);
                for (int j = 25; j < 30; j++)
                {
                    Console.Write(wall);
                    area[i - 1, j - 1] = wall;
                }
            }

            for (int i = 5; i < 6; i++)
            {
                Console.SetCursorPosition(25, i);
                for (int j = 25; j < 30; j++)
                {
                    Console.Write(wall);
                    area[i - 1, j - 1] = wall;
                }

                Console.SetCursorPosition(34, i);
                for (int j = 34; j < 37; j++)
                {
                    Console.Write(wall);
                    area[i - 1, j - 1] = wall;
                }
            }

            for (int i = 7; i < 8; i++)
            {
                Console.SetCursorPosition(16, i);
                for (int j = 16; j < 19; j++)
                {
                    Console.Write(wall);
                    area[i - 1, j - 1] = wall;
                }

                Console.SetCursorPosition(25, i);
                for (int j = 25; j < 30; j++)
                {
                    Console.Write(wall);
                    area[i - 1, j - 1] = wall;
                }
            }

            for (int i = 2; i < 10; i++)
            {
                Console.SetCursorPosition(56, i);
                for (int j = 56; j < 90; j++)
                {
                    Console.Write(wall);
                    area[i - 1, j - 1] = wall;
                }
            }

            for (int i = 10; i < 19; i++)
            {
                Console.SetCursorPosition(75, i);
                for (int j = 75; j < 90; j++)
                {
                    Console.Write(wall);
                    area[i - 1, j - 1] = wall;
                }
            }

            for (int i = 19; i < 33; i++)
            {
                Console.SetCursorPosition(75, i);
                for (int j = 75; j < 82; j++)
                {
                    Console.Write(wall);
                    area[i - 1, j - 1] = wall;
                }
            }

            for (int i = 11; i < 13; i++)
            {
                Console.SetCursorPosition(61, i);
                for (int j = 61; j < 67; j++)
                {
                    Console.Write(wall);
                    area[i - 1, j - 1] = wall;
                }
            }

            for (int i = 12; i < 16; i++)
            {
                Console.SetCursorPosition(53, i);
                for (int j = 53; j < 55; j++)
                {
                    Console.Write(wall);
                    area[i - 1, j - 1] = wall;
                }
            }

            for (int i = 14; i < 24; i++)
            {
                Console.SetCursorPosition(61, i);
                for (int j = 61; j < 67; j++)
                {
                    Console.Write(wall);
                    area[i - 1, j - 1] = wall;
                }
            }

            for (int i = 15; i < 24; i++)
            {
                Console.SetCursorPosition(68, i);
                for (int j = 68; j < 74; j++)
                {
                    Console.Write(wall);
                    area[i - 1, j - 1] = wall;
                }
            }

            for (int i = 19; i < 24; i++)
            {
                Console.SetCursorPosition(52, i);
                for (int j = 52; j < 61; j++)
                {
                    Console.Write(wall);
                    area[i - 1, j - 1] = wall;
                }
            }

            for (int i = 18; i < 22; i++)
            {
                Console.SetCursorPosition(46, i);
                for (int j = 46; j < 48; j++)
                {
                    Console.Write(wall);
                    area[i - 1, j - 1] = wall;
                }
            }

            for (int i = 23; i < 24; i++)
            {
                Console.SetCursorPosition(31, i);
                for (int j = 31; j < 52; j++)
                {
                    Console.Write(wall);
                    area[i - 1, j - 1] = wall;
                }
            }

            for (int i = 26; i < 33; i++)
            {
                Console.SetCursorPosition(53, i);
                for (int j = 53; j < 73; j++)
                {
                    Console.Write(wall);
                    area[i - 1, j - 1] = wall;
                }
            }

            for (int i = 25; i < 27; i++)
            {
                Console.SetCursorPosition(37, i);
                for (int j = 37; j < 48; j++)
                {
                    Console.Write(wall);
                    area[i - 1, j - 1] = wall;
                }
            }

            for (int i = 25; i < 31; i++)
            {
                Console.SetCursorPosition(23, i);
                for (int j = 23; j < 32; j++)
                {
                    Console.Write(wall);
                    area[i - 1, j - 1] = wall;
                }
            }

            for (int i = 23; i < 33; i++)
            {
                Console.SetCursorPosition(6, i);
                for (int j = 6; j < 12; j++)
                {
                    Console.Write(wall);
                    area[i - 1, j - 1] = wall;
                }
            }

            for (int i = 32; i < 33; i++)
            {
                Console.SetCursorPosition(12, i);
                for (int j = 12; j < 37; j++)
                {
                    Console.Write(wall);
                    area[i - 1, j - 1] = wall;
                }

                Console.SetCursorPosition(48, i);
                for (int j = 48; j < 53; j++)
                {
                    Console.Write(wall);
                    area[i - 1, j - 1] = wall;
                }
            }

            for (int i = 9; i < 21; i++)
            {
                Console.SetCursorPosition(14, i);
                for (int j = 14; j < 42; j++)
                {
                    Console.Write(wall);
                    area[i - 1, j - 1] = wall;
                }
            }

            for (int i = 9; i < 14; i++)
            {
                Console.SetCursorPosition(42, i);
                for (int j = 42; j < 47; j++)
                {
                    Console.Write(wall);
                    area[i - 1, j - 1] = wall;
                }
            }

            for (int i = 14; i < 16; i++)
            {
                Console.SetCursorPosition(42, i);
                for (int j = 42; j < 46; j++)
                {
                    Console.Write(wall);
                    area[i - 1, j - 1] = wall;
                }
            }

            for (int i = 9; i < 11; i++)
            {
                Console.SetCursorPosition(4, i);
                for (int j = 4; j < 7; j++)
                {
                    Console.Write(wall);
                    area[i - 1, j - 1] = wall;
                }
            }

            for (int i = 15; i < 17; i++)
            {
                Console.SetCursorPosition(4, i);
                for (int j = 4; j < 7; j++)
                {
                    Console.Write(wall);
                    area[i - 1, j - 1] = wall;
                }
            }

            for (int i = 12; i < 14; i++)
            {
                Console.SetCursorPosition(9, i);
                for (int j = 9; j < 12; j++)
                {
                    Console.Write(wall);
                    area[i - 1, j - 1] = wall;
                }
            }

            for (int i = 18; i < 20; i++)
            {
                Console.SetCursorPosition(9, i);
                for (int j = 9; j < 12; j++)
                {
                    Console.Write(wall);
                    area[i - 1, j - 1] = wall;
                }
            }

            for (int i = 33; i < 35; i++)
            {
                Console.SetCursorPosition(6, i);
                for (int j = 6; j < 8; j++)
                {
                    Console.Write(wall);
                    area[i - 1, j - 1] = wall;
                }

                Console.SetCursorPosition(16, i);
                for (int j = 16; j < 18; j++)
                {
                    Console.Write(wall);
                    area[i - 1, j - 1] = wall;
                }

                Console.SetCursorPosition(26, i);
                for (int j = 26; j < 28; j++)
                {
                    Console.Write(wall);
                    area[i - 1, j - 1] = wall;
                }

                Console.SetCursorPosition(48, i);
                for (int j = 48; j < 50; j++)
                {
                    Console.Write(wall);
                    area[i - 1, j - 1] = wall;
                }

                Console.SetCursorPosition(58, i);
                for (int j = 58; j < 60; j++)
                {
                    Console.Write(wall);
                    area[i - 1, j - 1] = wall;
                }

                Console.SetCursorPosition(68, i);
                for (int j = 68; j < 70; j++)
                {
                    Console.Write(wall);
                    area[i - 1, j - 1] = wall;
                }

                Console.SetCursorPosition(78, i);
                for (int j = 78; j < 80; j++)
                {
                    Console.Write(wall);
                    area[i - 1, j - 1] = wall;
                }
            }

            for (int i = 34; i < 36; i++)
            {
                Console.SetCursorPosition(11, i);
                for (int j = 11; j < 13; j++)
                {
                    Console.Write(wall);
                    area[i - 1, j - 1] = wall;
                }

                Console.SetCursorPosition(21, i);
                for (int j = 21; j < 23; j++)
                {
                    Console.Write(wall);
                    area[i - 1, j - 1] = wall;
                }

                Console.SetCursorPosition(31, i);
                for (int j = 31; j < 33; j++)
                {
                    Console.Write(wall);
                    area[i - 1, j - 1] = wall;
                }

                Console.SetCursorPosition(53, i);
                for (int j = 53; j < 55; j++)
                {
                    Console.Write(wall);
                    area[i - 1, j - 1] = wall;
                }

                Console.SetCursorPosition(63, i);
                for (int j = 63; j < 65; j++)
                {
                    Console.Write(wall);
                    area[i - 1, j - 1] = wall;
                }

                Console.SetCursorPosition(73, i);
                for (int j = 73; j < 75; j++)
                {
                    Console.Write(wall);
                    area[i - 1, j - 1] = wall;
                }

                Console.SetCursorPosition(83, i);
                for (int j = 83; j < 85; j++)
                {
                    Console.Write(wall);
                    area[i - 1, j - 1] = wall;
                }
            }

            Console.ForegroundColor = ConsoleColor.White;
        }

        public void RenderJewelsA()
        {
            for (int i = 2; i < 3; i++)
            {
                Console.SetCursorPosition(2, i);
                for (int j = 2; j < 56; j++)
                {
                    int a;
                    a = j % 10;
                    if ((a == 2) || (a == 5) || (a == 8))
                    {
                        Console.Write(jewel);
                        area[i - 1, j - 1] = jewel;
                    }
                    else
                    {
                        Console.Write(emptySpace);
                    }
                }
            }

            for (int i = 8; i < 9; i++)
            {
                Console.SetCursorPosition(2, i);
                for (int j = 2; j < 56; j++)
                {
                    int a;
                    a = j % 10;
                    if ((a == 2) || (a == 5) || (a == 8))
                    {
                        Console.Write(jewel);
                        area[i - 1, j - 1] = jewel;
                    }
                    else
                    {
                        Console.Write(emptySpace);
                    }
                }
            }

            for (int i = 3; i < 8; i++)
            {
                Console.SetCursorPosition(2, i);
                for (int j = 2; j < 13; j++)
                {
                    int a;
                    a = j % 10;
                    if ((a == 2) || (a == 5) || (a == 8))
                    {
                        Console.Write(jewel);
                        area[i - 1, j - 1] = jewel;
                    }
                    else
                    {
                        Console.Write(emptySpace);
                    }
                }

                Console.SetCursorPosition(42, i);
                for (int j = 42; j < 56; j++)
                {
                    int a;
                    a = j % 10;
                    if ((a == 2) || (a == 5) || (a == 8))
                    {
                        Console.Write(jewel);
                        area[i - 1, j - 1] = jewel;
                    }
                    else
                    {
                        Console.Write(emptySpace);
                    }
                }
            }

            for (int i = 9; i < 21; i++)
            {
                Console.SetCursorPosition(2, i);
                for (int j = 2; j < 3; j++)
                {
                    int a;
                    a = j % 10;
                    if ((a == 2) || (a == 5) || (a == 8))
                    {
                        Console.Write(jewel);
                        area[i - 1, j - 1] = jewel;
                    }
                    else
                    {
                        Console.Write(emptySpace);
                    }
                }

                Console.SetCursorPosition(8, i);
                for (int j = 8; j < 9; j++)
                {
                    int a;
                    a = j % 10;
                    if ((a == 2) || (a == 5) || (a == 8))
                    {
                        Console.Write(jewel);
                        area[i - 1, j - 1] = jewel;
                    }
                    else
                    {
                        Console.Write(emptySpace);
                    }
                }

                Console.SetCursorPosition(12, i);
                for (int j = 12; j < 13; j++)
                {
                    int a;
                    a = j % 10;
                    if ((a == 2) || (a == 5) || (a == 8))
                    {
                        Console.Write(jewel);
                        area[i - 1, j - 1] = jewel;
                    }
                    else
                    {
                        Console.Write(emptySpace);
                    }
                }
            }

            for (int i = 11; i < 15; i++)
            {
                Console.SetCursorPosition(5, i);
                for (int j = 5; j < 6; j++)
                {
                    int a;
                    a = j % 10;
                    if ((a == 2) || (a == 5) || (a == 8))
                    {
                        Console.Write(jewel);
                        area[i - 1, j - 1] = jewel;
                    }
                    else
                    {
                        Console.Write(emptySpace);
                    }
                }
            }

            for (int i = 17; i < 21; i++)
            {
                Console.SetCursorPosition(5, i);
                for (int j = 5; j < 6; j++)
                {
                    int a;
                    a = j % 10;
                    if ((a == 2) || (a == 5) || (a == 8))
                    {
                        Console.Write(jewel);
                        area[i - 1, j - 1] = jewel;
                    }
                    else
                    {
                        Console.Write(emptySpace);
                    }
                }
            }

            for (int i = 9; i < 19; i++)
            {
                Console.SetCursorPosition(48, i);
                for (int j = 48; j < 53; j++)
                {
                    int a;
                    a = j % 10;
                    if ((a == 2) || (a == 5) || (a == 8))
                    {
                        Console.Write(jewel);
                        area[i - 1, j - 1] = jewel;
                    }
                    else
                    {
                        Console.Write(emptySpace);
                    }
                }
            }

            for (int i = 10; i < 19; i++)
            {
                Console.SetCursorPosition(55, i);
                for (int j = 55; j < 59; j++)
                {
                    int a;
                    a = j % 10;
                    if ((a == 2) || (a == 5) || (a == 8))
                    {
                        Console.Write(jewel);
                        area[i - 1, j - 1] = jewel;
                    }
                    else
                    {
                        Console.Write(emptySpace);
                    }
                }
            }

            for (int i = 16; i < 23; i++)
            {
                Console.SetCursorPosition(42, i);
                for (int j = 42; j < 46; j++)
                {
                    int a;
                    a = j % 10;
                    if ((a == 2) || (a == 5) || (a == 8))
                    {
                        Console.Write(jewel);
                        area[i - 1, j - 1] = jewel;
                    }
                    else
                    {
                        Console.Write(emptySpace);
                    }
                }
            }

            for (int i = 10; i < 15; i++)
            {
                Console.SetCursorPosition(68, i);
                for (int j = 68; j < 73; j++)
                {
                    int a;
                    a = j % 10;
                    if ((a == 2) || (a == 5) || (a == 8))
                    {
                        Console.Write(jewel);
                        area[i - 1, j - 1] = jewel;
                    }
                    else
                    {
                        Console.Write(emptySpace);
                    }
                }
            }

            for (int i = 10; i < 11; i++)
            {
                Console.SetCursorPosition(62, i);
                for (int j = 62; j < 66; j++)
                {
                    int a;
                    a = j % 10;
                    if ((a == 2) || (a == 5) || (a == 8))
                    {
                        Console.Write(jewel);
                        area[i - 1, j - 1] = jewel;
                    }
                    else
                    {
                        Console.Write(emptySpace);
                    }
                }
            }

            for (int i = 13; i < 14; i++)
            {
                Console.SetCursorPosition(62, i);
                for (int j = 62; j < 66; j++)
                {
                    int a;
                    a = j % 10;
                    if ((a == 2) || (a == 5) || (a == 8))
                    {
                        Console.Write(jewel);
                        area[i - 1, j - 1] = jewel;
                    }
                    else
                    {
                        Console.Write(emptySpace);
                    }
                }
            }

            for (int i = 19; i < 23; i++)
            {
                Console.SetCursorPosition(48, i);
                for (int j = 48; j < 49; j++)
                {
                    int a;
                    a = j % 10;
                    if ((a == 2) || (a == 5) || (a == 8))
                    {
                        Console.Write(jewel);
                        area[i - 1, j - 1] = jewel;
                    }
                    else
                    {
                        Console.Write(emptySpace);
                    }
                }
            }

            for (int i = 21; i < 23; i++)
            {
                Console.SetCursorPosition(2, i);
                for (int j = 2; j < 39; j++)
                {
                    int a;
                    a = j % 10;
                    if ((a == 2) || (a == 5) || (a == 8))
                    {
                        Console.Write(jewel);
                        area[i - 1, j - 1] = jewel;
                    }
                    else
                    {
                        Console.Write(emptySpace);
                    }
                }
            }

            for (int i = 23; i < 36; i++)
            {
                Console.SetCursorPosition(2, i);
                for (int j = 2; j < 6; j++)
                {
                    int a;
                    a = j % 10;
                    if ((a == 2) || (a == 5) || (a == 8))
                    {
                        Console.Write(jewel);
                        area[i - 1, j - 1] = jewel;
                    }
                    else
                    {
                        Console.Write(emptySpace);
                    }
                }
            }

            for (int i = 23; i < 24; i++)
            {
                Console.SetCursorPosition(12, i);
                for (int j = 12; j < 29; j++)
                {
                    int a;
                    a = j % 10;
                    if ((a == 2) || (a == 5) || (a == 8))
                    {
                        Console.Write(jewel);
                        area[i - 1, j - 1] = jewel;
                    }
                    else
                    {
                        Console.Write(emptySpace);
                    }
                }
            }

            for (int i = 24; i < 25; i++)
            {
                Console.SetCursorPosition(12, i);
                for (int j = 12; j < 53; j++)
                {
                    int a;
                    a = j % 10;
                    if ((a == 2) || (a == 5) || (a == 8))
                    {
                        Console.Write(jewel);
                        area[i - 1, j - 1] = jewel;
                    }
                    else
                    {
                        Console.Write(emptySpace);
                    }
                }
            }

            for (int i = 25; i < 27; i++)
            {
                Console.SetCursorPosition(32, i);
                for (int j = 32; j < 36; j++)
                {
                    int a;
                    a = j % 10;
                    if ((a == 2) || (a == 5) || (a == 8))
                    {
                        Console.Write(jewel);
                        area[i - 1, j - 1] = jewel;
                    }
                    else
                    {
                        Console.Write(emptySpace);
                    }
                }

                Console.SetCursorPosition(48, i);
                for (int j = 48; j < 53; j++)
                {
                    int a;
                    a = j % 10;
                    if ((a == 2) || (a == 5) || (a == 8))
                    {
                        Console.Write(jewel);
                        area[i - 1, j - 1] = jewel;
                    }
                    else
                    {
                        Console.Write(emptySpace);
                    }
                }
            }

            for (int i = 25; i < 32; i++)
            {
                Console.SetCursorPosition(12, i);
                for (int j = 12; j < 23; j++)
                {
                    int a;
                    a = j % 10;
                    if ((a == 2) || (a == 5) || (a == 8))
                    {
                        Console.Write(jewel);
                        area[i - 1, j - 1] = jewel;
                    }
                    else
                    {
                        Console.Write(emptySpace);
                    }
                }
            }

            for (int i = 27; i < 32; i++)
            {
                Console.SetCursorPosition(32, i);
                for (int j = 32; j < 53; j++)
                {
                    int a;
                    a = j % 10;
                    if ((a == 2) || (a == 5) || (a == 8))
                    {
                        Console.Write(jewel);
                        area[i - 1, j - 1] = jewel;
                    }
                    else
                    {
                        Console.Write(emptySpace);
                    }
                }
            }

            for (int i = 24; i < 26; i++)
            {
                Console.SetCursorPosition(55, i);
                for (int j = 55; j < 73; j++)
                {
                    int a;
                    a = j % 10;
                    if ((a == 2) || (a == 5) || (a == 8))
                    {
                        Console.Write(jewel);
                        area[i - 1, j - 1] = jewel;
                    }
                    else
                    {
                        Console.Write(emptySpace);
                    }
                }
            }

            for (int i = 19; i < 33; i++)
            {
                Console.SetCursorPosition(82, i);
                for (int j = 82; j < 89; j++)
                {
                    int a;
                    a = j % 10;
                    if ((a == 2) || (a == 5) || (a == 8))
                    {
                        Console.Write(jewel);
                        area[i - 1, j - 1] = jewel;
                    }
                    else
                    {
                        Console.Write(emptySpace);
                    }
                }
            }

            for (int i = 33; i < 34; i++)
            {
                Console.SetCursorPosition(8, i);
                for (int j = 8; j < 16; j++)
                {
                    int a;
                    a = j % 10;
                    if ((a == 2) || (a == 5) || (a == 8))
                    {
                        Console.Write(jewel);
                        area[i - 1, j - 1] = jewel;
                    }
                    else
                    {
                        Console.Write(emptySpace);
                    }
                }

                Console.SetCursorPosition(18, i);
                for (int j = 18; j < 26; j++)
                {
                    int a;
                    a = j % 10;
                    if ((a == 2) || (a == 5) || (a == 8))
                    {
                        Console.Write(jewel);
                        area[i - 1, j - 1] = jewel;
                    }
                    else
                    {
                        Console.Write(emptySpace);
                    }
                }

                Console.SetCursorPosition(28, i);
                for (int j = 28; j < 36; j++)
                {
                    int a;
                    a = j % 10;
                    if ((a == 2) || (a == 5) || (a == 8))
                    {
                        Console.Write(jewel);
                        area[i - 1, j - 1] = jewel;
                    }
                    else
                    {
                        Console.Write(emptySpace);
                    }
                }

                Console.SetCursorPosition(52, i);
                for (int j = 52; j < 56; j++)
                {
                    int a;
                    a = j % 10;
                    if ((a == 2) || (a == 5) || (a == 8))
                    {
                        Console.Write(jewel);
                        area[i - 1, j - 1] = jewel;
                    }
                    else
                    {
                        Console.Write(emptySpace);
                    }
                }

                Console.SetCursorPosition(62, i);
                for (int j = 62; j < 66; j++)
                {
                    int a;
                    a = j % 10;
                    if ((a == 2) || (a == 5) || (a == 8))
                    {
                        Console.Write(jewel);
                        area[i - 1, j - 1] = jewel;
                    }
                    else
                    {
                        Console.Write(emptySpace);
                    }
                }

                Console.SetCursorPosition(72, i);
                for (int j = 72; j < 76; j++)
                {
                    int a;
                    a = j % 10;
                    if ((a == 2) || (a == 5) || (a == 8))
                    {
                        Console.Write(jewel);
                        area[i - 1, j - 1] = jewel;
                    }
                    else
                    {
                        Console.Write(emptySpace);
                    }
                }

                Console.SetCursorPosition(82, i);
                for (int j = 82; j < 89; j++)
                {
                    int a;
                    a = j % 10;
                    if ((a == 2) || (a == 5) || (a == 8))
                    {
                        Console.Write(jewel);
                        area[i - 1, j - 1] = jewel;
                    }
                    else
                    {
                        Console.Write(emptySpace);
                    }
                }
            }

            for (int i = 34; i < 35; i++)
            {
                Console.SetCursorPosition(8, i);
                for (int j = 8; j < 9; j++)
                {
                    int a;
                    a = j % 10;
                    if ((a == 2) || (a == 5) || (a == 8))
                    {
                        Console.Write(jewel);
                        area[i - 1, j - 1] = jewel;
                    }
                    else
                    {
                        Console.Write(emptySpace);
                    }
                }

                Console.SetCursorPosition(15, i);
                for (int j = 15; j < 16; j++)
                {
                    int a;
                    a = j % 10;
                    if ((a == 2) || (a == 5) || (a == 8))
                    {
                        Console.Write(jewel);
                        area[i - 1, j - 1] = jewel;
                    }
                    else
                    {
                        Console.Write(emptySpace);
                    }
                }

                Console.SetCursorPosition(18, i);
                for (int j = 18; j < 19; j++)
                {
                    int a;
                    a = j % 10;
                    if ((a == 2) || (a == 5) || (a == 8))
                    {
                        Console.Write(jewel);
                        area[i - 1, j - 1] = jewel;
                    }
                    else
                    {
                        Console.Write(emptySpace);
                    }
                }

                Console.SetCursorPosition(25, i);
                for (int j = 25; j < 26; j++)
                {
                    int a;
                    a = j % 10;
                    if ((a == 2) || (a == 5) || (a == 8))
                    {
                        Console.Write(jewel);
                        area[i - 1, j - 1] = jewel;
                    }
                    else
                    {
                        Console.Write(emptySpace);
                    }
                }

                Console.SetCursorPosition(28, i);
                for (int j = 28; j < 29; j++)
                {
                    int a;
                    a = j % 10;
                    if ((a == 2) || (a == 5) || (a == 8))
                    {
                        Console.Write(jewel);
                        area[i - 1, j - 1] = jewel;
                    }
                    else
                    {
                        Console.Write(emptySpace);
                    }
                }

                Console.SetCursorPosition(35, i);
                for (int j = 35; j < 36; j++)
                {
                    int a;
                    a = j % 10;
                    if ((a == 2) || (a == 5) || (a == 8))
                    {
                        Console.Write(jewel);
                        area[i - 1, j - 1] = jewel;
                    }
                    else
                    {
                        Console.Write(emptySpace);
                    }
                }

                Console.SetCursorPosition(52, i);
                for (int j = 52; j < 53; j++)
                {
                    int a;
                    a = j % 10;
                    if ((a == 2) || (a == 5) || (a == 8))
                    {
                        Console.Write(jewel);
                        area[i - 1, j - 1] = jewel;
                    }
                    else
                    {
                        Console.Write(emptySpace);
                    }
                }

                Console.SetCursorPosition(55, i);
                for (int j = 55; j < 56; j++)
                {
                    int a;
                    a = j % 10;
                    if ((a == 2) || (a == 5) || (a == 8))
                    {
                        Console.Write(jewel);
                        area[i - 1, j - 1] = jewel;
                    }
                    else
                    {
                        Console.Write(emptySpace);
                    }
                }

                Console.SetCursorPosition(62, i);
                for (int j = 62; j < 63; j++)
                {
                    int a;
                    a = j % 10;
                    if ((a == 2) || (a == 5) || (a == 8))
                    {
                        Console.Write(jewel);
                        area[i - 1, j - 1] = jewel;
                    }
                    else
                    {
                        Console.Write(emptySpace);
                    }
                }

                Console.SetCursorPosition(65, i);
                for (int j = 65; j < 66; j++)
                {
                    int a;
                    a = j % 10;
                    if ((a == 2) || (a == 5) || (a == 8))
                    {
                        Console.Write(jewel);
                        area[i - 1, j - 1] = jewel;
                    }
                    else
                    {
                        Console.Write(emptySpace);
                    }
                }

                Console.SetCursorPosition(72, i);
                for (int j = 72; j < 73; j++)
                {
                    int a;
                    a = j % 10;
                    if ((a == 2) || (a == 5) || (a == 8))
                    {
                        Console.Write(jewel);
                        area[i - 1, j - 1] = jewel;
                    }
                    else
                    {
                        Console.Write(emptySpace);
                    }
                }

                Console.SetCursorPosition(75, i);
                for (int j = 75; j < 76; j++)
                {
                    int a;
                    a = j % 10;
                    if ((a == 2) || (a == 5) || (a == 8))
                    {
                        Console.Write(jewel);
                        area[i - 1, j - 1] = jewel;
                    }
                    else
                    {
                        Console.Write(emptySpace);
                    }
                }

                Console.SetCursorPosition(82, i);
                for (int j = 82; j < 83; j++)
                {
                    int a;
                    a = j % 10;
                    if ((a == 2) || (a == 5) || (a == 8))
                    {
                        Console.Write(jewel);
                        area[i - 1, j - 1] = jewel;
                    }
                    else
                    {
                        Console.Write(emptySpace);
                    }
                }

                Console.SetCursorPosition(85, i);
                for (int j = 85; j < 89; j++)
                {
                    int a;
                    a = j % 10;
                    if ((a == 2) || (a == 5) || (a == 8))
                    {
                        Console.Write(jewel);
                        area[i - 1, j - 1] = jewel;
                    }
                    else
                    {
                        Console.Write(emptySpace);
                    }
                }
            }

            for (int i = 35; i < 36; i++)
            {
                Console.SetCursorPosition(8, i);
                for (int j = 8; j < 9; j++)
                {
                    int a;
                    a = j % 10;
                    if ((a == 2) || (a == 5) || (a == 8))
                    {
                        Console.Write(jewel);
                        area[i - 1, j - 1] = jewel;
                    }
                    else
                    {
                        Console.Write(emptySpace);
                    }
                }

                Console.SetCursorPosition(15, i);
                for (int j = 15; j < 19; j++)
                {
                    int a;
                    a = j % 10;
                    if ((a == 2) || (a == 5) || (a == 8))
                    {
                        Console.Write(jewel);
                        area[i - 1, j - 1] = jewel;
                    }
                    else
                    {
                        Console.Write(emptySpace);
                    }
                }

                Console.SetCursorPosition(25, i);
                for (int j = 25; j < 29; j++)
                {
                    int a;
                    a = j % 10;
                    if ((a == 2) || (a == 5) || (a == 8))
                    {
                        Console.Write(jewel);
                        area[i - 1, j - 1] = jewel;
                    }
                    else
                    {
                        Console.Write(emptySpace);
                    }
                }

                Console.SetCursorPosition(35, i);
                for (int j = 35; j < 36; j++)
                {
                    int a;
                    a = j % 10;
                    if ((a == 2) || (a == 5) || (a == 8))
                    {
                        Console.Write(jewel);
                        area[i - 1, j - 1] = jewel;
                    }
                    else
                    {
                        Console.Write(emptySpace);
                    }
                }

                Console.SetCursorPosition(48, i);
                for (int j = 48; j < 53; j++)
                {
                    int a;
                    a = j % 10;
                    if ((a == 2) || (a == 5) || (a == 8))
                    {
                        Console.Write(jewel);
                        area[i - 1, j - 1] = jewel;
                    }
                    else
                    {
                        Console.Write(emptySpace);
                    }
                }

                Console.SetCursorPosition(55, i);
                for (int j = 55; j < 63; j++)
                {
                    int a;
                    a = j % 10;
                    if ((a == 2) || (a == 5) || (a == 8))
                    {
                        Console.Write(jewel);
                        area[i - 1, j - 1] = jewel;
                    }
                    else
                    {
                        Console.Write(emptySpace);
                    }
                }

                Console.SetCursorPosition(65, i);
                for (int j = 65; j < 73; j++)
                {
                    int a;
                    a = j % 10;
                    if ((a == 2) || (a == 5) || (a == 8))
                    {
                        Console.Write(jewel);
                        area[i - 1, j - 1] = jewel;
                    }
                    else
                    {
                        Console.Write(emptySpace);
                    }
                }

                Console.SetCursorPosition(75, i);
                for (int j = 75; j < 83; j++)
                {
                    int a;
                    a = j % 10;
                    if ((a == 2) || (a == 5) || (a == 8))
                    {
                        Console.Write(jewel);
                        area[i - 1, j - 1] = jewel;
                    }
                    else
                    {
                        Console.Write(emptySpace);
                    }
                }

                Console.SetCursorPosition(85, i);
                for (int j = 85; j < 89; j++)
                {
                    int a;
                    a = j % 10;
                    if ((a == 2) || (a == 5) || (a == 8))
                    {
                        Console.Write(jewel);
                        area[i - 1, j - 1] = jewel;
                    }
                    else
                    {
                        Console.Write(emptySpace);
                    }
                }
            }

            for (int i = 32; i < 36; i++)
            {
                Console.SetCursorPosition(38, i);
                for (int j = 38; j < 46; j++)
                {
                    int a;
                    a = j % 10;
                    if ((a == 2) || (a == 5) || (a == 8))
                    {
                        Console.Write(jewel);
                        area[i - 1, j - 1] = jewel;
                    }
                    else
                    {
                        Console.Write(emptySpace);
                    }
                }
            }

            for (int i = 31; i < 32; i++)
            {
                Console.SetCursorPosition(25, i);
                for (int j = 25; j < 29; j++)
                {
                    int a;
                    a = j % 10;
                    if ((a == 2) || (a == 5) || (a == 8))
                    {
                        Console.Write(jewel);
                        area[i - 1, j - 1] = jewel;
                    }
                    else
                    {
                        Console.Write(emptySpace);
                    }
                }
            }

            for (int i = 3; i < 7; i++)
            {
                Console.SetCursorPosition(18, i);
                for (int j = 18; j < 19; j++)
                {
                    int a;
                    a = j % 10;
                    if ((a == 2) || (a == 5) || (a == 8))
                    {
                        Console.Write(jewel);
                        area[i - 1, j - 1] = jewel;
                    }
                    else
                    {
                        Console.Write(emptySpace);
                    }
                }
            }

            for (int i = 3; i < 8; i++)
            {
                Console.SetCursorPosition(22, i);
                for (int j = 22; j < 23; j++)
                {
                    int a;
                    a = j % 10;
                    if ((a == 2) || (a == 5) || (a == 8))
                    {
                        Console.Write(jewel);
                        area[i - 1, j - 1] = jewel;
                    }
                    else
                    {
                        Console.Write(emptySpace);
                    }
                }
            }

            for (int i = 4; i < 5; i++)
            {
                Console.SetCursorPosition(25, i);
                for (int j = 25; j < 29; j++)
                {
                    int a;
                    a = j % 10;
                    if ((a == 2) || (a == 5) || (a == 8))
                    {
                        Console.Write(jewel);
                        area[i - 1, j - 1] = jewel;
                    }
                    else
                    {
                        Console.Write(emptySpace);
                    }
                }
            }

            for (int i = 6; i < 7; i++)
            {
                Console.SetCursorPosition(25, i);
                for (int j = 25; j < 29; j++)
                {
                    int a;
                    a = j % 10;
                    if ((a == 2) || (a == 5) || (a == 8))
                    {
                        Console.Write(jewel);
                        area[i - 1, j - 1] = jewel;
                    }
                    else
                    {
                        Console.Write(emptySpace);
                    }
                }
            }

            for (int i = 3; i < 5; i++)
            {
                Console.SetCursorPosition(35, i);
                for (int j = 35; j < 36; j++)
                {
                    int a;
                    a = j % 10;
                    if ((a == 2) || (a == 5) || (a == 8))
                    {
                        Console.Write(jewel);
                        area[i - 1, j - 1] = jewel;
                    }
                    else
                    {
                        Console.Write(emptySpace);
                    }
                }
            }

            for (int i = 6; i < 8; i++)
            {
                Console.SetCursorPosition(35, i);
                for (int j = 35; j < 36; j++)
                {
                    int a;
                    a = j % 10;
                    if ((a == 2) || (a == 5) || (a == 8))
                    {
                        Console.Write(jewel);
                        area[i - 1, j - 1] = jewel;
                    }
                    else
                    {
                        Console.Write(emptySpace);
                    }
                }
            }

            Console.ForegroundColor = ConsoleColor.White;
        }

        public void AreaNameA()
        {
            char[] name1 = { 'P', '.', 'B', 'Ả', 'O', 'V', 'Ệ', };
            char[] name2 = { 'T', '.', 'B', 'Ộ', };
            char[] name3 = { 'W', 'C', };
            char[] name4 = { 'N', 'H', 'À', ' ', 'X', 'E' };
            char[] name5 = { 'K', 'H', 'U', ' ', 'V', 'Ă', 'N', ' ', 'P', 'H', 'Ò', 'N', 'G', };


            for (int i = 3; i < 4; i++)
            {
                int a = 0;
                Console.SetCursorPosition(70, i);
                for (int j = 70; j < 76; j++)
                {
                    Console.ForegroundColor = ConsoleColor.Black;
                    Console.BackgroundColor = ConsoleColor.Cyan;
                    Console.Write(name4[a]);
                    Console.BackgroundColor = ConsoleColor.Black;
                    a++;
                }
            }

            for (int i = 12; i < 13; i++)
            {
                int a = 0;
                Console.SetCursorPosition(83, i);
                for (int j = 83; j < 89; j++)
                {
                    Console.ForegroundColor = ConsoleColor.Black;
                    Console.BackgroundColor = ConsoleColor.Cyan;
                    Console.Write(name4[a]);
                    Console.BackgroundColor = ConsoleColor.Black;
                    a++;
                }
            }

            for (int i = 7; i < 8; i++)
            {
                int a = 0;
                Console.SetCursorPosition(72, i);
                for (int j = 72; j < 76; j++)
                {
                    Console.ForegroundColor = ConsoleColor.Black;
                    Console.BackgroundColor = ConsoleColor.Cyan;
                    Console.Write(name2[a]);
                    Console.BackgroundColor = ConsoleColor.Black;
                    a++;
                }
            }

            for (int i = 27; i < 28; i++)
            {
                int a = 0;
                Console.SetCursorPosition(76, i);
                for (int j = 76; j < 80; j++)
                {
                    Console.ForegroundColor = ConsoleColor.Black;
                    Console.BackgroundColor = ConsoleColor.Cyan;
                    Console.Write(name2[a]);
                    Console.BackgroundColor = ConsoleColor.Black;
                    a++;
                }
            }

            for (int i = 29; i < 30; i++)
            {
                int a = 0;
                Console.SetCursorPosition(56, i);
                for (int j = 56; j < 60; j++)
                {
                    Console.ForegroundColor = ConsoleColor.Black;
                    Console.BackgroundColor = ConsoleColor.Cyan;
                    Console.Write(name2[a]);
                    Console.BackgroundColor = ConsoleColor.Black;
                    a++;
                }
            }

            for (int i = 27; i < 28; i++)
            {
                int a = 0;
                Console.SetCursorPosition(26, i);
                for (int j = 26; j < 30; j++)
                {
                    Console.ForegroundColor = ConsoleColor.Black;
                    Console.BackgroundColor = ConsoleColor.Cyan;
                    Console.Write(name2[a]);
                    Console.BackgroundColor = ConsoleColor.Black;
                    a++;
                }
            }

            for (int i = 24; i < 25; i++)
            {
                int a = 0;
                Console.SetCursorPosition(7, i);
                for (int j = 7; j < 11; j++)
                {
                    Console.ForegroundColor = ConsoleColor.Black;
                    Console.BackgroundColor = ConsoleColor.Cyan;
                    Console.Write(name2[a]);
                    Console.BackgroundColor = ConsoleColor.Black;
                    a++;
                }
            }

            for (int i = 10; i < 11; i++)
            {
                int a = 0;
                Console.SetCursorPosition(43, i);
                for (int j = 43; j < 45; j++)
                {
                    Console.ForegroundColor = ConsoleColor.Black;
                    Console.BackgroundColor = ConsoleColor.Cyan;
                    Console.Write(name3[a]);
                    Console.BackgroundColor = ConsoleColor.Black;
                    a++;
                }
            }

            for (int i = 12; i < 13; i++)
            {
                int a = 0;
                Console.SetCursorPosition(43, i);
                for (int j = 43; j < 45; j++)
                {
                    Console.ForegroundColor = ConsoleColor.Black;
                    Console.BackgroundColor = ConsoleColor.Cyan;
                    Console.Write(name3[a]);
                    Console.BackgroundColor = ConsoleColor.Black;
                    a++;
                }
            }

            for (int i = 7; i < 8; i++)
            {
                int a = 0;
                Console.SetCursorPosition(58, i);
                for (int j = 58; j < 71; j++)
                {
                    Console.ForegroundColor = ConsoleColor.Black;
                    Console.BackgroundColor = ConsoleColor.Cyan;
                    Console.Write(name5[a]);
                    Console.BackgroundColor = ConsoleColor.Black;
                    a++;
                }
            }

            for (int i = 14; i < 15; i++)
            {
                int a = 0;
                Console.SetCursorPosition(21, i);
                for (int j = 21; j < 34; j++)
                {
                    Console.ForegroundColor = ConsoleColor.Black;
                    Console.BackgroundColor = ConsoleColor.Cyan;
                    Console.Write(name5[a]);
                    Console.BackgroundColor = ConsoleColor.Black;
                    a++;
                }
            }

            for (int i = 21; i < 22; i++)
            {
                int a = 0;
                Console.SetCursorPosition(53, i);
                for (int j = 53; j < 66; j++)
                {
                    Console.ForegroundColor = ConsoleColor.Black;
                    Console.BackgroundColor = ConsoleColor.Cyan;
                    Console.Write(name5[a]);
                    Console.BackgroundColor = ConsoleColor.Black;
                    a++;
                }
            }

            for (int i = 15; i < 16; i++)
            {
                int a = 0;
                Console.SetCursorPosition(77, i);
                for (int j = 77; j < 80; j++)
                {
                    Console.ForegroundColor = ConsoleColor.Black;
                    Console.BackgroundColor = ConsoleColor.Cyan;
                    Console.Write(name5[a]);
                    Console.BackgroundColor = ConsoleColor.Black;
                    a++;
                }
            }

            for (int i = 16; i < 17; i++)
            {
                int a = 4;
                Console.SetCursorPosition(77, i);
                for (int j = 77; j < 80; j++)
                {
                    Console.ForegroundColor = ConsoleColor.Black;
                    Console.BackgroundColor = ConsoleColor.Cyan;
                    Console.Write(name5[a]);
                    Console.BackgroundColor = ConsoleColor.Black;
                    a++;
                }
            }

            for (int i = 17; i < 18; i++)
            {
                int a = 8;
                Console.SetCursorPosition(76, i);
                for (int j = 76; j < 81; j++)
                {
                    Console.ForegroundColor = ConsoleColor.Black;
                    Console.BackgroundColor = ConsoleColor.Cyan;
                    Console.Write(name5[a]);
                    Console.BackgroundColor = ConsoleColor.Black;
                    a++;
                }
            }

            for (int i = 28; i < 29; i++)
            {
                int a = 0;
                Console.SetCursorPosition(63, i);
                for (int j = 63; j < 70; j++)
                {
                    Console.ForegroundColor = ConsoleColor.Black;
                    Console.BackgroundColor = ConsoleColor.Cyan;
                    Console.Write(name5[a]);
                    Console.BackgroundColor = ConsoleColor.Black;
                    a++;
                }
            }

            for (int i = 29; i < 30; i++)
            {
                int a = 8;
                Console.SetCursorPosition(64, i);
                for (int j = 64; j < 69; j++)
                {
                    Console.ForegroundColor = ConsoleColor.Black;
                    Console.BackgroundColor = ConsoleColor.Cyan;
                    Console.Write(name5[a]);
                    Console.BackgroundColor = ConsoleColor.Black;
                    a++;
                }
            }

            for (int i = 30; i < 31; i++)
            {
                int a = 0;
                Console.SetCursorPosition(76, i);
                for (int j = 76; j < 81; j++)
                {
                    Console.ForegroundColor = ConsoleColor.Black;
                    Console.BackgroundColor = ConsoleColor.Cyan;
                    Console.Write(name1[a]);
                    Console.BackgroundColor = ConsoleColor.Black;
                    a++;
                }
            }

            for (int i = 31; i < 32; i++)
            {
                int a = 5;
                Console.SetCursorPosition(78, i);
                for (int j = 78; j < 80; j++)
                {
                    Console.ForegroundColor = ConsoleColor.Black;
                    Console.BackgroundColor = ConsoleColor.Cyan;
                    Console.Write(name1[a]);
                    Console.BackgroundColor = ConsoleColor.Black;
                    a++;
                }
            }


        }

        public void RenderMapB()
        {
            //Border
            Console.ForegroundColor = ConsoleColor.Yellow;
            ;
            Console.SetCursorPosition(3, 1);
            for (int i = 0; i < 88; i++)
            {
                Console.Write(wall);
                area[0, i] = wall;
            }
            Console.SetCursorPosition(1, 36);
            for (int i = 0; i < 90; i++)
            {
                Console.Write(wall);
                area[35, i] = wall;
            }
            for (int i = 3; i < 18; i++)
            {
                Console.SetCursorPosition(1, i);
                Console.Write(wall);
                area[i - 1, 0] = wall;
            }
            for (int i = 2; i < 18; i++)
            {
                Console.SetCursorPosition(90, i);
                Console.Write(wall);
                area[i - 1, 89] = wall;
            }
            for (int i = 22; i < 36; i++)
            {
                Console.SetCursorPosition(1, i);
                Console.Write(wall);
                area[i - 1, 0] = wall;
                Console.SetCursorPosition(90, i);
                Console.Write(wall);
                area[i - 1, 89] = wall;
            }

            //Walls      
            for (int i = 2; i < 3; i++)
            {
                Console.SetCursorPosition(3, i);
                for (int j = 3; j < 11; j++)
                {
                    Console.Write(wall);
                    area[i - 1, j - 1] = wall;
                }
            }

            for (int i = 3; i < 4; i++)
            {
                Console.SetCursorPosition(2, i);
                for (int j = 2; j < 11; j++)
                {
                    Console.Write(wall);
                    area[i - 1, j - 1] = wall;
                }
            }

            for (int i = 2; i < 4; i++)
            {
                Console.SetCursorPosition(20, i);
                for (int j = 20; j < 22; j++)
                {
                    Console.Write(wall);
                    area[i - 1, j - 1] = wall;
                }
                Console.SetCursorPosition(40, i);
                for (int j = 40; j < 42; j++)
                {
                    Console.Write(wall);
                    area[i - 1, j - 1] = wall;
                }
                Console.SetCursorPosition(60, i);
                for (int j = 60; j < 62; j++)
                {
                    Console.Write(wall);
                    area[i - 1, j - 1] = wall;
                }
            }

            for (int i = 4; i < 6; i++)
            {
                Console.SetCursorPosition(30, i);
                for (int j = 30; j < 32; j++)
                {
                    Console.Write(wall);
                    area[i - 1, j - 1] = wall;
                }
                Console.SetCursorPosition(50, i);
                for (int j = 50; j < 52; j++)
                {
                    Console.Write(wall);
                    area[i - 1, j - 1] = wall;
                }
            }

            for (int i = 6; i < 9; i++)
            {
                Console.SetCursorPosition(6, i);
                for (int j = 6; j < 34; j++)
                {
                    Console.Write(wall);
                    area[i - 1, j - 1] = wall;
                }
                Console.SetCursorPosition(39, i);
                for (int j = 39; j < 72; j++)
                {
                    Console.Write(wall);
                    area[i - 1, j - 1] = wall;
                }
            }

            for (int i = 5; i < 10; i++)
            {
                Console.SetCursorPosition(72, i);
                for (int j = 72; j < 87; j++)
                {
                    Console.Write(wall);
                    area[i - 1, j - 1] = wall;
                }
            }

            for (int i = 9; i < 16; i++)
            {
                Console.SetCursorPosition(6, i);
                for (int j = 6; j < 7; j++)
                {
                    Console.Write(wall);
                    area[i - 1, j - 1] = wall;
                }
            }

            for (int i = 11; i < 14; i++)
            {
                Console.SetCursorPosition(16, i);
                for (int j = 16; j < 23; j++)
                {
                    Console.Write(wall);
                    area[i - 1, j - 1] = wall;
                }
                Console.SetCursorPosition(26, i);
                for (int j = 26; j < 37; j++)
                {
                    Console.Write(wall);
                    area[i - 1, j - 1] = wall;
                }
                Console.SetCursorPosition(56, i);
                for (int j = 56; j < 67; j++)
                {
                    Console.Write(wall);
                    area[i - 1, j - 1] = wall;
                }
                Console.SetCursorPosition(70, i);
                for (int j = 70; j < 77; j++)
                {
                    Console.Write(wall);
                    area[i - 1, j - 1] = wall;
                }
            }

            for (int i = 9; i < 14; i++)
            {
                Console.SetCursorPosition(45, i);
                for (int j = 45; j < 48; j++)
                {
                    Console.Write(wall);
                    area[i - 1, j - 1] = wall;
                }
            }

            for (int i = 10; i < 13; i++)
            {
                Console.SetCursorPosition(84, i);
                for (int j = 84; j < 87; j++)
                {
                    Console.Write(wall);
                    area[i - 1, j - 1] = wall;
                }
            }

            for (int i = 6; i < 18; i++)
            {
                Console.SetCursorPosition(4, i);
                for (int j = 4; j < 5; j++)
                {
                    Console.Write(wall);
                    area[i - 1, j - 1] = wall;
                }
            }

            for (int i = 16; i < 18; i++)
            {
                Console.SetCursorPosition(6, i);
                for (int j = 6; j < 13; j++)
                {
                    Console.Write(wall);
                    area[i - 1, j - 1] = wall;
                }
                Console.SetCursorPosition(43, i);
                for (int j = 43; j < 52; j++)
                {
                    Console.Write(wall);
                    area[i - 1, j - 1] = wall;
                }
                Console.SetCursorPosition(80, i);
                for (int j = 80; j < 87; j++)
                {
                    Console.Write(wall);
                    area[i - 1, j - 1] = wall;
                }
            }

            for (int i = 19; i < 21; i++)
            {
                Console.SetCursorPosition(18, i);
                for (int j = 18; j < 23; j++)
                {
                    Console.Write(wall);
                    area[i - 1, j - 1] = wall;
                }
                Console.SetCursorPosition(43, i);
                for (int j = 43; j < 52; j++)
                {
                    Console.Write(wall);
                    area[i - 1, j - 1] = wall;
                }
                Console.SetCursorPosition(58, i);
                for (int j = 58; j < 65; j++)
                {
                    Console.Write(wall);
                    area[i - 1, j - 1] = wall;
                }
                Console.SetCursorPosition(70, i);
                for (int j = 70; j < 75; j++)
                {
                    Console.Write(wall);
                    area[i - 1, j - 1] = wall;
                }
            }

            for (int i = 22; i < 24; i++)
            {
                Console.SetCursorPosition(6, i);
                for (int j = 6; j < 13; j++)
                {
                    Console.Write(wall);
                    area[i - 1, j - 1] = wall;
                }
                Console.SetCursorPosition(28, i);
                for (int j = 28; j < 35; j++)
                {
                    Console.Write(wall);
                    area[i - 1, j - 1] = wall;
                }
                Console.SetCursorPosition(43, i);
                for (int j = 43; j < 52; j++)
                {
                    Console.Write(wall);
                    area[i - 1, j - 1] = wall;
                }
                Console.SetCursorPosition(80, i);
                for (int j = 80; j < 87; j++)
                {
                    Console.Write(wall);
                    area[i - 1, j - 1] = wall;
                }
            }

            for (int i = 16; i < 24; i++)
            {
                Console.SetCursorPosition(16, i);
                for (int j = 16; j < 18; j++)
                {
                    Console.Write(wall);
                    area[i - 1, j - 1] = wall;
                }
                Console.SetCursorPosition(26, i);
                for (int j = 26; j < 28; j++)
                {
                    Console.Write(wall);
                    area[i - 1, j - 1] = wall;
                }
                Console.SetCursorPosition(35, i);
                for (int j = 35; j < 37; j++)
                {
                    Console.Write(wall);
                    area[i - 1, j - 1] = wall;
                }
                Console.SetCursorPosition(41, i);
                for (int j = 41; j < 43; j++)
                {
                    Console.Write(wall);
                    area[i - 1, j - 1] = wall;
                }
                Console.SetCursorPosition(56, i);
                for (int j = 56; j < 58; j++)
                {
                    Console.Write(wall);
                    area[i - 1, j - 1] = wall;
                }
                Console.SetCursorPosition(65, i);
                for (int j = 65; j < 67; j++)
                {
                    Console.Write(wall);
                    area[i - 1, j - 1] = wall;
                }
                Console.SetCursorPosition(75, i);
                for (int j = 75; j < 77; j++)
                {
                    Console.Write(wall);
                    area[i - 1, j - 1] = wall;
                }
            }

            for (int i = 24; i < 34; i++)
            {
                Console.SetCursorPosition(6, i);
                for (int j = 6; j < 7; j++)
                {
                    Console.Write(wall);
                    area[i - 1, j - 1] = wall;
                }
            }

            for (int i = 22; i < 34; i++)
            {
                Console.SetCursorPosition(4, i);
                for (int j = 4; j < 5; j++)
                {
                    Console.Write(wall);
                    area[i - 1, j - 1] = wall;
                }
            }

            for (int i = 25; i < 27; i++)
            {
                Console.SetCursorPosition(15, i);
                for (int j = 15; j < 78; j++)
                {
                    Console.Write(wall);
                    area[i - 1, j - 1] = wall;
                }
            }

            for (int i = 28; i < 29; i++)
            {
                Console.SetCursorPosition(54, i);
                for (int j = 54; j < 87; j++)
                {
                    Console.Write(wall);
                    area[i - 1, j - 1] = wall;
                }
            }

            for (int i = 29; i < 34; i++)
            {
                Console.SetCursorPosition(34, i);
                for (int j = 34; j < 87; j++)
                {
                    Console.Write(wall);
                    area[i - 1, j - 1] = wall;
                }
            }

            for (int i = 29; i < 30; i++)
            {
                Console.SetCursorPosition(9, i);
                for (int j = 9; j < 29; j++)
                {
                    Console.Write(wall);
                    area[i - 1, j - 1] = wall;
                }
            }

            for (int i = 30; i < 31; i++)
            {
                Console.SetCursorPosition(11, i);
                for (int j = 11; j < 29; j++)
                {
                    Console.Write(wall);
                    area[i - 1, j - 1] = wall;
                }
            }

            for (int i = 31; i < 32; i++)
            {
                Console.SetCursorPosition(11, i);
                for (int j = 11; j < 24; j++)
                {
                    Console.Write(wall);
                    area[i - 1, j - 1] = wall;
                }
            }

            for (int i = 32; i < 33; i++)
            {
                Console.SetCursorPosition(17, i);
                for (int j = 17; j < 29; j++)
                {
                    Console.Write(wall);
                    area[i - 1, j - 1] = wall;
                }
            }

            for (int i = 33; i < 34; i++)
            {
                Console.SetCursorPosition(7, i);
                for (int j = 7; j < 29; j++)
                {
                    Console.Write(wall);
                    area[i - 1, j - 1] = wall;
                }
            }

            for (int i = 35; i < 36; i++)
            {
                Console.SetCursorPosition(2, i);
                for (int j = 2; j < 88; j++)
                {
                    Console.Write(wall);
                    area[i - 1, j - 1] = wall;
                }
            }

            Console.ForegroundColor = ConsoleColor.White;
        }

        public void RenderJewelsB()
        {
            for (int i = 4; i < 6; i++)
            {
                Console.SetCursorPosition(2, i);
                for (int j = 2; j < 9; j++)
                {
                    int a;
                    a = j % 10;
                    if ((a == 2) || (a == 5) || (a == 8))
                    {
                        Console.Write(jewel);
                        area[i - 1, j - 1] = jewel;
                    }
                    else
                    {
                        Console.Write(emptySpace);
                    }
                }
            }

            for (int i = 2; i < 6; i++)
            {
                Console.SetCursorPosition(12, i);
                for (int j = 12; j < 19; j++)
                {
                    int a;
                    a = j % 10;
                    if ((a == 2) || (a == 5) || (a == 8))
                    {
                        Console.Write(jewel);
                        area[i - 1, j - 1] = jewel;
                    }
                    else
                    {
                        Console.Write(emptySpace);
                    }
                }

                Console.SetCursorPosition(22, i);
                for (int j = 22; j < 29; j++)
                {
                    int a;
                    a = j % 10;
                    if ((a == 2) || (a == 5) || (a == 8))
                    {
                        Console.Write(jewel);
                        area[i - 1, j - 1] = jewel;
                    }
                    else
                    {
                        Console.Write(emptySpace);
                    }
                }

                Console.SetCursorPosition(32, i);
                for (int j = 32; j < 39; j++)
                {
                    int a;
                    a = j % 10;
                    if ((a == 2) || (a == 5) || (a == 8))
                    {
                        Console.Write(jewel);
                        area[i - 1, j - 1] = jewel;
                    }
                    else
                    {
                        Console.Write(emptySpace);
                    }
                }

                Console.SetCursorPosition(42, i);
                for (int j = 42; j < 49; j++)
                {
                    int a;
                    a = j % 10;
                    if ((a == 2) || (a == 5) || (a == 8))
                    {
                        Console.Write(jewel);
                        area[i - 1, j - 1] = jewel;
                    }
                    else
                    {
                        Console.Write(emptySpace);
                    }
                }

                Console.SetCursorPosition(52, i);
                for (int j = 52; j < 59; j++)
                {
                    int a;
                    a = j % 10;
                    if ((a == 2) || (a == 5) || (a == 8))
                    {
                        Console.Write(jewel);
                        area[i - 1, j - 1] = jewel;
                    }
                    else
                    {
                        Console.Write(emptySpace);
                    }
                }

                Console.SetCursorPosition(62, i);
                for (int j = 62; j < 69; j++)
                {
                    int a;
                    a = j % 10;
                    if ((a == 2) || (a == 5) || (a == 8))
                    {
                        Console.Write(jewel);
                        area[i - 1, j - 1] = jewel;
                    }
                    else
                    {
                        Console.Write(emptySpace);
                    }
                }

            }

            for (int i = 2; i < 5; i++)
            {
                Console.SetCursorPosition(72, i);
                for (int j = 72; j < 89; j++)
                {
                    int a;
                    a = j % 10;
                    if ((a == 2) || (a == 5) || (a == 8))
                    {
                        Console.Write(jewel);
                        area[i - 1, j - 1] = jewel;
                    }
                    else
                    {
                        Console.Write(emptySpace);
                    }
                }
            }

            for (int i = 9; i < 11; i++)
            {
                Console.SetCursorPosition(8, i);
                for (int j = 8; j < 45; j++)
                {
                    int a;
                    a = j % 10;
                    if ((a == 2) || (a == 5) || (a == 8))
                    {
                        Console.Write(jewel);
                        area[i - 1, j - 1] = jewel;
                    }
                    else
                    {
                        Console.Write(emptySpace);
                    }
                }

                Console.SetCursorPosition(48, i);
                for (int j = 48; j < 72; j++)
                {
                    int a;
                    a = j % 10;
                    if ((a == 2) || (a == 5) || (a == 8))
                    {
                        Console.Write(jewel);
                        area[i - 1, j - 1] = jewel;
                    }
                    else
                    {
                        Console.Write(emptySpace);
                    }
                }
            }

            for (int i = 14; i < 16; i++)
            {
                Console.SetCursorPosition(8, i);
                for (int j = 8; j < 88; j++)
                {
                    int a;
                    a = j % 10;
                    if ((a == 2) || (a == 5) || (a == 8))
                    {
                        Console.Write(jewel);
                        area[i - 1, j - 1] = jewel;
                    }
                    else
                    {
                        Console.Write(emptySpace);
                    }
                }
            }

            for (int i = 24; i < 25; i++)
            {
                Console.SetCursorPosition(8, i);
                for (int j = 8; j < 88; j++)
                {
                    int a;
                    a = j % 10;
                    if ((a == 2) || (a == 5) || (a == 8))
                    {
                        Console.Write(jewel);
                        area[i - 1, j - 1] = jewel;
                    }
                    else
                    {
                        Console.Write(emptySpace);
                    }
                }
            }

            for (int i = 27; i < 28; i++)
            {
                Console.SetCursorPosition(8, i);
                for (int j = 8; j < 88; j++)
                {
                    int a;
                    a = j % 10;
                    if ((a == 2) || (a == 5) || (a == 8))
                    {
                        Console.Write(jewel);
                        area[i - 1, j - 1] = jewel;
                    }
                    else
                    {
                        Console.Write(emptySpace);
                    }
                }
            }

            for (int i = 34; i < 35; i++)
            {
                Console.SetCursorPosition(8, i);
                for (int j = 8; j < 88; j++)
                {
                    int a;
                    a = j % 10;
                    if ((a == 2) || (a == 5) || (a == 8))
                    {
                        Console.Write(jewel);
                        area[i - 1, j - 1] = jewel;
                    }
                    else
                    {
                        Console.Write(emptySpace);
                    }
                }
            }

            for (int i = 11; i < 14; i++)
            {
                Console.SetCursorPosition(8, i);
                for (int j = 8; j < 16; j++)
                {
                    int a;
                    a = j % 10;
                    if ((a == 2) || (a == 5) || (a == 8))
                    {
                        Console.Write(jewel);
                        area[i - 1, j - 1] = jewel;
                    }
                    else
                    {
                        Console.Write(emptySpace);
                    }
                }

                Console.SetCursorPosition(23, i);
                for (int j = 23; j < 26; j++)
                {
                    int a;
                    a = j % 10;
                    if ((a == 2) || (a == 5) || (a == 8))
                    {
                        Console.Write(jewel);
                        area[i - 1, j - 1] = jewel;
                    }
                    else
                    {
                        Console.Write(emptySpace);
                    }
                }

                Console.SetCursorPosition(37, i);
                for (int j = 37; j < 45; j++)
                {
                    int a;
                    a = j % 10;
                    if ((a == 2) || (a == 5) || (a == 8))
                    {
                        Console.Write(jewel);
                        area[i - 1, j - 1] = jewel;
                    }
                    else
                    {
                        Console.Write(emptySpace);
                    }
                }

                Console.SetCursorPosition(48, i);
                for (int j = 48; j < 56; j++)
                {
                    int a;
                    a = j % 10;
                    if ((a == 2) || (a == 5) || (a == 8))
                    {
                        Console.Write(jewel);
                        area[i - 1, j - 1] = jewel;
                    }
                    else
                    {
                        Console.Write(emptySpace);
                    }
                }

                Console.SetCursorPosition(67, i);
                for (int j = 67; j < 69; j++)
                {
                    int a;
                    a = j % 10;
                    if ((a == 2) || (a == 5) || (a == 8))
                    {
                        Console.Write(jewel);
                        area[i - 1, j - 1] = jewel;
                    }
                    else
                    {
                        Console.Write(emptySpace);
                    }
                }

                Console.SetCursorPosition(77, i);
                for (int j = 77; j < 84; j++)
                {
                    int a;
                    a = j % 10;
                    if ((a == 2) || (a == 5) || (a == 8))
                    {
                        Console.Write(jewel);
                        area[i - 1, j - 1] = jewel;
                    }
                    else
                    {
                        Console.Write(emptySpace);
                    }
                }
            }

            for (int i = 16; i < 18; i++)
            {
                Console.SetCursorPosition(13, i);
                for (int j = 13; j < 16; j++)
                {
                    int a;
                    a = j % 10;
                    if ((a == 2) || (a == 5) || (a == 8))
                    {
                        Console.Write(jewel);
                        area[i - 1, j - 1] = jewel;
                    }
                    else
                    {
                        Console.Write(emptySpace);
                    }
                }

                Console.SetCursorPosition(18, i);
                for (int j = 18; j < 26; j++)
                {
                    int a;
                    a = j % 10;
                    if ((a == 2) || (a == 5) || (a == 8))
                    {
                        Console.Write(jewel);
                        area[i - 1, j - 1] = jewel;
                    }
                    else
                    {
                        Console.Write(emptySpace);
                    }
                }

                Console.SetCursorPosition(28, i);
                for (int j = 28; j < 35; j++)
                {
                    int a;
                    a = j % 10;
                    if ((a == 2) || (a == 5) || (a == 8))
                    {
                        Console.Write(jewel);
                        area[i - 1, j - 1] = jewel;
                    }
                    else
                    {
                        Console.Write(emptySpace);
                    }
                }

                Console.SetCursorPosition(37, i);
                for (int j = 37; j < 41; j++)
                {
                    int a;
                    a = j % 10;
                    if ((a == 2) || (a == 5) || (a == 8))
                    {
                        Console.Write(jewel);
                        area[i - 1, j - 1] = jewel;
                    }
                    else
                    {
                        Console.Write(emptySpace);
                    }
                }

                Console.SetCursorPosition(52, i);
                for (int j = 52; j < 56; j++)
                {
                    int a;
                    a = j % 10;
                    if ((a == 2) || (a == 5) || (a == 8))
                    {
                        Console.Write(jewel);
                        area[i - 1, j - 1] = jewel;
                    }
                    else
                    {
                        Console.Write(emptySpace);
                    }
                }

                Console.SetCursorPosition(58, i);
                for (int j = 58; j < 63; j++)
                {
                    int a;
                    a = j % 10;
                    if ((a == 2) || (a == 5) || (a == 8))
                    {
                        Console.Write(jewel);
                        area[i - 1, j - 1] = jewel;
                    }
                    else
                    {
                        Console.Write(emptySpace);
                    }
                }

                Console.SetCursorPosition(68, i);
                for (int j = 68; j < 75; j++)
                {
                    int a;
                    a = j % 10;
                    if ((a == 2) || (a == 5) || (a == 8))
                    {
                        Console.Write(jewel);
                        area[i - 1, j - 1] = jewel;
                    }
                    else
                    {
                        Console.Write(emptySpace);
                    }
                }

                Console.SetCursorPosition(78, i);
                for (int j = 78; j < 79; j++)
                {
                    int a;
                    a = j % 10;
                    if ((a == 2) || (a == 5) || (a == 8))
                    {
                        Console.Write(jewel);
                        area[i - 1, j - 1] = jewel;
                    }
                    else
                    {
                        Console.Write(emptySpace);
                    }
                }
            }

            for (int i = 22; i < 24; i++)
            {
                Console.SetCursorPosition(13, i);
                for (int j = 13; j < 16; j++)
                {
                    int a;
                    a = j % 10;
                    if ((a == 2) || (a == 5) || (a == 8))
                    {
                        Console.Write(jewel);
                        area[i - 1, j - 1] = jewel;
                    }
                    else
                    {
                        Console.Write(emptySpace);
                    }
                }

                Console.SetCursorPosition(18, i);
                for (int j = 18; j < 26; j++)
                {
                    int a;
                    a = j % 10;
                    if ((a == 2) || (a == 5) || (a == 8))
                    {
                        Console.Write(jewel);
                        area[i - 1, j - 1] = jewel;
                    }
                    else
                    {
                        Console.Write(emptySpace);
                    }
                }

                Console.SetCursorPosition(37, i);
                for (int j = 37; j < 41; j++)
                {
                    int a;
                    a = j % 10;
                    if ((a == 2) || (a == 5) || (a == 8))
                    {
                        Console.Write(jewel);
                        area[i - 1, j - 1] = jewel;
                    }
                    else
                    {
                        Console.Write(emptySpace);
                    }
                }

                Console.SetCursorPosition(52, i);
                for (int j = 52; j < 56; j++)
                {
                    int a;
                    a = j % 10;
                    if ((a == 2) || (a == 5) || (a == 8))
                    {
                        Console.Write(jewel);
                        area[i - 1, j - 1] = jewel;
                    }
                    else
                    {
                        Console.Write(emptySpace);
                    }
                }

                Console.SetCursorPosition(58, i);
                for (int j = 58; j < 63; j++)
                {
                    int a;
                    a = j % 10;
                    if ((a == 2) || (a == 5) || (a == 8))
                    {
                        Console.Write(jewel);
                        area[i - 1, j - 1] = jewel;
                    }
                    else
                    {
                        Console.Write(emptySpace);
                    }
                }

                Console.SetCursorPosition(68, i);
                for (int j = 68; j < 75; j++)
                {
                    int a;
                    a = j % 10;
                    if ((a == 2) || (a == 5) || (a == 8))
                    {
                        Console.Write(jewel);
                        area[i - 1, j - 1] = jewel;
                    }
                    else
                    {
                        Console.Write(emptySpace);
                    }
                }

                Console.SetCursorPosition(78, i);
                for (int j = 78; j < 79; j++)
                {
                    int a;
                    a = j % 10;
                    if ((a == 2) || (a == 5) || (a == 8))
                    {
                        Console.Write(jewel);
                        area[i - 1, j - 1] = jewel;
                    }
                    else
                    {
                        Console.Write(emptySpace);
                    }
                }
            }

            for (int i = 18; i < 22; i++)
            {
                Console.SetCursorPosition(7, i);
                for (int j = 7; j < 16; j++)
                {
                    int a;
                    a = j % 10;
                    if ((a == 2) || (a == 5) || (a == 8))
                    {
                        Console.Write(jewel);
                        area[i - 1, j - 1] = jewel;
                    }
                    else
                    {
                        Console.Write(emptySpace);
                    }
                }

                Console.SetCursorPosition(25, i);
                for (int j = 25; j < 26; j++)
                {
                    int a;
                    a = j % 10;
                    if ((a == 2) || (a == 5) || (a == 8))
                    {
                        Console.Write(jewel);
                        area[i - 1, j - 1] = jewel;
                    }
                    else
                    {
                        Console.Write(emptySpace);
                    }
                }

                Console.SetCursorPosition(28, i);
                for (int j = 28; j < 33; j++)
                {
                    int a;
                    a = j % 10;
                    if ((a == 2) || (a == 5) || (a == 8))
                    {
                        Console.Write(jewel);
                        area[i - 1, j - 1] = jewel;
                    }
                    else
                    {
                        Console.Write(emptySpace);
                    }
                }

                Console.SetCursorPosition(38, i);
                for (int j = 38; j < 41; j++)
                {
                    int a;
                    a = j % 10;
                    if ((a == 2) || (a == 5) || (a == 8))
                    {
                        Console.Write(jewel);
                        area[i - 1, j - 1] = jewel;
                    }
                    else
                    {
                        Console.Write(emptySpace);
                    }
                }

                Console.SetCursorPosition(52, i);
                for (int j = 52; j < 56; j++)
                {
                    int a;
                    a = j % 10;
                    if ((a == 2) || (a == 5) || (a == 8))
                    {
                        Console.Write(jewel);
                        area[i - 1, j - 1] = jewel;
                    }
                    else
                    {
                        Console.Write(emptySpace);
                    }
                }

                Console.SetCursorPosition(68, i);
                for (int j = 68; j < 69; j++)
                {
                    int a;
                    a = j % 10;
                    if ((a == 2) || (a == 5) || (a == 8))
                    {
                        Console.Write(jewel);
                        area[i - 1, j - 1] = jewel;
                    }
                    else
                    {
                        Console.Write(emptySpace);
                    }
                }

                Console.SetCursorPosition(78, i);
                for (int j = 78; j < 88; j++)
                {
                    int a;
                    a = j % 10;
                    if ((a == 2) || (a == 5) || (a == 8))
                    {
                        Console.Write(jewel);
                        area[i - 1, j - 1] = jewel;
                    }
                    else
                    {
                        Console.Write(emptySpace);
                    }
                }
            }

            for (int i = 10; i < 11; i++)
            {
                Console.SetCursorPosition(72, i);
                for (int j = 72; j < 84; j++)
                {
                    int a;
                    a = j % 10;
                    if ((a == 2) || (a == 5) || (a == 8))
                    {
                        Console.Write(jewel);
                        area[i - 1, j - 1] = jewel;
                    }
                    else
                    {
                        Console.Write(emptySpace);
                    }
                }
            }

            for (int i = 18; i < 19; i++)
            {
                Console.SetCursorPosition(18, i);
                for (int j = 18; j < 23; j++)
                {
                    int a;
                    a = j % 10;
                    if ((a == 2) || (a == 5) || (a == 8))
                    {
                        Console.Write(jewel);
                        area[i - 1, j - 1] = jewel;
                    }
                    else
                    {
                        Console.Write(emptySpace);
                    }
                }

                Console.SetCursorPosition(45, i);
                for (int j = 45; j < 49; j++)
                {
                    int a;
                    a = j % 10;
                    if ((a == 2) || (a == 5) || (a == 8))
                    {
                        Console.Write(jewel);
                        area[i - 1, j - 1] = jewel;
                    }
                    else
                    {
                        Console.Write(emptySpace);
                    }
                }

                Console.SetCursorPosition(58, i);
                for (int j = 58; j < 63; j++)
                {
                    int a;
                    a = j % 10;
                    if ((a == 2) || (a == 5) || (a == 8))
                    {
                        Console.Write(jewel);
                        area[i - 1, j - 1] = jewel;
                    }
                    else
                    {
                        Console.Write(emptySpace);
                    }
                }

                Console.SetCursorPosition(72, i);
                for (int j = 72; j < 73; j++)
                {
                    int a;
                    a = j % 10;
                    if ((a == 2) || (a == 5) || (a == 8))
                    {
                        Console.Write(jewel);
                        area[i - 1, j - 1] = jewel;
                    }
                    else
                    {
                        Console.Write(emptySpace);
                    }
                }
            }

            for (int i = 21; i < 22; i++)
            {
                Console.SetCursorPosition(18, i);
                for (int j = 18; j < 23; j++)
                {
                    int a;
                    a = j % 10;
                    if ((a == 2) || (a == 5) || (a == 8))
                    {
                        Console.Write(jewel);
                        area[i - 1, j - 1] = jewel;
                    }
                    else
                    {
                        Console.Write(emptySpace);
                    }
                }

                Console.SetCursorPosition(45, i);
                for (int j = 45; j < 49; j++)
                {
                    int a;
                    a = j % 10;
                    if ((a == 2) || (a == 5) || (a == 8))
                    {
                        Console.Write(jewel);
                        area[i - 1, j - 1] = jewel;
                    }
                    else
                    {
                        Console.Write(emptySpace);
                    }
                }

                Console.SetCursorPosition(58, i);
                for (int j = 58; j < 63; j++)
                {
                    int a;
                    a = j % 10;
                    if ((a == 2) || (a == 5) || (a == 8))
                    {
                        Console.Write(jewel);
                        area[i - 1, j - 1] = jewel;
                    }
                    else
                    {
                        Console.Write(emptySpace);
                    }
                }

                Console.SetCursorPosition(72, i);
                for (int j = 72; j < 73; j++)
                {
                    int a;
                    a = j % 10;
                    if ((a == 2) || (a == 5) || (a == 8))
                    {
                        Console.Write(jewel);
                        area[i - 1, j - 1] = jewel;
                    }
                    else
                    {
                        Console.Write(emptySpace);
                    }
                }
            }

            for (int i = 6; i < 9; i++)
            {
                Console.SetCursorPosition(35, i);
                for (int j = 35; j < 39; j++)
                {
                    int a;
                    a = j % 10;
                    if ((a == 2) || (a == 5) || (a == 8))
                    {
                        Console.Write(jewel);
                        area[i - 1, j - 1] = jewel;
                    }
                    else
                    {
                        Console.Write(emptySpace);
                    }
                }
            }

            for (int i = 25; i < 27; i++)
            {
                Console.SetCursorPosition(8, i);
                for (int j = 8; j < 14; j++)
                {
                    int a;
                    a = j % 10;
                    if ((a == 2) || (a == 5) || (a == 8))
                    {
                        Console.Write(jewel);
                        area[i - 1, j - 1] = jewel;
                    }
                    else
                    {
                        Console.Write(emptySpace);
                    }
                }

                Console.SetCursorPosition(78, i);
                for (int j = 78; j < 87; j++)
                {
                    int a;
                    a = j % 10;
                    if ((a == 2) || (a == 5) || (a == 8))
                    {
                        Console.Write(jewel);
                        area[i - 1, j - 1] = jewel;
                    }
                    else
                    {
                        Console.Write(emptySpace);
                    }
                }
            }

            for (int i = 28; i < 29; i++)
            {
                Console.SetCursorPosition(8, i);
                for (int j = 8; j < 54; j++)
                {
                    int a;
                    a = j % 10;
                    if ((a == 2) || (a == 5) || (a == 8))
                    {
                        Console.Write(jewel);
                        area[i - 1, j - 1] = jewel;
                    }
                    else
                    {
                        Console.Write(emptySpace);
                    }
                }
            }

            for (int i = 29; i < 34; i++)
            {
                Console.SetCursorPosition(30, i);
                for (int j = 30; j < 34; j++)
                {
                    int a;
                    a = j % 10;
                    if ((a == 2) || (a == 5) || (a == 8))
                    {
                        Console.Write(jewel);
                        area[i - 1, j - 1] = jewel;
                    }
                    else
                    {
                        Console.Write(emptySpace);
                    }
                }
            }

            for (int i = 29; i < 33; i++)
            {
                Console.SetCursorPosition(8, i);
                for (int j = 8; j < 9; j++)
                {
                    int a;
                    a = j % 10;
                    if ((a == 2) || (a == 5) || (a == 8))
                    {
                        Console.Write(jewel);
                        area[i - 1, j - 1] = jewel;
                    }
                    else
                    {
                        Console.Write(emptySpace);
                    }
                }
            }

            for (int i = 31; i < 32; i++)
            {
                Console.SetCursorPosition(25, i);
                for (int j = 25; j < 29; j++)
                {
                    int a;
                    a = j % 10;
                    if ((a == 2) || (a == 5) || (a == 8))
                    {
                        Console.Write(jewel);
                        area[i - 1, j - 1] = jewel;
                    }
                    else
                    {
                        Console.Write(emptySpace);
                    }
                }
            }

            for (int i = 32; i < 33; i++)
            {
                Console.SetCursorPosition(12, i);
                for (int j = 12; j < 16; j++)
                {
                    int a;
                    a = j % 10;
                    if ((a == 2) || (a == 5) || (a == 8))
                    {
                        Console.Write(jewel);
                        area[i - 1, j - 1] = jewel;
                    }
                    else
                    {
                        Console.Write(emptySpace);
                    }
                }
            }

            for (int i = 6; i < 35; i++)
            {
                Console.SetCursorPosition(2, i);
                for (int j = 2; j < 3; j++)
                {
                    int a;
                    a = j % 10;
                    if ((a == 2) || (a == 5) || (a == 8))
                    {
                        Console.Write(jewel);
                        area[i - 1, j - 1] = jewel;
                    }
                    else
                    {
                        Console.Write(emptySpace);
                    }
                }
            }

            for (int i = 6; i < 35; i++)
            {
                Console.SetCursorPosition(5, i);
                for (int j = 5; j < 6; j++)
                {
                    int a;
                    a = j % 10;
                    if ((a == 2) || (a == 5) || (a == 8))
                    {
                        Console.Write(jewel);
                        area[i - 1, j - 1] = jewel;
                    }
                    else
                    {
                        Console.Write(emptySpace);
                    }
                }
            }

            for (int i = 5; i < 35; i++)
            {
                Console.SetCursorPosition(88, i);
                for (int j = 88; j < 89; j++)
                {
                    int a;
                    a = j % 10;
                    if ((a == 2) || (a == 5) || (a == 8))
                    {
                        Console.Write(jewel);
                        area[i - 1, j - 1] = jewel;
                    }
                    else
                    {
                        Console.Write(emptySpace);
                    }
                }
            }

            for (int i = 13; i < 14; i++)
            {
                Console.SetCursorPosition(85, i);
                for (int j = 85; j < 86; j++)
                {
                    int a;
                    a = j % 10;
                    if ((a == 2) || (a == 5) || (a == 8))
                    {
                        Console.Write(jewel);
                        area[i - 1, j - 1] = jewel;
                    }
                    else
                    {
                        Console.Write(emptySpace);
                    }
                }
            }
            Console.ForegroundColor = ConsoleColor.White;
        }

        public void AreaNameB()
        {
            char[] name1 = { 'O', 'C', 'B', };
            char[] name2 = { 'A', 'N', ' ', 'N', 'I', 'N', 'H', };
            char[] name3 = { 'Đ', 'Ư', 'Ờ', 'N', 'G', 'H', 'Ầ', 'M', };
            char[] name4 = { 'T', '.', 'B', 'Ộ', };
            char[] name5 = { 'T', '.', 'M', 'Á', 'Y', };
            char[] name6 = { 'W', 'C', };
            char[] name7 = { 'V', 'Ă', 'N', ' ', 'P', 'H', 'Ò', 'N', 'G', 'D', 'S', 'A', };
            char[] name8 = { 'P', 'A', 'N', 'O' };

            for (int i = 6; i < 7; i++)
            {
                int a = 0;
                Console.SetCursorPosition(44, i);
                for (int j = 44; j < 48; j++)
                {
                    Console.ForegroundColor = ConsoleColor.Black;
                    Console.BackgroundColor = ConsoleColor.Yellow;
                    Console.Write(name4[a]);
                    Console.BackgroundColor = ConsoleColor.Black;
                    a++;
                }

                int b = 0;
                Console.SetCursorPosition(75, i);
                for (int j = 75; j < 80; j++)
                {
                    Console.ForegroundColor = ConsoleColor.Black;
                    Console.BackgroundColor = ConsoleColor.Yellow;
                    Console.Write(name3[b]);
                    Console.BackgroundColor = ConsoleColor.Black;
                    b++;
                }
            }

            for (int i = 7; i < 8; i++)
            {
                int a = 0;
                Console.SetCursorPosition(18, i);
                for (int j = 18; j < 21; j++)
                {
                    Console.ForegroundColor = ConsoleColor.Black;
                    Console.BackgroundColor = ConsoleColor.Yellow;
                    Console.Write(name1[a]);
                    Console.BackgroundColor = ConsoleColor.Black;
                    a++;
                }


                int c = 0;
                Console.SetCursorPosition(62, i);
                for (int j = 62; j < 65; j++)
                {
                    Console.ForegroundColor = ConsoleColor.Black;
                    Console.BackgroundColor = ConsoleColor.Yellow;
                    Console.Write(name1[c]);
                    Console.BackgroundColor = ConsoleColor.Black;
                    c++;
                }

                int d = 5;
                Console.SetCursorPosition(76, i);
                for (int j = 76; j < 79; j++)
                {
                    Console.ForegroundColor = ConsoleColor.Black;
                    Console.BackgroundColor = ConsoleColor.Yellow;
                    Console.Write(name3[d]);
                    Console.BackgroundColor = ConsoleColor.Black;
                    d++;
                }
            }

            for (int i = 8; i < 9; i++)
            {
                int a = 0;
                Console.SetCursorPosition(43, i);
                for (int j = 43; j < 50; j++)
                {
                    Console.ForegroundColor = ConsoleColor.Black;
                    Console.BackgroundColor = ConsoleColor.Yellow;
                    Console.Write(name2[a]);
                    Console.BackgroundColor = ConsoleColor.Black;
                    a++;
                }
            }

            int e = 0;
            for (int i = 7; i < 11; i++)
            {
                Console.SetCursorPosition(85, i);
                for (int j = 85; j < 86; j++)
                {
                    Console.ForegroundColor = ConsoleColor.Black;
                    Console.BackgroundColor = ConsoleColor.Yellow;
                    Console.Write(name8[e]);
                    Console.BackgroundColor = ConsoleColor.Black;
                    e++;
                }
            }


            for (int i = 30; i < 31; i++)
            {
                int a = 0;
                Console.SetCursorPosition(12, i);
                for (int j = 12; j < 17; j++)
                {
                    Console.ForegroundColor = ConsoleColor.Black;
                    Console.BackgroundColor = ConsoleColor.Yellow;
                    Console.Write(name5[a]);
                    Console.BackgroundColor = ConsoleColor.Black;
                    a++;
                }

                int b = 0;
                Console.SetCursorPosition(43, i);
                for (int j = 43; j < 48; j++)
                {
                    Console.ForegroundColor = ConsoleColor.Black;
                    Console.BackgroundColor = ConsoleColor.Yellow;
                    Console.Write(name5[b]);
                    Console.BackgroundColor = ConsoleColor.Black;
                    b++;
                }

                int c = 0;
                Console.SetCursorPosition(67, i);
                for (int j = 67; j < 76; j++)
                {

                    Console.ForegroundColor = ConsoleColor.Black;
                    Console.BackgroundColor = ConsoleColor.Yellow;
                    Console.Write(name7[c]);
                    Console.BackgroundColor = ConsoleColor.Black;
                    c++;
                }


            }

            for (int i = 31; i < 32; i++)
            {
                int b = 9;
                Console.SetCursorPosition(70, i);
                for (int j = 70; j < 73; j++)
                {
                    Console.ForegroundColor = ConsoleColor.Black;
                    Console.BackgroundColor = ConsoleColor.Yellow;
                    Console.Write(name7[b]);
                    Console.BackgroundColor = ConsoleColor.Black;
                    b++;
                }

                int c = 0;
                Console.SetCursorPosition(20, i);
                for (int j = 20; j < 22; j++)
                {
                    Console.ForegroundColor = ConsoleColor.Black;
                    Console.BackgroundColor = ConsoleColor.Yellow;
                    Console.Write(name6[c]);
                    Console.BackgroundColor = ConsoleColor.Black;
                    c++;
                }
            }

            for (int i = 32; i < 33; i++)
            {
                int a = 0;
                Console.SetCursorPosition(43, i);
                for (int j = 43; j < 47; j++)
                {
                    Console.ForegroundColor = ConsoleColor.Black;
                    Console.BackgroundColor = ConsoleColor.Yellow;
                    Console.Write(name4[a]);
                    Console.BackgroundColor = ConsoleColor.Black;
                    a++;
                }
            }
        }

        public async Task RandomPosition(CancellationToken cancellationToken)
        {
            Console.ForegroundColor = ConsoleColor.Yellow;
            int[,] array =
            {
                {2,13},{2,19},{2,23},{2,33},{2,39},{2,43},{2,53},{2,59},{2,63},{2,73},{2,83},
                {2,89},{3,15},{3,25},{3,31},{3,35},{3,45},{3,51},{3,55},{3,65},{3,75},{3,85},
                {4,3},{4,9},{4,13},{4,19},{4,20},{4,23},{4,39},{4,40},{4,43},{4,47},{4,57},
                {4,60},{4,67},{4,71},{4,77},{4,81},{4,87},{5,16},{5,20},{5,27},{5,37},{5,60},
                {6,2},{6,34},{6,89},{7,35},{7,36},{8,3},{8,38},{8,87},{9,11},{9,12},{9,25},
                {9,26},{9,41},{9,42},{9,51},{9,52},{9,64},{9,65},{10,2},{10,21},{10,22},{10,35},
                {10,36},{10,57},{10,58},{10,76},{10,89},{11,12},{11,13},{11,14},{11,15},{11,48},{11,49},
                {11,80},{12,3},{12,23},{12,24},{12,41},{12,42},{12,51},{12,52},{12,68},{12,69},{12,78},
                {12,87},{13,7},{13,8},{13,9},{13,10},{13,37},{13,38},{13,48},{13,49},{13,54},{13,55},
                {13,82},{13,83},{14,2},{14,14},{14,15},{14,30},{14,31},{14,46},{14,47},{14,61},{14,62},
                {14,75},{14,76},{14,89},{15,14},{15,15},{15,23},{15,24},{15,38},{15,39},{15,53},{15,54},
                {15,69},{15,70},{15,85},{15,86},{16,3},{16,23},{16,24},{16,38},{16,39},{16,53},{16,54},
                {16,69},{16,70},{16,87},{18,23},{18,24},{18,38},{18,39},{18,53},{18,54},{18,69},{18,70},
                {18,89},{19,6},{19,7},{19,8},{19,9},{19,83},{19,84},{19,85},{19,86},{20,6},{20,7},
                {20,8},{20,9},{20,83},{20,84},{20,85},{20,86},{21,23},{21,24},{21,38},{21,38},{21,53},
                {21,54},{21,69},{21,70},{21,89},{22,2,},{23,23},{23,24},{23,38},{23,38},{23,53},{23,54},
                {23,69},{23,70},{23,87},{24,3},{24,23},{24,24},{24,38},{24,38},{24,53},{24,54},{24,69},
                {24,70},{24,85},{24,86},{25,80},{25,81},{25,89},{26,2},{26,7},{26,8},{26,9},{26,10},
                {26,80},{26,81},{27,23},{27,24},{27,38},{27,39},{27,63},{27,85},{27,86},{28,15},{28,16},
                {28,48},{28,49},{28,87},{29,30},{29,31},{30,2},{30,89},{31,31},{31,32},{32,3},{32,87},
                {33,30},{33,31},{34,2},{34,20},{34,53},{34,89}
            };

            // Lấy số lượng dòng trong mảng
            int rows = array.GetLength(0);
            int tries = 0;
            int maxTries = 200;
            // Sử dụng đối tượng Random để tạo số ngẫu nhiên
            Random rand = new Random();
            int p = 0;
            while (p < 21 && tries < maxTries)
            {
               
                if (Program.gameOver)
                {
                    Console.SetCursorPosition(115, Program.jmin + 5);
                    break; // Nếu gameOver thì thoát khỏi vòng lặp
                }
                if (cancellationToken.IsCancellationRequested)
                {
                    return; // Thoát khỏi phương thức nếu có yêu cầu hủy
                }
                await Task.Delay(1000);
                // Chọn ngẫu nhiên một cặp phần tử trong mảng
                int random1 = rand.Next(0, rows);

                int a = array[random1, 0];
                int b = array[random1, 1];

                if (area[a - 1, b - 1] != wall) //&& area[a - 1, b - 1] != jewel) // Kiểm tra vị trí có phải tường hay không
                {
                    Console.SetCursorPosition(b, a);
                    Console.Write(wall);
                    area[a - 1, b - 1] = wall;
                    p++;
                }
            }
        }
        private Random random = new Random();
        
        public void JewelSkill(int number, char jewelType, ConsoleColor color)
        {
            List<(int, int)> jewelPositions = new List<(int, int)>();
            for (int i = 0; i < 36; i++)
            {
                for (int j = 0; j < 90; j++)
                {
                    if (area[i, j] == jewel)
                    {
                        jewelPositions.Add((i, j));
                    }
                }
            }
            // Chọn 6 hạt jewel random
            var selectedPositions = jewelPositions.OrderBy(x => random.Next()).Take(number).ToList(); 
            for (int i = 1; i < number; i++)
            {
                area[selectedPositions[i].Item1, selectedPositions[i].Item2] = jewelType;
                Console.SetCursorPosition(selectedPositions[i].Item2 + 1, selectedPositions[i].Item1 + 1);
                Console.ForegroundColor = color;
                Console.Write(jewelType);
                Console.ForegroundColor = ConsoleColor.White;
            }
        }

        public void Map1()
        {
            RenderMapA();
            RenderJewelsA();
            AreaNameA();
            //JewelSkill(6, DarkMagentaJewel, ConsoleColor.DarkYellow);
            //JewelSkill(6,greenJewel, ConsoleColor.Green);

        }
        public async Task Map2()
        {
            RenderMapB();
            RenderJewelsB();
            AreaNameB();
            JewelSkill(6, greenJewel, ConsoleColor.Green); 
            
            await RandomPosition(cancellationTokenSource.Token);
            

        }
        public void Map3()
        {
            RenderMapA();
            RenderJewelsA();
            AreaNameA();
            JewelSkill(6, redJewel, ConsoleColor.Red);
            JewelSkill(6, blueJewel, ConsoleColor.Blue);

        }

        public async Task Map4()
        {
            RenderMapB();
            RenderJewelsB();
            AreaNameB();
            JewelSkill(6, DarkMagentaJewel, ConsoleColor.DarkYellow);

            await RandomPosition(cancellationTokenSource.Token);
            //jewelSkill();
        }
    }
}




