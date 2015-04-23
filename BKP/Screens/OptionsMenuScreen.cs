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
        MenuEntry soundOnEntry;
        MenuEntry musicOnEntry;

        static bool funOption = true;

        public bool soundOn = true;
        public bool musicOn = true;

        static int elf = 23;

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
            soundOnEntry = new MenuEntry(string.Empty);
            musicOnEntry = new MenuEntry(string.Empty);

            SetMenuEntryText();

            MenuEntry back = new MenuEntry("Back");

            // Hook up menu event handlers.
            funMenuEntry.Selected += FunMenuEntrySelected;
            soundOnEntry.Selected += SoundOnEntrySelected;
            musicOnEntry.Selected += MusicOnEntrySelected;
            back.Selected += OnCancel;
            
            // Add entries to the menu.
            MenuEntries.Add(funMenuEntry);
            MenuEntries.Add(soundOnEntry);
            MenuEntries.Add(musicOnEntry);
            MenuEntries.Add(back);
        }


        /// <summary>
        /// Fills in the latest values for the options screen menu text.
        /// </summary>
        void SetMenuEntryText()
        {
            funMenuEntry.Text = "Fun: " + (funOption ? "on" : "off");
            soundOnEntry.Text = "Sound effects: " + (soundOn ? "on" : "off");
            musicOnEntry.Text = "Music: " + (musicOn ? "on" : "off");
        }


        #endregion

        #region Handle Input

        void FunMenuEntrySelected(object sender, PlayerIndexEventArgs e)
        {
            funOption = !funOption;

            SetMenuEntryText();
        }

        public void SoundOnEntrySelected(object sender, PlayerIndexEventArgs e)
        {
            soundOn = !soundOn;
            Sound.soundOn = soundOn;
            SetMenuEntryText();
        }

        public void MusicOnEntrySelected(object sender, PlayerIndexEventArgs e)
        {
            musicOn = !musicOn;
            Sound.musicOn = musicOn;
            SetMenuEntryText();
        }


        #endregion
    }
}
