﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Input;

namespace BKP
{
    public class Sound
    {
        public SoundEffect rewind;
        public SoundEffect ff;
        public SoundEffect jump;
        public SoundEffect music;
        public SoundEffectInstance rewindInstance;
        public SoundEffectInstance ffInstance;
        public SoundEffectInstance jumpInstance;
        public static SoundEffectInstance musicInstance;
        public Controls control;

        public static bool soundOn = true;
        public static bool musicOn = true;

        public static string song = "";
        public static int level;


        public Sound()
        {

        }

        public void LoadContent(ContentManager content)
        {
            switch (level)
            {
                case 1:
                    song = "Vivacity";
                    break;

                case 2:
                    song = "Vivacity";
                    break;

                case 3:
                    song = "Vivacity";
                    break;

                case 4:
                    song = "Daily Beetle";
                    break;

                case 5:
                    song = "Call to Adventure";
                    break;

                case 6:
                    song = "Call to Adventure";
                    break;

                case 7: 
                    song = "Call to Adventure";
                    break;
            }
            rewind = content.Load<SoundEffect>("soundeffects/rewind");
            ff = content.Load<SoundEffect>("soundeffects/fastforward");
            jump = content.Load<SoundEffect>("soundeffects/jump");
            music = content.Load<SoundEffect>("soundeffects/" + song);
            
            rewindInstance = rewind.CreateInstance();
            ffInstance = ff.CreateInstance();
            jumpInstance = jump.CreateInstance();
            musicInstance = music.CreateInstance();

//             musicCall = content.Load<SoundEffect>("soundeffects/calltoadventure");
//             musicCallInstance = musicCall.CreateInstance();
//             //musicCallInstance.Play();
//             musicVivacity = content.Load<SoundEffect>("soundeffects/vivacity");
//             musicVivacityInstance = musicVivacity.CreateInstance();
//             //musicVivacityInstance.Play();
        }

        public void Update(Controls control, bool grounded)
        {
            if (musicOn == true)
            {
                musicInstance.IsLooped = true;
                musicInstance.Volume = 1f;
                musicInstance.Play();
            }

            if (soundOn == true)
            {
                if (control.isPressed(Keys.Left, Buttons.LeftTrigger) == true)
                {
                    musicInstance.Volume = 0f;
                    rewindInstance.IsLooped = true;
                    rewindInstance.Play();
                    //Console.WriteLine("Played rewind");
                }

                else if (control.isPressed(Keys.Right, Buttons.RightTrigger) == true)
                {
                    musicInstance.Volume = 0f;
                    ffInstance.IsLooped = true;
                    ffInstance.Play();
                    // Console.WriteLine("Played ff");
                }

                else if (control.isPressed(Keys.Down, Buttons.B) == true)
                {
                    musicInstance.Volume = 0f;
                }

                else if (control.isPressed(Keys.Space, Buttons.A) == true && grounded == true)
                {
                    jumpInstance.IsLooped = false;
                    jumpInstance.Play();
                }

                else
                {
                    rewindInstance.Stop();
                    ffInstance.Stop();
                }
            }        
             
        }

    }

}
