using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Collections;

namespace NibblerBackEnd
{
    class GameState
    {
        public Grid grid
        {
            get;
            private set;
        }
        public Caterpillar caterpillar
        {
            get;
            private set;
        }
        public ScoreAndLives scoreAndLives
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
            Grid grid = new Grid(ProtoGrid);
            return grid;
        }
    }
}
