using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using NibblerBackEnd;

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

        public Game1()
        {
            graphics = new GraphicsDeviceManager(this);
            Content.RootDirectory = "Content";
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

            base.Initialize();
            GameState = new GameState();
        }

        /// <summary>
        /// LoadContent will be called once per game and is the place to load
        /// all of your content.
        /// </summary>
        protected override void LoadContent()
        {
            // Create a new SpriteBatch, which can be used to draw textures.
            spriteBatch = new SpriteBatch(GraphicsDevice);

            
            //loads all but snake
            for (int i=0; i< GameState.Grid.tiles.GetLength(0);i++)
            {
                for (int j = 0; j < GameState.Grid.tiles.GetLength(1); j++)
                {
                    if (GameState.Grid.tiles[i,j] is null)
                    {
                        this.Grid[i, j] = this.Content.Load<Texture2D>("ground");
                    }
                    if (GameState.Grid.tiles[i, j] is Wall)
                    {
                        this.Grid[i, j] = this.Content.Load<Texture2D>("wall");
                    }
                    if (GameState.Grid.tiles[i, j] is CaterpillarGrower)
                    {
                        this.Grid[i, j] = this.Content.Load<Texture2D>("grower");
                    }
                    if (GameState.Grid.tiles[i, j] is CaterpillarShrinker)
                    {
                        this.Grid[i, j] = this.Content.Load<Texture2D>("shrinker");
                    }
                }
            }
            // TODO: use this.Content to load your game content here
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

            // TODO: Add your update logic here

            base.Update(gameTime);
        }

        /// <summary>
        /// This is called when the game should draw itself.
        /// </summary>
        /// <param name="gameTime">Provides a snapshot of timing values.</param>
        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.CornflowerBlue);

            // TODO: Add your drawing code here
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
