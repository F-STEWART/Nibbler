﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NibblerBackEnd
{
    class ScoreAndLives
    {
        public int Score
        {
            get;
            private set;
        }
        public event NoLivesHandler NoMoreLives;
        public delegate void NoLivesHandler();

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

        }
        public void SelfCollisionSub(Caterpillar Caterpillar)
        {
            Caterpillar.SelfCollision += LoseALife;
        }
    }
}
