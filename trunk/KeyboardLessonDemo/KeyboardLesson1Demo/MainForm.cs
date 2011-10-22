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
        static String LESSON_FOLDER = "lessons";        
        String[] lessonFiles;                       // Array of lesson script files


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
            lessonFiles = Directory.GetFiles(LESSON_FOLDER, "lesson*.txt");
            Console.WriteLine("lessons found: " + lessonFiles.Length);
   
            //display lessons found in listbox
            foreach (string lessonFile in lessonFiles)
            {
                //remove folder name and \ from the front of the filename
                string lessonName = lessonFile.Substring(LESSON_FOLDER.Length + 1);

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
            lesson.ShowDialog(this);
           
           
            
        }

        //shows the next lesson after the first has finished
        private void LessonForm_LessonFinished(object sender, EventArgs e)
        {
           
            this.Show();

            //grab the currently selected index
            int lessonIndex = this.LessonListBox.SelectedIndex;

            //select the next lesson with wrap around
            this.LessonListBox.SelectedIndex = (lessonIndex +1) % this.lessonFiles.Length;
         
        }

        //detects that the space key or the enter key was pressed 
        //to run the current lesson
        void MainFrom_KeyUp(object sender, KeyEventArgs e)
        {
            //Debug
            Console.Out.WriteLine("Main Form Received key " + e.KeyCode.ToString());

            //Known Bug: When the lesson form closes, the main form suddenly
            //           receives all the keys pressed when the lesson form was open
            //
            if (Keys.Return.Equals(e.KeyCode) )
           {
               RunCurrentLesson();
           }
            
        }
    }
}
