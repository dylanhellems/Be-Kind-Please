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
    class Platform : Sprite
    {
        private bool isFloor;

        public Platform(int x, int y, int width, int height, bool floor, int gid)
        {
            this.spriteX = x;
            this.spriteY = y;
            this.spriteWidth = width;
            this.spriteHeight = height;
            this.isFloor = floor;
            this.gid = gid - 1;
        }

        public bool IsFloor() {
            return isFloor;
        }

        public int getX()
        {
            return spriteX;
        }

        public int getY()
        {
            return spriteY;
        }

        public void setX(int x)
        {
            spriteX = x;
        }

        public void setY(int y)
        {
            spriteY = y;
        }

        public void LoadContent(ContentManager content)
        {
            image = content.Load<Texture2D>("tiles_spritesheet");
        }

        new public void Draw(SpriteBatch sb)
        {
            sb.Draw(image, new Rectangle(spriteX, spriteY, spriteWidth, spriteHeight), getTile(), Color.White);
        }
    }
}
