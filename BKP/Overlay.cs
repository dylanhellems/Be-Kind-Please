using System;
using System.Diagnostics;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Storage;

namespace BKP
{
    class Overlay : Drawable
    {
        public Overlay(int x, int y, int width, int height)
        {
            this.x = x;
            this.y = y;
            this.width = width;
            this.height = height;
        }

        override public void LoadContent(ContentManager content, String str)
        {
            texture = content.Load<Texture2D>(str);
        }

        override public void Draw(SpriteBatch sb)
        {
            sb.Draw(texture, new Rectangle(x, y, width, height), Color.White);
        }

        public override void Update(GameTime gameTime)
        {
            throw new NotImplementedException();
        }

        public void Update(Vector2 pos)
        {
            x = (int)Math.Round(pos.X) - 50;
            y = (int)Math.Round(pos.Y) - 250;
        }
    }
}
