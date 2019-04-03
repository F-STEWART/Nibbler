using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NibblerBackEnd
{
    class Wall : ICollidable
    {
        public int Points { get; set; }
        public int NumLivesGained { get; set; }
        public int NumNewTokens { get; set; }
        public event CollisionHandler Collision;

        public Wall()
        {
            this.Points = 0;
            this.NumLivesGained = 0;
            this.NumNewTokens = 0;
        }

        public void Collide(Caterpillar c)
        {

        }
    }
}
