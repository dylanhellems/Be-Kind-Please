using System;
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

        public Sound()
        {

        }

        public void LoadContent(ContentManager content, string l)
        {
            int level = LevelMenuScreen.levels.FindIndex(a => a == l) + 1;

            if (level == 1 || level == 2 || level == 3)
            {
                song = "Vivacity";
            }

            else if (level == 4)
            {
                song = "Daily Beetle";
            }

            else if (level == 5 || level == 6 || level == 7)
            {
                song = "Call to Adventure";
            }

            rewind = content.Load<SoundEffect>("soundeffects/rewind");
            ff = content.Load<SoundEffect>("soundeffects/fastforward");
            jump = content.Load<SoundEffect>("soundeffects/jump");
            music = content.Load<SoundEffect>("soundeffects/" + song);
            
            rewindInstance = rewind.CreateInstance();
            ffInstance = ff.CreateInstance();
            jumpInstance = jump.CreateInstance();
            musicInstance = music.CreateInstance();
        }

        public void Update(ContentManager content, Controls control, bool grounded)
        {
//             musicCall = content.Load<SoundEffect>("soundeffects/calltoadventure");
//             musicCallInstance = musicCall.CreateInstance();
//             //musicCallInstance.Play();
//             musicVivacity = content.Load<SoundEffect>("soundeffects/vivacity");
//             musicVivacityInstance = musicVivacity.CreateInstance();
//             //musicVivacityInstance.Play();
        }

        public void Update(Controls control, bool grounded, int state)
        {
            if (musicOn == true)
            {             
                musicInstance.IsLooped = true;
                musicInstance.Volume = 1f;
                musicInstance.Play();
            }

            if (soundOn == true)
            {
                if (state == -1)
                {
                    musicInstance.Volume = 0f;
                    rewindInstance.IsLooped = true;
                    rewindInstance.Play();
                    //Console.WriteLine("Played rewind");
                }

                else if (state == 2)
                {
                    musicInstance.Volume = 0f;
                    ffInstance.IsLooped = true;
                    ffInstance.Play();
                    // Console.WriteLine("Played ff");
                }

                else if (state == 0)
                {
                    musicInstance.Volume = 0f;
                }

                else if (control.onPress(Keys.Space, Buttons.A) == true && grounded == true)
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

        public void StopAll() 
        {
            musicInstance.Stop();
            rewindInstance.Stop();
            ffInstance.Stop();
        }

    }

}
