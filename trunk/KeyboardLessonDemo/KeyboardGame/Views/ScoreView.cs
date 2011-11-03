using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Text;
using System.Windows.Forms;

namespace KeyboardGame.Views
{
    public partial class ScoreView : KeyboardGame.Views.TalkingView
    {
        public ScoreView()
        {
            InitializeComponent();
        }

        public void SetScore(int score)
        {
            this.ScoreLabel.Text = "" + score;
        }
    }
}
