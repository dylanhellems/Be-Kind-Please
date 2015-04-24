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
        public SpriteFont font;

        public Player player;

        public Overlay pause, rewind, ff;
        public ScrollingForeground vhsEffect;

        public List<Obstacle> platforms;
        public List<NonObstacle> backNobstacles, foreNobstacles;

        public string level;
        //public ScrollingBackground background;
        public TmxMap map;
        public int endX, floory;

        public string time;
        public TimeSpan sinceInit;
        public GameTime timer;

        float pauseAlpha;

        public Sound sound;

        #endregion

        #region Initialization


        /// <summary>
        /// Constructor.
        /// </summary>
        public GameplayScreen(string level = "Content/levels/test_extended.tmx")
        {
            TransitionOnTime = TimeSpan.FromSeconds(1.5);
            TransitionOffTime = TimeSpan.FromSeconds(0.5);

            this.level = level;

            Initialize();
        }

        public void Initialize()
        {
            TransitionPosition = 1.0F;
            map = new TmxMap(level);
            endX = (int.Parse(map.Properties["endx"]) + 1) * 68;
            floory = (int.Parse(map.Properties["floory"]));
            int playerX = 150;
            int playerY;
            if (map.Properties.ContainsKey("playery"))
            {
                playerY = 650 - ((floory - (int.Parse(map.Properties["playery"]))) * 68);
            }
            else
            {
                playerY = 600;
            }
            player = new Player(playerX, playerY, 50, 50);
            platforms = new List<Obstacle>();
            for (int i = 0; i < map.Layers["platforms"].Tiles.Count; i++)
            {
                TmxLayerTile tile = map.Layers["platforms"].Tiles[i];
                int x = tile.X * 68;
                int y = 650 - ((floory - tile.Y) * 68);
                int gid = tile.Gid;
                if (gid > 0)
                {
                    platforms.Add(new Obstacle(x, y, 68, 68, gid, false));
                }
            }
            backNobstacles = new List<NonObstacle>();
            for (int i = 0; i < map.Layers["background"].Tiles.Count; i++)
            {
                TmxLayerTile tile = map.Layers["background"].Tiles[i];
                int x = tile.X * 68;
                int y = 650 - ((floory - tile.Y) * 68);
                int gid = tile.Gid;
                if (gid > 0)
                {
                    backNobstacles.Add(new NonObstacle(x, y, 68, 68, gid));
                }
            }

            if (map.Layers.Contains("leftright"))
            {
                for (int i = 0; i < map.Layers["leftright"].Tiles.Count; i++)
                {
                    TmxLayerTile tile = map.Layers["leftright"].Tiles[i];
                    int x = tile.X * 68;
                    int y = 650 - ((floory - tile.Y) * 68);
                    int gid = tile.Gid;
                    if (gid > 0)
                    {
                        platforms.Add(new MovingObstacle(x, y, 68, 68, gid, 4, true, 0));
                    }
                }
            }

            if (map.Layers.Contains("rightleft"))
            {
                for (int i = 0; i < map.Layers["rightleft"].Tiles.Count; i++)
                {
                    TmxLayerTile tile = map.Layers["rightleft"].Tiles[i];
                    int x = tile.X * 68;
                    int y = 650 - ((floory - tile.Y) * 68);
                    int gid = tile.Gid;
                    if (gid > 0)
                    {
                        platforms.Add(new MovingObstacle(x, y, 68, 68, gid, 4, true, 1));
                    }
                }
            }

            if (map.Layers.Contains("updown"))
            {
                for (int i = 0; i < map.Layers["updown"].Tiles.Count; i++)
                {
                    TmxLayerTile tile = map.Layers["updown"].Tiles[i];
                    int x = tile.X * 68;
                    int y = 650 - ((floory - tile.Y) * 68);
                    int gid = tile.Gid;
                    if (gid > 0)
                    {
                        platforms.Add(new MovingObstacle(x, y, 68, 68, gid, 4, true, 2));
                    }
                }
            }

            if (map.Layers.Contains("downup"))
            {
                for (int i = 0; i < map.Layers["downup"].Tiles.Count; i++)
                {
                    TmxLayerTile tile = map.Layers["downup"].Tiles[i];
                    int x = tile.X * 68;
                    int y = 650 - ((floory - tile.Y) * 68);
                    int gid = tile.Gid;
                    if (gid > 0)
                    {
                        platforms.Add(new MovingObstacle(x, y, 68, 68, gid, 4, true, 3));
                    }
                }
            }

            foreNobstacles = new List<NonObstacle>();
            for (int i = 0; i < map.Layers["foreground"].Tiles.Count; i++)
            {
                TmxLayerTile tile = map.Layers["foreground"].Tiles[i];
                int x = tile.X * 68;
                int y = 650 - ((floory - tile.Y) * 68);
                int gid = tile.Gid;
                if (gid > 0)
                {
                    foreNobstacles.Add(new NonObstacle(x, y, 68, 68, gid));
                }
            }

            pause = new Overlay(50, 400, 100, 100);
            rewind = new Overlay(50, 400, 100, 100);
            ff = new Overlay(50, 400, 100, 100);
            vhsEffect = new ScrollingForeground();

            Joystick.Init();
            Console.WriteLine("Number of joysticks: " + Sdl.SDL_NumJoysticks());
            controls = new Controls();

            timer = new GameTime();
            time = "";
            sinceInit = new TimeSpan(0);
            sound = new Sound();
        }


        /// <summary>
        /// Load graphics content for the game.
        /// </summary>
        public override void LoadContent()
        {
            if (content == null)
                content = new ContentManager(ScreenManager.Game.Services, "Content");

            gameFont = content.Load<SpriteFont>("gamefont");
            List<string> walks = new List<string>();
            for (int i = 1; i < 12; i++)
            {
                if (i < 10)
                {
                    walks.Add("Player/p3_walk0" + i);
                }
                else
                {
                    walks.Add("Player/p3_walk" + i);
                }
            }
            player.LoadContent(content, walks, "Player/p3_jump.png");
            foreach (Drawable platform in platforms)
            {
                platform.LoadContent(content, "tiles_spritesheet_extended");
            }
            foreach (Drawable nobstacle in backNobstacles)
            {
                nobstacle.LoadContent(content, "tiles_spritesheet_extended");
            }
            foreach (Drawable nobstacle in foreNobstacles)
            {
                nobstacle.LoadContent(content, "tiles_spritesheet_extended");
            }
            pause.LoadContent(content, "pause");
            rewind.LoadContent(content, "rewind");
            ff.LoadContent(content, "fastforward");
            vhsEffect.Load(ScreenManager.GraphicsDevice, content.Load<Texture2D>("vhs_effect_2"));
            sound.LoadContent(content);
            //background = new ScrollingBackground();
            //background.Load(ScreenManager.GraphicsDevice, content.Load<Texture2D>("gamebackground"));

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

            if (!(player.getCenterX() > endX))
            {
                vp = ScreenManager.GraphicsDevice.Viewport;
                screenCenter = new Vector2(vp.Width / 2, vp.Height / 2);
                cameraWorldPosition = new Vector2(player.getX() + 300, Math.Min(player.getY() + 50, 600));
            }

            // Gradually fade in or out depending on whether we are covered by the pause screen.
            if (coveredByOtherScreen)
                pauseAlpha = Math.Min(pauseAlpha + 1f / 32, 1);
            else
                pauseAlpha = Math.Max(pauseAlpha - 1f / 32, 0);

            if (IsActive)
            {
                //set our keyboardstate tracker update can change the gamestate on every cycle
                controls.Update();
                sound.Update(content, controls, player.isGrounded());
                sound.Update(controls, player.isGrounded(), player.getState());

                if (controls.onPress(Keys.Back, Buttons.Back))
                {
                    Initialize();
                    LoadContent();
                    sinceInit = new TimeSpan(0);
                }

                //if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed || Keyboard.GetState().IsKeyDown(Keys.Escape))
                //    Exit();

                // TODO: Add your update logic here
                //Up, down, left, right affect the coordinates of the sprite
                if (!(player.getCenterX() > endX))
                {
                    player.Update(controls, gameTime, platforms, false);
                }
                else
                {
                    if (player.getCenterY() >= 850)
                    {
                        ScreenManager.AddScreen(new EndLevelScreen(level, sinceInit), ControllingPlayer);
                    }
                    player.Update(controls, gameTime, platforms, true);
                }
                pause.Update(cameraWorldPosition, 300, -250);
                rewind.Update(cameraWorldPosition, 300, -250);
                ff.Update(cameraWorldPosition, 300, -250);
                vhsEffect.Update(gameTime, player.x, (int)cameraWorldPosition.Y, !(player.getState() == 0));

                //base.Update(gameTime);
                foreach (Obstacle platform in platforms)
                {
                    platform.Update(gameTime, player.getState());
                }

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

            // This first translates the camera target back to the origin (0,0).
            // In SpriteBatch the origin normally appears in the top left of the screen
            // and generally you want to center it. So this then translates from the
            // origin to the center
            Vector2 translation = -cameraWorldPosition + screenCenter;
            Matrix cameraMatrix = Matrix.CreateTranslation(translation.X, translation.Y, 0) * Matrix.CreateScale(1.0f, 1.0f, 1f);

            spriteBatch.Begin(0, null, null, null, null, null, cameraMatrix);
            //background.Draw(spriteBatch);
            foreach (Obstacle platform in platforms)
            {
                platform.Draw(spriteBatch);
            }
            foreach (NonObstacle nobstacle in backNobstacles)
            {
                nobstacle.Draw(spriteBatch);
            }

            player.Draw(spriteBatch);

            foreach (NonObstacle nobstacle in foreNobstacles)
            {
                nobstacle.Draw(spriteBatch);
            }

            if (!(player.getCenterX() > endX))
            {
                switch (player.getState())
                {
                    case -1:
                        vhsEffect.Draw(spriteBatch);
                        rewind.Draw(spriteBatch);
                        break;
                    case 0:
                        vhsEffect.Draw(spriteBatch);
                        pause.Draw(spriteBatch);
                        break;
                    case 2:
                        vhsEffect.Draw(spriteBatch);
                        ff.Draw(spriteBatch);
                        break;
                    default:
                        break;
                }
            }

            if (!(player.getCenterX() > endX))
            {
                sinceInit += gameTime.ElapsedGameTime;
                time = string.Format("{0:mm\\:ss\\.ff}", sinceInit);
            }

            if (sinceInit.TotalMilliseconds < 3000)
            {
                float alpha = 1.0f - (float)(sinceInit.TotalMilliseconds / 3000.0);
                spriteBatch.DrawString(gameFont, LevelMenuScreen.levelNames[level], cameraWorldPosition - new Vector2(70, 70), Color.White * alpha);
            }

            Color afterPar = (sinceInit.TotalMilliseconds < ScreenManager.pars[level].TotalMilliseconds) ? (sinceInit.TotalMilliseconds < (ScreenManager.pars[level].TotalMilliseconds * 3/4)) ? Color.White : Color.Orange : Color.Red;

            spriteBatch.DrawString(gameFont, time, cameraWorldPosition - (new Vector2(70, vp.Height / 2)), afterPar);
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
