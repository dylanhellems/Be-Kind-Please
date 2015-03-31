#region File Description
//-----------------------------------------------------------------------------
// GameplayScreen.cs
//
// Microsoft XNA Community Game Platform
// Copyright (C) Microsoft Corporation. All rights reserved.
//-----------------------------------------------------------------------------
#endregion

#region Using Statements
using System;
using System.Diagnostics;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Storage;
using Tao.Sdl;
using TiledSharp;

#endregion

namespace BKP
{
    /// <summary>
    /// This screen implements the actual game logic. It is just a
    /// placeholder to get the idea across: you'll probably want to
    /// put some more interesting gameplay in here!
    /// </summary>
    class GameplayScreen : GameScreen
    {
        #region Fields

        ContentManager content;
        SpriteFont gameFont;

        public Viewport vp;
        public Vector2 cameraWorldPosition, screenCenter;

        public Controls controls;
        public GraphicsDeviceManager graphics;
        public SpriteBatch spriteBatch;
        public SpriteFont font;

        public Player player;

        public Overlay pause, rewind, ff;
        public List<Drawable> platforms;
        public List<Drawable> nobstacles;

        public ScrollingBackground background;
        public TmxMap map;
        public int endX, floory;

        public string time;
        public TimeSpan sinceInit;
        public GameTime timer;

        float pauseAlpha;

        #endregion

        #region Initialization


        /// <summary>
        /// Constructor.
        /// </summary>
        public GameplayScreen()
        {
            TransitionOnTime = TimeSpan.FromSeconds(1.5);
            TransitionOffTime = TimeSpan.FromSeconds(0.5);

            player = new Player(50, 550, 50, 50);
            map = new TmxMap("Content/levels/level1.tmx");
            endX = (int.Parse(map.Properties["endx"]) + 1) * 70;
            floory = (int.Parse(map.Properties["floory"]));
            platforms = new List<Drawable>();
            for (int i = 0; i < map.Layers["platforms"].Tiles.Count; i++)
            {
                TmxLayerTile tile = map.Layers["platforms"].Tiles[i];
                int x = tile.X * 70;
                int y = 650 - ((floory - tile.Y) * 70);
                int gid = tile.Gid;
                if (gid > 0)
                {
                    platforms.Add(new Obstacle(x, y, 70, 70, gid));
                }
            }
            nobstacles = new List<Drawable>();
            for (int i = 0; i < map.Layers["background"].Tiles.Count; i++)
            {
                TmxLayerTile tile = map.Layers["background"].Tiles[i];
                int x = tile.X * 70;
                int y = 650 - ((floory - tile.Y) * 70);
                int gid = tile.Gid;
                if (gid > 0)
                {
                    nobstacles.Add(new NonObstacle(x, y, 70, 70, gid));
                }
            }

            pause = new Overlay(50, 400, 100, 100);
            rewind = new Overlay(50, 400, 100, 100);
            ff = new Overlay(50, 400, 100, 100);

            Joystick.Init();
            Console.WriteLine("Number of joysticks: " + Sdl.SDL_NumJoysticks());
            controls = new Controls();

            timer = new GameTime();
            time = "";
            sinceInit = new TimeSpan(0);
        }

