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
    class MovingObstacle : Obstacle
    {
        public int speed;

        public MovingObstacle(int x, int y, int width, int height, int gid, int speed) 
            : base(x, y, width, height, gid)
        {
            this.speed = speed;
        }

        override public void LoadContent(ContentManager content, string str)
        {

        }

        override public void Draw(SpriteBatch sb)
        {

        }

        override public void Update(GameTime gameTime)
        {

        }
    }
}
