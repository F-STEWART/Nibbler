using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using NibblerBackEnd;
using System;
using System.Text;
using System.Threading;

namespace Nibbler
{
    public class Game1 : Game
    {
        GraphicsDeviceManager graphics;
        SpriteBatch spriteBatch;
        Texture2D[,] Grid;
        GameState GameState;
        GridSprite GridImages;
        CaterpillarSprite CaterpillarImages;
        int UpdaterCount = 0;

        public Game1()
        {
            graphics = new GraphicsDeviceManager(this);
            Content.RootDirectory = "Content";
            this.IsFixedTimeStep = true;
            this.TargetElapsedTime = TimeSpan.FromSeconds(1d / 300d);
        }
        
        protected override void Initialize()
        {
            GameState = new GameState();
            GraphicsDevice.Clear(Color.White);
            graphics.PreferredBackBufferWidth = GameState.Grid.tiles.GetLength(0) * 32;  // set this value to the desired width of the window
            graphics.PreferredBackBufferHeight = GameState.Grid.tiles.GetLength(1) * 32;   // set this value to the desired height of the window
            graphics.ApplyChanges();
            Grid = new Texture2D[GameState.Grid.tiles.GetLength(0), GameState.Grid.tiles.GetLength(1)];
            GridImages = new GridSprite(this, GameState.Grid);
            CaterpillarImages = new CaterpillarSprite(this, GameState);
            Components.Add(GridImages);
            Components.Add(CaterpillarImages);
            base.Initialize();
        }
        
        protected override void LoadContent()
        {
            spriteBatch = new SpriteBatch(GraphicsDevice);
        }

        protected override void UnloadContent()
        {
            // TODO: Unload any non ContentManager content here
        }

        protected override void Update(GameTime gameTime)
        {
            if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed || Keyboard.GetState().IsKeyDown(Keys.Escape))
                Exit();

            KeyboardState state = Keyboard.GetState();
            
            if (state.IsKeyDown(Keys.Right))
                GameState.Caterpillar.ChangeDirection(Direction.RIGHT);
            if (state.IsKeyDown(Keys.Left))
                GameState.Caterpillar.ChangeDirection(Direction.LEFT);
            if (state.IsKeyDown(Keys.Up))
                GameState.Caterpillar.ChangeDirection(Direction.UP);
            if (state.IsKeyDown(Keys.Down))
                GameState.Caterpillar.ChangeDirection(Direction.DOWN);
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
        
        protected override void Draw(GameTime gameTime)
        {
            base.Draw(gameTime);
        }
    }
}
