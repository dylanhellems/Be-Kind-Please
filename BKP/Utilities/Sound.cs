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
        public SoundEffectInstance rewindInstance;
        public SoundEffectInstance ffInstance;
        public SoundEffectInstance jumpInstance;
        public Controls control;

        public Sound()
        {

        }

        public void LoadContent(ContentManager content)
        {
            rewind = content.Load<SoundEffect>("soundeffects/rewind");
            ff = content.Load<SoundEffect>("soundeffects/fastforward");
            jump = content.Load<SoundEffect>("soundeffects/jump");
            
            //rewindInstance = rewind.CreateInstance();
            //ffInstance = ff.CreateInstance();
            //jumpInstance = jump.CreateInstance();
        }

        public void Update(Controls control, bool grounded)
        {
            //Console.WriteLine(control.isPressed(Keys.Left, Buttons.LeftTrigger));
            //Console.WriteLine(control.isPressed(Keys.Right, Buttons.RightTrigger));

            //if (control.isPressed(Keys.Left, Buttons.LeftTrigger) == true)
            //{
            //    rewindInstance.IsLooped = true;
            //    rewindInstance.Play();
            //    //Console.WriteLine("Played rewind");
            //}

            //else if (control.isPressed(Keys.Right, Buttons.RightTrigger) == true)
            //{
            //    ffInstance.IsLooped = true;
            //    ffInstance.Play();
            //   // Console.WriteLine("Played ff");
            //}

            //else if (control.isPressed(Keys.Space, Buttons.A) == true && grounded == true)
            //{
            //    jumpInstance.IsLooped = false;
            //    jumpInstance.Play();
            //}

            //else
            //{
            //    rewindInstance.Stop();
            //    ffInstance.Stop();
            //}

        }

    }

}
