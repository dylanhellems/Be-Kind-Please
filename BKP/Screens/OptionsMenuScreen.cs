#region File Description
//-----------------------------------------------------------------------------
// OptionsMenuScreen.cs
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
    /// The options screen is brought up over the top of the main menu
    /// screen, and gives the user a chance to configure the game
    /// in various hopefully useful ways.
    /// </summary>
    class OptionsMenuScreen : MenuScreen
    {
        #region Fields

        MenuEntry musicMenuEntry;
        MenuEntry fxMenuEntry;

        static bool musicOption = true;
        static bool fxOption = true;

        #endregion

        #region Initialization


        /// <summary>
        /// Constructor.
        /// </summary>
        public OptionsMenuScreen()
            : base("")
        {
            // Create our menu entries.
            //ungulateMenuEntry = new MenuEntry(string.Empty);
            //languageMenuEntry = new MenuEntry(string.Empty);
            //frobnicateMenuEntry = new MenuEntry(string.Empty);
            //elfMenuEntry = new MenuEntry(string.Empty);
            musicMenuEntry = new MenuEntry(string.Empty);
            fxMenuEntry = new MenuEntry(string.Empty);

            SetMenuEntryText();

            MenuEntry back = new MenuEntry("Back");

            // Hook up menu event handlers.
            musicMenuEntry.Selected += MusicMenuEntrySelected;
            fxMenuEntry.Selected += FXMenuEntrySelected;
            back.Selected += OnCancel;
            
            // Add entries to the menu.
            MenuEntries.Add(musicMenuEntry);
            MenuEntries.Add(fxMenuEntry);
            MenuEntries.Add(back);
        }


        /// <summary>
        /// Fills in the latest values for the options screen menu text.
        /// </summary>
        void SetMenuEntryText()
        {
            musicMenuEntry.Text = "Fun: " + (musicOption ? "on" : "off");
            fxMenuEntry.Text = "Fun: " + (fxOption ? "on" : "off");
        }


        #endregion

        #region Handle Input

        void MusicMenuEntrySelected(object sender, PlayerIndexEventArgs e)
        {
            musicOption = !musicOption;

            SetMenuEntryText();
        }

        void FXMenuEntrySelected(object sender, PlayerIndexEventArgs e)
        {
            fxOption = !fxOption;

            SetMenuEntryText();
        }

        #endregion
    }
}
