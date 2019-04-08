using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NibblerBackEnd
{
    public class Point
    {
        public int X { get; private set; }

        public int Y { get; private set; }

        public Point(int X, int Y)
        {
            this.X = X;
            this.Y = Y;
        }

        public static Boolean operator ==(Point a, Point b)
        {
            if (a.X == b.X && a.Y == b.Y)
                return true;
            else
                return false;
        }

        public static Boolean operator !=(Point a, Point b)
        {
            if (a.X == b.X && a.Y == b.Y)
                return false;
            else
                return true;
        }
    }
}
