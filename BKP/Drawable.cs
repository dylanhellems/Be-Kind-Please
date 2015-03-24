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
        public double width;
        public double height;
        public double x;
        public double y;
        public Texture2D texture;
        
        public bool isTouching(Drawable drawable) {
            return false;
        }

        public double getCenterX() {
            return 0;
        }

        public double getCenterY() {
            return 0;
        }

        abstract public void LoadContent(ContentManager content, String str);
        abstract public void Draw(SpriteBatch sprite, GameTime gameTime);
        abstract public void Update(GameTime gameTime);
    }
}
