using System;
using System.Media;
using System.Threading;

namespace PacmanGame
{
    abstract class Object
    {
        public char currentStatePlace;
        
        public int x, y; 
        char objectSymbol;
        public enum direction { left, up, right, down }
        public direction objectDirection = direction.right;
        public direction previousObjectDirection = direction.right;
        public int X
        {
            set
            {
                x = value;
            }
            get
            {
                return x;
            }
        }
        public int Y
        {
            set
            {
                y = value;
            }
            get
            {
                return y;
            }
        }
        public Random randomize = new Random();

        public abstract char GetSymbol();

        public abstract void Step();

        public char GetSymbolByDirection(direction Direction)
        {
            if (Direction == direction.left) return Program.map[x - 1, y];
            if (Direction == direction.right) return Program.map[x + 1, y];
            if (Direction == direction.up) return Program.map[x, y - 1];
            return Program.map[x, y + 1];
        }
        public abstract void GhostIsEated();
        public void KillPacman()
        {
            if (Pacman.modeInvisible == false && Pacman.modeEating == false)
            {
                if (Program.pacman.x == x && Program.pacman.y == y)
                {
                    Program.PlaySound("Gameover.wav");
                    Program.gameOver = true;
                }


            }
            else if (Pacman.modeEating && this is SmartGhost ghost) // Kiểm tra nếu đối tượng là SmartGhost
            {
                if (Program.pacman.x == x && Program.pacman.y == y)
                {
                    ghost.GhostIsEated();
                }
            }
        }
        public virtual void ChangePositionByDirection(direction Direction)
        {
            //giới hạn của khu vực map
            if (x > 89) x = 0;
            else if (x < 0) x = 89;

            Program.map.RenderChar(x, y, currentStatePlace);
            if (Direction == direction.left) x--;
            if (Direction == direction.right) x++;
            if (Direction == direction.up) y--;
            if (Direction == direction.down) y++;

            //Khi 2 con ma gặp nhau
            if (Program.map[x, y] != Map.stupidGhostSymbol && Program.map[x, y] != Map.smartGhostSymbol)
            {
                currentStatePlace = Program.map[x, y];
            }
            //Vẽ ký tự game
            Program.map.RenderChar(x, y, GetSymbol());
        }
    }
}
