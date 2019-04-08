using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using NibblerBackEnd;
using System;
using System.Text;
using System.Threading;

namespace Nibbler
{
    /// <summary>
    /// This is the main type for your game.
    /// </summary>
    public class Game1 : Game
    {
        GraphicsDeviceManager graphics;
        SpriteBatch spriteBatch;
        Texture2D[,] Grid;
        GameState GameState;
        int UpdaterCount = 0;

        public Game1()
        {
            graphics = new GraphicsDeviceManager(this);
            Content.RootDirectory = "Content";
            this.IsFixedTimeStep = true;
            this.TargetElapsedTime = TimeSpan.FromSeconds(1d / 300d);
        }

        /// <summary>
        /// Allows the game to perform any initialization it needs to before starting to run.
        /// This is where it can query for any required services and load any non-graphic
        /// related content.  Calling base.Initialize will enumerate through any components
        /// and initialize them as well.
        /// </summary>
        protected override void Initialize()
        {
            // TODO: Add your initialization logic here

            GameState = new GameState();
            Grid = new Texture2D[GameState.Grid.tiles.GetLength(0), GameState.Grid.tiles.GetLength(1)];
            base.Initialize();
        }

        /// <summary>
        /// LoadContent will be called once per game and is the place to load
        /// all of your content.
        /// </summary>
        protected override void LoadContent()
        {
            // Create a new SpriteBatch, which can be used to draw textures.
            spriteBatch = new SpriteBatch(GraphicsDevice);

            // TODO: use this.Content to load your game content here

            GenerateTextures();
        }

        public void GenerateTextures()
        {
            //Generate texture grid
            for (int i = 0; i < GameState.Grid.tiles.GetLength(0); i++)
            {
                for (int j = 0; j < GameState.Grid.tiles.GetLength(1); j++)
                {
                    if (GameState.Grid.tiles[i, j] is null)
                    {
                        this.Grid[i, j] = this.Content.Load<Texture2D>("ground");
                    }
                    else if (GameState.Grid.tiles[i, j] is Wall)
                    {
                        this.Grid[i, j] = this.Content.Load<Texture2D>("wall");
                    }
                    else if (GameState.Grid.tiles[i, j] is CaterpillarGrower)
                    {
                        this.Grid[i, j] = this.Content.Load<Texture2D>("grower");
                    }
                    else if (GameState.Grid.tiles[i, j] is CaterpillarShrinker)
                    {
                        this.Grid[i, j] = this.Content.Load<Texture2D>("shrinker");
                    }
                }
            }

            //Add snake to texture grid
            for (int i = 0; i < GameState.Grid.tiles.GetLength(0); i++)
            {
                for (int j = 0; j < GameState.Grid.tiles.GetLength(1); j++)
                {
                    if (GameState.Caterpillar.Contains(new NibblerBackEnd.Point(i, j)))
                    {
                        if (GameState.Caterpillar.GetHead() == new NibblerBackEnd.Point(i, j))
                            this.Grid[i, j] = this.Content.Load<Texture2D>("snake_head");
                       // else if (GameState.Caterpillar.GetTail() == new NibblerBackEnd.Point(i, j))
                       //     this.Grid[i, j] = this.Content.Load<Texture2D>("snake_tail");
                        else
                            this.Grid[i, j] = this.Content.Load<Texture2D>("snake_body");
                    }
                }
            }
        }

        /// <summary>
        /// UnloadContent will be called once per game and is the place to unload
        /// game-specific content.
        /// </summary>
        protected override void UnloadContent()
        {
            // TODO: Unload any non ContentManager content here
        }

        /// <summary>
        /// Allows the game to run logic such as updating the world,
        /// checking for collisions, gathering input, and playing audio.
        /// </summary>
        /// <param name="gameTime">Provides a snapshot of timing values.</param>
        protected override void Update(GameTime gameTime)
        {
            if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed || Keyboard.GetState().IsKeyDown(Keys.Escape))
                Exit();

            KeyboardState state = Keyboard.GetState();

            // TODO: Add your update logic here
            if (state.IsKeyDown(Keys.Right))
                GameState.Caterpillar.ChangeDirection(Direction.RIGHT);
            if (state.IsKeyDown(Keys.Left))
                GameState.Caterpillar.ChangeDirection(Direction.LEFT);
            if (state.IsKeyDown(Keys.Up))
                GameState.Caterpillar.ChangeDirection(Direction.UP);
            if (state.IsKeyDown(Keys.Down))
                GameState.Caterpillar.ChangeDirection(Direction.DOWN);
            GenerateTextures();
            if (UpdaterCount < 60)
            {
                UpdaterCount++;
            }
            else
            {
                GameState.Update();
                UpdaterCount = 0;
            }
            base.Update(gameTime);
        }

        /// <summary>
        /// This is called when the game should draw itself.
        /// </summary>
        /// <param name="gameTime">Provides a snapshot of timing values.</param>
        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.White);
            graphics.PreferredBackBufferWidth = GameState.Grid.tiles.GetLength(0) * 32;  // set this value to the desired width of your window
            graphics.PreferredBackBufferHeight = GameState.Grid.tiles.GetLength(1) * 32;   // set this value to the desired height of your window
            graphics.ApplyChanges();

            // Draws texture grid onto screen
            spriteBatch.Begin();
            for (int i = 0; i < GameState.Grid.tiles.GetLength(0); i++)
            {
                for (int j = 0; j < GameState.Grid.tiles.GetLength(1); j++)
                {
                    spriteBatch.Draw(Grid[i,j], new Vector2(i*32f, j*32f));
                }
            }
            spriteBatch.End();

            base.Draw(gameTime);
        }
    }
}
