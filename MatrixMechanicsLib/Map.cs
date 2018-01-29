using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MatrixMechanicsLib
{
    public class Map
    {
        public int Width { get; private set; }
        public int Height { get; private set; }

        public class Cell
        {
            Coords Coords;
            public Cell(int x, int y)
            {
                Coords = new Coords(x, y);
            }
            private List<IMappabable> Contents = new List<IMappabable>();
            public bool IsObstruction { get; private set; } = false;
            public bool Add(IMappabable item)
            {
                if (Contents.Contains(item) || !item.IsStackable)
                {
                    return false;
                }
                else
                {
                    Contents.Add(item);
                    if (item.IsObstruction) IsObstruction = true;
                    OrderByZ();
                    item.Orientation.Pos.Set(Coords);
                    return true;
                }
            }
            public IMappabable GetTopMost()
            {
                return Contents.FirstOrDefault();
            }
            private void OrderByZ()
            {
                Contents.OrderBy(item => item.ZPosition);
            }
        }

        private Cell[,] Grid;
        public Cell this[int x, int y]
        {
            get { return Grid[x, y]; }
        }

        public Map(int width, int height)
        {
            Width = width;
            Height = height;
            Grid = new Cell[Width, Height];
            //Initialize Grid Cells
            for (int x = 0; x < Width; x++)
            {
                for (int y = 0; y < Height; y++)
                {
                    Grid[x, y] = new Cell(x,y);
                }
            }
        }

        public Map(string mapString, char separator, Dictionary<char, IMappabable> dictionary)
        {
            string[] mapSlices = mapString.Split(separator);
            Width = mapSlices.Max(element => element.Length);
            Height = mapSlices.Length;
            Grid = new Cell[Width, Height];
            //Initialize Grid Cells
            for (int x = 0; x < Width; x++)
            {
                for (int y = 0; y < Height; y++)
                {
                    Grid[x, y] = new Cell(x,y);
                }
            }

            for (int y = 0; y < Height; y++)
            {
                for (int x = 0; x < Width; x++)
                {
                    if (dictionary.ContainsKey(mapSlices[y][x]))
                    {
                        Grid[x, y].Add(dictionary[mapSlices[y][x]]);
                    }
                }
            }
        }

        public void FillFromString(string mapString, char separator, Dictionary<char,IMappabable> dictionary)
        {
            string[] mapSlices = mapString.Split(separator);

            for (int y = 0; y < Math.Min(Height,mapSlices.Length); y++)
            {
                for (int x = 0; x < Math.Min(Width,mapSlices[y].Length) ; x++)
                {
                    if (dictionary.ContainsKey(mapSlices[y][x]))
                    {
                        Grid[x, y].Add(dictionary[mapSlices[y][x]]);
                    }
                }
            }
        }

        public char[,] GetAsCharArray(Dictionary<char, IMappabable> dictionary)
        {
            char[,] output = new char[Width, Height];

            for (int x = 0; x < Width; x++)
            {
                for (int y = 0; y < Height; y++)
                {
                    if (dictionary.Values.Select(v => v.Type).Contains(Grid[x, y].GetTopMost().Type))
                    {
                        output[x, y] = dictionary.Where(pair => pair.Value == Grid[x, y].GetTopMost()).Select(pair => pair.Key).FirstOrDefault();
                    }  
                }
            }

            return output;
        }
    }
}
