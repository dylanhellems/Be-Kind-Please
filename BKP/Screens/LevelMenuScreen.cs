#region File Description
//-----------------------------------------------------------------------------
// LevelMenuScreen.cs
//
// Microsoft XNA Community Game Platform
// Copyright (C) Microsoft Corporation. All rights reserved.
//-----------------------------------------------------------------------------
#endregion

#region Using Statements
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
            MenuEntry level1MenuEntry = new MenuEntry("Act 1");
            MenuEntry level2MenuEntry = new MenuEntry("Act 2");
            MenuEntry level3MenuEntry = new MenuEntry("Act 3");
            MenuEntry level5MenuEntry = new MenuEntry("TEST LEVEL");
            MenuEntry level4MenuEntry = new MenuEntry("Prologue");
            MenuEntry level6MenuEntry = new MenuEntry("TEST LEVEL 6");
            MenuEntry back = new MenuEntry("Back");

            // Hook up menu event handlers.
            level1MenuEntry.Selected += Level1MenuEntrySelected;
            level2MenuEntry.Selected += Level2MenuEntrySelected;
            level3MenuEntry.Selected += Level3MenuEntrySelected;
            level5MenuEntry.Selected += Level5MenuEntrySelected;
            level4MenuEntry.Selected += Level4MenuEntrySelected;
            level6MenuEntry.Selected += Level6MenuEntrySelected;
            back.Selected += OnCancel;

            // Add entries to the menu.
            MenuEntries.Add(level1MenuEntry);
            MenuEntries.Add(level2MenuEntry);
            MenuEntries.Add(level3MenuEntry);
            MenuEntries.Add(level5MenuEntry);
            MenuEntries.Add(level4MenuEntry);
            MenuEntries.Add(level6MenuEntry);
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

        void Level5MenuEntrySelected(object sender, PlayerIndexEventArgs e)
        {
            LoadingScreen.Load(ScreenManager, true, e.PlayerIndex,
                               new GameplayScreen("Content/levels/level5.tmx"));
        }

        void Level6MenuEntrySelected(object sender, PlayerIndexEventArgs e)
        {
            LoadingScreen.Load(ScreenManager, true, e.PlayerIndex,
                               new GameplayScreen("Content/levels/level6.tmx"));
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
