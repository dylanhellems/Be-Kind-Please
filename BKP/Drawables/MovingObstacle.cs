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
        public int dir;

        public MovingObstacle(int x, int y, int width, int height, int gid, int speed, int dir, bool isLethal, int type) 
            : base(x, y, width, height, gid, isLethal)
        {
            this.initX = x;
            this.initY = y;
            this.speed = speed;
            this.type = type;
            this.dir = dir;
        }

        override public void Update(GameTime gameTime)
        {
            switch (type)
            {
                case 0:
                    if (dir == 1)
                    {
                        x += speed;
                    }
                    if (dir == 0)
                    {
                        x -= speed;
                    }
                    if (x > initX + 300)
                    {
                        dir = 0;
                    }
                    if (x < initX - 300)
                    {
                        dir = 1;
                    }
                    break;
                default:
                    break;
            }

        }
    }
}
