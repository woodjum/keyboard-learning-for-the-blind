using System;
using System.Collections;
using System.IO;
using System.Windows.Forms;


// added libraries
using MouseKeyboardLibrary;    // Global keyboard hook from http://www.codeproject.com/KB/system/globalmousekeyboardlib.aspx
using SpeechLib;               // SAPI

namespace KeyboardLesson
{
    public partial class LessonForm : Form
    {
        // Information about each screen within a lesson
        class LessonScreen
        {
            public string instruction;
            public string message;
            public string toType;
            public int    state;
        };

        // States within a lesson
        enum States { Instruction = 1, Practice = 2, End = 3 };
        
        States currentState = States.Instruction; // Initial state
        
        //Script file containing configuration for this lesson
        String lessonFile = "Lesson1.txt"; //Initial state

        // Use to access the keyboard library hook
        KeyboardHook keyboardHook = new KeyboardHook();

        // SAPI voice and default set to Asyn and Purge flag
        SpVoice speech = new SpVoice();

        // SVSFlagsAsync: Asynchronous speech (i.e. Speak will return immediately, while the speech continues)
        // SVSFPurgeBeforeSpeak: Purge pending speech
        SpeechVoiceSpeakFlags spFlagsAsynPurge = SpeechVoiceSpeakFlags.SVSFlagsAsync | 
                                                 SpeechVoiceSpeakFlags.SVSFPurgeBeforeSpeak;

        ArrayList screenArray = new ArrayList();    // Array of LessonScreen
        int screenIndex       = -1;                  // screenArray index
        bool skipKeyPress     = false;              // Must continue to process KeyPress Event
        
        // Press 'Esc' key 2 times to exit
        int escExitCnt        = 0;                  // Number of times 'Esc' key pressed
        bool isEscOnceDone    = false;              // Is first 'Esc' key processeed

        //
        // Summary:
        //     Occurs when the lesson is complete
        public event EventHandler LessonFinished;


        public LessonForm()
        {
            InitializeComponent();
        }

        public LessonForm(String lessonFile):this()
        {
            this.lessonFile = lessonFile;
        }

        private void LessonForm_Load(object sender, EventArgs e)
        {
            // Initialize keyboard hook and start 
            keyboardHook.KeyDown += new KeyEventHandler(keyboardHook_KeyDown);
            keyboardHook.KeyUp += new KeyEventHandler(keyboardHook_KeyUp);
            keyboardHook.KeyPress += new KeyPressEventHandler(keyboardHook_KeyPress);
            keyboardHook.Start();

            // Initialize speech 
            speech.Rate   = 6;   // speechRate ranges from -10 to 10.
            speech.Volume = 100; // volume ranging from 0 to 100


            speech.Voice  = speech.GetVoices("Name=Microsoft Simplified Chinese", "").Item(0); // Default to Chinese Voice

            // Read Lesson text file to get screen display info
            bool result = ReadLessonInfoFromFile();

            if (!result)
            {
                // If error reading info, quit the program
                this.Close();
            }
            else
            {
                // Otherwise start lesson
                //currentState = States.Instruction;
                DetermineNextState();
                PlayCurrentState();
            }
        }

        private void LessonForm_FormClosed(object sender, FormClosedEventArgs e)
        {
            try
            {
                // Speak "Goodbye" before closing program
                speech.Speak("再见", SpeechVoiceSpeakFlags.SVSFPurgeBeforeSpeak | 
                                    SpeechVoiceSpeakFlags.SVSFDefault);
                
                keyboardHook.Stop();
                
            }
            catch (Exception ex)
            {
                MessageBox.Show("Exception!\n" + ex.Message);
            }

        }

        private void PlayCurrentState()
        {
            switch (currentState)
            {
                case States.Instruction:
                    {
                        PlayInstruction();
                        break;
                    }
                case States.Practice:
                    {
                        PlayPractice();
                        break;
                    }
                case States.End:
                    {
                        EndLesson();
                        break;
                    }
                default:
                    {
                        MessageBox.Show("Unknown State");
                        break;
                    }
            }
        }

