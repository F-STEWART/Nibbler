using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NibblerBackEnd
{
    public class ScoreAndLives
    {
        public int Score
        {
            get;
            private set;
        }
        public int Lives
        {
            get;
            private set;
        }
        public event NoLivesHandler NoMoreLives;
        public delegate void NoLivesHandler();
        public ScoreAndLives()
        {
            this.Score = 0;
            this.Lives = 3;
        }

        public void EndGame()
        {
            OnEndGame();
        }
        protected void OnEndGame()
        {
            if(NoMoreLives != null)
            {
                NoMoreLives();
            }
        }
        public void AddPointsAndLives(ICollidable Sender, EventArgs e)
        {
            if (Sender is CaterpillarGrower || Sender is CaterpillarShrinker)
            {
                this.Score += Sender.Points;
            }
            else if (Sender is Wall)
            {
                this.Lives--;
            }
            if(this.Lives == 0)
            {
                EndGame();
            }
        }
        public void AddCollisionEvent(ICollidable Sender)
        {
            if (Sender != null)
            {
                Sender.Collision += AddPointsAndLives;
            }
        }
        public void LoseALife()
        {
            this.Lives--;
        }
        public void SelfCollisionSub(Caterpillar Caterpillar)
        {
            Caterpillar.SelfCollision += LoseALife;
        }
    }
}
