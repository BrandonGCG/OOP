using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace CptStache
{
    class MenuSprite
    {
        //Variables
        private Texture2D m_txr;
        private Texture2D m_txr_H;
        private Vector2 m_pos;
        public bool mouseover;
        private Rectangle m_rect;

        //Constructor
        public MenuSprite(Texture2D txr, Texture2D txr_H, int xpos, int ypos)
        {
            m_txr = txr;
            m_txr_H = txr_H;

            m_pos = new Vector2(xpos, ypos);

            m_rect = new Rectangle(xpos, ypos, txr.Width, txr.Height);

            mouseover = false;
        }

        //Update
        public void UpdateMe()
        {
            var mouseState = Mouse.GetState();
            var mousePoint = new Point(mouseState.X, mouseState.Y);
            
            if (m_rect.Contains(mousePoint))
            {
                mouseover = true;
            }
            else
            {
                mouseover = false;
            }

        }

        //Draw
        public void DrawMe(SpriteBatch sb)
        {
            if (mouseover == false)
            {
                sb.Draw(m_txr, m_pos, Color.White);
            }
            else
            {
                sb.Draw(m_txr_H, m_pos, Color.White);
            }
        }
    }
}
