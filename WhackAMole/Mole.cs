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
        public struct HitBox
        {
            public float X;
            public float Y;
            public float W;
            public float H;
        }
        private Texture2D m_hole;
        private Texture2D m_foreGround;
        private Texture2D m_kod;
        float m_speed = 20.0f;
        float movedUnits = 0.0f;
        const double DELAY = 1.0;
        double remainingDelay = DELAY;
        double kodDelay = DELAY;
        Vector2 m_molePos;
        HitBox m_hitbox;
        bool moveUp = true;
        Random rand = new Random();
        bool m_hit = false;
        bool m_retreating = false;
        public void Update(double dt)
        {
            
            if(m_hit)
            {
                
                kodDelay -= dt;
                if (kodDelay <= 0 &&!m_retreating)
                {
                    
                    kodDelay = DELAY;
                    m_retreating = true;
                    m_speed = rand.Next(5, 40);
                }
                if(m_retreating)
                {
                    
                    if(m_molePos.Y < m_pos.Y)
                    {
                        m_molePos.Y += (float)dt * m_speed;
                        m_hitbox.Y = m_molePos.Y;
                    }
                    else
                    {
                        m_hit = false;
                        m_retreating = false;
                        movedUnits = 0.0f;
                        moveUp = true;
                    }
                    
                }
            }
            else
            {
                movedUnits += (float)dt * m_speed;
                if (movedUnits < m_texture2D.Height * 0.75)
                {
                    if (moveUp)
                    {
                        m_molePos.Y -= (float)dt * m_speed;
                        m_hitbox.Y = m_molePos.Y;
                    }
                    else
                    {
                        m_molePos.Y += (float)dt * m_speed;
                        m_hitbox.Y = m_molePos.Y;
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


            


        }
        public Mole(Texture2D moleT, Texture2D holeT, Texture2D foreGroundT,Texture2D kod, Vector2 pos)
        {
            m_texture2D = moleT;
            m_hole = holeT;
            m_foreGround = foreGroundT;
            m_pos = pos;
            m_molePos = pos;
            m_kod = kod;
            m_speed = rand.Next(5,40);
            m_hitbox.X = pos.X;
            m_hitbox.Y = pos.Y;
            m_hitbox.W = moleT.Width;
            m_hitbox.H = moleT.Height;

        }
        public Vector2 GetPos() { return m_pos; }
        public void SetHit() { m_hit = true; }
        public HitBox GetHitBox() { return m_hitbox; }
        public void Draw(SpriteBatch sb, float scale)
        {
            sb.Draw(m_hole, m_pos, null, Color.White, 0.0f, new Vector2(0, 0), scale, SpriteEffects.None, 0);
            if(m_hit)
            {
                sb.Draw(m_kod, m_molePos, null, Color.White, 0.0f, new Vector2(0, 0), scale, SpriteEffects.None, 0);
            }
            else
            {
                sb.Draw(m_texture2D, m_molePos, null, Color.White, 0.0f, new Vector2(0, 0), scale, SpriteEffects.None, 0);
            }
            
            sb.Draw(m_foreGround, m_pos, null, Color.White, 0.0f, new Vector2(0, 0), scale, SpriteEffects.None, 0);
            
            

        }
    }
}
