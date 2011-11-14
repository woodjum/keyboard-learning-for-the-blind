using System;
using System.IO;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using System.Collections;

namespace KeyboardLesson
{
    public partial class MainForm : Form
    {
        const String lessonFolder = "lessons";        
        String[] lessonFiles;                       // Array of lesson script files

        //Tempory fix: main form will ignore input after a lesson completes
        // for a fixed amount of time because keys from the lesson replay on
        // the main form
        const double inputDrainInSec = 5;   //How many seconds to ignore input when draining
        DateTime processInputAfterTime = DateTime.MinValue; //when to resume processing of input

        public MainForm()
        {
            try
            {
                InitializeComponent();
                Initialize();
                
            }
            catch (Exception ex)
            {
                MessageBox.Show("Exception!\n" + ex.Message);
                this.Close();
            }
        }

        //read in configuration and discover lesson files
        private void Initialize()
        {
            //find all avalible lesson files in the lessons directory
            lessonFiles = Directory.GetFiles(lessonFolder, "lesson*.txt");
            Console.WriteLine("lessons found: " + lessonFiles.Length);
   
            //display lessons found in listbox
            foreach (string lessonFile in lessonFiles)
            {
                //remove folder name and \ from the front of the filename
                string lessonName = lessonFile.Substring(lessonFolder.Length + 1);

                //remove the .txt from the end of the filename
                lessonName = lessonName.Substring(0,lessonName.Length - 4);

                //display the remaining filename as the lesson name
                this.LessonListBox.Items.Add(lessonName);
            }

            //have the first lesson automatically selected
            if (lessonFiles.Length > 0)
            {
                this.LessonListBox.SetSelected(0, true);
            }

            //attach hook for key presses
            this.LessonListBox.KeyUp  += new KeyEventHandler(MainFrom_KeyUp);
        }

        private void RunCurrentLesson()
        {
            //hide the main screen
            this.Hide();

            //determine what lesson is the current one
            int lessonIndex = this.LessonListBox.SelectedIndex;
            String lessonFile = lessonFiles[lessonIndex];

            //run the current lesson
            LessonForm lesson = new LessonForm(lessonFile);
            lesson.TopMost = true;
            lesson.LessonFinished += new EventHandler(this.LessonForm_LessonFinished);
            lesson.FormClosed += new FormClosedEventHandler(this.LessonForm_FormClosed);
            lesson.ShowDialog(this);
           
           
            
        }

        //shows the next lesson after the first has finished
        private void LessonForm_LessonFinished(object sender, EventArgs e)
        {
           
           

            //grab the currently selected index
            int lessonIndex = this.LessonListBox.SelectedIndex;

            //select the next lesson with wrap around
            this.LessonListBox.SelectedIndex = (lessonIndex +1) % this.lessonFiles.Length;
         
            //Temporary fix: set the time such that input from the lesson will be drained
            this.processInputAfterTime = DateTime.UtcNow.AddSeconds(MainForm.inputDrainInSec);
        }

        //display the main form if the smaller one closes
        private void LessonForm_FormClosed(object sender, FormClosedEventArgs e)
        {
            this.Show();

        }

        //detects that the space key or the enter key was pressed 
        //to run the current lesson
        void MainFrom_KeyUp(object sender, KeyEventArgs e)
        {
            //Debug
            Console.Out.Write("Main Form Received key " + e.KeyCode.ToString());

            //Known Bug: When the lesson form closes, the main form suddenly
            //           receives all the keys pressed when the lesson form was open
            //Temporary Fix: ignore input for a fixed abmount of time after the lesson finishes
            if (this.processInputAfterTime.Ticks < DateTime.UtcNow.Ticks)
            {
                //Debug
                Console.Out.WriteLine(" (processed)");
                if (Keys.Space.Equals(e.KeyCode))
                {
                    RunCurrentLesson();
                }
            }
            else
            {
                //Debug
                Console.Out.WriteLine(" (ignored)");
            }
            
        }
    }
}
