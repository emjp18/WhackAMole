using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
namespace WhackAMole
{
    internal class Mole : Tex
    {
        private Texture2D m_hole;
        private Texture2D m_foreGround;
        float m_speed = 20.0f;
        float movedUnits = 0.0f;
        const double DELAY = 1.0;
        double remainingDelay = DELAY;
        Vector2 m_molePos;
        bool moveUp = true;
        Random rand = new Random();
        public void Update(double dt)
        {

            
            movedUnits += (float)dt * m_speed;
            if (movedUnits< m_texture2D.Height*0.75)
            {
                if(moveUp)
                {
                    m_molePos.Y -= (float)dt * m_speed;
                }
                else
                {
                    m_molePos.Y += (float)dt * m_speed;
                }
                
            }
            else
            {
                
                remainingDelay -= dt;
                if (remainingDelay <= 0)
                {
                    movedUnits = 0.0f;
                    remainingDelay = DELAY;
                    moveUp = !moveUp;
                    m_speed = rand.Next(5, 40);
                }
            }

        }
        public Mole(Texture2D moleT, Texture2D holeT, Texture2D foreGroundT, Vector2 pos)
        {
            m_texture2D = moleT;
            m_hole = holeT;
            m_foreGround = foreGroundT;
            m_pos = pos;
            m_molePos = pos;
            m_speed = rand.Next(5,40);

        }


        public void Draw(SpriteBatch sb, float scale)
        {
            sb.Draw(m_hole, m_pos, null, Color.White, 0.0f, new Vector2(0, 0), scale, SpriteEffects.None, 0);
            sb.Draw(m_texture2D, m_molePos, null, Color.White, 0.0f, new Vector2(0, 0), scale, SpriteEffects.None, 0);
            sb.Draw(m_foreGround, m_pos, null, Color.White, 0.0f, new Vector2(0, 0), scale, SpriteEffects.None, 0);
            
            

        }
    }
}
