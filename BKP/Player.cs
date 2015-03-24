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
	class Player : Sprite
    //class Player : Drawable
    {
        //private bool moving;
        //private bool walledL;
        //private bool walledR;
        private int state;
        private bool paused;
        private bool grounded;
		private int speed;
		private int x_accel;
		private double friction;
		public double x_vel;
		public double y_vel;
		public int movedX;
		private bool pushing;
		public double gravity = .3;
		public int maxFallSpeed = 20;
		private int jumpPoint = 0;
        private CappedStack<Vector3> pastPos;
        
        public Player(int x, int y, int width, int height)
        {
            this.spriteX = x;
            this.spriteY = y;
            this.spriteWidth = width;
            this.spriteHeight = height;
			grounded = false;
			pushing = false;
            paused = false;
            state = 1;


			// Movement
			speed = 5;
			friction = .15;
			x_accel = 0;
			x_vel = 0;
			y_vel = 0;
			movedX = 0;
            pastPos = new CappedStack<Vector3>(0);
        }

        public int getX(){
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

        public int getState()
        {
            return state;
        }

        public void LoadContent(ContentManager content)
        {
            image = content.Load<Texture2D>("prep2.png");
        }

		public void Update(Controls controls, GameTime gameTime, List<Platform> platforms)
		{
			Jump(controls, gameTime);
            Move(controls, platforms);
		}

		public void Move(Controls controls, List<Platform> platforms)
		{
            // Stop
            if (controls.isPressed(Keys.Down, Buttons.B))
            {
                state = 0;
                // Do nothing
            }
            else if (paused)
            {
                state = 0;
                // Rewind
                if (controls.isPressed(Keys.Left, Buttons.LeftTrigger))
                {
                    state = -1;
                    if (pastPos.Count > 0)
                    {
                        Vector3 last = pastPos.Pop();
                        spriteX = (int)last.X;
                        spriteY = (int)last.Y;
                        y_vel = (int)last.Z;
                        paused = false;
                    }
                }
            }
            else
            {
                // Rewind
                if (controls.isPressed(Keys.Left, Buttons.LeftTrigger))
                {
                    state = -1;
                    if (pastPos.Count > 0)
                    {
                        Vector3 last = pastPos.Pop();
                        spriteX = (int)last.X;
                        spriteY = (int)last.Y;
                        y_vel = last.Z;
                        checkCollisions(platforms);
                    }
                }
                else
                {
                    pastPos.Push(new Vector3(spriteX, spriteY, (float)y_vel));

                    if (controls.isPressed(Keys.Right, Buttons.RightTrigger))
                    {
                        state = 2;
                        x_accel = speed * 2;
                    }
                    else
                    {
                        state = 1;
                        x_accel = speed;
                    }
                    double playerFriction = pushing ? (friction * 3) : friction;
                    x_vel = x_vel * (1 - playerFriction) + x_accel * .10;
                    movedX = Convert.ToInt32(x_vel);
                    spriteX += movedX;

                    // Gravity
                    if (!grounded)
                    {
                        y_vel += gravity;
                        if (y_vel > maxFallSpeed)
                            y_vel = maxFallSpeed;
                        spriteY += Convert.ToInt32(y_vel);
                    }
                    else
                    {
                        y_vel = 1;
                    }

                    grounded = false;

                    

                    // Check up/down collisions, then left/right
                    checkCollisions(platforms);
                }
            }
            

		}

		private void checkCollisions(List<Platform> platforms)
		{
            int lastY = (int)pastPos.Peek().Y;
            int lastX = (int)pastPos.Peek().X;
            if (spriteY >= 700)
            {
                paused = true;
            }
            foreach (Platform platform in platforms)
            {
                if (this.isTouching(platform))
                {
                    Vector2 up = new Vector2(0, platform.getY());
                    up.Normalize();
                    Vector2 toPlayer = new Vector2(platform.getCenterX() - this.getCenterX(), platform.getCenterY() - this.getCenterY());
                    toPlayer.Normalize();
                    double angleToPlayer = Vector2.Dot(up, toPlayer) / (up.Length() * toPlayer.Length());
                    if (platform.IsFloor())
                    {
                        angleToPlayer = 1;
                    }

                    //double horizontal = Math.Sqrt(Math.Abs((this.getCenterX() * this.getCenterX()) + (platform.getCenterX() * platform.getCenterX())));
                    //double vertical = Math.Sqrt(Math.Abs((this.getCenterY() * this.getCenterY()) + (platform.getCenterY() * platform.getCenterY())));
                    //Debugger.Log(0, "", angleToPlayer + "\n");

                    if (angleToPlayer < 0.6 && angleToPlayer > -0.6)
                    {
                        if (this.getCenterX() < platform.getCenterX())
                        {
                            // Collision on right side of player
                            spriteX = lastX;
                            paused = true;
                        }
                    }
                    else if (angleToPlayer > 0.6 || angleToPlayer < -0.6)
                    {
                        if (this.getCenterY() < platform.getCenterY())
                        {
                            // Collision on bottom side of player
                            grounded = true;
                        }
                        else
                        {
                            // Collision on top side of player
                            spriteY += 1;
                            y_vel = 0;
                        }
                    }
                }
            }
		}

		private void Jump(Controls controls, GameTime gameTime)
		{
			// Jump on button press
			if (controls.onPress(Keys.Space, Buttons.A) && grounded)
			{
                y_vel = -10;
                jumpPoint = (int)(gameTime.TotalGameTime.TotalMilliseconds);
                grounded = false;
			}

			// Cut jump short on button release
			else if (controls.onRelease(Keys.Space, Buttons.A) && y_vel < 0)
			{
				y_vel /= 2;
			}
		} 


/*        public double speed;
        public double grounded;
        public bool paused;
        public CappedStack<Vector3> pastPos;
        public int state;

        public bool play()
        {
            return false;
        }

        public bool pause()
        {
            return false;
        }

        public bool fastForward()
        {
            return false;
        }

        public bool rewind()
        {
            return false;
        }

        public void jump()
        {

        }

        public void checkCollisions()
        {

        }

        public int getState()
        {
            return state;
        }

        public void LoadContent(ContentManager content)
        {

        }

        public void Draw(SpriteBatch sprite, GameTime gameTime)
        {

        }

        public void Update(GameTime gameTime)
        {

        } */
    }
}
