using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using System.Reflection;
using System.Media;
using static System.Net.Mime.MediaTypeNames;

namespace PacmanGame
{
    class Program
    {
        public static bool gameOver = false;
        public static int score = 0;
        public static int level = 0;
        public static int revival = 3;
        public static int maxscore1 = 100;
        public static int maxscore2 = 400;
        public static int maxscore3 = 800;
        public static int maxscore4 = 1100;

        public static int speed = 250;
        public enum direction { left, up, right, down }
        public enum PlayerStatus { Active, Dead }

        static Thread background1 = new Thread(BackgroundGame1);
        static Thread background2 = new Thread(BackgroundGame2);
        static Thread background3 = new Thread(BackgroundGame3);
        static Thread background4 = new Thread(BackgroundGame4);
        public static Map map = new Map();
        public static Pacman pacman;
        public static List<StupidGhost> stupidGhosts = new List<StupidGhost>();
        public static List<SmartGhost> smartGhosts = new List<SmartGhost>();
        public static Leaderboard leaderboard = new Leaderboard();
        public static int currentLevel = 0; 
        public static int windowWidth;
        public static int windowHeight;
        public static string playerMSSV = "";
        public static int currentY;
        public static Player currentPlayer;


        static void Main(string[] args)
        {
            Console.InputEncoding = Encoding.UTF8;
            Console.OutputEncoding = Encoding.UTF8;
            Console.CursorVisible = false;
            Console.SetWindowSize(140, 40); // Đặt chiều rộng mặc định cửa sổ khi khởi động
            Console.SetBufferSize(140, 40); // Đặt kích thước buffer trùng với kích thước cửa sổ
            windowWidth = Console.WindowWidth;
            windowHeight = Console.WindowHeight;
            // Nếu muốn đặt tiêu đề cho cửa sổ console
            Console.Title = "Pac-Ueher";

            Console.SetCursorPosition(windowWidth / 2, windowHeight / 3);

            PrintText("PAC-UEHER", ConsoleColor.White);
            PrintText("------Nào chúng ta cùng giết ma------\n", ConsoleColor.Yellow);
            //Yêu cầu người chơi nhập tên
            PrintText("Nhập MSSV của bạn: ", ConsoleColor.Yellow);

            bool validMSSV = false;

            while (!validMSSV)
            {
                playerMSSV = Console.ReadLine();

                // Kiểm tra nếu độ dài là 11 và tất cả ký tự đều là số
                if (playerMSSV.Length == 11 && playerMSSV.All(char.IsDigit))
                {
                    validMSSV = true; // MSSV hợp lệ
                }
                else
                {
                    PrintText("MSSV không hợp lệ. Vui lòng nhập lại: ", ConsoleColor.Red);
                }
            }
            leaderboard.LoadFromFile();
            List<Player> allPlayers = leaderboard.LoadFromFile();
            currentPlayer = allPlayers.FirstOrDefault(p => p.MSSV == playerMSSV);

            if (currentPlayer != null)
            {
                if (currentPlayer.Level > 4)
                { currentPlayer.Level = 4; }
                score = currentPlayer.Score; // Khôi phục điểm số
                bool playing = true;
                while (playing)
                {
                    if (currentPlayer.Status == PlayerStatus.Dead)// Kiểm tra trạng thái người chơi
                    {
                        PrintText("Bạn đã chết! Bạn muốn chơi lại từ đầu (1) hay thoát khỏi trò chơi (2) ", ConsoleColor.Yellow);
                        string choice = Console.ReadLine();

                        if (choice == "1")
                        {
                            // Chơi lại từ đầu
                            currentLevel = 0; // Bắt đầu từ level 1
                            score = 0; // Đặt lại điểm số
                            currentPlayer.Status = PlayerStatus.Active;// Đặt trạng thái thành Active
                            break;

                        }
                        else if (choice == "2")
                        {
                            PrintText("Hẹn PAC-UEHER khi bạn đã sẵn sàng.", ConsoleColor.White);
                            Thread.Sleep(4000);
                            Environment.Exit(0);
                        }
                    }
                    else
                    {
                        if (score == maxscore1 || score == maxscore2 || score == maxscore3)
                        {
                            currentLevel = currentPlayer.Level;
                            PrintText($"Chào mừng trở lại {currentPlayer.Name}! Bạn đang ở Level: {currentPlayer.Level + 1}, Điểm: {currentPlayer.Score}", ConsoleColor.Green);
                        }
                        else
                        {
                            currentLevel = currentPlayer.Level - 1; // Khởi động từ cấp độ hiện tại
                            PrintText($"Chào mừng trở lại {currentPlayer.Name}! Bạn đang ở Level: {currentPlayer.Level}, Điểm: {currentPlayer.Score}", ConsoleColor.Green);
                        }
                        PrintText("Bạn muốn chơi lại từ đầu (1) hay tiếp tục chơi (2)? ", ConsoleColor.Yellow);
                        string choice = Console.ReadLine();

                        if (choice == "1")
                        {
                            currentLevel = 0; // Bắt đầu từ level 1
                            score = 0; // Đặt lại điểm số
                            break; // Thoát khỏi vòng lặp
                        }
                        else if (choice == "2")
                        {
                            if (score >= maxscore4 || currentLevel > 3)
                            {
                                PrintText("Bạn đã chơi hết level. Chơi lại từ đầu.", ConsoleColor.Red);
                                currentLevel = 0; // Bắt đầu lại từ level 1
                                score = 0; // Đặt lại điểm số
                                break; // Thoát khỏi vòng lặp
                            }
                            else
                            {
                                break; // Tiếp tục chơi
                            }
                        }
                        else
                        {
                            PrintText("Lựa chọn không hợp lệ. Vui lòng chọn 1 hoặc 2.", ConsoleColor.Red);
                        }
                    }
                }
            }
            else
            {
                PrintText("Người chơi mới! Vui lòng nhập tên của bạn: ", ConsoleColor.Yellow);
                string playerName = Console.ReadLine();
                currentPlayer = new Player(playerMSSV, playerName, 0, 1);
            }
            // Lưu thông tin người chơi vào leaderboard
            currentPlayer.Status = PlayerStatus.Active; // Đặt trạng thái thành Active
            leaderboard.AddOrUpdatePlayer(currentPlayer);

            PrintText("Bạn đã sẵn sàng chưa?", ConsoleColor.Yellow);
            PrintText("Nhấn R nếu đã sẵn sàng:", ConsoleColor.Red);
            ConsoleKeyInfo keyInfo = Console.ReadKey();
            if (keyInfo.KeyChar == 'r' || keyInfo.KeyChar == 'R')
            {
                Console.WriteLine(" Loading...");
            }
            else
            {
                PrintText("Bạn chưa nhấn R. Hẹn PAC-UEHER khi bạn đã sẵn sàng.", ConsoleColor.White);
                Thread.Sleep(4000);
                Environment.Exit(0);
            }
            Console.Clear();

            LevelUp();
            LevelUp();
            LevelUp();
            LevelUp();

            ClearMap();
            Console.Clear();
            PrintText("Nhấn phím bất kỳ để xem bảng xếp hạng...", ConsoleColor.White);
            Console.ReadKey();
            currentPlayer.Score = score; // Cập nhật điểm số hiện tại
            currentPlayer.Level = currentLevel; // Cập nhật cấp độ hiện tại
            leaderboard.AddOrUpdatePlayer(currentPlayer);
            leaderboard.DisplayLeaderboard(currentPlayer);
            PrintText("Nhấn phím bất kỳ để thoát...", ConsoleColor.White);
            Console.ReadKey();
        }
        static void PrintText(string text, ConsoleColor color) //in chữ ra giữa màn hình
        {
            // Lấy vị trí hiện tại của con trỏ
            currentY = Console.CursorTop;
            int newY = currentY + 1; // Di chuyển xuống 1 dòng
            Console.SetCursorPosition((windowWidth - text.Length) / 2, newY);
            Console.ForegroundColor = color;
            Console.Write(text);
            Console.ResetColor();
        }
        static void Level1()
        {
            Console.SetCursorPosition(windowWidth / 2, windowHeight / 3);
            PrintText("LEVEL 1: KHÁM PHÁ CƠ SỞ A THÔI NÀO PAC-UEHER ƠI!!!", ConsoleColor.White);
            PrintText("Loading...", ConsoleColor.Yellow);
            Thread.Sleep(2000);
            Console.Clear();
            map.Map1();
            thongtin();
            Khungthongbao();
            Program.pacman = new Pacman(40, 27);
            Program.stupidGhosts.Add(new StupidGhost(44, 4, Object.direction.left));
            Program.smartGhosts.Add(new SmartGhost(44, 2, Object.direction.right));
            Program.smartGhosts.Add(new SmartGhost(48, 13, Object.direction.down));
            background1.Start();
            background1.IsBackground = true;
            pacman.Control(background1);
        }
        static void Level2()
        {
            Console.Clear();
            Console.SetCursorPosition(windowWidth / 2, windowHeight / 3);
            PrintText("LEVEL 2: KHÁM PHÁ CƠ SỞ B THÔI NÀO PAC-UEHER ƠI!!!", ConsoleColor.White);
            PrintText("Bạn có thêm chức năng tàng hình nhưng mà vật cản sẽ hiện lên bất ngờ nha...", ConsoleColor.Yellow);
            Thread.Sleep(4000);
            Console.Clear();
            gameOver = false;
            ClearMap();
            Console.SetCursorPosition(100, 5);
            Console.ForegroundColor = ConsoleColor.Green;
            Console.WriteLine(Map.greenJewel + ": Chức năng tàng hình");
            

            Task task = map.Map2();
            thongtin();
            Program.stupidGhosts.Clear(); // Xóa danh sách trước khi thêm mới
            Program.smartGhosts.Clear();

            Program.pacman = new Pacman(40, 27);
            Program.stupidGhosts.Add(new StupidGhost(44, 4, Object.direction.left));
            Program.smartGhosts.Add(new SmartGhost(44, 2, Object.direction.right));
            Program.smartGhosts.Add(new SmartGhost(48, 13, Object.direction.down));

            if (background1.IsAlive)
            {
                background1.Abort();
            }
            background2.Start();
            background2.IsBackground = true;
            pacman.Control(background2);

        }
        static void Level3()
        {
            Console.Clear();
            Console.SetCursorPosition(windowWidth / 2, windowHeight / 3);
            PrintText("LEVEL 3: KHÁM PHÁ CƠ SỞ A THÔI NÀO PAC-UEHER ƠI!!!", ConsoleColor.White);
            PrintText("Bạn có thêm chức năng di chuyển nhanh và di chuyển ngược...", ConsoleColor.Yellow);
            Thread.Sleep(4000);
            Console.Clear();
            gameOver = false;
            ClearMap();
            map.Map3();
            thongtin();
            Console.SetCursorPosition(100, 5);
            Console.ForegroundColor = ConsoleColor.Green;
            Console.WriteLine(Map.redJewel + ": Chức năng tăng tốc");
            Console.SetCursorPosition(100, 6);
            Console.ForegroundColor = ConsoleColor.Blue;
            Console.WriteLine(Map.blueJewel + ": Chức năng đi ngược");
            Program.stupidGhosts.Clear(); // Xóa danh sách trước khi thêm mới
            Program.smartGhosts.Clear();
            Program.pacman = new Pacman(40, 27);
            Program.stupidGhosts.Add(new StupidGhost(44, 4, Object.direction.left));
            Program.smartGhosts.Add(new SmartGhost(44, 2, Object.direction.right));
            Program.smartGhosts.Add(new SmartGhost(48, 13, Object.direction.down));
            if (background2.IsAlive)
            {background2.Abort();}
            background3.Start();
            background3.IsBackground = true;
            pacman.Control(background3);
        }
        static void Level4()
        {
            Console.Clear();
            gameOver = false;
            Console.SetCursorPosition(windowWidth / 2, windowHeight / 3);
            PrintText("LEVEL 4: KHÁM PHÁ CƠ SỞ B THÔI NÀO PAC-UEHER ƠI!!!", ConsoleColor.White);
            PrintText("Bạn có thể ăn được con ma thông minh nhưng mà vật cản sẽ hiện lên bất ngờ nha...", ConsoleColor.Yellow);
            Thread.Sleep(4000);
            Console.Clear();
            ClearMap();
            thongtin();
            Khungthongbao();
            Console.SetCursorPosition(100, 4);
            Console.ForegroundColor = ConsoleColor.Green;
            Console.WriteLine(Map.DarkMagentaJewel + ": Chức năng ăn ma thông minh");
            Task task = map.Map4();
            Program.stupidGhosts.Clear(); // Nếu cần, xóa danh sách trước khi thêm mới
            Program.smartGhosts.Clear();
            Program.pacman = new Pacman(40, 27);
            Program.stupidGhosts.Add(new StupidGhost(44, 4, Object.direction.left));
            Program.smartGhosts.Add(new SmartGhost(44, 2, Object.direction.right));
            Program.smartGhosts.Add(new SmartGhost(48, 13, Object.direction.down));
            if (background3.IsAlive)
            {background3.Abort();}
            background4.Start();
            background4.IsBackground = true;
            pacman.Control(background4);
        }

