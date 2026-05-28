using System;
using System.Media;

namespace Cybersecurity_Awareness_Chatbot_Part2
{//  start namespace
    public class voice_greeting
    {//  start class
        public void greet()
        {
            // with app.configure
            // auto get the path of where the project is 
            string paths = AppDomain.CurrentDomain.BaseDirectory.Replace(@"\bin\Debug\", @"\C.A.C_greet.wav");

            //then play the sound by creating an instance for it
            SoundPlayer greet = new SoundPlayer(paths);
            //and play the sound 
            greet.Play();

        }// end method
    }// end class
}// end namespace