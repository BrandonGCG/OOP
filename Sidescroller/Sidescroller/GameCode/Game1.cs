using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System;
using System.Collections.Generic;

namespace Sidescroller
{

    public class Game1 : Game
    {
        GraphicsDeviceManager graphics;
        SpriteBatch spriteBatch;

        //Static Graphics
        StaticGraphic background;

        //Player Classes
        int score;
        const int ROCKETLAUNCH = -1;
        const int DRONEDESTROYED = 10;
        const int DRONEESCAPED = -5;
        Crosshair p1crosshair;
        PlayerShip p1Ship;
        int playerLives;
        float crosshairAngle;
        List<PlayerRocket> rockets;
        float rocketCooldown;

        //Environment and Enemies
        List<SpaceDust> dustlist;
        const int DUSTPARTICLES = 32;
        float droneSpawnRate = 3f;
        float droneTriggerCountdown;
        int dronesPerSpawn;
        List<Drone> dronelist;
        List<Explosion> explosions;

        //Utility
        MouseState mouse_curr, mouse_old;
        KeyboardState kb_curr;
        SpriteFont debugfont;
        public static readonly Random RNG = new Random();
        Rectangle screenBounds;

        public Game1()
        {
            graphics = new GraphicsDeviceManager(this);
            Content.RootDirectory = "Content";

            graphics.PreferredBackBufferWidth = 800;
            graphics.PreferredBackBufferHeight = 480;

            IsMouseVisible = true;

            graphics.SynchronizeWithVerticalRetrace = false;
            IsFixedTimeStep = false;
        }

        protected override void Initialize()
        {
            // TODO: Add your initialization logic here

            //Player classes
            rockets = new List<PlayerRocket>();
            playerLives = 3;
            score = 0;

            //Environment and Enemies
            dustlist = new List<SpaceDust>();
            droneTriggerCountdown = droneSpawnRate;
            dronelist = new List<Drone>();
            explosions = new List<Explosion>();

            //Utility
            screenBounds = GraphicsDevice.Viewport.Bounds;

            base.Initialize();
        }

        protected override void LoadContent()
        {
            // Create a new SpriteBatch, which can be used to draw textures.
            spriteBatch = new SpriteBatch(GraphicsDevice);

            // TODO: use this.Content to load your game content here

            //Static Graphics
            background = new StaticGraphic(new Rectangle(0, 0, graphics.PreferredBackBufferWidth, graphics.PreferredBackBufferHeight), Content.Load<Texture2D>("Textures\\starfield"));

            //Player Classes
            p1crosshair = new Crosshair(new Rectangle(0, 0, 7, 21), Content.Load<Texture2D>("Textures\\crosshair2"), Color.Green, Color.Red);
            p1Ship = new PlayerShip(new Rectangle(50, 100, 30, 12), Content.Load<Texture2D>("Textures\\rtype ship9"));

            //Environment and Enemies
            for (int i = 0; i < DUSTPARTICLES; i++)
            {
                dustlist.Add(new SpaceDust(new Rectangle(0, 0, 1, 1), Content.Load<Texture2D>("Textures\\pixel"), graphics.PreferredBackBufferWidth, graphics.PreferredBackBufferHeight));
            }

            //Utility
            debugfont = Content.Load<SpriteFont>("debug");
        }

        protected override void UnloadContent()
        {
            // TODO: Unload any non ContentManager content here
        }

        protected override void Update(GameTime gameTime)
        {
            if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed || Keyboard.GetState().IsKeyDown(Keys.Escape))
                Exit();

            // TODO: Add your update logic here

            //Utility
            mouse_curr = Mouse.GetState();
            kb_curr = Keyboard.GetState();
            if (playerLives == 0)
            {
                Exit();
            }

            //Player Classes
            p1crosshair.UpdateMe(mouse_curr, graphics.PreferredBackBufferWidth, graphics.PreferredBackBufferHeight, gameTime);
            p1Ship.updateme(kb_curr, gameTime, graphics.PreferredBackBufferWidth, graphics.PreferredBackBufferHeight);
            for (int i = 0; i < dronelist.Count; i ++)
            {
                if (p1Ship.Rect.Intersects(dronelist[i].Rect))
                {
                    playerLives--;
                    dronelist.RemoveAt(i);
                }
            }
            #region rocket handler
            crosshairAngle = (float)Math.Atan2(mouse_curr.Y - p1Ship.Rect.Y, mouse_curr.X - p1Ship.Rect.X);

