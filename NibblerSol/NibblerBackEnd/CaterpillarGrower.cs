using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NibblerBackEnd
{
    public class CaterpillarGrower : ICollidable
    {
        public int Points { get; set; }
        public int NumLivesGained { get; set; }
        public int NumNewTokens { get; set; }

        public event CollisionHandler Collision;

        public CaterpillarGrower(int Points, int NumLivesGained)
        {
            this.Points = Points;
            this.NumLivesGained = NumLivesGained;
            this.NumNewTokens = 1;
        }

        public void Collide(Caterpillar c)
        {
            c.Grow(1);
            OnCollide();
        }

        protected virtual void OnCollide()
        {
            if (Collision != null)
            {
                Collision(this, null);
            }
        }
    }
}
