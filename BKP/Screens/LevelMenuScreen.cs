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


        /// <summary>
        /// Constructor fills in the menu contents.
        /// </summary>
        public LevelMenuScreen()
            : base("")
        {
            // Create our menu entries.
            MenuEntry level1MenuEntry = new LevelMenuEntry("Act 1", "Content/levels/level1.tmx");
            MenuEntry level2MenuEntry = new LevelMenuEntry("Act 2", "Content/levels/level2.tmx");
            MenuEntry level3MenuEntry = new LevelMenuEntry("Act 3", "Content/levels/level3.tmx");
            MenuEntry level4MenuEntry = new LevelMenuEntry("Act 4", "Content/levels/level4.tmx");
            MenuEntry level6MenuEntry = new LevelMenuEntry("Act 5", "Content/levels/level6.tmx");
            MenuEntry level7MenuEntry = new LevelMenuEntry("Act 6", "Content/levels/level7.tmx");
            MenuEntry level8MenuEntry = new LevelMenuEntry("Act 7", "Content/levels/level8.tmx");
            MenuEntry back = new MenuEntry("Back");

            // Hook up menu event handlers.
            level1MenuEntry.Selected += Level1MenuEntrySelected;
            level2MenuEntry.Selected += Level2MenuEntrySelected;
            level3MenuEntry.Selected += Level3MenuEntrySelected;
            level4MenuEntry.Selected += Level4MenuEntrySelected;
            level6MenuEntry.Selected += Level6MenuEntrySelected;
            level7MenuEntry.Selected += Level7MenuEntrySelected;
            level8MenuEntry.Selected += Level8MenuEntrySelected;
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
