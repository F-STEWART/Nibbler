using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NibblerBackEnd
{
    public class Wall : ICollidable
    {
        //points are how many points the player gains for Collision, in this case 0
        public int Points { get; set; }
        //Negative, walls cause the player to lose a life.
        public int NumLivesGained { get; set; }
        //None as the wall isn't a token that needs to be replaced
        public int NumNewTokens { get; set; }
        public event CollisionHandler Collision;

        public Wall()
        {
            this.Points = 0;
            this.NumLivesGained = -1;
            this.NumNewTokens = 0;
        }
        //A publisher method that is called when the caterpillar collides with this object.
        public void Collide(Caterpillar c)
        {
            c.Die();
            OnCollide();
        }
        //The actual event call.
        protected virtual void OnCollide()
        {
            if(Collision != null)
            {
                Collision(this, null);
            }
        }
    }
}
