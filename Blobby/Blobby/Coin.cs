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
    class Coin : Static2D
    {
        private enum Menu
        {
            Play, Help, Exit
        }
        private Menu m_currState;
        private Rectangle m_animcell;
        private float m_frameTimer;
        private float m_fps;

        private Vector2 m_pos;
        private Vector2 m_velocity;
        private float m_speed;

        //Constructor
        public Coin(Texture2D txr, int x, int y) : base(txr, x, y)
        {
            m_currState = Menu.Play;
            m_animcell = new Rectangle(0, 0, txr.Width / 8, txr.Height);
            m_fps = 24;
            m_frameTimer = 1;

            m_pos = new Vector2(x, y);
            m_rect = new Rectangle(x, y, txr.Width / 8, txr.Height);
            m_velocity = new Vector2(0, 0);
            m_speed = 2f;
        }

        //Update
        public void UpdateMe(GamePadState currPad, GamePadState oldpad, Game1 game, Rectangle playPos, Rectangle helpPos, Rectangle exitPos)
        {
            m_rect.X = (int)m_pos.X;
            m_rect.Y = (int)m_pos.Y;

            if (currPad.ThumbSticks.Left.X < 0 && oldpad.ThumbSticks.Left.X == 0)
            {
                if (m_currState == Menu.Play)
                {
                    m_currState = Menu.Exit;
                }
                else if (m_currState == Menu.Help)
                {
                    m_currState = Menu.Play;
                }
                else if (m_currState == Menu.Exit)
                {
                    m_currState = Menu.Help;
                }
            }
            if (currPad.ThumbSticks.Left.X > 0 && oldpad.ThumbSticks.Left.X == 0)
            {
                if (m_currState == Menu.Exit)
                {
                    m_currState = Menu.Play;
                }
                else if (m_currState == Menu.Play)
                {
                    m_currState = Menu.Help;
                }
                else if (m_currState == Menu.Help)
                {
                    m_currState = Menu.Exit;
                }
            }

            if (currPad.Buttons.A == ButtonState.Pressed)
            {
                if (m_currState == Menu.Exit)
                {
                    game.Quit();
                }
            }

            if (m_currState == Menu.Play)
            {
                m_velocity.X = playPos.X - m_rect.X - m_rect.Width;
                m_velocity.Y = playPos.Y - m_rect.Y;
            }
            else if (m_currState == Menu.Help)
            {
                m_velocity.X = helpPos.X - m_rect.X - m_rect.Width;
                m_velocity.Y = helpPos.Y - m_rect.Y;
            }
            else if (m_currState == Menu.Exit)
            {
                m_velocity.X = exitPos.X - m_rect.X - m_rect.Width;
                m_velocity.Y = exitPos.Y - m_rect.Y;
            }
            m_velocity.Normalize();

            m_velocity *= (m_speed);

            m_pos.Y += m_velocity.Y;
            m_pos.X += m_velocity.X;
        }

        //Draw
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

            sb.Draw(m_txr, m_pos, m_animcell, Color.White);
        }
    }
}
