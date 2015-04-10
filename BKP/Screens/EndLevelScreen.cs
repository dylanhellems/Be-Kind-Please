#region File Description
//-----------------------------------------------------------------------------
// PauseMenuScreen.cs
//
// Microsoft XNA Community Game Platform
// Copyright (C) Microsoft Corporation. All rights reserved.
//-----------------------------------------------------------------------------
#endregion

#region Using Statements
using System;
using Microsoft.Xna.Framework;
#endregion

namespace BKP
{
    /// <summary>
    /// The pause menu comes up over the top of the game,
    /// giving the player options to resume or quit.
    /// </summary>
    class EndLevelScreen : MenuScreen
    {
        #region Initialization

        public string level;
        public TimeSpan time;

        /// <summary>
        /// Constructor.
        /// </summary>
        public EndLevelScreen(string level, TimeSpan time)
            : base("You Win!")
        {
            this.level = level;
            this.time = time;
            if (!ScreenManager.times.ContainsKey(level)) {
                ScreenManager.times[level] = time;
            }
            if (time.TotalMilliseconds < ScreenManager.times[level].TotalMilliseconds)
            {
                ScreenManager.times[level] = time;
            }

            selectedEntry = 1;

            // Create our menu entries.
            MenuEntry restartGameMenuEntry = new MenuEntry("Restart Level");
            MenuEntry quitGameMenuEntry = new MenuEntry("Back to the Menu");
            
            // Hook up menu event handlers.
            restartGameMenuEntry.Selected += RestartLevelSelected;
            quitGameMenuEntry.Selected += QuitGameMenuEntrySelected;

            // Add entries to the menu.
            MenuEntries.Add(restartGameMenuEntry);
            MenuEntries.Add(quitGameMenuEntry);
        }


        #endregion

        #region Handle Input


        /// <summary>
        /// Event handler for when the Quit Game menu entry is selected.
        /// </summary>
        void QuitGameMenuEntrySelected(object sender, PlayerIndexEventArgs e)
        {
            LoadingScreen.Load(ScreenManager, false, null, new BackgroundScreen(),
                                                           new MainMenuScreen());
        }

        void RestartLevelSelected(object sender, PlayerIndexEventArgs e)
        {
            LoadingScreen.Load(ScreenManager, true, e.PlayerIndex,
                               new GameplayScreen(level));
        }


        #endregion
    }
}
