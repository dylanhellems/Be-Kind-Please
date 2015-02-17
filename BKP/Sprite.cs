using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Storage;
using Microsoft.Xna.Framework.Audio;
namespace BKP
{
	class Sprite
	{
		protected int spriteX, spriteY;
		protected int spriteWidth, spriteHeight;
		protected Texture2D image;

        public Sprite()
        {
            // Do nothing
        }

        public Sprite(int x, int y, int width, int height)
        {
            this.spriteX = x;
            this.spriteY = y;
            this.spriteWidth = width;
            this.spriteHeight = height;
        }

        public int getCenterX()
        {
            return spriteX + spriteWidth / 2;
        }

        public int getCenterY()
        {
            return spriteY + spriteHeight / 2;
        }

        public bool isTouching(Sprite s) {
            if (this.spriteX < (s.spriteX + s.spriteWidth) && (this.spriteX + this.spriteWidth) > s.spriteX && this.spriteY < (s.spriteY + s.spriteHeight) && (this.spriteY + this.spriteHeight) > s.spriteY) 
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        public void LoadContent(ContentManager content, String file)
        {
            image = content.Load<Texture2D>(file);
        }

        public void Draw(SpriteBatch sb)
        {
            sb.Draw(image, new Rectangle(spriteX, spriteY, spriteWidth, spriteHeight), Color.White);
        }

        public void Update(Vector2 pos)
        {
            spriteX = (int)Math.Round(pos.X) - 50;
            spriteY = (int)Math.Round(pos.Y) - 250;
        }
	}
}

