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
        double width;
        double height;
        double x;
        double y;
        Texture2D texture;
        
        public bool isTouching(Drawable) {
            return false;
        }

        public double getCenterX() {
            return 0;
        }

        public double getCenterY() {
            return 0;
        }

        abstract public void LoadContent();
        abstract public void Draw();
        abstract public void Update();
    }
}
