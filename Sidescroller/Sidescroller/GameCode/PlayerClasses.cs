using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sidescroller
{
    class Crosshair : StaticGraphic
    {
        private Color m_tint;
        private Color m_tint2;
        private Vector2 m_center;
        private float m_rot;
        private float m_rotspeed;

        public Crosshair(Rectangle rect, Texture2D txr, Color tint, Color tint2) : base(rect, txr)
        {
            m_tint = tint;
            m_tint2 = tint2;
            m_center = new Vector2(m_rect.Width / 2, m_rect.Height / 2);
            m_rot = 0f;
            m_rotspeed = 1f;
        }

        //Update method
        public void UpdateMe(MouseState ms_curr, int maxX, int maxY, GameTime gt)
        {
            m_rect.X = ms_curr.X;
            m_rect.Y = ms_curr.Y;
            m_rect.X = MathHelper.Clamp(m_rect.X, 0, maxX);
            m_rect.Y = MathHelper.Clamp(m_rect.Y, 0, maxY);
            m_rot += m_rotspeed * (float)gt.ElapsedGameTime.TotalSeconds;
        }

        //Draw method
        public void drawme(SpriteBatch sBatch, MouseState ms_curr)
        {
            if (ms_curr.LeftButton == ButtonState.Pressed)
            {
                sBatch.Draw(m_txr, m_rect, null, m_tint2, m_rot, m_center, SpriteEffects.None, 0);
            }
            else
            {
                sBatch.Draw(m_txr, m_rect, null, m_tint, m_rot, m_center, SpriteEffects.None, 0);
            }
        }
    }

    class PlayerShip : MotionGraphic
    {
        public Point Center
        {
            get
            {
                return m_rect.Center;
            }
        }
        public PlayerShip(Rectangle rect, Texture2D txrShip) : base(rect, txrShip)
        {

        }

        //Update method
        public void updateme(KeyboardState kb, GameTime gt, int maxX, int maxY)
        {
            m_velocity = m_velocity * 0.9999f;
            if (m_velocity.X < 0.1f && m_velocity.X > 0|| m_velocity.X > -0.1f && m_velocity.X < 0)
            {
                m_velocity.X = 0;
            }
            if (m_velocity.Y < 0.1f && m_velocity.Y > 0 || m_velocity.Y > -0.1f && m_velocity.Y < 0)
            {
                m_velocity.Y = 0;
            }
            if (kb.IsKeyDown(Keys.W))
            {
                m_velocity.Y = -100;
            }
            if (kb.IsKeyDown(Keys.S))
            {
                m_velocity.Y = 100;
            }
            if (kb.IsKeyDown(Keys.A))
            {
                m_velocity.X = -100;
            }
            if (kb.IsKeyDown(Keys.D))
            {
                m_velocity.X = 100;
            }


            m_position += m_velocity * (float) gt.ElapsedGameTime.TotalSeconds;
            m_position.X = MathHelper.Clamp(m_position.X, 0, maxX - m_txr.Width);
            m_position.Y = MathHelper.Clamp(m_position.Y, 0, maxY - m_txr.Height);
        }
    }

    class PlayerRocket : MotionGraphic
    {
        private float m_rot;
        private Color m_dumbtint;
        public PlayerRocket(Rectangle rect, Texture2D txr, Vector2 vel) : base(rect, txr)
        {
            m_velocity = vel;
            m_rot = (float)Math.Atan2(vel.Y, vel.X);
            m_dumbtint = new Color(0f, 1f, 1f);
        }

        public void updateme(GameTime gt)
        {
            m_position += m_velocity;
        }

        public override void drawme(SpriteBatch sBatch)
        {
            m_rect.X = (int)m_position.X;
            m_rect.Y = (int)m_position.Y;

            if (m_rot == 0)
            {
                sBatch.Draw(m_txr, m_rect, null, m_dumbtint, m_rot, Vector2.Zero, SpriteEffects.None, 0);
            }
            else
            {
                sBatch.Draw(m_txr, m_rect, null, Color.White, m_rot, Vector2.Zero, SpriteEffects.None, 0);
            }
            
        }
    }
}
