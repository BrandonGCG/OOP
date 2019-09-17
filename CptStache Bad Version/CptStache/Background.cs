using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace CptStache
{
    class Background
    {
        //Variables
        private Texture2D m_txr;
        private Vector2 m_pos;

        //Constructor
        public Background(Texture2D txr)
        {
            m_txr = txr;
            m_pos = new Vector2(0,0);
        }

        //Update
        public void UpdateMe()
        {

        }

        //Draw
        public void DrawMe(SpriteBatch sb)
        {
            sb.Draw(m_txr, m_pos, Color.White);
        }
    }
}
