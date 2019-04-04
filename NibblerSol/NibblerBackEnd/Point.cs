using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NibblerBackEnd
{
    class Point
    {
        public int X { get; private set; }

        public int Y { get; private set; }

        public Point(int X, int Y)
        {
            this.X = X;
            this.Y = Y;
        }
    }
}
