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


        /// <summary>
        /// Constructor fills in the menu contents.
        /// </summary>
        public LevelMenuScreen()
            : base("")
        {
            // Create our menu entries.
            MenuEntry level1MenuEntry = new LevelMenuEntry("Prologue", levels[0], false);
            MenuEntry level2MenuEntry = new LevelMenuEntry("Act 1", levels[1], false);
            MenuEntry level3MenuEntry = new LevelMenuEntry("Act 2", levels[2], false);
            MenuEntry level4MenuEntry = new LevelMenuEntry("Intermission", levels[3], false);
            MenuEntry level6MenuEntry = new LevelMenuEntry("Act 3", levels[4], false);
            MenuEntry level7MenuEntry = new LevelMenuEntry("Epilogue", levels[5], false);
            MenuEntry level8MenuEntry;
            if (ScreenManager.times[levels[5]].TotalMilliseconds < 30000 && ScreenManager.times[levels[5]].TotalMilliseconds > 0)
            {
                level8MenuEntry = new LevelMenuEntry("Encore", levels[6], false);
            }
            else
            {
                level8MenuEntry = new LevelMenuEntry("Encore", levels[6], true);
            }
            MenuEntry back = new MenuEntry("Back");

            // Hook up menu event handlers.
            level1MenuEntry.Selected += Level1MenuEntrySelected;
            level2MenuEntry.Selected += Level2MenuEntrySelected;
            level3MenuEntry.Selected += Level3MenuEntrySelected;
            level4MenuEntry.Selected += Level4MenuEntrySelected;
            level6MenuEntry.Selected += Level6MenuEntrySelected;
            level7MenuEntry.Selected += Level7MenuEntrySelected;
            if (ScreenManager.times[levels[5]].TotalMilliseconds < 30000 && ScreenManager.times[levels[5]].TotalMilliseconds > 0)
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
        }

        void Level2MenuEntrySelected(object sender, PlayerIndexEventArgs e)
        {
            LoadingScreen.Load(ScreenManager, true, e.PlayerIndex,
                               new GameplayScreen("Content/levels/level2.tmx"));
        }

        void Level3MenuEntrySelected(object sender, PlayerIndexEventArgs e)
        {
            LoadingScreen.Load(ScreenManager, true, e.PlayerIndex,
                               new GameplayScreen("Content/levels/level3.tmx"));
        }

        void Level4MenuEntrySelected(object sender, PlayerIndexEventArgs e)
        {
            LoadingScreen.Load(ScreenManager, true, e.PlayerIndex,
                               new GameplayScreen("Content/levels/level4.tmx"));
        }

        void Level6MenuEntrySelected(object sender, PlayerIndexEventArgs e)
        {
            LoadingScreen.Load(ScreenManager, true, e.PlayerIndex,
                               new GameplayScreen("Content/levels/level6.tmx"));
        }

        void Level7MenuEntrySelected(object sender, PlayerIndexEventArgs e)
        {
            LoadingScreen.Load(ScreenManager, true, e.PlayerIndex,
                               new GameplayScreen("Content/levels/level7.tmx"));
        }

        void Level8MenuEntrySelected(object sender, PlayerIndexEventArgs e)
        {
            LoadingScreen.Load(ScreenManager, true, e.PlayerIndex,
                               new GameplayScreen("Content/levels/level8.tmx"));
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
