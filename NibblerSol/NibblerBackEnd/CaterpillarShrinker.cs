using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NibblerBackEnd 
{
    class CaterpillarShrinker : ICollidable
    {
        public int Points { get; set; }
        public int NumLivesGained { get; set; }
        public int NumNewTokens { get; set; }

        public event CollisionHandler Collision;

        public CaterpillarShrinker(int Points, int NumLivesGained)
        {
            this.Points = Points;
            this.NumLivesGained = NumLivesGained;
            this.NumNewTokens = 1;
        }

        public void Collide(Caterpillar c)
        {

        }
    }
}
