using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MatrixMechanicsLib;

namespace Testing
{
    class Program
    {
        public class Wall : IMappabable
        {
            public Orientation Orientation { get; set; }
            public bool IsObstruction { get; set; }
            public bool IsStackable { get; set; }
            public uint ZPosition { get; set; }
            public Type Type { get; }

            public Wall()
            {
                IsObstruction = true;
                IsStackable = false;
                ZPosition = 0;
                Type = this.GetType();
            }
        }

        static void Main(string[] args)
        {
            Wall wall;
            Dictionary<char, IMappabable> CharReference = new Dictionary<char, IMappabable>
            {
                { '#', wall = new Wall()}
            };

            Map Arena = new Map("###_# #_###", '_', CharReference);
            Console.WriteLine(Arena[0, 0].IsObstruction);

            Console.ReadLine();
        }
    }
}
