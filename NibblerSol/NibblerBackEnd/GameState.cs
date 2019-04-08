using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Collections;

namespace NibblerBackEnd
{
    public class GameState
    {
        public Grid Grid
        {
            get;
            private set;
        }
        public Caterpillar Caterpillar
        {
            get;
            private set;
        }
        public ScoreAndLives ScoreAndLives
        {
            get;
            private set;
        }
        public bool ShouldContinue
        {
            get;
            private set;
        }
        public GameState()
        {
            this.Grid = Load("sampleGrid.txt");
            int MiddleX = this.Grid.tiles.GetLength(0) / 2;
            int MiddleY = this.Grid.tiles.GetLength(1) / 2;
            this.Caterpillar = new Caterpillar(new Point(MiddleX, MiddleY), this.Grid);
            this.Grid.AquireCaterpillar(this.Caterpillar);
            RandomToken(this.Grid, this.Caterpillar);
            this.ScoreAndLives = new ScoreAndLives();
            this.Grid.AquireScoreAndLives(this.ScoreAndLives);


            Subscribing(this.Grid, this.ScoreAndLives);
        }
        public static ICollidable RandomToken(Grid Grid, Caterpillar Caterpillar)
        {
            ICollidable Result;
            Random rand = new Random();
            if(rand.NextDouble() * (double)100 < 90)
            {
                Result = new CaterpillarGrower(3, 0);
            }
            else
            {
                Result = new CaterpillarShrinker(1, 0);
            }
            bool PositionIsNotNull = true;
            do
            {
                int RandomX = (int)(rand.NextDouble() * (double)Grid.tiles.GetLength(0));
                int RandomY = (int)(rand.NextDouble() * (double)Grid.tiles.GetLength(1));
                if (!Caterpillar.Contains(new Point(RandomX, RandomY)) && (Grid.tiles[RandomX, RandomY] == null))
                {
                    Grid.tiles[RandomX, RandomY] = Result;
                }
            } while (PositionIsNotNull);
            return Result;
        }
        private static bool IsRectangular(List<String> ImputStrings)
        {
            bool IsRectangular = true;
            int Xlength = ImputStrings[3].Length;
            for(int i = 3; i < ImputStrings.Count; i++)
            {
                if(ImputStrings[i].Length != Xlength)
                {
                    IsRectangular = false;
                }
            }
            return IsRectangular;
        }
        private static bool HasWalls(ICollidable[,] ProtoGrid)
        {
            bool HasWalls = true;
            for(int i = 0; i < ProtoGrid.GetLength(0); i++)
            {
                if(ProtoGrid[i,0].GetType() != typeof(Wall) || ProtoGrid[i,ProtoGrid.GetLength(1)].GetType() != typeof(Wall))
                {
                    HasWalls = false;
                }
            }
            for (int i = 0; i < ProtoGrid.GetLength(1); i++)
            {
                if (ProtoGrid[0, i].GetType() != typeof(Wall) || ProtoGrid[ProtoGrid.GetLength(0), i].GetType() != typeof(Wall))
                {
                    HasWalls = false;
                }
            }
            return HasWalls;
        }
        private static Grid Load(String FileName)
        {
            List<String> Contents = new List<String>();
            using (StreamReader sr = File.OpenText(FileName)) 
            {
                String s = "";
                while((s = (sr.ReadLine())) != null)
                {
                    Contents.Add(s);
                }
            }
            if (!IsRectangular(Contents))
            {
                throw new ArgumentException("The contents of the file are not Rectangular");
            }
            int Xlength = Contents[3].Length;
            int Ylength = Contents.Count - 3;
            ICollidable[,] ProtoGrid = new ICollidable[Xlength, Ylength];
            for (int i = 0; i < Xlength; i++)
            {
                for(int j = 0; j < Ylength; j++)
                {
                    if(Contents[i][j+3] == 'W')
                    {
                        ProtoGrid[i, j] = new Wall();
                    }
                    else
                    {
                        ProtoGrid[i, j] = null;
                    }
                }
            }
            if (!HasWalls(ProtoGrid))
            {
                throw new ArgumentException("The contents of the file do not have as surrounding wall");
            }
            Grid grid = new Grid(ProtoGrid, RandomToken);
            
            return grid;
        }
        private void Subscribing(Grid Grid, ScoreAndLives ScoreAndLives)
        {
            for(int i = 0; i < Grid.tiles.GetLength(0); i++)
            {
                for(int j = 0; j < Grid.tiles.GetLength(1); j++)
                {
                    
                    Grid.AddCollisionEvent(Grid.tiles[i, j]);
                    ScoreAndLives.AddCollisionEvent(Grid.tiles[i, j]);


                }
            }
            AddSelfCollisionSub();
            ScoreAndLives.SelfCollisionSub(Caterpillar);
        }
        private void DeathSubscription()
        {
            ScoreAndLives.NoMoreLives += SetPause;
        }
        private void SetPause()
        {
            this.ShouldContinue = false;
        }
        private void AddSelfCollisionSub()
        {
            this.Caterpillar.SelfCollision += Reset;
        }
        private void Reset()
        {
            this.Grid = Load("sampleGrid.txt");
            int MiddleX = this.Grid.tiles.GetLength(0) / 2;
            int MiddleY = this.Grid.tiles.GetLength(1) / 2;
            this.Caterpillar = new Caterpillar(new Point(MiddleX, MiddleY), this.Grid);
            this.Grid.AquireCaterpillar(this.Caterpillar);
            RandomToken(this.Grid, this.Caterpillar);
            this.Grid.AquireScoreAndLives(this.ScoreAndLives);


            Subscribing(this.Grid, this.ScoreAndLives);
        }
        public void Update()
        {
            if (this.ShouldContinue)
            {
                this.Caterpillar.Update();
                Point Current = this.Caterpillar.GetHead();
                if (Grid.tiles[Current.X, Current.Y] != null)
                {
                    Grid.Collide(this.Caterpillar);
                }
            }
        }
    }
}
