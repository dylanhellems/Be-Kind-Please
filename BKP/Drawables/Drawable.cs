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
    abstract class Drawable
    {
        public int width;
        public int height;
        public int x;
        public int y;
        public int gid;
        public Texture2D texture;
        
        public bool isTouching(Drawable d) {
            if (this.x < (d.x + d.width) && (this.x + this.width) > d.x && this.y < (d.y + d.height) && (this.y + this.height) > d.y)
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        public Rectangle getTile()
        {
            int row = (gid - 1) / 12;
            int col = (gid - 1) % 12;
            return new Rectangle(col * 72, row * 72, 68, 68);
        }

        public int getCenterX() {
            return x + width / 2;
        }

        public int getCenterY() {
            return y + height / 2;
        }

        abstract public void LoadContent(ContentManager content, string str);
        abstract public void Draw(SpriteBatch sb);
        abstract public void Update(GameTime gameTime);
    }
}
