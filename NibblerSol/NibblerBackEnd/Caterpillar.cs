using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NibblerBackEnd
{
    class Caterpillar
    {
        private Queue<Point> Squares;
        public Direction Direction;
        public Grid Grid;
        public IEnumerable<Point> Points;
        public event EventHandler SelfCollision;

        public void Die()
        {
            
        }
    }
}
