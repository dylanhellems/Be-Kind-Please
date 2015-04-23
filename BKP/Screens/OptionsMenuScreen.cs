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

        MenuEntry funMenuEntry;
        MenuEntry musicMenuEntry;
        MenuEntry fxMenuEntry;

        static bool funOption = true;
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
            funMenuEntry = new MenuEntry(string.Empty);
            musicMenuEntry = new MenuEntry(string.Empty);
            fxMenuEntry = new MenuEntry(string.Empty);
            SetMenuEntryText();

            MenuEntry back = new MenuEntry("Back");

            // Hook up menu event handlers.

            funMenuEntry.Selected += FunMenuEntrySelected;
            musicMenuEntry.Selected += MusicMenuEntrySelected;
            fxMenuEntry.Selected += FXMenuEntrySelected;
            back.Selected += OnCancel;
            
            // Add entries to the menu.
            MenuEntries.Add(funMenuEntry);
            MenuEntries.Add(musicMenuEntry);
            MenuEntries.Add(fxMenuEntry);
            MenuEntries.Add(back);
        }


        /// <summary>
        /// Fills in the latest values for the options screen menu text.
        /// </summary>
        void SetMenuEntryText()
        {
            funMenuEntry.Text = "Fun: " + (funOption ? "on" : "off");
            musicMenuEntry.Text = "Music: " + (musicOption ? "on" : "off");
            fxMenuEntry.Text = "Sound effects: " + (fxOption ? "on" : "off");
        }


        #endregion

        #region Handle Input

        void FunMenuEntrySelected(object sender, PlayerIndexEventArgs e)
        {
            funOption = !funOption;
        }

        void MusicMenuEntrySelected(object sender, PlayerIndexEventArgs e)
        {
            musicOption = !musicOption;
            Sound.musicOn = musicOption;
            SetMenuEntryText();
        }

        void FXMenuEntrySelected(object sender, PlayerIndexEventArgs e)
        {
            fxOption = !fxOption;
            Sound.soundOn = fxOption;
            SetMenuEntryText();
        }
        #endregion
    }
}
