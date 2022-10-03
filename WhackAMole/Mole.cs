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
        int m_index = 0;
        Rectangle m_bounds;
        private Texture2D m_hole;
        private Texture2D m_foreGround;
        private Texture2D m_kod;
        float m_speed = 20.0f;
        float m_movedUnits = 0.0f;
        const double M_DELAY = 1.0;
        double m_remainingDelay = M_DELAY;
        double m_kodDelay = M_DELAY;
        Vector2 m_molePos;
        private float m_groundPlane;
        bool m_moveUp = true;
        Random m_rand = new Random();
        bool m_hit = false;
        bool m_retreating = false;
        bool m_canClick = true;
       public Rectangle GetBounds() { return m_bounds; }
        public bool GetHit() { return m_hit;}
        public void Update(double dt, MouseState ms, ref int scoreValue)
        {
            if (ms.LeftButton == ButtonState.Released)
            {
                m_canClick = true;
            }
            if (ms.LeftButton == ButtonState.Pressed && m_canClick)
            {
                
                if (m_bounds.Contains(ms.X, ms.Y)
                    && !m_hit
                    && ms.Y < m_groundPlane)
                {
                    
                    SetHit();
                    scoreValue++;
                }

                m_canClick = false;

            }


            m_bounds.Y = (int)m_molePos.Y;
            if (m_hit)
            {
                
                m_kodDelay -= dt;
                if (m_kodDelay <= 0 &&!m_retreating)
                {

                    m_kodDelay = M_DELAY;
                    m_retreating = true;
                    m_speed = m_rand.Next(5, 40);
                }
                if(m_retreating)
                {
                    
                    if(m_molePos.Y < m_pos.Y)
                    {
                        m_molePos.Y += (float)dt * m_speed;
                        
                    }
                    else
                    {
                        m_hit = false;
                        m_retreating = false;
                        m_movedUnits = 0.0f;
                        m_moveUp = true;
                    }
                    
                }
            }
            else
            {
                m_movedUnits += (float)dt * m_speed;
                if (m_movedUnits < m_texture2D.Height * 0.75)
                {
                    if (m_moveUp)
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

                    m_remainingDelay -= dt;
                    if (m_remainingDelay <= 0)
                    {
                        m_movedUnits = 0.0f;
                        m_remainingDelay = M_DELAY;
                        m_moveUp = !m_moveUp;
                        m_speed = m_rand.Next(5, 40);
                    }
                }
            }


            


        }
        public Mole(Texture2D moleT, Texture2D holeT, Texture2D foreGroundT,Texture2D kod, Vector2 pos, int index)
        {
            m_texture2D = moleT;
            m_hole = holeT;
            m_foreGround = foreGroundT;
            m_pos = pos;
            m_molePos = pos;
            m_kod = kod;
            m_speed = m_rand.Next(5,40);
            m_groundPlane = pos.Y;
            m_bounds.X = (int)pos.X;
            m_bounds.Y = (int)pos.Y;
            m_bounds.Width = moleT.Width;
            m_bounds.Height = moleT.Height;
            m_index = index;
        }
        public Texture2D GetTex() { return m_texture2D; }
        public Vector2 GetPos() { return m_pos; }
        public void SetHit() { m_hit = true; }
        public float GetGroundPlane() { return m_groundPlane; }
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
