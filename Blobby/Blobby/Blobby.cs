using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Blobby
{
    class Blobby : Static2D
    {
        private enum AnimState
        {
            WalkingRight, WalkingLeft
        }
        //Variables
        private AnimState m_currState;
        private Rectangle m_animcell;
        private float m_frameTimer;
        private float m_fps;
        private float m_speed;
        private Vector2 m_velocity;

        //Constructor
        public Blobby(Texture2D txr, int x, int y) : base(txr, x, y)
        {
            m_fps = 24;
            m_currState = AnimState.WalkingRight;
            m_animcell = new Rectangle(0, 0, txr.Width / 8, txr.Height);
            m_frameTimer = 1;

            m_rect = new Rectangle(x, y, m_txr.Width / 8, m_txr.Height);
            m_velocity = Vector2.Zero;
            m_speed = 1;
        }

        public void UpdateMe(GamePadState currPad, Rectangle screenSize)
        {
            //Movement
            m_velocity.X = 0;
            m_fps = 0;
            if(currPad.ThumbSticks.Right.X > 0)
            {
                m_velocity.X = m_speed;
                m_currState = AnimState.WalkingRight;
                m_fps = 24;
            }
            else if(currPad.ThumbSticks.Right.X < 0)
            {
                m_velocity.X = -m_speed;
                m_currState = AnimState.WalkingLeft;
                m_fps = 24;
            }

            m_rect.X += (int)m_velocity.X;

            //Screen Wrap
            if(m_rect.X + m_rect.Width < 0)
            {
                m_rect.X = screenSize.Width - 1;
            }
            else if(m_rect.X > screenSize.Width)
            {
                m_rect.X = 1 - m_rect.Width;
            }
        }

        public void DrawMe(SpriteBatch sb, GameTime gt)
        {
            if (m_frameTimer <= 0)
            {
                m_animcell.X = (m_animcell.X + m_animcell.Width);
                if (m_animcell.X >= m_txr.Width)
                {
                    m_animcell.X = 0;
                }

                m_frameTimer = 1;
            }
            else
            {
                m_frameTimer -= (float)gt.ElapsedGameTime.TotalSeconds * m_fps;
            }

            switch (m_currState)
            {
                case AnimState.WalkingRight:
                    sb.Draw(m_txr, new Vector2(m_rect.X, m_rect.Y), m_animcell, Color.White);
                    break;
                case AnimState.WalkingLeft:
                    sb.Draw(m_txr, new Vector2(m_rect.X, m_rect.Y), m_animcell, Color.White, 0, Vector2.Zero, 1, SpriteEffects.FlipHorizontally, 0);
                    break;
            }
        }
    }
}
