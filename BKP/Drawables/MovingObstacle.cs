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
            this.dir = dir;
        }

        override public void Update(GameTime gameTime, int playerState)
        {
            switch (type)
            {
                case 0: // left to right
                    if (x >= initX || x <= initX - 300)
                    {
                        speed = -1 * speed;
                        x = initX;
                        x += speed / 2;
                    }
                    x += speed;
                    //switch (playerState)
                    //{
                    //    case -1:
                    //        x -= speed;
                    //        break;
                    //    case 1:
                    //        x += speed;
                    //        break;
                    //    case 2:
                    //        x += 2 * speed;
                    //        break;
                    //    default:
                    //        break;
                    //}
                    break;

                case 1: // up down
                    if (y >= initY || y <= initY - 300)
                    {
                        speed = -1 * speed;
                        y += speed/2;
                    }
                    y += speed;
                    //switch (playerState)
                    //{
                    //    case -1:
                    //        y -= speed;
                    //        break;
                    //    case 1:
                    //        y += speed;
                    //        break;
                    //    case 2:
                    //        y += 2 * speed;
                    //        break;
                    //    default:
                    //        break;
                    //}
                    break;

                default:
                    break;
            }

        }
    }
}
