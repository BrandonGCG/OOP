using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Blobby
{
    class Static2D
    {
        //Variables
        protected Texture2D m_txr;
        protected Rectangle m_rect;

        public Rectangle Rect
        {
            get
            {
                return m_rect;
            }
        }

        //Constructor
        public Static2D(Texture2D txr, int x, int y)
        {
            m_txr = txr;

            m_rect = new Rectangle(x, y, txr.Width, txr.Height);
        }

        public void DrawMe(SpriteBatch sb)
        {
            sb.Draw(m_txr, m_rect, Color.White);
        }


    }
}
