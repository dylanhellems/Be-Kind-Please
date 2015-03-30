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
	class Player : Drawable
    {
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
            this.x = x;
            this.y = y;
            this.width = width;
            this.height = height;
			grounded = false;
			pushing = false;
            paused = false;
            state = 1;


			// Movement
			speed = 7;
			friction = .15;
			x_accel = 0;
			x_vel = 0;
			y_vel = 0;
			movedX = 0;
            pastPos = new CappedStack<Vector3>(0);
        }

        public int getX(){
            return x;
        }

        public int getY()
        {
            return y;
        }

        public void setX(int x)
        {
            this.x = x;
        }

        public void setY(int y)
        {
            this.y = y;
        }

        public int getState()
        {
            return state;
        }

        override public void LoadContent(ContentManager content, string str)
        {
            texture = content.Load<Texture2D>(str);
        }

        public override void Update(GameTime gameTime)
        {
            throw new NotImplementedException();
        }

		public void Update(Controls controls, GameTime gameTime, List<Drawable> platforms)
		{
			Jump(controls, gameTime);
            Move(controls, platforms);
		}

        override public void Draw(SpriteBatch sb)
        {
            sb.Draw(texture, new Rectangle(x, y, width, height), Color.White);
        }

		public void Move(Controls controls, List<Drawable> platforms)
		{
            // Pause
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
                        x = (int)last.X;
                        y = (int)last.Y;
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
                        x = (int)last.X;
                        y = (int)last.Y;
                        y_vel = last.Z;
                        checkCollisions(platforms);
                    }
                }
                else
                {
                    pastPos.Push(new Vector3(x, y, (float)y_vel));

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
                    x += movedX;

                    // Gravity
                    if (!grounded)
                    {
                        if (state == 2)
                        {
                            y_vel += gravity * 2;
                        }
                        else {
                            y_vel += gravity;
                        }
                        if (y_vel > maxFallSpeed)
                            y_vel = maxFallSpeed;
                        y += Convert.ToInt32(y_vel);
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

		private void checkCollisions(List<Drawable> platforms)
		{
            int lastY = (int)pastPos.Peek().Y;
            int lastX = (int)pastPos.Peek().X;
            if (y >= 700)
            {
                paused = true;
            }
            foreach (Drawable platform in platforms)
            {
                if (this.isTouching(platform))
                {
                    Vector2 up = new Vector2(0, platform.getCenterY());
                    up.Normalize();
                    Vector2 toPlayer = new Vector2(platform.getCenterX() - this.getCenterX(), platform.getCenterY() - this.getCenterY());
                    toPlayer.Normalize();
                    double angleToPlayer = Vector2.Dot(up, toPlayer) / (up.Length() * toPlayer.Length());

                    if (angleToPlayer < 0.6 && angleToPlayer > -0.6)
                    {
                        if (this.getCenterX() < platform.getCenterX())
                        {
                            // Collision on right side of player
                            x = lastX;
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
                            y += 1;
                            y_vel = 0;
                        }
                        moveToStopOverlap(platform);
                    }
                }
            }
		}

        private void moveToStopOverlap(Drawable platform)
        {
            if (this.y < platform.y)
            {
                this.y = platform.y - (this.height - 1);
            }
            else
            {
                this.y = platform.y + (platform.height - 1);
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
    }
}
