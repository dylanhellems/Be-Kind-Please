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

        public MovingObstacle(int x, int y, int width, int height, int gid, int speed, bool isLethal, int type) 
            : base(x, y, width, height, gid, isLethal)
        {
            this.initX = x;
            this.initY = y;
            this.speed = speed;
            this.type = type;
        }

        override public void Update(GameTime gameTime, int playerState)
        {
            switch (type)
            {
                case 0: // left to right
                    if (x <= initX || x >= initX + 200)
                    {
                        speed = -1 * speed;
                        x -= speed / 2;
                    }
                    x -= speed;
                    break;

                case 1: // right to left
                    if (x >= initX || x <= initX - 200)
                    {
                        speed = -1 * speed;
                        x += speed / 2;
                    }
                    x += speed;
                    break;

                case 2: // up to down
                    if (y <= initY || y >= initY + 300)
                    {
                        speed = -1 * speed;
                        y -= speed / 2;
                    }
                    y -= speed;
                    break;

                case 3: // down to up
                    if (y >= initY || y <= initY - 300)
                    {
                        speed = -1 * speed;
                        y += speed/2;
                    }
                    y += speed;
                    break;

                default:
                    break;
            }

        }
    }
}
