using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NibblerBackEnd;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Media;


namespace Nibbler
{
    public class GridSprite : DrawableGameComponent
    {

        private SpriteBatch spriteBatch;
        private Game1 game;
        private Grid Grid;
        private Texture2D[,] GridImages;


        public GridSprite(Game1 game)
            : base(game)
        {
            this.game = game;
            this.GridImages = new Texture2D[Grid.tiles.GetLength(0), Grid.tiles.GetLength(1)];
        }

        public GridSprite(Game1 game, Grid grid)
            : base(game)
        {
            this.game = game;
            this.Grid = grid;
            this.GridImages = new Texture2D[Grid.tiles.GetLength(0), Grid.tiles.GetLength(1)];
        }

        public override void Initialize()
        {
            base.Initialize();
        }

        public override void Update(GameTime gameTime)
        {
            GenerateTextures();
            base.Update(gameTime);
        }

        public override void Draw(GameTime gameTime)
        {
            // Draws texture grid onto screen
            spriteBatch.Begin();
            for (int i = 0; i < Grid.tiles.GetLength(0); i++)
            {
                for (int j = 0; j < Grid.tiles.GetLength(1); j++)
                {
                    spriteBatch.Draw(GridImages[i, j], new Vector2(i * 32f, j * 32f));
                }
            }
            spriteBatch.End();
            base.Draw(gameTime);
        }

        protected override void LoadContent()
        {
            GenerateTextures();

            base.LoadContent();
        }

        public void GenerateTextures()
        {
            spriteBatch = new SpriteBatch(GraphicsDevice);

            for (int i = 0; i < Grid.tiles.GetLength(0); i++)
            {
                for (int j = 0; j < Grid.tiles.GetLength(1); j++)
                {
                    if (Grid.tiles[i, j] is null)
                    {
                        this.GridImages[i, j] = game.Content.Load<Texture2D>("ground");
                    }
                    else if (Grid.tiles[i, j] is Wall)
                    {
                        this.GridImages[i, j] = game.Content.Load<Texture2D>("wall");
                    }
                    else if (Grid.tiles[i, j] is CaterpillarGrower)
                    {
                        this.GridImages[i, j] = game.Content.Load<Texture2D>("grower");
                    }
                    else if (Grid.tiles[i, j] is CaterpillarShrinker)
                    {
                        this.GridImages[i, j] = game.Content.Load<Texture2D>("shrinker");
                    }
                }
            }

        }
    }
}
