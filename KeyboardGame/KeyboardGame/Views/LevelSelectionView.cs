using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

namespace KeyboardGame.Views
{
    public partial class LevelSelectionView : KeyboardGame.Views.TalkingView
    {
        public LevelSelectionView()
        {
            InitializeComponent();
        }

        public string GetTitle()
        {
            return this.TitleLabel.Text;
        }

        public ListBox GetLevelListBox()
        {
            return this.LevelListBox;
        }

        public void DisplayLevels(IEnumerable<string> levelNames)
        {
            this.LevelListBox.Items.Clear();
            foreach (var levelName in levelNames)
            {
                this.LevelListBox.Items.Add(levelName);
            }

            this.LevelListBox.SelectedIndex = 0;
        }
    }
}