        public void Initialize()
        {
            TransitionPosition = 1.0F;

            player = new Player(50, 550, 50, 50);
            map = new TmxMap("Content/levels/level1.tmx");
            endX = (int.Parse(map.Properties["endx"]) + 1) * 70;
            floory = (int.Parse(map.Properties["floory"]));
            platforms = new List<Drawable>();
            for (int i = 0; i < map.Layers["platforms"].Tiles.Count; i++)
            {
                TmxLayerTile tile = map.Layers["platforms"].Tiles[i];
                int x = tile.X * 70;
                int y = 650 - ((floory - tile.Y) * 70);
                int gid = tile.Gid;
                if (gid > 0)
                {
                    platforms.Add(new Obstacle(x, y, 70, 70, gid));
                }
            }
            nobstacles = new List<Drawable>();
            for (int i = 0; i < map.Layers["background"].Tiles.Count; i++)
            {
                TmxLayerTile tile = map.Layers["background"].Tiles[i];
                int x = tile.X * 70;
                int y = 650 - ((floory - tile.Y) * 70);
                int gid = tile.Gid;
                if (gid > 0)
                {
                    nobstacles.Add(new NonObstacle(x, y, 70, 70, gid));
                }
            }

            pause = new Overlay(50, 400, 100, 100);
            rewind = new Overlay(50, 400, 100, 100);
            ff = new Overlay(50, 400, 100, 100);

            Joystick.Init();
            Console.WriteLine("Number of joysticks: " + Sdl.SDL_NumJoysticks());
            controls = new Controls();

            timer = new GameTime();
            time = "";
            sinceInit = new TimeSpan(0);

            LoadContent();
        }


        /// <summary>
        /// Load graphics content for the game.
        /// </summary>
        public override void LoadContent()
        {
            if (content == null)
                content = new ContentManager(ScreenManager.Game.Services, "Content");

            gameFont = content.Load<SpriteFont>("gamefont");

            font = content.Load<SpriteFont>("font");
            player.LoadContent(content, "prep2.png");
            foreach (Drawable platform in platforms)
            {
                platform.LoadContent(content, "tiles_spritesheet");
            }
            foreach (Drawable nobstacle in nobstacles)
            {
                nobstacle.LoadContent(content, "tiles_spritesheet");
            }
            pause.LoadContent(content, "pause");
            rewind.LoadContent(content, "rewind");
            ff.LoadContent(content, "fastforward");
            background = new ScrollingBackground();
            background.Load(ScreenManager.GraphicsDevice, content.Load<Texture2D>("gamebackground"));

            // once the load has finished, we use ResetElapsedTime to tell the game's
            // timing mechanism that we have just finished a very long frame, and that
            // it should not try to catch up.
            ScreenManager.Game.ResetElapsedTime();
        }


        /// <summary>
        /// Unload graphics content used by the game.
        /// </summary>
        public override void UnloadContent()
        {
            content.Unload();
        }


        #endregion

        #region Update and Draw


        /// <summary>
        /// Updates the state of the game. This method checks the GameScreen.IsActive
        /// property, so the game will stop updating when the pause menu is active,
        /// or if you tab away to a different application.
        /// </summary>
        public override void Update(GameTime gameTime, bool otherScreenHasFocus,
                                                       bool coveredByOtherScreen)
        {
            base.Update(gameTime, otherScreenHasFocus, false);

            // Gradually fade in or out depending on whether we are covered by the pause screen.
            if (coveredByOtherScreen)
                pauseAlpha = Math.Min(pauseAlpha + 1f / 32, 1);
            else
                pauseAlpha = Math.Max(pauseAlpha - 1f / 32, 0);

            if (IsActive)
            {
                //set our keyboardstate tracker update can change the gamestate on every cycle
                controls.Update();

                if (controls.onPress(Keys.Back, Buttons.Back))
                {
                    Initialize();
                    sinceInit = new TimeSpan(0);
                }

                //if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed || Keyboard.GetState().IsKeyDown(Keys.Escape))
                //    Exit();

                // TODO: Add your update logic here
                //Up, down, left, right affect the coordinates of the sprite
                if (!(player.getCenterX() > endX))
                {
                    player.Update(controls, gameTime, platforms);
                    background.Update(controls, player.getX());
                }
                pause.Update(cameraWorldPosition);
                rewind.Update(cameraWorldPosition);
                ff.Update(cameraWorldPosition);

                //base.Update(gameTime);

                sinceInit += gameTime.ElapsedGameTime;
            }
        }


