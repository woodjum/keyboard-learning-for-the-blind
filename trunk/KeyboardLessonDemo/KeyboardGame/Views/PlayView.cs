using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

namespace KeyboardGame.Views
{
    public partial class PlayView : KeyboardGame.Views.TalkingView
    {
        public PlayView()
        {
            InitializeComponent();
        }


        public void SetPrompt(string prompt)
        {
            this.Prompt.Text = prompt;
        }

        public void SetPromptVisibility(bool visible)
        {
            this.Prompt.Visible = visible;
        }

        public void AppendInput(string input)
        {
            this.UserInput.Text += input;
        }

        public void ClearInput()
        {
            this.UserInput.Text = "";
        }
    }
}
