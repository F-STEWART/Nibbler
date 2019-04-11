using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NibblerBackEnd
{
    public class Grid
    {
        //This is the actual grid if there is nothing in a given cell it is just null.
        public ICollidable[,] tiles { get; private set; }
        //Length of the grid
        public int Length { get; private set; }
        //Width of the grid
        public int Width { get; private set; }
        // Needed for newToken Subscriptions
        private ScoreAndLives ScoreAndLives;
        // Needed to define Token generator
        public delegate ICollidable RandomToken(Grid Grid, Caterpillar Caterpillar);
        // From Gamestate generates a random location for a new token
        private RandomToken TokenGenerator;
        //...It is the caterpillar
        private Caterpillar Caterpillar;
        //Initializes what is easy to initialize in the constructor. Important to note that tiles is passed fully formed and initialized.
        public Grid(ICollidable[,] tiles, RandomToken TokenGenerator)
        {
            this.tiles = tiles;
            this.TokenGenerator = TokenGenerator;
            this.Caterpillar = null;
        }
        //Gets score and lives for Subscription purposes
        public void AquireScoreAndLives(ScoreAndLives ScoreAndLives)
        {
            this.ScoreAndLives = ScoreAndLives;
        }
        //Gets Caterpillar for subscription purposes
        public void AquireCaterpillar(Caterpillar Caterpillar)
        {
            this.Caterpillar = Caterpillar;
        }
        //Collision subscription call, used for every token after the first.
        public void AddCollisionEvent(ICollidable subject)
        {
            //Prevents the call if subject is null, or if the calling collision was a wall as that does not necessitate a new token 
            if (subject != null && subject.GetType() != typeof(Wall))
            {
                subject.Collision += AddNewTokens;
            }
        }
        //Adds new tokens when an old token is eaten
        private void AddNewTokens(ICollidable sender, EventArgs e)
        {
            //Makes a new random type of token and inserts it into the grid (in the method TokenGenerator, not here)
            ICollidable NewToken = TokenGenerator(this, Caterpillar);

            //Adds some fancy new Subscriptions to the new token.
            this.AddCollisionEvent(NewToken);
            ScoreAndLives.AddCollisionEvent(NewToken);
            //This removes the eaten token
            this.tiles[Caterpillar.GetHead().X, Caterpillar.GetHead().Y] = null;
        }
        //Collision event call.
        public void Collide(Caterpillar Caterpillar)
        {
            //needed to find which tile is being affected
            Point Current = Caterpillar.GetHead();
            this.tiles[Current.X, Current.Y].Collide(Caterpillar);
        }
    }
}
