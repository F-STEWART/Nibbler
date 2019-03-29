using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NibblerBackEnd 
{
    class CaterpillarShrinker : ICollidable
    {
        public int points { get; set; }
        public int NumLivesGained { get; set; }
        public int NumNewTokens { get; set; }

        public event EventHandler Collision;

        public void Collide(Caterpillar c)
        {

        }
    }
}
