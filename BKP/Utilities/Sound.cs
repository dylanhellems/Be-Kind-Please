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
        public SoundEffectInstance rewindInstance;
        public SoundEffectInstance ffInstance;
        public Controls control;

        public Sound()
        {

        }

        public void LoadContent(ContentManager content)
        {
            rewind = content.Load<SoundEffect>("Sound/rewind");
            ff = content.Load<SoundEffect>("Sound/fastforward");
            control = new Controls();
            rewindInstance = rewind.CreateInstance();
            ffInstance = ff.CreateInstance();
        }

        public void Update()
        {
            if (control.onPress(Keys.Left, Buttons.LeftTrigger) == true)
            {
                rewindInstance.Volume = 1.0f;
                rewindInstance.IsLooped = true;
                rewindInstance.Play();
                //Console.WriteLine("Played rewind");
            }

            else
            {
                rewindInstance.Stop();
                //Console.WriteLine("Stopped rewind");
            }

            if (control.onPress(Keys.Right, Buttons.RightTrigger) == true)
            {
                ffInstance.Volume = 1.0f;
                ffInstance.IsLooped = true;
                ffInstance.Play();
                //Console.WriteLine("Played ff");
            }

            else
            {
                ffInstance.Stop();
                //Console.WriteLine("Stopped ff");
            }
            
        }

    }

}
