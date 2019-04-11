using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NibblerBackEnd
{
    public class CaterpillarGrower : ICollidable
    {
        //Points gained from this token
        public int Points { get; set; }
        //Currently always 0, no methods to gain lives in this game.
        public int NumLivesGained { get; set; }
        //Always 1 as there needs to be a replacement token.
        public int NumNewTokens { get; set; }

        public event CollisionHandler Collision;
        //Just sets the above fields.
        public CaterpillarGrower(int Points, int NumLivesGained)
        {
            this.Points = Points;
            this.NumLivesGained = NumLivesGained;
            this.NumNewTokens = 1;
        }
        //Publisher event calling method, plus caterpillar grows
        public void Collide(Caterpillar c)
        {
            c.Grow(1);
            OnCollide();
        }
        //The actual event call
        protected virtual void OnCollide()
        {
            if (Collision != null)
            {
                Collision(this, null);
            }
        }
    }
}
