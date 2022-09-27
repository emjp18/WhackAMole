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
    internal class Button
    {
        private float m_posX;
        private float m_posY;
        private float m_width; 
        private float m_height;
        string m_text;
        BUTTON_TYPE m_type;
        public enum PRESSED {HIT, MISS };
        public enum BUTTON_TYPE { PLAY, OPTIONS, QUIT, MENU, MEDIUM, HIGH, BACK}
        private PRESSED m_pressed = PRESSED.MISS;
        private void SetPos(BUTTON_TYPE type)
        {
            switch (type)
            {
                case BUTTON_TYPE.PLAY:
                    {
                        m_posX *= 0.25f * 2;
                        m_posY *= 0.25f * 1;
                        break;
                    }
                case BUTTON_TYPE.OPTIONS:
                    {
                        m_posX *= 0.25f * 2;
                        m_posY *= 0.25f * 2;
                        break;
                    }
                case BUTTON_TYPE.QUIT:
                    {
                        m_posX *= 0.25f * 1;
                        m_posY *= 0.25f * 3;
                        break;
                    }
                case BUTTON_TYPE.MENU:
                    {
                        m_posX *= 0.25f * 1;
                        m_posY *= 0.25f * 4;
                        break;
                    }
                case BUTTON_TYPE.MEDIUM:
                    {
                        m_posX *= 0.25f * 1;
                        m_posY *= 0.25f * 3;
                        break;
                    }
                case BUTTON_TYPE.HIGH:
                    {
                        m_posX *= 0.25f * 1;
                        m_posY *= 0.25f * 2;
                        break;
                    }
                case BUTTON_TYPE.BACK:
                    {
                        m_posX *= 0 * 1;
                        m_posY *= 0 * 1;
                        break;
                    }
            }
        }
        public Button()
        {

        }
        public Button(float w, float h, float x, float y, string text, BUTTON_TYPE type)
        {
            m_width = w;
            m_height = h;
            m_posX = x;
            m_posY = y;
            m_text = text;
            SetPos(type);
            m_type = type;

        }
        public void Init(float w, float h, float x, float y, string text, BUTTON_TYPE type)
        {
            m_width = w;
            m_height = h;
            m_posX = x;
            m_posY = y;
            m_text = text;
            SetPos(type);
            m_type = type;
        }
        public void OnResize(int px, int py)
        {
            m_posX = px;
            m_posY = py;
            SetPos(m_type);
        }
        public string GetText() { return m_text; } 
        public PRESSED Update(MouseState ms)
        {
            if (ms.LeftButton==ButtonState.Pressed)
            {
                if(ms.X>= m_posX && ms.X<= m_posX+m_width
                    && ms.Y >= m_posY && ms.Y <= m_posY+m_height)
                {
                    return PRESSED.HIT;
                }
                else
                {
                    return PRESSED.MISS;
                }
            }
            else
            {
                return PRESSED.MISS;    
            }
        }
        public void Draw(SpriteBatch sb, SpriteFont sf)
        {
            sb.DrawString(sf, m_text, new Vector2(m_posX, m_posY), Color.Red);
            
        }
    }
}
