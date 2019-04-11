using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Collections;
using System.Threading;

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
        //If this is false the game ends, as a key statment in update turns false
        public bool ShouldContinue
        {
            get;
            private set;
        }
        //GameState constructor, this is what controls everything in the back end
        public GameState()
        {
            //This calls load and the string is the name of the file in the repo that contains the construction of the grid
            this.Grid = Load("sampleGrid.txt");
            //Making the points for the caterpillar start location
            int MiddleX = this.Grid.tiles.GetLength(0) / 2;
            int MiddleY = this.Grid.tiles.GetLength(1) / 2;
            //makes the caterpillar in the middle of the grid
            this.Caterpillar = new Caterpillar(new Point(MiddleX, MiddleY), this.Grid);
            //the grid needs a reference to the caterpillar for subscription purposes, so that is done here. 
            this.Grid.AquireCaterpillar(this.Caterpillar);
            //Calls to make the first token
            RandomToken(this.Grid, this.Caterpillar);
            //constructs score and lives
            this.ScoreAndLives = new ScoreAndLives();
            //Again this is for subscription purposes
            this.Grid.AquireScoreAndLives(this.ScoreAndLives);
            //when this is false the game ends
            this.ShouldContinue = true;

            //Initial subscriptions
            Subscribing(this.Grid, this.ScoreAndLives);
        }
        //The reason this method is here is because it is called in Game state once, after that it is called as a delegate in grid.
        public static ICollidable RandomToken(Grid Grid, Caterpillar Caterpillar)
        {
            ICollidable Result;
            Random rand = new Random();
            //there is an 80% chance that a token is a grower, and a 20% chance it is a shinker
            if(rand.NextDouble() * ((double)100) < 80)
            {
                Result = new CaterpillarGrower(3, 0);
            }
            else
            {
                Result = new CaterpillarShrinker(1, 0);
            }
            //This is for determining a random empty position, if after the do while loop iterates it is not false
            //That means that the position was null and a token is placed, otherwise it will look again.
            bool PositionIsNotNull = true;
            do
            {
                int RandomX = (int)(rand.NextDouble() * (double)Grid.tiles.GetLength(0));
                int RandomY = (int)(rand.NextDouble() * (double)Grid.tiles.GetLength(1));
                if (!Caterpillar.Contains(new Point(RandomX, RandomY)) && (Grid.tiles[RandomX, RandomY] == null))
                {
                    Grid.tiles[RandomX, RandomY] = Result;
                    PositionIsNotNull = false;
                }
            } while (PositionIsNotNull);
            //in some instances it is necessary to have subscriptions with the token hence a return, otherwise the method is self sufficient.
            return Result;
        }
        //This tests to see if the file source is rectangular before turning it into the tile array
        private static bool IsRectangular(List<String> ImputStrings)
        {
            bool IsRectangular = true;
            //the [3] is because the grid itself starts at the 4th line
            int Xlength = ImputStrings[3].Length;
            for(int i = 3; i < ImputStrings.Count; i++)
            {
                //if this returns true the method will return false which will intentionally crash the program
                if(ImputStrings[i].Length != Xlength)
                {
                    IsRectangular = false;
                }
            }
            return IsRectangular;
        }
        //This checks the tile array to see if there are walls on all edges
        private static bool HasWalls(ICollidable[,] ProtoGrid)
        {
            //If this is set to false, the method will return false and the program will intentionally crash.
            bool HasWalls = true;
            for(int i = 0; i < ProtoGrid.GetLength(0); i++)
            {
                //This complicated if statement checks if the top and bottom walls, are walls, if not it will turn Has Walls false
                if((ProtoGrid[i, 0] == null || ProtoGrid[i,0].GetType() != typeof(Wall)) || (ProtoGrid[i, ProtoGrid.GetLength(1) - 1] == null || ProtoGrid[i,ProtoGrid.GetLength(1)-1].GetType() != typeof(Wall)))
                {
                    HasWalls = false;
                }
            }
            for (int i = 0; i < ProtoGrid.GetLength(1); i++)
            {
                //This complicated if statement checks the left and right walls are walls, if not it will turn Has Walls false
                if ((ProtoGrid[0, i] == null || ProtoGrid[0, i].GetType() != typeof(Wall)) || (ProtoGrid[ProtoGrid.GetLength(0) - 1, i] == null || ProtoGrid[ProtoGrid.GetLength(0)-1, i].GetType() != typeof(Wall)))
                {
                    HasWalls = false;
                }
            }
            //If this is true the programs runs normally, but false will crash the program as the level is not properly set up.
            return HasWalls;
        }
        //This essentially loads the grid to it's initial state from a text file.
        private static Grid Load(String FileName)
        {
            //This is the contents of the file as a list of strings
            List<String> Contents = new List<String>();
            using (StreamReader sr = File.OpenText(FileName)) 
            {
                String s = "";
                //this is just the initialization from the file.
                while((s = (sr.ReadLine())) != null)
                {
                    Contents.Add(s);
                }
            }
            //This tests if the file is rectangular and throws an exception if not.
            if (!IsRectangular(Contents))
            {
                throw new ArgumentException("The contents of the file are not Rectangular");
            }
            //Here we calculate the length and width of the grid the reason for the [3] is that the actual grid starts at the fourth line in the file
            int Xlength = Contents[3].Length;
            int Ylength = Contents.Count - 3;
            ICollidable[,] ProtoGrid = new ICollidable[Xlength, Ylength];
            //This initialises the walls from the file into the tile array
            for (int i = 0; i < Xlength; i++)
            {
                for(int j = 0; j < Ylength; j++)
                {
                    //This may seem like an error being [j+3][i] instead of [i+3][j], but this is correct contents is an array of strings which are arrays of
                    //chars, and the important thing is that the effective x, y coordinates are effectively reversed here
                    if(Contents[j+3][i] == 'W')
                    {
                        ProtoGrid[i, j] = new Wall();
                    }
                    else
                    {
                        ProtoGrid[i, j] = null;
                    }
                }
            }
            //This checks if there are walls surrounding the edges, if not it throws an exception.
            if (!HasWalls(ProtoGrid))
            {
                throw new ArgumentException("The contents of the file do not have as surrounding wall");
            }
            Grid grid = new Grid(ProtoGrid, RandomToken);
            
            return grid;
        }
        //This does all the initial subscriptions
        private void Subscribing(Grid Grid, ScoreAndLives ScoreAndLives)
        {
            for(int i = 0; i < Grid.tiles.GetLength(0); i++)
            {
                for(int j = 0; j < Grid.tiles.GetLength(1); j++)
                {
                    //If there is something to subscribe here, it will be subscribed
                    //both in grid and score and lives
                    Grid.AddCollisionEvent(Grid.tiles[i, j]);
                    ScoreAndLives.AddCollisionEvent(Grid.tiles[i, j]);


                }
            }
            //Caterpillars self collision subs
            AddSelfCollisionSub();
            ScoreAndLives.SelfCollisionSub(Caterpillar);
            //This subscribes the game lose scenario.
            DeathSubscription();
        }
        //These methods subscribe and get executed when all lives are lost, this pauses the game.
        private void DeathSubscription()
        {
            ScoreAndLives.NoMoreLives += SetPause;
        }
        private void SetPause()
        {
            this.ShouldContinue = false;
        }
        //This will do the subscription which causes the catterpillar to reset.
        private void AddSelfCollisionSub()
        {
            this.Caterpillar.SelfCollision += this.Caterpillar.Die;
        }
        //This resets the board on death
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
        // The update method is called constantly, and checks for collisions.
        //If the user has lost all their lives, Should continue will be false and no code will be called.
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
