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

        public Grid(ICollidable[,] tiles, ScoreAndLives ScoreAndLives)
        {
            this.tiles = tiles;
            this.ScoreAndLives = ScoreAndLives;




            
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



            this.AddCollisionEvent(NewToken);
            ScoreAndLives.AddCollisionEvent(NewToken);
        }
    }
}
