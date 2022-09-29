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
    internal class Animation : Tex
    {
        int rectIndex = 0;
        Rectangle[] rect;
        double timer = 0;
        double threshold = 5;
        const double SPEED = 50;
        public Animation(Texture2D tex, Vector2 pos)
        {
            m_texture2D = tex;
            m_pos = pos;
            rect = new Rectangle[16];
            rect[0].X = 0;
            rect[0].Y = 0;
            rect[0].Width = (int)(tex.Width * 0.25);
            rect[0].Height = (int)(tex.Height * 0.25);

            rect[1].X = (int)(tex.Width * 0.25);
            rect[1].Y = 0;
            rect[1].Width = (int)(tex.Width * 0.25);
            rect[1].Height = (int)(tex.Height * 0.25);

            rect[2].X = (int)(tex.Width * 0.5);
            rect[2].Y = 0;
            rect[2].Width = (int)(tex.Width * 0.25);
            rect[2].Height = (int)(tex.Height * 0.25);

            rect[3].X = (int)(tex.Width * 0.75);
            rect[3].Y = 0;
            rect[3].Width = (int)(tex.Width * 0.25);
            rect[3].Height = (int)(tex.Height * 0.25);


            rect[4].X = 0;
            rect[4].Y = (int)(tex.Height * 0.25);
            rect[4].Width = (int)(tex.Width * 0.25);
            rect[4].Height = (int)(tex.Height * 0.25);

            rect[5].X = (int)(tex.Width * 0.25);
            rect[5].Y = (int)(tex.Height * 0.25);
            rect[5].Width = (int)(tex.Width * 0.25);
            rect[5].Height = (int)(tex.Height * 0.25);

            rect[6].X = (int)(tex.Width * 0.5);
            rect[6].Y = (int)(tex.Height * 0.25);
            rect[6].Width = (int)(tex.Width * 0.25);
            rect[6].Height = (int)(tex.Height * 0.25);

            rect[7].X = (int)(tex.Width * 0.75);
            rect[7].Y = (int)(tex.Height * 0.25);
            rect[7].Width = (int)(tex.Width * 0.25);
            rect[7].Height = (int)(tex.Height * 0.25);


            rect[8].X = 0;
            rect[8].Y = (int)(tex.Height * 0.5);
            rect[8].Width = (int)(tex.Width * 0.25);
            rect[8].Height = (int)(tex.Height * 0.25);

            rect[9].X = (int)(tex.Width * 0.25);
            rect[9].Y = (int)(tex.Height * 0.5);
            rect[9].Width = (int)(tex.Width * 0.25);
            rect[9].Height = (int)(tex.Height * 0.25);

            rect[10].X = (int)(tex.Width * 0.5);
            rect[10].Y = (int)(tex.Height * 0.5);
            rect[10].Width = (int)(tex.Width * 0.25);
            rect[10].Height = (int)(tex.Height * 0.25);

            rect[11].X = (int)(tex.Width * 0.75);
            rect[11].Y = (int)(tex.Height * 0.5);
            rect[11].Width = (int)(tex.Width * 0.25);
            rect[11].Height = (int)(tex.Height * 0.25);

            rect[12].X = 0;
            rect[12].Y = (int)(tex.Height * 0.75);
            rect[12].Width = (int)(tex.Width * 0.25);
            rect[12].Height = (int)(tex.Height * 0.25);

            rect[13].X = (int)(tex.Width * 0.25);
            rect[13].Y = (int)(tex.Height * 0.75);
            rect[13].Width = (int)(tex.Width * 0.25);
            rect[13].Height = (int)(tex.Height * 0.25);

            rect[14].X = (int)(tex.Width * 0.5);
            rect[14].Y = (int)(tex.Height * 0.75);
            rect[14].Width = (int)(tex.Width * 0.25);
            rect[14].Height = (int)(tex.Height * 0.25);

            rect[15].X = (int)(tex.Width * 0.75);
            rect[15].Y = (int)(tex.Height * 0.75);
            rect[15].Width = (int)(tex.Width * 0.25);
            rect[15].Height = (int)(tex.Height * 0.25);
        }

        public void SetPos(Vector2 pos) { m_pos = pos; }
        public void Update(double dt)
        {
            if (timer > threshold)
            {
                if(rectIndex<15)
                {
                    rectIndex++;
                }
                else
                {
                    rectIndex = 0;
                }
                timer = 0;
            }
            else
            {
                timer += dt*SPEED;
            }
        }

        public void Draw(SpriteBatch sb, float scale)
        {

            sb.Draw(m_texture2D,m_pos, rect[rectIndex], Color.White);
            
        }
    }
}
