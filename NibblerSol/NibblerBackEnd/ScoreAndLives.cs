using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NibblerBackEnd
{
    //This class keeps track of the total score and remaining lives of the player
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
        //This event is called when there are no more lives, it ends the game.
        public event NoLivesHandler NoMoreLives;
        public delegate void NoLivesHandler();
        //This is just the default value of score and lives.
        public ScoreAndLives()
        {
            this.Score = 0;
            this.Lives = 3;
        }
        //This method is called when there are no more lives, this causes the event NoMoreLives to be called.
        public void EndGame()
        {
            OnEndGame();
        }
        //Just the event part of EndGame
        protected void OnEndGame()
        {
            if(NoMoreLives != null)
            {
                NoMoreLives();
            }
        }
        public void AddPointsAndLives(ICollidable Sender, EventArgs e)
        {
            /*if (Sender is CaterpillarGrower || Sender is CaterpillarShrinker)
            {
                this.Score += Sender.Points;
            }
            else if (Sender is Wall)
            {
                this.Lives--;
            }*/
            //This will add points if the caterpillar collides with a token
            this.Score += Sender.Points;
            //This will cause the caterpillar to lose a life if it collides with a wall.
            this.Lives += Sender.NumLivesGained;
            //If there are no more lives, the game ends. 
            if(this.Lives <= 0)
            {
                EndGame();
            }
        }
        //This is an event subscription for colliding with ICollidables
        public void AddCollisionEvent(ICollidable Sender)
        {
            if (Sender != null)
            {
                Sender.Collision += AddPointsAndLives;
            }
        }
        // This is a methid called by the Self collision event
        protected void LoseALife()
        {
            this.Lives--;
        }
        //This is a self collision event subscription.
        public void SelfCollisionSub(Caterpillar Caterpillar)
        {
            Caterpillar.SelfCollision += LoseALife;
        }
    }
}
