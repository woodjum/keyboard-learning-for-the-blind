using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

namespace KeyboardGame.Views
{
    public partial class GameIntroView : KeyboardGame.Views.TalkingView
    {
        public GameIntroView()
        {
            InitializeComponent();
        }

        public void SetTitle(String title){
            this.TitleLabel.Text = title;
        }

        public void SetDescription(String description){
            this.DescriptionLabel.Text = description;
        }
    }
}