        public static void thongbao()
        {
            Khungthongbao();
            Program.PlaySound("Win.wav");

            // Tạo một mảng các thông điệp để xử lý vị trí
            var messages = new[]
            {
                $"Bạn qua Level {currentLevel}...",
                "A: Level tiếp theo",
                "B: Tạm ngừng"
            };

            // Hiển thị thông điệp
            for (int i = 0; i < messages.Length; i++)
            {
                Console.SetCursorPosition(98, 21 + i);
                Console.WriteLine(messages[i]);
            }

            // Nhận lựa chọn của người dùng
            char choice = ' ';
            while (choice != 'a' && choice != 'b')
            {
                Console.SetCursorPosition(95, 25);
                Console.Write("Bạn muốn chọn A hay là B: ");

                choice = char.ToLower((char)Console.Read()); // Đọc phím từ người dùng và chuyển đổi thành chữ thường

                switch (choice)
                {
                    case 'a':
                        currentPlayer.Level = currentLevel; // Cập nhật cấp độ hiện tại
                        currentPlayer.Score = score; // Cập nhật điểm số trước khi lưu
                        leaderboard.AddOrUpdatePlayer(currentPlayer);
                        break; // Thực hiện tiếp
                    case 'b':
                        bangxephang(); // Thực hiện hành động tạm ngừng hoặc bảng xếp hạng
                        break; // Thực hiện tạm ngừng
                    default:
                        Console.SetCursorPosition(95, 27);
                        Console.WriteLine("Bạn vui lòng chọn A hoặc B.");
                        Console.SetCursorPosition(103, 25);
                        break;
                }
            }
        }

