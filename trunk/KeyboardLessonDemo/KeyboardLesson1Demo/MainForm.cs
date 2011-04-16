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
        int lessonIndex = 0;                        // lessonArray index

        public MainForm()
        {
            try
            {
                InitializeComponent();
                Initialize();
                RunCurrentLesson();
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
        }

        private void RunCurrentLesson()
        {
            //hide the main screen
            this.Hide();

            //determine what lesson is the current one
            String lessonFile = lessonFiles[lessonIndex];

            //run the current lesson
            LessonForm lesson = new LessonForm(lessonFile);
            lesson.TopMost = true;
            lesson.LessonFinished += new EventHandler(this.LessonForm_LessonFinished);
            lesson.Show();


            
        }

        //shows the next lesson after the first has finished
        private void LessonForm_LessonFinished(object sender, EventArgs e)
        {
            this.Show();

            //run the next lesson
            this.lessonIndex++;

            //only run the next lesson if there are any
            if (lessonIndex < lessonFiles.Length)
            {
                RunCurrentLesson();
            }
        }
    }
}
