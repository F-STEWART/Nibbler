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
    class CaterpillarSprite : DrawableGameComponent
    {
        private SpriteBatch spriteBatch;
        private Game1 game;
        private GameState GameState;
        private Texture2D[,] GridImages;


        public CaterpillarSprite(Game1 game)
            : base(game)
        {
            this.game = game;
            this.GridImages = new Texture2D[GameState.Grid.tiles.GetLength(0), GameState.Grid.tiles.GetLength(1)];
        }

        public CaterpillarSprite(Game1 game, GameState GameState)
            : base(game)
        {
            this.game = game;
            this.GameState = GameState;
            this.GridImages = new Texture2D[GameState.Grid.tiles.GetLength(0), GameState.Grid.tiles.GetLength(1)];
        }

        public override void Initialize()
        {
            base.Initialize();
        }

        public override void Update(GameTime gameTime)
        {
            ClearContent();
            GenerateTextures();
            base.Update(gameTime);
        }

        public override void Draw(GameTime gameTime)
        {
            // Draws texture grid onto screen
            spriteBatch.Begin();
            for (int i = 0; i < GridImages.GetLength(0); i++)
            {
                for (int j = 0; j < GridImages.GetLength(1); j++)
                {
                    if(!(GridImages[i, j] is null))
                        spriteBatch.Draw(GridImages[i, j], new Vector2(i * 32f, j * 32f));
                }
            }
            spriteBatch.End();
            base.Draw(gameTime);
        }

        protected override void LoadContent()
        {
            ClearContent();
            GenerateTextures();

            base.LoadContent();
        }

        private void ClearContent()
        {
            for (int i = 0; i < GridImages.GetLength(0); i++)
            {
                for (int j = 0; j < GridImages.GetLength(1); j++)
                {
                    GridImages[i, j] = null;
                }
            }
        }

        public void GenerateTextures()
        {
            spriteBatch = new SpriteBatch(GraphicsDevice);

            for (int i = 0; i < GameState.Grid.tiles.GetLength(0); i++)
            {
                for (int j = 0; j < GameState.Grid.tiles.GetLength(1); j++)
                {
                    //If the point is part of the snake
                    if (GameState.Caterpillar.Contains(new NibblerBackEnd.Point(i, j)))
                    {
                        //Initialize Head
                        if (GameState.Caterpillar.GetHead() == new NibblerBackEnd.Point(i, j))
                        {
                            switch (GameState.Caterpillar.Direction)
                            {
                                case Direction.UP:
                                    GridImages[i, j] = game.Content.Load<Texture2D>("snake_head_up");
                                    break;
                                case Direction.DOWN:
                                    GridImages[i, j] = game.Content.Load<Texture2D>("snake_head_down");
                                    break;
                                case Direction.LEFT:
                                    GridImages[i, j] = game.Content.Load<Texture2D>("snake_head_left");
                                    break;
                                case Direction.RIGHT:
                                    GridImages[i, j] = game.Content.Load<Texture2D>("snake_head_right");
                                    break;
                            }
                        }
                        //Initilize tail
                        else if (GameState.Caterpillar.GetTail() == new NibblerBackEnd.Point(i, j))
                        {
                            if (GameState.Caterpillar.GetTailLeader().X > GameState.Caterpillar.GetTail().X)
                                this.GridImages[i, j] = game.Content.Load<Texture2D>("snake_tail_right");
                            else if (GameState.Caterpillar.GetTailLeader().X < GameState.Caterpillar.GetTail().X)
                                this.GridImages[i, j] = game.Content.Load<Texture2D>("snake_tail_left");
                            else if (GameState.Caterpillar.GetTailLeader().Y < GameState.Caterpillar.GetTail().Y)
                                this.GridImages[i, j] = game.Content.Load<Texture2D>("snake_tail_up");
                            else if (GameState.Caterpillar.GetTailLeader().Y > GameState.Caterpillar.GetTail().Y)
                                this.GridImages[i, j] = game.Content.Load<Texture2D>("snake_tail_down");
                        }
                        //Initialize body
                        else
                            this.GridImages[i, j] = game.Content.Load<Texture2D>("snake_body");
                    }
                }
            }

        }
    }
}
