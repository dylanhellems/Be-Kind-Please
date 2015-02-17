using System;
using System.Diagnostics;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Storage;
using Tao.Sdl;

namespace BKP
{
    class ScrollingBackground
    {
        Vector2 screenpos, origin, texturesize;
        Texture2D tex;
        int screenHeight, screenWidth;

        public ScrollingBackground()
        {
            // Do nothing
        }

        public void Load(GraphicsDevice device, Texture2D backgroundTexture)
        {
            tex = backgroundTexture;
            screenHeight = device.Viewport.Height;
            screenWidth = device.Viewport.Width;
            origin = new Vector2(tex.Width / 2, tex.Height / 2);
            screenpos = new Vector2(screenWidth / 2, tex.Height + 190);
            texturesize = new Vector2(tex.Width, 0);
        }

        public void Update(Controls controls, int playerX)
        {
            if ((playerX + 300) >= screenpos.X + (texturesize.X / 2))
            {
                screenpos.X += texturesize.X;
            }
            else if ((playerX + 300) <= screenpos.X - (texturesize.X / 2))
            {
                screenpos.X -= texturesize.X;
            }
        }

        public void Draw(SpriteBatch batch)
        {
            batch.Draw(tex, screenpos + texturesize, null, Color.White, 0, origin, 1, SpriteEffects.None, 0f);
            batch.Draw(tex, screenpos, null, Color.White, 0, origin, 1, SpriteEffects.None, 0f);
            batch.Draw(tex, screenpos - texturesize, null, Color.White, 0, origin, 1, SpriteEffects.None, 0f);
        }
    }
}