        private void PlayInstruction()
        {
            try
            {
                // Speak instruction
                Lesson1Intro.Text = ((LessonScreen)screenArray[screenIndex]).instruction;
                string speakString = Lesson1Intro.Text;
                speakString = PinYinSpeechConverter.Convert(speakString);

                speech.Speak(speakString, spFlagsAsynPurge);

                // Reset / Refresh screen
                lb_ToTypeString.Hide();
                lb_ToType.Hide();
                textBox1.Hide();
                this.Refresh();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Exception!\n" + ex.Message);
            }
        }

        private void PlayPractice()
        {
            try
            {
                // Speak instruction
                Lesson1Intro.Text = ((LessonScreen)screenArray[screenIndex]).instruction;
                string message = ((LessonScreen)screenArray[screenIndex]).message;
                string toTypeString = ((LessonScreen)screenArray[screenIndex]).toType;
                string speakString = Lesson1Intro.Text;

                //Read message of set else generate the message
                if (string.IsNullOrEmpty(message))
                {
                    // generate message
                   
                    switch (toTypeString)
                    {
                        case " ":
                            speakString = speakString + "请按空格键";
                            break;
                        case ",":
                            speakString = speakString + "请按逗号";
                            break;
                        case ".":
                            speakString = speakString + "请按句号";
                            break;
                        case ";":
                            speakString = speakString + "请按分号";
                            break;
                        default:
                            speakString = speakString + "请按" + toTypeString;
                            break;
                    }
                }
                else
                {
                    //use set message
                    speakString = speakString + message;
                }

                speakString = PinYinSpeechConverter.Convert(speakString);

                //speech.Speak("<s>是</s><pron sym=\"a 1 bo 1 ci 1\">abc</pron>", SpeechVoiceSpeakFlags.SVSFDefault);
                speech.Speak(speakString, spFlagsAsynPurge);

                // Reset / Refresh screen
                Lesson1Intro.Text = ((LessonScreen)screenArray[screenIndex]).instruction;
                lb_ToTypeString.Text = toTypeString;
                lb_ToTypeString.Show();
                lb_ToType.Show();
                textBox1.Focus();
                textBox1.Clear();
                textBox1.Show();
                this.Refresh();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Exception!\n" + ex.Message);
            }
        }

        private void EndLesson()
        {
            try
            {
                // Display last screen before close
                Lesson1Intro.Text = ((LessonScreen)screenArray[screenIndex]).instruction;
                lb_ToTypeString.Hide();
                lb_ToType.Hide();
                textBox1.Hide();
                this.Refresh();

                // Speak lesson end message
                string speakString = Lesson1Intro.Text;
                speakString = PinYinSpeechConverter.Convert(speakString);
                speech.Speak(speakString, SpeechVoiceSpeakFlags.SVSFPurgeBeforeSpeak | 
                                          SpeechVoiceSpeakFlags.SVSFDefault);



                
                // Close the lesson
                this.Close();

                // Trigger the lesson finised event  
                if (this.LessonFinished != null)
                {
                    this.LessonFinished(this, new EventArgs());
                }

            }
            catch (Exception ex)
            {
                MessageBox.Show("Exception!\n" + ex.Message);
            }
        }

        private void DetermineNextState()
        {
            // Use the screen index to go to the next screen through the screen array
            currentState = (States) ((LessonScreen)screenArray[++screenIndex]).state;
        }

