using System;
using System.Collections.Generic;
using System.Threading;

namespace PacmanGame
{
    class SmartGhost : Object
    {
        public int startX; // Lưu vị trí ban đầu X
        public int startY; // Lưu vị trí ban đầu Y
        public SmartGhost(int x, int y, direction Direction)
        {
            this.X = x;
            this.Y = y;
            this.startX = x;  // Lưu vị trí ban đầu
            this.startY = y;  // Lưu vị trí ban đầu
            currentStatePlace = Program.map.Jewel;
            objectDirection = Direction;
            Program.map.RenderChar(x, y, GetSymbol());
        }

        //Ký tự của ma
        public override char GetSymbol()
        {
            return Map.smartGhostSymbol;
        }

        //Tìm kiếm những hướng khả dụng
        public void DetectDirection()
        {
            List<direction> variantsOfDirection = new List<direction>();
            if (objectDirection == direction.up)
            {
                if (Program.map[x, y - 1] != Map.wall)
                {
                    variantsOfDirection.Add(direction.up);
                }
                if (Program.map[x - 1, y] != Map.wall)
                {
                    variantsOfDirection.Add(direction.left);
                }
                if (Program.map[x + 1, y] != Map.wall)
                {
                    variantsOfDirection.Add(direction.right);
                }
            }
            else if (objectDirection == direction.down)
            {
                if (Program.map[x, y + 1] != Map.wall)
                {
                    variantsOfDirection.Add(direction.down);
                }
                if (Program.map[x - 1, y] != Map.wall)
                {
                    variantsOfDirection.Add(direction.left);
                }
                if (Program.map[x + 1, y] != Map.wall)
                {
                    variantsOfDirection.Add(direction.right);
                }
            }
            else if (objectDirection == direction.left)
            {
                if (Program.map[x, y - 1] != Map.wall)
                {
                    variantsOfDirection.Add(direction.up);
                }
                if (Program.map[x - 1, y] != Map.wall)
                {
                    variantsOfDirection.Add(direction.left);
                }
                if (Program.map[x, y + 1] != Map.wall)
                {
                    variantsOfDirection.Add(direction.down);
                }
            }
            else
            {
                if (Program.map[x, y - 1] != Map.wall)
                {
                    variantsOfDirection.Add(direction.up);
                }
                if (Program.map[x, y + 1] != Map.wall)
                {
                    variantsOfDirection.Add(direction.down);
                }
                if (Program.map[x + 1, y] != Map.wall)
                {
                    variantsOfDirection.Add(direction.right);
                }
            }

            //Chọn hướng di chuyển dựa trên tọa độ Pac-Ueher
            Pacman pacman = Program.pacman;

            if (x < pacman.x && objectDirection != direction.left && variantsOfDirection.Contains(direction.right))
            {
                objectDirection = direction.right;
            }
            else if (x > pacman.x && objectDirection != direction.right && variantsOfDirection.Contains(direction.left))
            {
                objectDirection = direction.left;
            }
            else if (y > pacman.y && objectDirection != direction.down && variantsOfDirection.Contains(direction.up))
            {
                objectDirection = direction.up;
            }
            else if (y < pacman.y && objectDirection != direction.up && variantsOfDirection.Contains(direction.down))
            {
                objectDirection = direction.down;
            }
            else
            {
                
                if (variantsOfDirection.Count > 0) // Kiểm tra nếu danh sách không rỗng
                {
                    Random random = new Random();
                    int index = random.Next(variantsOfDirection.Count);
                    objectDirection = variantsOfDirection[index];
                }
                else
                {
                    // Xử lý trường hợp không có hướng nào hợp lệ
                    if (objectDirection == direction.up)
                        objectDirection = direction.down;
                    else if (objectDirection == direction.down)
                        objectDirection = direction.up;
                    else if (objectDirection == direction.left)
                        objectDirection = direction.right;
                    else 
                        objectDirection = direction.left;
                }
            }
        }

        //Phương thức đi của ma thông minh
        public override void Step()
        {
            KillPacman();
            DetectDirection();
            ChangePositionByDirection(objectDirection);
            KillPacman();
        }
        public void Respawn()
        {
            // Hồi sinh ma tại vị trí ban đầu
            this.X = startX;
            this.Y = startY;
            Program.smartGhosts.Add(new SmartGhost(startX, startY, Object.direction.down));
        }
        public override void GhostIsEated()
        {
            {
                Program.smartGhosts.Remove(this);
                Console.SetCursorPosition(this.X, this.Y);
                Console.Write(' ');
                new Thread(() =>
                {
                    Thread.Sleep(5000); // Đợi 5 giây
                    Respawn(); // Hồi sinh ma tại vị trí ban đầu
                }).Start();
            }
        }
    }
}
