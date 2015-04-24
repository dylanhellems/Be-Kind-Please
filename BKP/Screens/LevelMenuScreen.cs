#region File Description
//-----------------------------------------------------------------------------
// LevelMenuScreen.cs
//
// Microsoft XNA Community Game Platform
// Copyright (C) Microsoft Corporation. All rights reserved.
//-----------------------------------------------------------------------------
#endregion

#region Using Statements
using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
#endregion

namespace BKP
{
    /// <summary>
    /// The main menu screen is the first thing displayed when the game starts up.
    /// </summary>
    class LevelMenuScreen : MenuScreen
    {
        #region Initialization

        public static List<string> levels = new List<string>(){
                                         "Content/levels/level1.tmx",
                                         "Content/levels/level2.tmx",
                                         "Content/levels/level3.tmx",
                                         "Content/levels/level4.tmx",
                                         "Content/levels/level6.tmx",
                                         "Content/levels/level7.tmx",
                                         "Content/levels/level8.tmx"                                         
                                     };
        public static Dictionary<string, string> levelNames = new Dictionary<string, string>();


        /// <summary>
        /// Constructor fills in the menu contents.
        /// </summary>
        public LevelMenuScreen()
            : base("")
        {
            List<bool> locks = new List<bool>();
            locks.Add(false);
            for (int i = 1; i < 7; i++)
            {
                if (ScreenManager.times.ContainsKey(levels[i - 1]))
                {
                    locks.Add(!(ScreenManager.times[levels[i - 1]].TotalMilliseconds < ScreenManager.pars[levels[i - 1]].TotalMilliseconds && ScreenManager.times[levels[i - 1]].TotalMilliseconds > 0));
                }
                else
                {
                    locks.Add(true);
                }
            }

            if (levelNames.Count == 0)
            {
                levelNames.Add("Content/levels/level1.tmx", "Prologue");
                levelNames.Add("Content/levels/level2.tmx", "Act 1");
                levelNames.Add("Content/levels/level3.tmx", "Act 2");
                levelNames.Add("Content/levels/level4.tmx", "Intermission");
                levelNames.Add("Content/levels/level6.tmx", "Act 3");
                levelNames.Add("Content/levels/level7.tmx", "Epilogue");
                levelNames.Add("Content/levels/level8.tmx", "Encore");
            }

            // Create our menu entries.
            MenuEntry level1MenuEntry = new LevelMenuEntry("Prologue", levels[0], locks[0]);
            MenuEntry level2MenuEntry = new LevelMenuEntry("Act 1", levels[1], locks[1]);
            MenuEntry level3MenuEntry = new LevelMenuEntry("Act 2", levels[2], locks[2]);
            MenuEntry level4MenuEntry = new LevelMenuEntry("Intermission", levels[3], locks[3]);
            MenuEntry level6MenuEntry = new LevelMenuEntry("Act 3", levels[4], locks[4]);
            MenuEntry level7MenuEntry = new LevelMenuEntry("Epilogue", levels[5], locks[5]);
            MenuEntry level8MenuEntry = new LevelMenuEntry("Encore", levels[6], locks[6]);
            MenuEntry back = new MenuEntry("Back");

            // Hook up menu event handlers.
            level1MenuEntry.Selected += Level1MenuEntrySelected;
            if (!locks[1])
            {
                level2MenuEntry.Selected += Level2MenuEntrySelected;
            }
            if (!locks[2])
            {
                level3MenuEntry.Selected += Level3MenuEntrySelected;
            }
            if (!locks[3])
            {
                level4MenuEntry.Selected += Level4MenuEntrySelected;
            }
            if (!locks[4])
            {
                level6MenuEntry.Selected += Level6MenuEntrySelected;
            }
            if (!locks[5])
            {
                level7MenuEntry.Selected += Level7MenuEntrySelected;
            }
            if (!locks[6])
            {
                level8MenuEntry.Selected += Level8MenuEntrySelected;
            }
            back.Selected += OnCancel;

            // Add entries to the menu.
            MenuEntries.Add(level1MenuEntry);
            MenuEntries.Add(level2MenuEntry);
            MenuEntries.Add(level3MenuEntry);
            MenuEntries.Add(level4MenuEntry);
            MenuEntries.Add(level6MenuEntry);
            MenuEntries.Add(level7MenuEntry);
            MenuEntries.Add(level8MenuEntry);
            MenuEntries.Add(back);
        }


        #endregion

        #region Handle Input


        /// <summary>
        /// Event handler for when a level menu entry is selected.
        /// </summary>
        void Level1MenuEntrySelected(object sender, PlayerIndexEventArgs e)
        {
            LoadingScreen.Load(ScreenManager, true, e.PlayerIndex,
                               new GameplayScreen("Content/levels/level1.tmx"));
            Sound.song = "Vivacity";
        }

        void Level2MenuEntrySelected(object sender, PlayerIndexEventArgs e)
        {
            LoadingScreen.Load(ScreenManager, true, e.PlayerIndex,
                               new GameplayScreen("Content/levels/level2.tmx"));
            Sound.song = "Vivacity";
        }

        void Level3MenuEntrySelected(object sender, PlayerIndexEventArgs e)
        {
            LoadingScreen.Load(ScreenManager, true, e.PlayerIndex,
                               new GameplayScreen("Content/levels/level3.tmx"));
            Sound.song = "Vivacity";
        }

        void Level4MenuEntrySelected(object sender, PlayerIndexEventArgs e)
        {
            LoadingScreen.Load(ScreenManager, true, e.PlayerIndex,
                               new GameplayScreen("Content/levels/level4.tmx"));
            Sound.song = "Daily Beetle";
        }

        void Level6MenuEntrySelected(object sender, PlayerIndexEventArgs e)
        {
            LoadingScreen.Load(ScreenManager, true, e.PlayerIndex,
                               new GameplayScreen("Content/levels/level6.tmx"));
            Sound.song = "Call to Adventure";
        }


        void Level7MenuEntrySelected(object sender, PlayerIndexEventArgs e)
        {
            LoadingScreen.Load(ScreenManager, true, e.PlayerIndex,
                               new GameplayScreen("Content/levels/level7.tmx"));
            Sound.song = "Call to Adventure";
        }

        void Level8MenuEntrySelected(object sender, PlayerIndexEventArgs e)
        {
            LoadingScreen.Load(ScreenManager, true, e.PlayerIndex,
                               new GameplayScreen("Content/levels/level8.tmx"));
            Sound.song = "Call to Adventure";
        }

        /// <summary>
        /// Event handler for when the user selects ok on the "are you sure
        /// you want to exit" message box.
        /// </summary>
        void ConfirmExitMessageBoxAccepted(object sender, PlayerIndexEventArgs e)
        {
            ScreenManager.Game.Exit();
        }


        #endregion
    }
}
