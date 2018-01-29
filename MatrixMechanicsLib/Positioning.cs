using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MatrixMechanicsLib
{
    public class Orientation
    {
        //Directional Coordinatess
        public enum Direction { North, East, South, West }
        public static Dictionary<Direction, Coords> DirCoords = new Dictionary<Direction, Coords>()
        {
            { Direction.North, new Coords(0,-1) },
            { Direction.East,  new Coords(1, 0) },
            { Direction.South, new Coords(0, 1) },
            { Direction.West , new Coords(-1,0) }
        };

        public Coords Pos;
        public Direction Dir;

        public Orientation(Coords pos, Direction dir = Direction.North)
        {
            Pos = pos;
            Dir = dir;
        }
        public void Step(int count = 1)
        {
            Pos.Set(Pos + (DirCoords[Dir] * count));
        }
        public void Turn(int quarterCount)
        {
            quarterCount = quarterCount % 4;
            Dir += quarterCount;
            if ((int)Dir >=  4) Dir -= 4;
            if ((int)Dir <= -1) Dir += 4;
        }

    }

    public class Coords
    {
        public int X { get; private set; }
        public int Y { get; private set; }

        public Coords(int x, int y)
        {
            X = x;
            Y = y;
        }

        public void Set(Coords coords)
        {
            X = coords.X;
            Y = coords.Y;
        }

        //OPERATOR OVERLOADING
        public static Coords operator +(Coords co1, Coords co2) // +
        {
            return new Coords(co1.X + co2.X, co1.Y + co2.Y); 
        }
        public static Coords operator -(Coords co1, Coords co2) // -
        {
            return new Coords(co1.X - co2.X, co1.Y - co2.Y);
        }
        public static bool operator ==(Coords co1, Coords co2) // ==
        {
            return (co1.X == co2.X && co1.Y == co2.Y);
        }
        public static bool operator !=(Coords co1, Coords co2) // !=
        {
            return !(co1.X == co2.X && co1.Y == co2.Y);
        }
        public static Coords operator *(Coords co1, int num) // *
        {
            return new Coords(co1.X * num, co1.Y * num);
        }
        public static Coords operator /(Coords co1, int num) // /
        {
            return new Coords(co1.X / num, co1.Y / num);
        }
    } 

    public interface IMappabable
    {
        Orientation Orientation { get; set; }
        bool IsObstruction { get; set; }
        bool IsStackable { get; set; }
        uint ZPosition { get; set; }
        Type Type { get; }
    }
}
