using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Media;

namespace CptStache
{
    /// <summary>
    /// This is the main type for your game.
    /// </summary>
    public class Game1 : Game
    {
        GraphicsDeviceManager graphics;
        SpriteBatch spriteBatch;

        //Variables
        Background bg;
        MenuSprite ng;
        MenuSprite ex;
        MenuSprite lg;
        MenuSprite st;

        Song bgMusic;

        public Game1()
        {
            graphics = new GraphicsDeviceManager(this);
            Content.RootDirectory = "Content";
            IsMouseVisible = true;
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

            bg = new Background(Content.Load<Texture2D>("background"));

            ng = new MenuSprite(Content.Load<Texture2D>("newgame"), Content.Load<Texture2D>("mo_newgame"), 5, 0);
            ex = new MenuSprite(Content.Load<Texture2D>("exit"), Content.Load<Texture2D>("mo_exit"), 5, 180);
            lg = new MenuSprite(Content.Load<Texture2D>("loadgame"), Content.Load<Texture2D>("mo_loadgame"), 5, 60);
            st = new MenuSprite(Content.Load<Texture2D>("settings"), Content.Load<Texture2D>("mo_settings"), 5, 120);

            bgMusic = Content.Load<Song>("dogface");
            MediaPlayer.Play(bgMusic);
            MediaPlayer.IsRepeating = true;
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

            ng.UpdateMe();
            ex.UpdateMe();
            lg.UpdateMe();
            st.UpdateMe();

            if (ex.mouseover == true && Mouse.GetState().LeftButton == ButtonState.Pressed)
            {
                Exit();
            }

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

            bg.DrawMe(spriteBatch);
            ng.DrawMe(spriteBatch);
            ex.DrawMe(spriteBatch);
            lg.DrawMe(spriteBatch);
            st.DrawMe(spriteBatch);

            spriteBatch.End();

            base.Draw(gameTime);
        }
    }
}