        // For each key pressed from the keyboard, the event sequence is: KeyDown -> KeyPress -> KeyUp
        void keyboardHook_KeyDown(object sender, KeyEventArgs e)
        {
            skipKeyPress         = true;                 // global variable to skip key press or not

            bool repeatCurrState = true;                 // whether to repeat current state or not
            string curKeyCode    = e.KeyCode.ToString(); // current key pressed

            // Default to SVSFPurgeBeforeSpeak to make sure the key is speaked out
            SpeechVoiceSpeakFlags spFlagsPurge = SpeechVoiceSpeakFlags.SVSFPurgeBeforeSpeak; // Purge only
            
            // Given a key (from key down):
            // (1) Determine whether to speak the key
            // (2) Determine whether to repeat the lesson instruction / practice message
            // (3) Determine whether to skip the key press event processing
            try
            {
                if (curKeyCode == "Return")
                {
                    // 'Enter' key is used to skip 
                    speech.Speak("回车键", spFlagsPurge);

                    // If the 'Enter' key is pressed during instruction, it will go to next screen
                    // If the 'Enter' key is pressed during practice, it just repeats the question

                }
                else if (curKeyCode == "Escape")
                {
                    // 'Esc' key is used to exit when pressed twice
                    speech.Speak("退出键", spFlagsPurge);

                    escExitCnt++;
                    if (escExitCnt < 2)
                    {
                        repeatCurrState = false;
                        string speakString = "你确定要退出？再按'退出键'退出，或随意按一个键回到原来的练习。";
                        speech.Speak(speakString, spFlagsAsynPurge);
                    }
                    else
                    {
                        // If 'Esc' key is pressed twice, close the program
                        this.Close();
                    }
                }
                else if (curKeyCode == "Space")
                {
                    speech.Speak("空格键", spFlagsPurge);

                    if (currentState == States.Instruction)
                        DetermineNextState();
                    else if (currentState == States.Practice && " " == ((LessonScreen)screenArray[screenIndex]).toType)
                        skipKeyPress = false;
                }
                else if (curKeyCode == "Back")
                {
                    speech.Speak("退格键", spFlagsPurge);
                }
                else if (curKeyCode == "Tab")
                {
                    speech.Speak("跳格键", spFlagsPurge);
                }
                else if (curKeyCode == "Up")
                {
                    speech.Speak("向上", spFlagsPurge);
                }
                else if (curKeyCode == "Down")
                {
                    speech.Speak("向下", spFlagsPurge);
                }
                else if (curKeyCode == "Right")
                {
                    speech.Speak("右边", spFlagsPurge);
                }
                else if (curKeyCode == "Left")
                {
                    speech.Speak("左边", spFlagsPurge);
                }
                else if (curKeyCode == "LShiftKey")
                {
                    speech.Speak("左换档键", spFlagsPurge);
                }
                else if (curKeyCode == "RShiftKey")
                {
                    speech.Speak("右换档键", spFlagsPurge);
                }
                else if (curKeyCode == "LControlKey")
                {
                    speech.Speak("左控制键", spFlagsPurge);
                }
                else if (curKeyCode == "RControlKey")
                {
                    speech.Speak("右控制键", spFlagsPurge);
                }
                else if (curKeyCode == "LMenu")
                {
                    speech.Speak("左交替键", spFlagsPurge);
                }
                else if (curKeyCode == "RMenu")
                {
                    speech.Speak("右交替键", spFlagsPurge);
                }
                else if (curKeyCode == "LWin")
                {
                    speech.Speak("窗口键", spFlagsPurge);
                }
                else if (curKeyCode == "Home")
                {
                    speech.Speak("起始键", spFlagsPurge);
                }
                else if (curKeyCode == "End")
                {
                    speech.Speak("结束键", spFlagsPurge);
                }
                else if (curKeyCode == "Insert")
                {
                    speech.Speak("插入键", spFlagsPurge);
                }
                else if (curKeyCode == "Delete")
                {
                    speech.Speak("删除键", spFlagsPurge);
                }
                else if (curKeyCode == "PageUp")
                {
                    speech.Speak("上一页键", spFlagsPurge);
                }
                // the keyboard hook library uses "Next" instead of "PageDown"
                else if (curKeyCode == "Next")
                {
                    speech.Speak("下一页键", spFlagsPurge);
                }
                else
                {

                    // For other keys, must continue to process KeyPress Event
                    skipKeyPress = false;

                    if (0 < escExitCnt && !isEscOnceDone)
                    {
                        repeatCurrState = true;
                        isEscOnceDone   = true;
                    }
                    else
                    {
                        repeatCurrState = false;   // do not repeat instruction
                    }
                }

                // Speak the key character that is pressed and repeat instruction
                if (States.Instruction == currentState)
                {
                    //TODO: test to see if this line is needed, not sure when this would be useful
                    //speech.Speak(curKeyCode, SpeechVoiceSpeakFlags.SVSFPurgeBeforeSpeak);
                    repeatCurrState = true;
                }
                    
                if (repeatCurrState)
                {
                    // Repeat current state
                    PlayCurrentState();
                }

            }
            catch (Exception ex)
            {
                MessageBox.Show("Exception!\n" + ex.Message);
            }
        }