        public static void LevelUp()
        {
            if (currentLevel < 6)
                currentLevel++;
            {
                switch (currentLevel)
                {
                    case 1:
                        Level1();
                        break;
                    case 2:
                        Level2();
                        break;
                    case 3:
                        Level3();
                        break;
                    case 4:
                        Level4();
                        break;
                    default:
                        gameOver = true; // Kết thúc trò chơi
                        Xoakhungthongbao();
                        Console.SetCursorPosition(95, 25);
                        Console.WriteLine("Bạn đã hoàn thành tất cả các level!");
                        bangxephang();
                        break;
                }
            }
            
        }

        static void ClearMap() //xóa Map
        {
            for (int i = 0; i < map.area.GetLength(0); i++)
            {
                for (int j = 0; j < map.area.GetLength(1); j++)
                {
                    map.area[i, j] = Map.emptySpace; // Xóa bản đồ logic
                }
            }
        }
        public static void BackgroundGame(int maxscore) //luồng chạy song song
        {
            int[] ShuffleQuestion = Shuffle(questions);
            int z = 0;
            int revivallevel = revival;

            while (true)
            {
                // Kiểm tra trạng thái người chơi
                if (score < maxscore && gameOver == true)

                {
                    currentPlayer.Status = PlayerStatus.Dead;
                    Console.SetCursorPosition(imin + 2, jmin + 2);
                    Console.ForegroundColor = ConsoleColor.Cyan;
                    Console.WriteLine("Bạn đã thua...");

                    if (revivallevel > 0)
                    {
                        char answer = ' '; // Khởi tạo biến answer

                        // Sử dụng vòng lặp while để đảm bảo người dùng chọn đúng
                        while (answer != 'y' && answer != 'n')
                        {
                            Console.SetCursorPosition(imin + 2, jmin + 4);
                            Console.ForegroundColor = ConsoleColor.Yellow;
                            Console.Write("Bạn muốn hồi sinh? (y/n): ");

                            answer = char.ToLower(Console.ReadKey(true).KeyChar); // Đọc phím từ người dùng

                            if (answer == 'y')
                            {
                                revivallevel--;
                                Console.SetCursorPosition(112, 2);
                                Console.ForegroundColor = ConsoleColor.White;
                                Console.WriteLine(revivallevel);
                                AskRevivalQuestion(ShuffleQuestion, z); // Gọi câu hỏi hồi sinh
                                z++;
                                currentPlayer.Status = PlayerStatus.Active; // Đặt lại trạng thái người chơi
                            }
                            else if (answer == 'n')
                            {
                                bangxephang(); // Hiển thị bảng xếp hạng
                                return; // Thoát vòng lặp
                            }
                            else
                            {
                                Console.SetCursorPosition(imin + 2, jmin + 6); // Hiển thị thông báo tại vị trí mới
                                Console.ForegroundColor = ConsoleColor.Red;
                                Console.WriteLine("Vui lòng nhập đúng (y/n).");
                            }
                        }
                    }
                    else
                    {
                        Console.SetCursorPosition(imin + 2, jmin + 3);
                        Console.ForegroundColor = ConsoleColor.Red;
                        Console.WriteLine("Bạn không còn mạng hồi sinh!");
                        Thread.Sleep(2000);
                        bangxephang();
                        return; // Thoát vòng lặp nếu không còn mạng hồi sinh
                    }
                }

                if (!gameOver)
                {
                    pacman.Step();
                    for (int i = Program.smartGhosts.Count - 1; i >= 0; i--)
                    {
                        Program.smartGhosts[i].Step();
                    }

                    // Sau khi có 30 viên ngọc, bắt đầu hành động của StupidGhost
                    foreach (StupidGhost ghost in Program.stupidGhosts)
                        ghost.Step();

                    //Hiển thị điểm qua màn
                    Console.ForegroundColor = ConsoleColor.Yellow;
                    Console.SetCursorPosition(106, 8);
                    Console.Write("Điểm qua màn: {0}", maxscore);

                    // Hiển thị điểm số
                    Console.ForegroundColor = ConsoleColor.Yellow;
                    Console.SetCursorPosition(110, 10);
                    Console.Write("Điểm: {0}", score);

                    Thread.Sleep(speed);
                }

                if (score >= maxscore)
                {
                    gameOver = true;
                    thongbao();
                    return;
                }
            }
        }
        static void BackgroundGame1() //luồng của level1
        {
            BackgroundGame(maxscore1);
        }
        public static void BackgroundGame2()//luồng của level2
        {
            BackgroundGame(maxscore2);
        }
        public static void BackgroundGame3()//luồng của level3
        {
            BackgroundGame(maxscore3);
        }
        public static void BackgroundGame4() //luồng của level4
        {
            BackgroundGame(maxscore4);
        }
        public static void thongtin()
        {
            Khungthongbao();
            Console.SetCursorPosition(94, 1);
            Console.ForegroundColor = ConsoleColor.White;
            Console.WriteLine("Thời gian");
            for (int i = 1; i < 4; i++)
            {
                Console.SetCursorPosition(104, i);
                Console.ForegroundColor = ConsoleColor.White;
                Console.WriteLine("|");
            }
            Console.SetCursorPosition(106, 1);
            Console.ForegroundColor = ConsoleColor.White;
            Console.WriteLine("Lần hồi sinh");
            Console.SetCursorPosition(112, 2);
            Console.ForegroundColor = ConsoleColor.White;
            Console.WriteLine(revival);
            for (int i = 1; i < 4; i++)
            {
                Console.SetCursorPosition(122, i);
                Console.ForegroundColor = ConsoleColor.White;
                Console.WriteLine("|");
            }
            Console.SetCursorPosition(124, 1);
            Console.ForegroundColor = ConsoleColor.White;
            Console.WriteLine("Level");
            Console.SetCursorPosition(126, 2);
            Console.ForegroundColor = ConsoleColor.White;
            Console.WriteLine(currentLevel);



        }
        public static void PlaySound(string soundFileName) //phát âm thanh
        {
            try
            {
                Assembly assembly = Assembly.GetExecutingAssembly();// Lấy assembly hiện tại
                string resourceName = $"PacmanGame.Resources.{soundFileName}";//Gán đường dẫn
                Xoakhungthongbao();
                using (Stream stream = assembly.GetManifestResourceStream(resourceName))
                {
                    if (stream != null)
                    {
                        using (SoundPlayer player = new SoundPlayer(stream))
                        {
                            player.Play(); // Phát âm thanh song song với luồng chính
                        }
                    }
                    else
                    {
                        Console.SetCursorPosition(imin + 2, jmin + 2);
                        Console.WriteLine($"Không tìm thấy âm thanh: {soundFileName}");
                    }
                }
            }
            catch (Exception ex)
            {
                Console.SetCursorPosition(imin + 2, jmin + 2);
                Console.WriteLine("Lỗi khi phát âm thanh: " + ex.Message);
            }
        }
        // Class lưu trữ thông tin người chơi
        public class Player
        {
            public string MSSV { get; set; }
            public string Name { get; set; }
            public int Score { get; set; }
            public int Level { get; set; }
            public PlayerStatus Status { get; set; } // Trạng thái người chơi