        /// <summary>
        /// Lets the game respond to player input. Unlike the Update method,
        /// this will only be called when the gameplay screen is active.
        /// </summary>
        public override void HandleInput(InputState input)
        {
            if (input == null)
                throw new ArgumentNullException("input");

            // Look up inputs for the active player profile.
            int playerIndex = (int)ControllingPlayer.Value;

            KeyboardState keyboardState = input.CurrentKeyboardStates[playerIndex];
            GamePadState gamePadState = input.CurrentGamePadStates[playerIndex];

            // The game pauses either if the user presses the pause button, or if
            // they unplug the active gamepad. This requires us to keep track of
            // whether a gamepad was ever plugged in, because we don't want to pause
            // on PC if they are playing with a keyboard and have no gamepad at all!
            bool gamePadDisconnected = !gamePadState.IsConnected &&
                                       input.GamePadWasConnected[playerIndex];

            if (input.IsPauseGame(ControllingPlayer) || gamePadDisconnected)
            {
                ScreenManager.AddScreen(new PauseMenuScreen(), ControllingPlayer);
            }
            else
            {
                
            }
        }


        /// <summary>
        /// Draws the gameplay screen.
        /// </summary>
        public override void Draw(GameTime gameTime)
        {
            // This game has a blue background. Why? Because!
            ScreenManager.GraphicsDevice.Clear(ClearOptions.Target,
                                               new Color(120, 174, 212), 0, 0);

            // Our player and enemy are both actually just text strings.
            SpriteBatch spriteBatch = ScreenManager.SpriteBatch;

            vp = ScreenManager.GraphicsDevice.Viewport;
            screenCenter = new Vector2(vp.Width / 2, vp.Height / 2);
            cameraWorldPosition = new Vector2(player.getX() + 300, Math.Min(player.getY() + 50, 600));

            // This first translates the camera target back to the origin (0,0).
            // In SpriteBatch the origin normally appears in the top left of the screen
            // and generally you want to center it. So this then translates from the
            // origin to the center
            Vector2 translation = -cameraWorldPosition + screenCenter;
            Matrix cameraMatrix = Matrix.CreateTranslation(translation.X, translation.Y, 0) * Matrix.CreateScale(1.0f, 1.0f, 1f);

            spriteBatch.Begin(0, null, null, null, null, null, cameraMatrix);
            background.Draw(spriteBatch);
            player.Draw(spriteBatch);
            foreach (Obstacle platform in platforms)
            {
                platform.Draw(spriteBatch);
            }
            foreach (NonObstacle nobstacle in nobstacles)
            {
                nobstacle.Draw(spriteBatch);
            }
            if (player.getCenterX() > endX)
            {
                pause.Draw(spriteBatch);
            }
            else
            {
                switch (player.getState())
                {
                    case -1:
                        rewind.Draw(spriteBatch);
                        break;
                    case 0:
                        pause.Draw(spriteBatch);
                        break;
                    case 2:
                        ff.Draw(spriteBatch);
                        break;
                    default:
                        break;
                }
            }

            if (!(player.getCenterX() > endX))
            {
                time = string.Format("{0:mm\\:ss\\.ff}", sinceInit);
            }
            else
            {
                spriteBatch.DrawString(font, "You Win!", cameraWorldPosition - (new Vector2(vp.Width / 4, vp.Height / 4)), new Color(255, 255, 255));
            }

            spriteBatch.DrawString(font, time, cameraWorldPosition - (new Vector2(vp.Width / 4, vp.Height / 2)), new Color(255, 255, 255));
            spriteBatch.End();

            // If the game is transitioning on or off, fade it out to black.
            if (TransitionPosition > 0 || pauseAlpha > 0)
            {
                float alpha = MathHelper.Lerp(1f - TransitionAlpha, 1f, pauseAlpha / 2);

                ScreenManager.FadeBackBufferToBlack(alpha);
            }
        }


        #endregion
    }
}
