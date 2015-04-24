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
using System.Diagnostics;
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
        public int levelindex;
        public TimeSpan time;

        /// <summary>
        /// Constructor.
        /// </summary>
        public EndLevelScreen(string level, TimeSpan time)
            : base(((ScreenManager.times[level].TotalMilliseconds <= 0) || (time.TotalMilliseconds < ScreenManager.times[level].TotalMilliseconds)) ? "New Best Time!" : "Try Again!")
        {
            this.level = level;
            this.levelindex = LevelMenuScreen.levels.FindIndex(a => a == level);
            this.time = time;
            if (ScreenManager.times[level].TotalMilliseconds <= 0) {
                Debug.Print(ScreenManager.times[level].TotalMilliseconds.ToString());
                ScreenManager.times[level] = time;
            }
            else if (time.TotalMilliseconds < ScreenManager.times[level].TotalMilliseconds)
            {
                ScreenManager.times[level] = time;
            }

            // Create our menu entries.
            MenuEntry restartGameMenuEntry = new MenuEntry("Restart Level");
            MenuEntry nextLevelMenuEntry = new MenuEntry("Next Level");
            MenuEntry quitGameMenuEntry = new MenuEntry("Back to the Menu");
            
            // Hook up menu event handlers.
            restartGameMenuEntry.Selected += RestartLevelSelected;
            nextLevelMenuEntry.Selected += NextLevelSelected;
            quitGameMenuEntry.Selected += QuitGameMenuEntrySelected;


            selectedEntry = 1;

            // Add entries to the menu.
            MenuEntries.Add(restartGameMenuEntry);
            if (levelindex < LevelMenuScreen.levels.Count - 1 && ScreenManager.times[LevelMenuScreen.levels[levelindex]].TotalMilliseconds < ScreenManager.pars[LevelMenuScreen.levels[levelindex]].TotalMilliseconds && ScreenManager.times[LevelMenuScreen.levels[levelindex]].TotalMilliseconds > 0)
            {
                MenuEntries.Add(nextLevelMenuEntry);
            }
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
            Sound.musicInstance.Stop();
        }

        void RestartLevelSelected(object sender, PlayerIndexEventArgs e)
        {
            LoadingScreen.Load(ScreenManager, true, e.PlayerIndex,
                               new GameplayScreen(level));
            Sound.musicInstance.Stop();
        }

        void NextLevelSelected(object sender, PlayerIndexEventArgs e)
        {
                LoadingScreen.Load(ScreenManager, true, e.PlayerIndex,
                                   new GameplayScreen(LevelMenuScreen.levels[levelindex + 1]));
                Sound.musicInstance.Stop();
                Sound.level = levelindex+2;
                Console.WriteLine(Sound.level);

        }

        #endregion
    }
}
