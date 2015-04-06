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
        private Texture2D texJump;
        private List<Texture2D> texWalks;
        private int animCount;
        private int lastFrame;
        
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
            animCount = 0;
            lastFrame = 0;
            texWalks = new List<Texture2D>();


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

        public void setState(int state)
        {
            this.state = state;
        }

        public override void LoadContent(ContentManager content, string str)
        {
            throw new NotImplementedException();
        }

        public void LoadContent(ContentManager content, List<string> walks, string jump)
        {
            foreach (string walk in walks)
            {
                texWalks.Add(content.Load<Texture2D>(walk));
            }
            texJump = content.Load<Texture2D>(jump);
        }

        public override void Update(GameTime gameTime)
        {
            throw new NotImplementedException();
        }

		public void Update(Controls controls, GameTime gameTime, List<Obstacle> platforms, bool levelComplete)
		{
            if (levelComplete)
            {
                state = 1;
                x_accel = speed;
                double playerFriction = pushing ? (friction * 3) : friction;
                x_vel = x_vel * (1 - playerFriction) + x_accel * .10;
                movedX = Convert.ToInt32(x_vel);
                x += movedX;

                // Gravity
                if (!grounded)
                {
                    if (state == 2)
                    {
                        y_vel += gravity * 1.5;
                    }
                    else
                    {
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
            else
            {
                Jump(controls, gameTime);
                Move(controls, platforms);
                lastFrame += gameTime.ElapsedGameTime.Milliseconds;
                if (lastFrame > 50 && state != 0)
                {
                    lastFrame = 0;
                    if (state == 1)
                    {
                        animCount++;
                    }
                    else if (state == -1)
                    {
                        animCount--;
                    }
                    else
                    {
                        animCount += 2;
                    }
                    if (animCount > 10)
                    {
                        animCount = 0;
                    }
                    if (animCount < 0)
                    {
                        animCount = 10;
                    }
                }
            }
		}

        override public void Draw(SpriteBatch sb)
        {
            if (!grounded)
            {
                sb.Draw(texJump, new Rectangle(x, y, width, height), Color.White);
            }
            else
            {
                sb.Draw(texWalks[animCount], new Rectangle(x, y, width, height), Color.White);
            }
        }

		public void Move(Controls controls, List<Obstacle> platforms)
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
                        //checkCollisions(platforms);
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
                            y_vel += gravity * 1.5;
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

		private void checkCollisions(List<Obstacle> platforms)
		{
            int lastY = (int)pastPos.Peek().Y;
            int lastX = (int)pastPos.Peek().X;
            if (y >= 800)
            {
                paused = true;
            }
            foreach (Obstacle platform in platforms)
            {
                if (this.isTouching(platform))
                {
                    if (platform.IsLethal())
                    {
                        // Collision with lethal obstacle
                        x = lastX;
                        paused = true;
                    }
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
                this.y = platform.y + platform.height;
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
