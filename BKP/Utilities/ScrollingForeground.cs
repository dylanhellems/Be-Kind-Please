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
    class ScrollingForeground
    {
        Vector2 screenpos, origin, texturesize;
        Texture2D tex;
        int screenHeight, screenWidth;
        int flip;
        int flipper;
        float yOff;

        public ScrollingForeground()
        {
            // Do nothing
        }

        public void Load(GraphicsDevice device, Texture2D backgroundTexture)
        {
            tex = backgroundTexture;
            screenHeight = device.Viewport.Height;
            screenWidth = device.Viewport.Width;
            origin = new Vector2(150, 0);
            screenpos = new Vector2(screenWidth / 2, screenHeight / 2);
            texturesize = new Vector2(0, tex.Height);
            flip = 0;
            flipper = new Random().Next(100);
            yOff = 0;
        }

        public void Update(GameTime gameTime, int playerX, int worldY, bool state)
        {
            float elapsed = (float)gameTime.ElapsedGameTime.TotalMilliseconds / 10;
            flip++;
            if (flip == 100)
            {
                flip = 0;
            }
            if (state)
            {
                yOff += elapsed;
                screenpos.X = playerX;
                screenpos.Y = worldY + yOff;
                if (yOff > tex.Height)
                {
                    yOff = 0;
                }
            }
        }

        public void Draw(SpriteBatch batch)
        {
            SpriteEffects se = SpriteEffects.None;
            if (flip % 7 == 0)
            {
                se = SpriteEffects.FlipHorizontally;
            }
            if (flip == flipper)
            {
                se = SpriteEffects.FlipVertically;
                flipper = new Random().Next(100);
            }
            batch.Draw(tex, screenpos + 2 * texturesize, null, Color.White, 0, origin, 1, se, 0f);
            batch.Draw(tex, screenpos + texturesize, null, Color.White, 0, origin, 1, se, 0f);
            batch.Draw(tex, screenpos, null, Color.White, 0, origin, 1, se, 0f);
        }
    }
}
