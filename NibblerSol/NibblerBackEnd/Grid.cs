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

        private ScoreAndLives ScoreAndLives;

        public delegate ICollidable RandomToken(Grid Grid, Caterpillar Caterpillar);

        private RandomToken TokenGenerator;

        private Caterpillar Caterpillar;

        public Grid(ICollidable[,] tiles, RandomToken TokenGenerator)
        {
            this.tiles = tiles;
            this.TokenGenerator = TokenGenerator;
            this.Caterpillar = null;




            
        }
        public void AquireScoreAndLives(ScoreAndLives ScoreAndLives)
        {

        }
        public void AquireCaterpillar(Caterpillar Caterpillar)
        {
            this.Caterpillar = Caterpillar;
        }
        public void AddCollisionEvent(ICollidable subject)
        {
            if (subject != null)
            {
                subject.Collision += AddNewTokens;
            }
        }

        private void AddNewTokens(ICollidable sender, EventArgs e)
        {
            ICollidable NewToken = new Wall();
            TokenGenerator(this, Caterpillar);


            this.AddCollisionEvent(NewToken);
            ScoreAndLives.AddCollisionEvent(NewToken);
        }
        public void Collide(Caterpillar Caterpillar)
        {
            Point Current = Caterpillar.GetHead();
            this.tiles[Current.X, Current.Y].Collide(Caterpillar);
        }
    }
}
