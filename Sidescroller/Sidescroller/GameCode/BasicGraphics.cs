using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace Sidescroller
{
    class StaticGraphic
    {
        protected Rectangle m_rect;
        protected Texture2D m_txr;

        public StaticGraphic(Rectangle rectPosition, Texture2D txrImage)
        {
            m_rect = rectPosition;
            m_txr = txrImage; 
        }

        public StaticGraphic(Texture2D txrImage, int xPos, int yPos, int width, int height)
            : this(new Rectangle(xPos, yPos, width, height), txrImage)
        {
        }

        public virtual void drawme(SpriteBatch sBatch)
        {
            sBatch.Draw(m_txr, m_rect, Color.White);
        }
    }

    class MotionGraphic : StaticGraphic
    {
        protected Vector2 m_position;
        protected Vector2 m_velocity;
        public Rectangle Rect
        {
            get
            {
                return m_rect;
            }
        }

        public Vector2 Velocity
        {
            get
            {
                return m_velocity;
            }
        }

        public MotionGraphic(Rectangle rect, Texture2D txr)
            : base(rect, txr)
        {
            m_position = new Vector2(rect.X, rect.Y);
            m_velocity = Vector2.Zero;
        }

        public override void drawme(SpriteBatch sBatch)
        {
            m_rect.X = (int)m_position.X;
            m_rect.Y = (int)m_position.Y;

            sBatch.Draw(m_txr, m_rect, Color.White);
        }
    }

    class Animated2D : MotionGraphic
    {
        protected Rectangle m_srcRect;
        protected float m_updateTrigger;
        protected int m_framesPerSecond;
        public Animated2D(Texture2D spriteSheet, int fps, Rectangle rect)
            : base(rect, spriteSheet)
        {
            m_srcRect = new Rectangle(0, 0, rect.Width, rect.Height);
            m_updateTrigger = 0;
            m_framesPerSecond = fps;

            m_position = new Vector2(rect.X, rect.Y);
            m_velocity = Vector2.Zero;
        }

        public virtual void updateme(GameTime gt)
        {
            m_updateTrigger += (float)gt.ElapsedGameTime.TotalSeconds * m_framesPerSecond;

            if (m_updateTrigger >= 1)
            {
                m_updateTrigger = 0;
                m_srcRect.X += m_srcRect.Width;
                if (m_srcRect.X == m_txr.Width)
                    m_srcRect.X = 0;
            }

            m_position = m_position + m_velocity;
        }

        public override void drawme(SpriteBatch sBatch)
        {
            m_rect.X = (int)m_position.X;
            m_rect.Y = (int)m_position.Y;

            sBatch.Draw(m_txr, m_rect, m_srcRect, Color.White);
        }
    }
}
