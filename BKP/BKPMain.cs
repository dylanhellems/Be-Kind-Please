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
    /// This is the main type for your game
    /// </summary>
    public class BKPMain : Game
    {
        Viewport vp;
        Vector2 cameraWorldPosition;
        Vector2 screenCenter;
        GraphicsDeviceManager graphics;
        SpriteBatch spriteBatch;
        Player player1;
        List<Platform> platforms;
        Sprite pause, rewind, ff;
        Controls controls;
        ScrollingBackground background;
        TmxMap map;

        public BKPMain()
        {
            graphics = new GraphicsDeviceManager(this);
            Content.RootDirectory = "Content";
        }

        /// <summary>
        /// Allows the game to perform any initialization it needs to before starting to run.
        /// This is where it can query for any required services and load any non-graphic
        /// related content.  Calling base.Initialize will enumerate through any components
        /// and initialize them as well.
        /// </summary>
        protected override void Initialize()
        {
            // TODO: Add your initialization logic here

            player1 = new Player(50, 550, 50, 50);
            map = new TmxMap("Content/levels/test.tmx");
            platforms = new List<Platform>();
            Debug.Print("" + map.Layers["platforms"].Tiles.Count);
            for (int i = 0; i < map.Layers["platforms"].Tiles.Count; i++)
            {
                TmxLayerTile tile = map.Layers["platforms"].Tiles[i];
                int x = tile.X*70;
                int y = 250 + ((tile.Y - 1))*70;
                int gid = tile.Gid;
                if (gid > 0)
                {
                    Debug.Print("" + tile.X + " - " + tile.Y);
                    platforms.Add(new Platform(x, y, 70, 70, false));
                }
            }
            for (int i = 0; i < map.Layers["floor"].Tiles.Count; i++)
            {
                TmxLayerTile tile = map.Layers["floor"].Tiles[i];
                int x = tile.X*70;
                int y = 650;
                int gid = tile.Gid;
                if (gid > 0)
                {
                    Debug.Print("" + tile.X + " - " + tile.Y);
                    platforms.Add(new Platform(x, y, 70, 70, true));
                }
            }
            pause = new Sprite(50, 400, 100, 100);
            rewind = new Sprite(50, 400, 100, 100);
            ff = new Sprite(50, 400, 100, 100);
            base.Initialize();

            Joystick.Init();
            Console.WriteLine("Number of joysticks: " + Sdl.SDL_NumJoysticks());
            controls = new Controls();

        }

        /// <summary>
        /// LoadContent will be called once per game and is the place to load
        /// all of your content.
        /// </summary>
        protected override void LoadContent()
        {
            // Create a new SpriteBatch, which can be used to draw textures.
            spriteBatch = new SpriteBatch(GraphicsDevice);
            player1.LoadContent(this.Content);
            foreach (Platform platform in platforms) {
                platform.LoadContent(this.Content);
            }
            pause.LoadContent(this.Content, "pause");
            rewind.LoadContent(this.Content, "rewind");
            ff.LoadContent(this.Content, "fastforward");
            background = new ScrollingBackground();
            background.Load(GraphicsDevice, Content.Load<Texture2D>("gamebackground"));
            // TODO: use this.Content to load your game content here
        }

        /// <summary>
        /// UnloadContent will be called once per game and is the place to unload
        /// all content.
        /// </summary>
        protected override void UnloadContent()
        {
            // TODO: Unload any non ContentManager content here
        }

        /// <summary>
        /// Allows the game to run logic such as updating the world,
        /// checking for collisions, gathering input, and playing audio.
        /// </summary>
        /// <param name="gameTime">Provides a snapshot of timing values.</param>
        protected override void Update(GameTime gameTime)
        {
            //set our keyboardstate tracker update can change the gamestate on every cycle
            controls.Update();

            if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed || Keyboard.GetState().IsKeyDown(Keys.Escape))
                Exit();

            // TODO: Add your update logic here
            //Up, down, left, right affect the coordinates of the sprite

            player1.Update(controls, gameTime, platforms);
            background.Update(controls, player1.getX());
            pause.Update(cameraWorldPosition);
            rewind.Update(cameraWorldPosition);
            ff.Update(cameraWorldPosition);

            base.Update(gameTime);
        }

        /// <summary>
        /// This is called when the game should draw itself.
        /// </summary>
        /// <param name="gameTime">Provides a snapshot of timing values.</param>
        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(new Color(120, 174, 212));

            vp = GraphicsDevice.Viewport;
            screenCenter = new Vector2(vp.Width / 2, vp.Height / 2);
            cameraWorldPosition = new Vector2(player1.getX() + 300, Math.Min(player1.getY() + 50, 600));
            
            // This first translates the camera target back to the origin (0,0).
            // In SpriteBatch the origin normally appears in the top left of the screen
            // and generally you want to center it. So this then translates from the
            // origin to the center
            Vector2 translation = -cameraWorldPosition + screenCenter;
            Matrix cameraMatrix = Matrix.CreateTranslation(translation.X, translation.Y, 0) * Matrix.CreateScale(1.5f, 1.5f, 1f);

            spriteBatch.Begin(0, null, null, null, null, null, cameraMatrix);
            background.Draw(spriteBatch);
            player1.Draw(spriteBatch);
            foreach (Platform platform in platforms)
            {
                platform.Draw(spriteBatch);
            }
            switch (player1.getState())
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
            spriteBatch.End();

            base.Draw(gameTime);
        }
    }

}

