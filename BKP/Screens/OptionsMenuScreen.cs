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

<<<<<<< HEAD
        MenuEntry funMenuEntry;
        MenuEntry soundOnEntry;
        MenuEntry musicOnEntry;

        static bool funOption = true;

        public bool soundOn = true;
        public bool musicOn = true;

        static int elf = 23;
=======
        MenuEntry musicMenuEntry;
        MenuEntry fxMenuEntry;

        static bool musicOption = true;
        static bool fxOption = true;
>>>>>>> origin/expo

        #endregion

        #region Initialization


        /// <summary>
        /// Constructor.
        /// </summary>
        public OptionsMenuScreen()
            : base("")
        {
            // Create our menu entries.
<<<<<<< HEAD

            funMenuEntry = new MenuEntry(string.Empty);
            soundOnEntry = new MenuEntry(string.Empty);
            musicOnEntry = new MenuEntry(string.Empty);
=======
            //ungulateMenuEntry = new MenuEntry(string.Empty);
            //languageMenuEntry = new MenuEntry(string.Empty);
            //frobnicateMenuEntry = new MenuEntry(string.Empty);
            //elfMenuEntry = new MenuEntry(string.Empty);
            musicMenuEntry = new MenuEntry(string.Empty);
            fxMenuEntry = new MenuEntry(string.Empty);
>>>>>>> origin/expo

            SetMenuEntryText();

            MenuEntry back = new MenuEntry("Back");

            // Hook up menu event handlers.
<<<<<<< HEAD
            funMenuEntry.Selected += FunMenuEntrySelected;
            soundOnEntry.Selected += SoundOnEntrySelected;
            musicOnEntry.Selected += MusicOnEntrySelected;
            back.Selected += OnCancel;
            
            // Add entries to the menu.
            MenuEntries.Add(funMenuEntry);
            MenuEntries.Add(soundOnEntry);
            MenuEntries.Add(musicOnEntry);
=======
            musicMenuEntry.Selected += MusicMenuEntrySelected;
            fxMenuEntry.Selected += FXMenuEntrySelected;
            back.Selected += OnCancel;
            
            // Add entries to the menu.
            MenuEntries.Add(musicMenuEntry);
            MenuEntries.Add(fxMenuEntry);
>>>>>>> origin/expo
            MenuEntries.Add(back);
        }


        /// <summary>
        /// Fills in the latest values for the options screen menu text.
        /// </summary>
        void SetMenuEntryText()
        {
<<<<<<< HEAD
            funMenuEntry.Text = "Fun: " + (funOption ? "on" : "off");
            soundOnEntry.Text = "Sound effects: " + (soundOn ? "on" : "off");
            musicOnEntry.Text = "Music: " + (musicOn ? "on" : "off");
=======
            musicMenuEntry.Text = "Fun: " + (musicOption ? "on" : "off");
            fxMenuEntry.Text = "Fun: " + (fxOption ? "on" : "off");
>>>>>>> origin/expo
        }


        #endregion

        #region Handle Input

<<<<<<< HEAD
        void FunMenuEntrySelected(object sender, PlayerIndexEventArgs e)
        {
            funOption = !funOption;
=======
        void MusicMenuEntrySelected(object sender, PlayerIndexEventArgs e)
        {
            musicOption = !musicOption;
>>>>>>> origin/expo

            SetMenuEntryText();
        }

<<<<<<< HEAD
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


=======
        void FXMenuEntrySelected(object sender, PlayerIndexEventArgs e)
        {
            fxOption = !fxOption;

            SetMenuEntryText();
        }

>>>>>>> origin/expo
        #endregion
    }
}
