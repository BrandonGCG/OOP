using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace Blobby
{
    public class Game1 : Game
    {
        public void Quit()
        {
            Exit();
        }
        GraphicsDeviceManager graphics;
        SpriteBatch spriteBatch;

        //Menu
        Static2D background, play, help, exit;

        //Blobby
        Blobby p1;

        //Coin
        Coin coin;

        //Utility
        GamePadState currpad, oldpad;

        public Game1()
        {
            graphics = new GraphicsDeviceManager(this);
            Content.RootDirectory = "Content";

            graphics.PreferredBackBufferWidth = 480;
            graphics.PreferredBackBufferHeight = 272;
        }

        protected override void Initialize()
        {
            // TODO: Add your initialization logic here

            base.Initialize();
        }

        protected override void LoadContent()
        {
            // Create a new SpriteBatch, which can be used to draw textures.
            spriteBatch = new SpriteBatch(GraphicsDevice);

            // TODO: use this.Content to load your game content here

            //Static2D
            background = new Static2D(Content.Load<Texture2D>("background"), 0, 0);
            play = new Static2D(Content.Load<Texture2D>("play"), 32, 100);
            help = new Static2D(Content.Load<Texture2D>("help"), 177, 150);
            exit = new Static2D(Content.Load<Texture2D>("exit"), 321, 100);

            //Blobby
            p1 = new Blobby(Content.Load<Texture2D>("blobbity_run_right"), 0, 208);

            //Coin
            coin = new Coin(Content.Load<Texture2D>("spinning_coin_gold"), 0, 0);
        }

        protected override void UnloadContent()
        {
            // TODO: Unload any non ContentManager content here
        }

        protected override void Update(GameTime gameTime)
        {
            if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed || Keyboard.GetState().IsKeyDown(Keys.Escape))
                Exit();

            currpad = GamePad.GetState(PlayerIndex.One);
            // TODO: Add your update logic here

            p1.UpdateMe(currpad, GraphicsDevice.Viewport.Bounds);
            coin.UpdateMe(currpad, oldpad, this, play.Rect, help.Rect, exit.Rect);

            oldpad = currpad;

            base.Update(gameTime);
        }

        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.CornflowerBlue);

            // TODO: Add your drawing code here

            spriteBatch.Begin();

            background.DrawMe(spriteBatch);
            play.DrawMe(spriteBatch);
            help.DrawMe(spriteBatch);
            exit.DrawMe(spriteBatch);

            p1.DrawMe(spriteBatch, gameTime);

            coin.DrawMe(spriteBatch, gameTime);

            spriteBatch.End();

            base.Draw(gameTime);
        }
    }
}
