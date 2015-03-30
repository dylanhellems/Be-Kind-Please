﻿using System;
using System.Diagnostics;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Storage;

namespace BKP
{
    class NonObstacle : Drawable
    {
        public NonObstacle(int x, int y, int width, int height, int gid)
        {
            this.x = x;
            this.y = y;
            this.width = width;
            this.height = height;
            this.gid = gid;
        }

        override public void LoadContent(ContentManager content, string str)
        {
            texture = content.Load<Texture2D>(str);
        }

        override public void Draw(SpriteBatch sb)
        {
            sb.Draw(texture, new Rectangle(x, y, width, height), getTile(), Color.White);
        }

        override public void Update(GameTime gameTime)
        {

        }
    }
}
