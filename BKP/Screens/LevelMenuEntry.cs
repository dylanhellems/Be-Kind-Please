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
        public bool locked;

        public LevelMenuEntry(string text, string level, bool locked)
            : base(text)
        {
            this.level = level;
            this.locked = locked;

            if (!ScreenManager.times.ContainsKey(level))
            {
                ScreenManager.times[level] = new TimeSpan(0);
            }
        }

        public override void Draw(MenuScreen screen, bool isSelected, Microsoft.Xna.Framework.GameTime gameTime)
        {
 	        base.Draw(screen, isSelected, gameTime);

            // Draw the selected entry in yellow, otherwise white.
            Color color;
            if (locked)
            {
                color = Color.Gray;
            }
            else
            {
                color = (isSelected ? Color.Yellow : Color.White);
            }

            // Pulsate the size of the selected menu entry.
            double time = gameTime.TotalGameTime.TotalSeconds;

            float pulsate = (float)Math.Sin(time * 6) + 1;

            float scale = 1 + pulsate * 0.05f * selectionFade;

            // Modify the alpha to fade text out during transitions.
            color *= screen.TransitionAlpha;

            // Draw text, centered on the middle of each line.
            ScreenManager screenManager = screen.ScreenManager;
            SpriteBatch spriteBatch = screenManager.SpriteBatch;
            SpriteFont font = screenManager.Font;

            Vector2 origin = new Vector2(0, font.LineSpacing / 2);

            spriteBatch.DrawString(font, Text, Position, color, 0,
                                   origin, scale, SpriteEffects.None, 0);

            int currIndex = LevelMenuScreen.levels.FindIndex(a => a == level);

            if (isSelected)
            {
                Vector2 pos = new Vector2(625, 350);
                Vector2 scaler = new Vector2(1.5f, 1.5f);

                if (ScreenManager.times[level].TotalMilliseconds > 0)
                {
                    spriteBatch.DrawString(font, "Best Time:", pos - new Vector2(25, 75), Color.White, 0, new Vector2(0, 0), scaler, SpriteEffects.None, 0);
                    spriteBatch.DrawString(font, string.Format("{0:mm\\:ss\\.ff}", ScreenManager.times[level]), pos, Color.White, 0, new Vector2(0, 0), scaler, SpriteEffects.None, 0);
                }
                else if (currIndex == 0 || ScreenManager.times[LevelMenuScreen.levels[currIndex - 1]].TotalMilliseconds < ScreenManager.pars[LevelMenuScreen.levels[currIndex - 1]].TotalMilliseconds && ScreenManager.times[LevelMenuScreen.levels[currIndex - 1]].TotalMilliseconds > 0)
                {
                    spriteBatch.DrawString(font, "Best Time:", pos - new Vector2(25, 75), Color.White, 0, new Vector2(0, 0), scaler, SpriteEffects.None, 0);
                    spriteBatch.DrawString(font, "--:--.--", pos, Color.White, 0, new Vector2(0, 0), scaler, SpriteEffects.None, 0);
                }
                else
                {
                    spriteBatch.DrawString(font, "Last Par:", pos - new Vector2(0, 75), Color.White, 0, new Vector2(0, 0), scaler, SpriteEffects.None, 0);
                    spriteBatch.DrawString(font, string.Format("{0:mm\\:ss\\.ff}", ScreenManager.pars[level]), pos, Color.White, 0, new Vector2(0, 0), scaler, SpriteEffects.None, 0);
                }
            }
        }

    }
}
