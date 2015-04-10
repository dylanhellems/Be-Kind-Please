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
                Vector2 pos = new Vector2(625, 350);
                Vector2 scale = new Vector2(2, 2);

                if (ScreenManager.times.ContainsKey(level))
                {
                    spriteBatch.DrawString(font, string.Format("{0:mm\\:ss\\.ff}", ScreenManager.times[level]), pos, Color.White, 0, new Vector2(0, 0), scale, SpriteEffects.None, 0);
                }
                else
                {
                    spriteBatch.DrawString(font, "--:--.--", pos, Color.White, 0, new Vector2(0, 0), scale, SpriteEffects.None, 0);
                }
            }
        }

    }
}