            for (int i = 0; i < rockets.Count; i++)
            {
                if (screenBounds.Intersects(rockets[i].Rect))
                {
                    rockets[i].updateme(gameTime);
                    for (int j = 0; j < dronelist.Count; j++)
                    {
                        if (rockets[i].Rect.Intersects(dronelist[j].Rect))
                        {
                            explosions.Add(new Explosion(Content.Load<Texture2D>("Textures\\explosion"), new Rectangle(dronelist[j].Rect.X, dronelist[j].Rect.Y, 25, 25), 5, gameTime));
                            score += DRONEDESTROYED;
                            rockets.RemoveAt(i);
                            dronelist.RemoveAt(j);
                            break;
                        }
                    }
                }
                else
                {
                    rockets.RemoveAt(i);
                }
            }
            if ((mouse_curr.LeftButton == ButtonState.Pressed) && (mouse_old.LeftButton == ButtonState.Released) && (mouse_curr.X - p1Ship.Rect.X > 0) && (crosshairAngle >= -0.78f) && (crosshairAngle <= 0.78f)&& (rocketCooldown <= 0f))
            {
                score += ROCKETLAUNCH;
                Vector2 direction = new Vector2(mouse_curr.X - p1Ship.Center.X, mouse_curr.Y - p1Ship.Center.Y);
                direction = Vector2.Normalize(direction) * 300;
                rockets.Add(new PlayerRocket(new Rectangle(p1Ship.Center.X, p1Ship.Center.Y, 14, 6), Content.Load<Texture2D>("Textures\\rtype rocket"), direction * (float)gameTime.ElapsedGameTime.TotalSeconds));
                rocketCooldown = 0.5f;
            }
            if ((mouse_curr.RightButton == ButtonState.Pressed) && (mouse_old.RightButton == ButtonState.Released) && (mouse_curr.X - p1Ship.Rect.X > 0) && (rocketCooldown <= 0f))
            {
                score += ROCKETLAUNCH;
                rockets.Add(new PlayerRocket(new Rectangle(p1Ship.Center.X, p1Ship.Center.Y, 14, 6), Content.Load<Texture2D>("Textures\\rtype rocket"), new Vector2(200, 0) * (float)gameTime.ElapsedGameTime.TotalSeconds));
                rocketCooldown = 0.5f;
            }
            if (rocketCooldown >= 0f)
            {
                rocketCooldown -= (float)gameTime.ElapsedGameTime.TotalSeconds;
            }
            #endregion

            //Environment and Enemies
            for (int i = 0; i < DUSTPARTICLES; i++)
            {
                dustlist[i].updateme(gameTime);
            }

            #region drone handler
            for (int i = 0; i < dronelist.Count; i++)
            {
                if (dronelist[i].State == DroneState.dead)
                {
                    dronelist.RemoveAt(i);
                    score += DRONEESCAPED;
                }
                else
                {
                    dronelist[i].updateme(gameTime);
                }
            }

            if (droneTriggerCountdown > 0)
            {
                droneTriggerCountdown -= (float)gameTime.ElapsedGameTime.TotalSeconds;
            }
            else
            {
                dronesPerSpawn = RNG.Next(6);
                for (int i = 0; i < dronesPerSpawn; i++)
                {
                    dronelist.Add(new Drone(Content.Load<Texture2D>("Textures\\rtype evilrocket"), new Rectangle(0, 0, 27, 13), 5, gameTime, graphics.PreferredBackBufferHeight, graphics.PreferredBackBufferWidth));
                }
                droneSpawnRate -= 0.1f;
                droneTriggerCountdown = droneSpawnRate;
            }
            #endregion

            for (int i = 0; i < explosions.Count; i++)
            {
                if (explosions[i].State == DroneState.dead)
                {
                    explosions.RemoveAt(i);
                }
                else
                {
                    explosions[i].updateme(gameTime);
                }
            }
            base.Update(gameTime);
            mouse_old = mouse_curr;
        }

        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.CornflowerBlue);

            // TODO: Add your drawing code here

            float fps = 1 / (float)gameTime.ElapsedGameTime.TotalSeconds;

            spriteBatch.Begin();

            //StaticGraphics
            background.drawme(spriteBatch);

            //Player Classes
            #region
            p1Ship.drawme(spriteBatch);
            p1crosshair.drawme(spriteBatch, mouse_curr);
            for (int i = 0; i < rockets.Count; i++)
            {
                rockets[i].drawme(spriteBatch);
            }
            #endregion

            //Environment and Enemies
            #region
            for (int i = 0; i < DUSTPARTICLES; i++)
            {
                dustlist[i].drawme(spriteBatch);
            }
            for (int i = 0; i < dronelist.Count; i++)
            {
                dronelist[i].drawme(spriteBatch);
            }
            for (int i = 0; i < explosions.Count; i++)
            {
                explosions[i].drawme(spriteBatch);
            }
            #endregion

            //Debug
            if (kb_curr.IsKeyDown(Keys.L))
            {
                spriteBatch.DrawString(debugfont, "fps: " + fps
                    + "\nResolution: " + graphics.PreferredBackBufferWidth + "x " + graphics.PreferredBackBufferHeight +
                    "\nMouse Position X: " + mouse_curr.X + "Y: " + mouse_curr.Y
                    + "\np1Ship Position X: " + p1Ship.Rect.X + "Y: " + p1Ship.Rect.Y
                    + "\np1Ship Velocity: " + p1Ship.Velocity
                    + "\nDust Particles: " + dustlist.Count
                    + "\nDrones: " + dronelist.Count
                    + "\nDrone Spawn Rate: " + droneSpawnRate
                    + "\nDrone Spawn In: " + droneTriggerCountdown
                    + "\nRockets: " + rockets.Count
                    + "\nRocket Cooldown" + rocketCooldown
                    + "\nCrosshair Angle: " + crosshairAngle + " radians"
                    + "\nLives: " + playerLives
                    , Vector2.Zero, Color.White);
            }

            spriteBatch.End();

            base.Draw(gameTime);
        }
    }
}
