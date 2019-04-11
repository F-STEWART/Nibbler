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
    class ScoreAndLivesSprite : DrawableGameComponent
    {
        private SpriteBatch spriteBatch;
        private Game1 game;
        private SpriteFont font;
        private ScoreAndLives ScoreAndLives;


        public ScoreAndLivesSprite(Game1 game)
            : base(game)
        {
            this.game = game;
        }

        public ScoreAndLivesSprite(Game1 game, ScoreAndLives ScoreAndLives)
            : base(game)
        {
            this.game = game;
            this.ScoreAndLives = ScoreAndLives;
        }

        public override void Initialize()
        {
            base.Initialize();
        }

        public override void Update(GameTime gameTime)
        {
            base.Update(gameTime);
        }

        public override void Draw(GameTime gameTime)
        {
            spriteBatch.Begin();
            spriteBatch.Draw(game.Content.Load<Texture2D>("white_rectangle"), new Vector2(0, game.graphics.PreferredBackBufferHeight - 100));
            spriteBatch.DrawString(font, "Score: " + ScoreAndLives.Score, new Vector2(5, game.graphics.PreferredBackBufferHeight - 100), Color.Black);
            spriteBatch.DrawString(font, "Lives: " + ScoreAndLives.Lives, new Vector2(180, game.graphics.PreferredBackBufferHeight - 100), Color.Black);
            spriteBatch.End();
            base.Draw(gameTime);
        }

        protected override void LoadContent()
        {
            spriteBatch = new SpriteBatch(GraphicsDevice);
            font = game.Content.Load<SpriteFont>("Score");
            base.LoadContent();
        }
    }
}
