using System;
using System.Collections.Generic;

namespace PacmanGame
{
    class StupidGhost : Object
    {
        
        public StupidGhost(int x, int y, direction Direction)
        {
            this.X = x;
            this.Y = y;
            currentStatePlace = Program.map.EmptySpace;
            objectDirection = Direction;
            Program.map.RenderChar(x, y, GetSymbol());
        }

        //Vẽ ký hiện ma ngốc
        public override char GetSymbol()
        {
            return Map.stupidGhostSymbol;
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
                if (Program.map[x, y + 1] != Map.wall)
                {
                    variantsOfDirection.Add(direction.down);
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
                if (Program.map[x, y - 1] != Map.wall)
                {
                    variantsOfDirection.Add(direction.up);
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
                if (Program.map[x + 1, y] != Map.wall)
                {
                    variantsOfDirection.Add(direction.right);
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
                if (Program.map[x - 1, y] != Map.wall)
                {
                    variantsOfDirection.Add(direction.left);
                }
            }

            //Chọn ngẫu nhiên hướng di chuyển trong danh sách khả dụng
            Random random = new Random();
            int index = random.Next(variantsOfDirection.Count);
            objectDirection = variantsOfDirection[index];
           
        }
        public override void GhostIsEated()
        {

        }
        //Phương thức đi của ma ngốc
        public override void Step()
        {
            KillPacman();
            DetectDirection();
            ChangePositionByDirection(objectDirection);
            KillPacman();
        }
    }
}
