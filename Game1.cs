using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace dOOPle_Jump
{
    public class Game1 : Game
    {
        private GraphicsDeviceManager _graphics;
        private SpriteBatch _spriteBatch;
        private GameManager mrBob;

        public Game1()
        {
            _graphics = new GraphicsDeviceManager(this);
            mrBob = new GameManager();
            Content.RootDirectory = "Content";
            IsMouseVisible = true;
        }

        protected override void Initialize()
        {
            // TODO: Add your initialization logic here
            mrBob.Init(Content);
            _graphics.PreferredBackBufferWidth = (int)Constants.SCREEN_SIZE.X;
            _graphics.PreferredBackBufferHeight = (int)Constants.SCREEN_SIZE.Y;
            _graphics.ApplyChanges();
            base.Initialize();
        }

        protected override void LoadContent()
        {
            _spriteBatch = new SpriteBatch(GraphicsDevice);

            // TODO: use this.Content to load your game content here
        }

        protected override void Update(GameTime gameTime)
        {
            if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed || Keyboard.GetState().IsKeyDown(Keys.Escape))
                Exit();

            mrBob.deltaT = (float)gameTime.ElapsedGameTime.TotalSeconds;
            mrBob.Update();


            base.Update(gameTime);
        }

        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.CornflowerBlue);

            _spriteBatch.Begin();
            mrBob.Render(_spriteBatch);
            _spriteBatch.End();

            base.Draw(gameTime);
        }
    }
}
