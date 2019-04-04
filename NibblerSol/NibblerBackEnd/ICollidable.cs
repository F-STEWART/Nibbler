using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NibblerBackEnd
{
    public interface ICollidable
    {
        int Points { get; set; }
        int NumLivesGained { get; set; }
        int NumNewTokens { get; set; }

        event CollisionHandler Collision;

        void Collide(Caterpillar c);
    }
}