            // Constructor cho người chơi mới
            public Player(string MSSV, string name, int score, int level, PlayerStatus status = PlayerStatus.Active)
            {
                this.MSSV = MSSV;
                this.Name = name;
                this.Score = score;
                this.Level = level;
                this.Status = status; // Khởi tạo trạng thái

            }
            // Constructor chỉ nhận MSSV và điểm, sử dụng cho người chơi cũ
            public Player(string mssv, int score, int level, PlayerStatus status = PlayerStatus.Active)
            {
                this.MSSV = mssv;
                this.Score = score;
                this.Level = level;
                this.Name = ""; // Cần khởi tạo tên nếu không có
                this.Status = status; // Khởi tạo trạng thái

            }
        }

        // Class quản lý danh sách điểm số
        public class Leaderboard
        {
            private List<Player> players = new List<Player>();
            private string filePath = "highscores.txt";

            // Thêm người chơi mới hoặc cập nhật điểm số người chơi cũ
            public void AddOrUpdatePlayer(Player player)
            {
                var existingPlayer = players.FirstOrDefault(p => p.MSSV == player.MSSV);
                if (existingPlayer != null)
                {
                    existingPlayer.Score = Math.Max(existingPlayer.Score, player.Score); // Cập nhật nếu điểm mới cao hơn
                    existingPlayer.Level = Math.Max(existingPlayer.Level, player.Level);
                    existingPlayer.Name = player.Name; // Cập nhật tên nếu có thay đổi
                    existingPlayer.Status = player.Status; // Cập nhật trạng thái
                }
                else
                {
                    players.Add(player); // Thêm người chơi mới
                }

                // Sắp xếp lại danh sách ngay sau khi cập nhật
                players.Sort((p1, p2) => p2.Score.CompareTo(p1.Score));

                // Lưu lại danh sách vào file
                SaveToFile();
            }

