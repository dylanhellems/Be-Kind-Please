#region Using Statements
using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
#endregion

namespace BKP
{
    class LevelMenuEntry : MenuEntry
    {

        public string level;

        public LevelMenuEntry(string text, string level)
            : base(text)
        {
            this.level = level;
        }

        public override void Draw(MenuScreen screen, bool isSelected, Microsoft.Xna.Framework.GameTime gameTime)
        {
 	        base.Draw(screen, isSelected, gameTime);

            if (isSelected)
            {

                ScreenManager screenManager = screen.ScreenManager;
                SpriteBatch spriteBatch = screenManager.SpriteBatch;
                SpriteFont font = screenManager.Font;
                Vector2 pos = Position + new Vector2(250, -20);

                if (ScreenManager.times.ContainsKey(level))
                {
                    spriteBatch.DrawString(font, string.Format("{0:mm\\:ss\\.ff}", ScreenManager.times[level]), pos, Color.White);
                }
                else
                {
                    spriteBatch.DrawString(font, "--:--.--", pos, Color.White);
                }
            }
        }

    }
}
