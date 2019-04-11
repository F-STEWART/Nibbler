using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NibblerBackEnd 
{
    public class CaterpillarShrinker : ICollidable
    {
        //The number of points gained for this token
        public int Points { get; set; }
        //Currently always 0 No way to gain lives, otherwise this would gain lives
        public int NumLivesGained { get; set; }
        //The token needs to be replaced so this number is 1
        public int NumNewTokens { get; set; }

        public event CollisionHandler Collision;

        public CaterpillarShrinker(int Points, int NumLivesGained)
        {
            this.Points = Points;
            this.NumLivesGained = NumLivesGained;
            this.NumNewTokens = 1;
        }
        //Publisher event call method, also shrinks the caterpillar
        public void Collide(Caterpillar c)
        {
            if (c.GetLength() > 1) {
                c.Shrink();
            }
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