            public void SaveToFile()
            {
                using (StreamWriter writer = new StreamWriter(filePath, false)) // false để ghi đè tệp
                {
                    foreach (var player in players)
                    {
                        // Lưu MSSV, tên và điểm
                        writer.WriteLine($"{player.MSSV}:{player.Name}:{player.Score}:{player.Level}:{player.Status}");
                    }
                }
            }
            // Tải danh sách người chơi từ tệp
            public List<Player> LoadFromFile()
            {
                try
                {
                    if (!File.Exists(filePath))
                    {
                        // Tạo tệp mới nếu chưa tồn tại
                        using (File.Create(filePath)) { } // Sử dụng File.Create để tạo tệp
                    }

                    string[] lines = File.ReadAllLines(filePath);
                    players.Clear(); // Xóa danh sách trước khi tải mới

                    foreach (string line in lines)
                    {
                        try
                        {
                            var parts = line.Split(':');
                            if (parts.Length == 5) // Cập nhật để xử lý thêm ngày giờ
                            {
                                string mssv = parts[0].Trim();
                                string name = parts[1].Trim();
                                int score = int.Parse(parts[2].Trim());
                                int level = int.Parse(parts[3].Trim());
                                PlayerStatus status = (PlayerStatus)Enum.Parse(typeof(PlayerStatus), parts[4].Trim());
                                players.Add(new Player(mssv, name, score, level, status));
                            }
                        }
                        catch (FormatException ex)
                        {
                            Console.WriteLine($"Lỗi định dạng trong dòng: '{line}'. Thông báo lỗi: {ex.Message}");
                        }
                        catch (ArgumentException ex)
                        {
                            Console.WriteLine($"Lỗi phân tích trong dòng: '{line}'. Thông báo lỗi: {ex.Message}");
                        }
                        catch (Exception ex)
                        {
                            Console.WriteLine($"Lỗi không xác định trong dòng: '{line}'. Thông báo lỗi: {ex.Message}");
                        }
                    }

                    players.Sort((p1, p2) => p2.Score.CompareTo(p1.Score)); // Sắp xếp danh sách người chơi theo điểm

                    return players;
                }
                catch (IOException ex)
                {
                    Console.WriteLine($"Lỗi khi đọc tệp: {ex.Message}");
                    return new List<Player>(); // Trả về danh sách rỗng trong trường hợp có lỗi
                }
                catch (Exception ex)
                {
                    Console.WriteLine($"Lỗi không xác định: {ex.Message}");
                    return new List<Player>(); // Trả về danh sách rỗng trong trường hợp có lỗi
                }
            }
            // Hiển thị bảng điểm
            public void DisplayLeaderboard(Player currentPlayer)
            {
                Console.Clear();
                Console.WriteLine("Bảng xếp hạng (Top 20):");

                // Lấy tối đa 20 người chơi
                var topPlayers = players.Take(20).ToList();

                // Thêm người chơi hiện tại vào danh sách
                if (!topPlayers.Any(p => p.MSSV == currentPlayer.MSSV))
                {
                    topPlayers.Add(currentPlayer);
                }

                // Sắp xếp lại danh sách
                topPlayers = topPlayers.OrderByDescending(p => p.Score).Take(20).ToList();

                // Hiển thị bảng xếp hạng với số thứ tự
                bool isInTop20 = topPlayers.Any(p => p.MSSV == currentPlayer.MSSV);
                for (int i = 0; i < topPlayers.Count; i++)
                {
                    if (topPlayers[i].MSSV == currentPlayer.MSSV)
                    {
                        Console.ForegroundColor = ConsoleColor.Blue; // Màu xanh dương cho người chơi hiện tại
                    }
                    else
                    {
                        Console.ForegroundColor = ConsoleColor.Red; // Màu đỏ cho người chơi khác
                    }
                    Console.WriteLine($"{i + 1}. {topPlayers[i].MSSV}: {topPlayers[i].Name}: {topPlayers[i].Score}");
                }

                if (!isInTop20)
                {
                    Console.ForegroundColor = ConsoleColor.Yellow; // Màu vàng cho thông báo
                    Console.WriteLine("Bạn không nằm trong top 20.");
                }

                Console.ResetColor();
                Console.ReadKey();
            }
        }
        public static int imin = 93;
        public static int imax = 137;
        public static int jmin = 20;
        public static int jmax = 30;
        public static void Khungthongbao()
        {
            // Vẽ các đường dọc và ngang bằng một vòng lặp duy nhất
            for (int j = jmin; j <= jmax; j++)
            {
                for (int i = imin; i <= imax; i++)
                {
                    if (i == imin || i == imax) // Vẽ các đường dọc
                    {
                        Console.SetCursorPosition(i, j);
                        Console.Write("|");
                    }
                    else if (j == jmin || j == jmax) // Vẽ các đường ngang
                    {
                        Console.SetCursorPosition(i, j);
                        Console.Write("-");
                    }
                }
            }
        }
        public static void Xoakhungthongbao()
        {
            for (int j = jmin + 1; j < jmax; j++)
            {
                for (int i = imin + 1; i < imax; i++)
                {
                    Console.SetCursorPosition(i, j);
                    Console.Write(" ");
                }
            }
        }
        public static string[] questions =
        {
            "Đại học Kinh Tế TPHCM thành lập năm",
            "Hiện tại UEH có mấy trường thành viên?",
            "UEH có bao nhiêu khoa?",
            "Trụ sở chính của UEH là cơ sở nào?",
            "Theo BXH QS Asia 2024, UEH đạt Top:",
            "Hiện tại UEH hoạt động theo mô hình nào?",
            "UEH có bao nhiêu mô hình phân loại rác?",
            "Khoa BIT thuộc trường thành viên nào?",
            "1 trong 3 trạm thực hành phân loại 3 UEH:",
            "1 trong 7 trạm thực hành phân loại 7 UEH:"
        };
        public static string[,] options =
        {
            {"A. 1967","B. 1976"},
            {"A. 3","B. 4" },
            {"A. 14","B. 15" },
            {"A. Cơ sở B","B. Cơ sở A" },
            {"A. TOP 201+","B. TOP 301+" },
            {"A. “Đa lĩnh vực”","B. “Đa nghề”" },
            {"A. 3","B. 2" },
            {"A.Trường Kinh doanh","B.Trường CN & TK"},
            {"A. Rác tái chế","B. nhựa tái chế"},
            {"A. Rác thải tái chế","B. Nhựa tái chế" }
        };
        public static string[] answerquestion =
        {
            "B",
            "A",
            "B",
            "B",
            "B",
            "A",
            "B",
            "B",
            "A",
            "B",
        };
        public static int[] Shuffle(string[] questions)
        {
            // Tạo mảng chứa các chỉ số của câu hỏi
            int[] allQuestions = new int[questions.Length];
            for (int i = 0; i < allQuestions.Length; i++)
            {
                allQuestions[i] = i;
            }
            Random rand = new Random();
            // Shuffle câu hỏi (Fisher-Yates algorithm)
            for (int i = allQuestions.Length - 1; i > 0; i--)
            {
                int j = rand.Next(i + 1);
                int temp = allQuestions[i];
                allQuestions[i] = allQuestions[j];
                allQuestions[j] = temp;
            }
            return allQuestions;
        }
        public static void AskRevivalQuestion(int[] questionIndex, int w)
        {
            Xoakhungthongbao();
            Console.SetCursorPosition(imin + 2, jmin + 2);
            Console.ForegroundColor = ConsoleColor.Red;
            Console.Write("Trả lời 1 câu hỏi để hồi sinh");
            Console.SetCursorPosition(imin + 2, jmin + 3);
            Console.ForegroundColor = ConsoleColor.Cyan;
            Console.WriteLine(questions[questionIndex[w]]);
            Console.SetCursorPosition(imin + 2, jmin + 4);
            Console.WriteLine(options[questionIndex[w], 0]);
            Console.SetCursorPosition(imin + 2, jmin + 5);
            Console.WriteLine(options[questionIndex[w], 1]);

            char chosenAnswer;

            while (true) // Bắt đầu vòng lặp
            {
                Console.SetCursorPosition(imin + 2, jmin + 6);
                Console.ForegroundColor = ConsoleColor.Cyan;
                Console.Write("Câu trả lời của bạn là: ");
                chosenAnswer = char.ToUpper((char)Console.Read()); // Đọc phím từ người dùng và chuyển đổi thành chữ hoa
                Console.ReadLine(); 
                // Kiểm tra xem người dùng đã nhập đúng A hoặc B không
                if (chosenAnswer == 'A' || chosenAnswer == 'B')
                {
                    if (chosenAnswer.ToString() == answerquestion[questionIndex[w]])
                    {
                        Console.SetCursorPosition(imin + 2, jmin + 8);
                        Console.ForegroundColor = ConsoleColor.Red;
                        Console.Write("Đáp án chính xác! Bạn quá giỏi");
                        Xoakhungthongbao();

                        for (int i = 0; i < Program.smartGhosts.Count; i++)
                        {
                            var ghost = Program.smartGhosts[i];
                            // Kiểm tra nếu vị trí của Pacman và con ma trùng nhau
                            if (pacman.X == ghost.X && pacman.Y == ghost.Y)
                            {
                                // Gọi phương thức để đặt lại vị trí của con ma khi có va chạm
                                ghost.GhostIsEated();
                            }
                        }

                        gameOver = false; // Trả lời đúng, hồi sinh thành công
                        break; // Thoát khỏi vòng lặp nếu câu trả lời đúng
                    }
                    else
                    {
                        Console.SetCursorPosition(imin + 2, jmin + 8);
                        Console.ForegroundColor = ConsoleColor.Red;
                        Console.Write($"Đáp án chính xác là: {answerquestion[questionIndex[w]]}");
                        Thread.Sleep(2000);
                        bangxephang();
                        break; // Thoát khỏi vòng lặp nếu câu trả lời sai
                    }
                }
                else
                {
                    // Nếu người dùng nhập không phải A hoặc B, yêu cầu nhập lại
                    Console.SetCursorPosition(imin + 2, jmin + 9);
                    Console.ForegroundColor = ConsoleColor.Red;
                    Console.WriteLine("Vui lòng chọn A hoặc B.");
                }
            }
        }

        public static void bangxephang()
        {
            Console.Clear();
            PrintText("Trò chơi đã được tạm ngừng. Lưu trạng thái...", ConsoleColor.Yellow);
            currentPlayer.Level = currentLevel; // Cập nhật cấp độ hiện tại
            currentPlayer.Score = score; // Cập nhật điểm số trước khi lưu
            leaderboard.AddOrUpdatePlayer(currentPlayer);
            leaderboard.AddOrUpdatePlayer(currentPlayer); // Lưu thông tin người chơi

            // In thông báo trước khi hiển thị bảng xếp hạng
            PrintText("Nhấn phím bất kỳ để xem bảng xếp hạng...", ConsoleColor.Yellow);
            Console.ReadKey(); // Chờ người dùng nhấn phím trước khi hiển thị bảng xếp hạng

            // Hiển thị bảng xếp hạng với điểm số hiện tại
            leaderboard.DisplayLeaderboard(currentPlayer);

            // In thông báo và chờ người dùng nhấn phím để thoát
            PrintText("Nhấn phím bất kỳ để thoát...", ConsoleColor.Green);
            Console.ReadKey(); // Chờ người dùng nhấn phím để thoát
            Environment.Exit(0);

        }

    }
}

