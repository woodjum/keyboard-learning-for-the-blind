using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Windows.Forms;

// added libraries
using MouseKeyboardLibrary;    // Global keyboard hook from http://www.codeproject.com/KB/system/globalmousekeyboardlib.aspx

namespace KeyboardGame
{
    public class TalkingController
    {


        // Use to access the keyboard library hook
        protected KeyboardHook keyboardHook = new KeyboardHook();

        private int escExitCnt;
        protected Boolean speakKeysPressed = true;

        protected TalkingWindow talkingWindow;
//        private TalkingModel talkingModel;

        public TalkingController()
        {

            // Initialize keyboard hook and start 
            keyboardHook.KeyDown += new KeyEventHandler(keyboardHook_KeyDown);
            keyboardHook.KeyUp += new KeyEventHandler(keyboardHook_KeyUp);
            keyboardHook.KeyPress += new KeyPressEventHandler(keyboardHook_KeyPress);
            keyboardHook.Start();

            // Inititalize window 
            talkingWindow = new TalkingWindow();
            talkingWindow.FormClosed += new FormClosedEventHandler(TalkingController_FormClosed);
        }

        public void Run(){
            Application.Run(talkingWindow);
        }

         void keyboardHook_KeyDown(object sender, KeyEventArgs e)
        {
            //nothing especial to do on key down
        }

         void keyboardHook_KeyPress(object sender, KeyPressEventArgs e)
        {
            //nothing to do for key press (codes are not as informative)
        }

         void keyboardHook_KeyUp(object sender, KeyEventArgs e)
        {
            //Handle exiting or speak the key pressed
            string curKeyCode = getChineseKeyTranslation(e.KeyCode); // current key pressed
           
            try
            {

                //use a case statement to figure what key it is
                if (Keys.Escape.Equals(e.KeyCode))
                {
                    // 'Esc' key is used to exit when pressed twice
                    talkingWindow.Speak("退出键",false);


                    escExitCnt++;
                    if (escExitCnt < 2)
                    {
                        string speakString = "你确定要退出？再按'退出键'退出，或随意按一个键回到原来的练习。";
                        talkingWindow.Speak(speakString);
                    }
                    else
                    {
                        // If 'Esc' key is pressed twice, close the program
                        talkingWindow.Close();
                    }
                }
                else
                {
                    // Speak the key character that is pressed
                    if (this.speakKeysPressed)
                    {
                        // TODO : handle hang (possibly) caused by Converter (location 2)
                        string speakString = PinYinSpeechConverter.Convert(curKeyCode);
                        talkingWindow.Speak(speakString);
                    }
                }

            }
            catch (Exception ex)
            {
                MessageBox.Show("Exception!\n" + ex.Message);
            }
        }

        // translates the given key to a chinese string that 
        // can be spoken
        private String getChineseKeyTranslation(Keys key)
        {
            switch (key) 
            {
                case Keys.Escape:
                    return "退出键";
                default:
                    return key.ToString();
            }
        }

         void TalkingController_FormClosed(object sender, FormClosedEventArgs e)
        {
            try
            {
                // Speak "Goodbye" before closing program
                talkingWindow.Speak("再见",false);
                keyboardHook.Stop();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Exception!\n" + ex.Message);
            }
        }


    }
}
