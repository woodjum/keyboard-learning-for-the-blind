using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using System.IO;

namespace ExerciseGenerator
{
    public partial class GeneratorForm : Form
    {
        public GeneratorForm()
        {
            InitializeComponent();
        }

        private void chkSeed_CheckedChanged(object sender, EventArgs e)
        {
            if (chkSeed.Checked)
            {
                txtSeed.Enabled = true;
            }
            else
            {
                txtSeed.Enabled = false;
            }
        }

        private void btnGenerate_Click(object sender, EventArgs e)
        {
            int minSequence = int.Parse(txtMinSequences.Text);
            int maxSequence = int.Parse(txtMaxSequences.Text);
            int maxSequenceLength = int.Parse(txtSequenceLength.Text);
            string exerciseName = txtExerciseName.Text;
            string exerciseFileName = @".\" + exerciseName.Replace(' ', '_') + ".exercise";

            Generator generator = new Generator(txtCharacters.Text, maxSequenceLength, maxSequence, minSequence);
            
            if (chkSeed.Checked)
            {
                generator.SetSeed(int.Parse(txtSeed.Text));
            }

            string sequence = generator.Generate();

            using (StreamWriter writer = new StreamWriter(exerciseFileName))
            {
                writer.WriteLine("Name={0}", exerciseName);
                writer.WriteLine("Description=");
                writer.WriteLine("Characters={0}", generator.ValidCharacters);
                writer.WriteLine("Sequence={0}", sequence);
            }

            MessageBox.Show(string.Format("Exercise generated and saved as {0}", (new FileInfo(exerciseFileName)).FullName));
        }
    }
}
