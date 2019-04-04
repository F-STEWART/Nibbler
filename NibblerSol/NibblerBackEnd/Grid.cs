using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NibblerBackEnd
{
    class Grid
    {

        public ICollidable[,] tiles { get; private set; }

        public int Length { get; private set; }

        public int Width { get; private set; }

        public Grid(ICollidable[,] tiles)
        {
            this.tiles = tiles;





            
        }
        public void AddCollisionEvent(ICollidable subject)
        {
            if (subject != null)
            {
                subject.Collision += AddNewTokens;
            }
        }

        public void AddNewTokens(ICollidable sender, EventArgs e)
        {

        }
    }
}
