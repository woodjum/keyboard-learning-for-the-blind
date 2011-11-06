using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using KeyboardGame.Views;

using SpeechLib;               // SAPI

namespace KeyboardGame
{
    public partial class TalkingWindow : Form
    {

       
        // SAPI voice and default set to Asyn and Purge flag
        SpVoice speech = new SpVoice();

        // SVSFlagsAsync: Asynchronous speech (i.e. Speak will return immediately, while the speech continues)
        // SVSFPurgeBeforeSpeak: Purge pending speech
        SpeechVoiceSpeakFlags spFlagsAsynPurge = SpeechVoiceSpeakFlags.SVSFlagsAsync | 
                                                 SpeechVoiceSpeakFlags.SVSFPurgeBeforeSpeak;
        SpeechVoiceSpeakFlags spFlagsSynPurge = SpeechVoiceSpeakFlags.SVSFPurgeBeforeSpeak|
                                                SpeechVoiceSpeakFlags.SVSFDefault;

      
        TalkingView currnetView;

        public TalkingWindow()
        {
            InitializeComponent();
            this.Load += new System.EventHandler(this.TalkingWindow_Load);
           
        }

        private void TalkingWindow_Load(object sender, EventArgs e)
        {
             // Initialize speech 
            speech.Rate   = 2;   // speechRate ranges from -10 to 10.
            speech.Volume = 100; // volume ranging from 0 to 100
            speech.Voice = speech.GetVoices("Name=Microsoft Simplified Chinese", "").Item(0); // Default to Chinese Voice
            
        }

      
        public void SetCurrentView(TalkingView view)
        {
            if (view != null)
            {
                if (this.currnetView != null)
                {
                    this.Controls.Remove(this.currnetView);
                }
                this.Controls.Add(view);
                this.currnetView = view;
            }
        }


        public void Speak(String speakString)
        {
            this.Speak(speakString, true);
        }

        public void Speak(String speakString, bool async)
        {
            if (async)
            {
                speech.Speak(speakString, spFlagsAsynPurge);
            }
            else
            {
                speech.Speak(speakString, spFlagsSynPurge);
                
            }
        }

        public void PlaySound(string soundFilename) 
        {
            SpFileStream fs = new SpFileStream(); // File stream for loading wav file
            try
            {
                if (fs != null)
                {
                    // Use SVSFDefault to make sure speech play the wav file
                    fs.Open(soundFilename, SpeechStreamFileMode.SSFMOpenForRead, false);
                    speech.SpeakStream(fs, SpeechVoiceSpeakFlags.SVSFDefault);
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
        }

    }
}
