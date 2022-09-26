﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace WhackAMole
{
    internal class Sprite
    {
        protected Texture2D m_texture2D;
        protected Vector2 m_pos;

        public Sprite(Texture2D tex, Vector2 pos)
        {
            m_texture2D = tex;
            m_pos = pos;
        }

        public void Update(double dt)
        {
            return;
        }
        
        

        public void Draw(SpriteBatch sb)
        {
            sb.Draw(m_texture2D, m_pos, Color.White);
        }
    }
}
