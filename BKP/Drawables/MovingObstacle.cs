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
        public int type;
        public int initX, initY;

        public MovingObstacle(int x, int y, int width, int height, int gid, int speed, bool isLethal, int type) 
            : base(x, y, width, height, gid, isLethal)
        {
            this.initX = x;
            this.initY = y;
            this.speed = speed;
            this.type = type;
        }

        override public void Update(GameTime gameTime)
        {
            switch (type)
            {
                case 0:
                    break;
                default:
                    break;
            }

        }
    }
}
