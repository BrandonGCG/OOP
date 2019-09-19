using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sidescroller
{
    class SpaceDust : MotionGraphic
    {
        Color m_tint;
        public SpaceDust(Rectangle rect, Texture2D txr, int screenWidth, int screenHeight) : base(rect, txr)
        {
            m_position.X = Game1.RNG.Next(screenWidth);
            m_position.Y = Game1.RNG.Next(screenHeight);

            float v = (float)Game1.RNG.NextDouble();
            m_velocity.X = -(v + 1) * 100;

            m_tint = new Color(v, v, 1.0f);
        }

        //Update Method
        public void updateme(GameTime gt)
        {
            m_position += m_velocity * (float)gt.ElapsedGameTime.TotalSeconds;

            if (m_position.X < 0)
            {
                float v = (float)Game1.RNG.NextDouble();
                m_velocity.X = -(v + 1) * 100;
                m_position.X = 800;
                m_position.Y = Game1.RNG.Next(480);
                m_tint = new Color(v, v, 1.0f);
            }
        }

        //Draw method
        public override void drawme(SpriteBatch sBatch)
        {
            m_rect.X = (int)m_position.X;
            m_rect.Y = (int)m_position.Y;
            sBatch.Draw(m_txr, m_rect, m_tint);
        }
    }

    enum DroneState
    {
        live,
        dying,
        dead
    }

    class Drone : Animated2D
    {
        private DroneState m_state;
        public DroneState State
        {
            get
            {
                return m_state;
            }
        }
        private float m_scale;
        private Color m_tint;
        
        public Drone(Texture2D txr, Rectangle rect, int fps, GameTime gt, int screenHeight, int screenWidth) : base(txr, fps, rect)
        {
            m_scale = (float)Game1.RNG.NextDouble() + 0.5f;
            m_rect.Width = (int)(27 * m_scale);
            m_rect.Height = (int)(13 * m_scale);
            m_position.X = screenWidth;
            m_position.Y = Game1.RNG.Next(0, screenHeight - rect.Height);
            m_velocity.X = -(Game1.RNG.Next(50, 200) * (float)gt.ElapsedGameTime.TotalSeconds);
            m_state = DroneState.live;
            m_tint = new Color(1.0f, 1.0f, (float)Game1.RNG.NextDouble());
        }

        public override void updateme(GameTime gt)
        {
            base.updateme(gt);
            
            if (m_rect.Right < 0)
            {
                m_state = DroneState.dead;
            }
        }

        public override void drawme(SpriteBatch sBatch)
        {
            m_rect.X = (int)m_position.X;
            m_rect.Y = (int)m_position.Y;

            sBatch.Draw(m_txr, m_rect, m_srcRect, m_tint);
        }
    }

    class Explosion : Animated2D
    {
        private DroneState m_state;
        public DroneState State
        {
            get
            {
                return m_state;
            }
        }

        public Explosion(Texture2D txr, Rectangle rect, int fps, GameTime gt) : base(txr, fps, rect)
        {
            m_state = DroneState.dying;
        }

        public override void updateme(GameTime gt)
        {
            m_updateTrigger += (float)gt.ElapsedGameTime.TotalSeconds * m_framesPerSecond;

            if (m_updateTrigger >= 1)
            {
                m_updateTrigger = 0;
                m_srcRect.X += m_srcRect.Width;
                if (m_srcRect.X == m_txr.Width - m_srcRect.Width)
                    m_state = DroneState.dead;
            }

            m_position = m_position + m_velocity;
        }
    }

    class Pickup : Animated2D
    {
        private DroneState m_state;
        public DroneState State
        {
            get
            {
                return m_state;
            }
        }
        public Pickup(Texture2D txr, Rectangle rect, int fps, GameTime gt) : base(txr, fps, rect)
        {
           
        }

        public override void updateme(GameTime gt)
        {
            base.updateme(gt);

            if (m_rect.Right < 0)
            {
                m_state = DroneState.dead;
            }
        }
    }
}