        void keyboardHook_KeyPress(object sender, KeyPressEventArgs e)
        {
            // Only process the key press event whenthe it is in Practice state
            if (false == skipKeyPress && States.Practice == currentState)
            {
                //display text that was typed
                textBox1.Text = e.KeyChar.ToString() + textBox1.Text;
                textBox1.Refresh();

                SpFileStream fs = new SpFileStream(); // File stream for loading wav file

                try
                {
                    // Speak the key character that is pressed
                    string curKeyCode = e.KeyChar.ToString();
                    switch (curKeyCode)
                    {
                        case ",":
                            speech.Speak("逗号", spFlagsAsynPurge);
                            break;
                        case ".":
                            speech.Speak("句号", spFlagsAsynPurge);
                            break;
                        case ";":
                            speech.Speak("分号", spFlagsAsynPurge);
                            break;
                        default:
                            speech.Speak(PinYinSpeechConverter.Convert(e.KeyChar.ToString()), spFlagsAsynPurge);
                            break;
                    }

                    // Correct!
                    if (e.KeyChar.ToString().ToLower() == ((LessonScreen)screenArray[screenIndex]).toType)
                    {
                        // Play a sound to indicate corrent key press
                        if (fs != null)
                        {
                            // Use SVSFDefault to make sure speech play the wav file
                            fs.Open("correct.wav", SpeechStreamFileMode.SSFMOpenForRead, false);
                            speech.SpeakStream(fs, SpeechVoiceSpeakFlags.SVSFDefault);
                        }

                        // Based on current screen, determine the next screen
                        DetermineNextState();
                    }
                    // Wrong!
                    else
                    {
                        // Play a sound to indicate wrong key press
                        if (fs != null)
                        {
                            // Use SVSFDefault to make sure speech play the wav file
                            fs.Open("wrong.wav", SpeechStreamFileMode.SSFMOpenForRead, false);
                            speech.SpeakStream(fs, SpeechVoiceSpeakFlags.SVSFDefault);
                        }
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Exception!\n" + ex.Message);
                }
                finally
                {
                    // Make sure file stream close
                    if (fs != null)
                    {
                        fs.Close();
                    }
                }

                PlayCurrentState();
            }
        }

        void keyboardHook_KeyUp(object sender, KeyEventArgs e)
        {
            // Nothing happen during key up event
        }


        // Read lesson information from a text file which has a specific format to describe a screen and the flow
        private bool ReadLessonInfoFromFile()
        {
            bool result = false;
            int lineIndex = -1;
            try
            {
                // Create an instance of StreamReader to read from a file.
                // The using statement also closes the StreamReader.
                using (StreamReader sr = new StreamReader(this.lessonFile))
                {
                    result = true;
                    string line = string.Empty;

                    // Read lines from the file until the end of the file is reached.
                    while ((line = sr.ReadLine()) != null)
                    {
                        if (line == "" || line.StartsWith("#"))
                        {
                            continue;
                        }
                        else if (line == "Screen:")
                        {
                            lineIndex++;
                            LessonScreen aScreen = new LessonScreen();
                            if (aScreen != null)
                            {
                                screenArray.Add(aScreen);
                            }
                            else
                            {
                                MessageBox.Show("Error creating objects. Close program!");
                                this.Close();
                            }
                        }
                        else
                        {
                            int delimIndex = line.IndexOf("=");
                            LessonScreen aScreen = (LessonScreen)screenArray[lineIndex];
                            string key = line.Substring(0, delimIndex);
                            string value = line.Substring(delimIndex + 1, line.Length - delimIndex - 1).Trim();

                            if (key == "Instruction")
                            {
                                aScreen.instruction = value;
                            }
                            else if (key == "Message")
                            {
                                aScreen.message = value;
                            }
                            else if (key == "ToType")
                            {
                                aScreen.toType = value;
                                // Special: convert "Space" to " "
                                if (value.ToLower().Equals("space"))
                                    aScreen.toType = " ";
                            }
                            else if (key == "State")
                            {
                                aScreen.state = Int32.Parse(value);
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "The file could not be found or read\n.");
            }

            return result;
        }

    }
}

