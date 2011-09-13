namespace ExerciseGenerator
{
    partial class GeneratorForm
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.txtCharacters = new System.Windows.Forms.TextBox();
            this.btnGenerate = new System.Windows.Forms.Button();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.chkSeed = new System.Windows.Forms.CheckBox();
            this.txtSeed = new System.Windows.Forms.TextBox();
            this.label4 = new System.Windows.Forms.Label();
            this.txtMaxSequences = new System.Windows.Forms.TextBox();
            this.label3 = new System.Windows.Forms.Label();
            this.txtSequenceLength = new System.Windows.Forms.TextBox();
            this.txtMinSequences = new System.Windows.Forms.TextBox();
            this.label2 = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.groupBox3 = new System.Windows.Forms.GroupBox();
            this.txtExerciseName = new System.Windows.Forms.TextBox();
            this.groupBox1.SuspendLayout();
            this.groupBox2.SuspendLayout();
            this.groupBox3.SuspendLayout();
            this.SuspendLayout();
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.txtCharacters);
            this.groupBox1.Location = new System.Drawing.Point(13, 62);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(559, 144);
            this.groupBox1.TabIndex = 1;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Characters to Include";
            // 
            // txtCharacters
            // 
            this.txtCharacters.Location = new System.Drawing.Point(6, 19);
            this.txtCharacters.Multiline = true;
            this.txtCharacters.Name = "txtCharacters";
            this.txtCharacters.Size = new System.Drawing.Size(546, 118);
            this.txtCharacters.TabIndex = 1;
            // 
            // btnGenerate
            // 
            this.btnGenerate.Location = new System.Drawing.Point(12, 312);
            this.btnGenerate.Name = "btnGenerate";
            this.btnGenerate.Size = new System.Drawing.Size(560, 23);
            this.btnGenerate.TabIndex = 7;
            this.btnGenerate.Text = "Generate";
            this.btnGenerate.UseVisualStyleBackColor = true;
            this.btnGenerate.Click += new System.EventHandler(this.btnGenerate_Click);
            // 
            // groupBox2
            // 
            this.groupBox2.Controls.Add(this.chkSeed);
            this.groupBox2.Controls.Add(this.txtSeed);
            this.groupBox2.Controls.Add(this.label4);
            this.groupBox2.Controls.Add(this.txtMaxSequences);
            this.groupBox2.Controls.Add(this.label3);
            this.groupBox2.Controls.Add(this.txtSequenceLength);
            this.groupBox2.Controls.Add(this.txtMinSequences);
            this.groupBox2.Controls.Add(this.label2);
            this.groupBox2.Controls.Add(this.label1);
            this.groupBox2.Location = new System.Drawing.Point(12, 213);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new System.Drawing.Size(560, 93);
            this.groupBox2.TabIndex = 2;
            this.groupBox2.TabStop = false;
            this.groupBox2.Text = "Other Settings";
            // 
            // chkSeed
            // 
            this.chkSeed.AutoSize = true;
            this.chkSeed.Location = new System.Drawing.Point(152, 70);
            this.chkSeed.Name = "chkSeed";
            this.chkSeed.Size = new System.Drawing.Size(15, 14);
            this.chkSeed.TabIndex = 5;
            this.chkSeed.UseVisualStyleBackColor = true;
            // 
            // txtSeed
            // 
            this.txtSeed.Enabled = false;
            this.txtSeed.Location = new System.Drawing.Point(173, 67);
            this.txtSeed.MaxLength = 9;
            this.txtSeed.Name = "txtSeed";
            this.txtSeed.Size = new System.Drawing.Size(88, 20);
            this.txtSeed.TabIndex = 6;
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(6, 70);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(83, 13);
            this.label4.TabIndex = 22;
            this.label4.Text = "Generator seed:";
            // 
            // txtMaxSequences
            // 
            this.txtMaxSequences.Location = new System.Drawing.Point(445, 13);
            this.txtMaxSequences.MaxLength = 3;
            this.txtMaxSequences.Name = "txtMaxSequences";
            this.txtMaxSequences.Size = new System.Drawing.Size(36, 20);
            this.txtMaxSequences.TabIndex = 3;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(278, 16);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(135, 13);
            this.label3.TabIndex = 20;
            this.label3.Text = "Max number of sequences:";
            // 
            // txtSequenceLength
            // 
            this.txtSequenceLength.Location = new System.Drawing.Point(173, 40);
            this.txtSequenceLength.MaxLength = 1;
            this.txtSequenceLength.Name = "txtSequenceLength";
            this.txtSequenceLength.Size = new System.Drawing.Size(36, 20);
            this.txtSequenceLength.TabIndex = 4;
            // 
            // txtMinSequences
            // 
            this.txtMinSequences.Location = new System.Drawing.Point(173, 13);
            this.txtMinSequences.MaxLength = 3;
            this.txtMinSequences.Name = "txtMinSequences";
            this.txtMinSequences.Size = new System.Drawing.Size(36, 20);
            this.txtMinSequences.TabIndex = 2;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(6, 43);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(112, 13);
            this.label2.TabIndex = 21;
            this.label2.Text = "Max sequence length:";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(6, 16);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(132, 13);
            this.label1.TabIndex = 19;
            this.label1.Text = "Min number of sequences:";
            // 
            // groupBox3
            // 
            this.groupBox3.Controls.Add(this.txtExerciseName);
            this.groupBox3.Location = new System.Drawing.Point(12, 13);
            this.groupBox3.Name = "groupBox3";
            this.groupBox3.Size = new System.Drawing.Size(560, 47);
            this.groupBox3.TabIndex = 0;
            this.groupBox3.TabStop = false;
            this.groupBox3.Text = "Exercise Name";
            // 
            // txtExerciseName
            // 
            this.txtExerciseName.Location = new System.Drawing.Point(6, 19);
            this.txtExerciseName.Name = "txtExerciseName";
            this.txtExerciseName.Size = new System.Drawing.Size(547, 20);
            this.txtExerciseName.TabIndex = 0;
            // 
            // GeneratorForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(584, 342);
            this.Controls.Add(this.groupBox3);
            this.Controls.Add(this.groupBox2);
            this.Controls.Add(this.btnGenerate);
            this.Controls.Add(this.groupBox1);
            this.Name = "GeneratorForm";
            this.Text = "Exercise Sequence Generator";
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.groupBox2.ResumeLayout(false);
            this.groupBox2.PerformLayout();
            this.groupBox3.ResumeLayout(false);
            this.groupBox3.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.TextBox txtCharacters;
        private System.Windows.Forms.Button btnGenerate;
        private System.Windows.Forms.GroupBox groupBox2;
        private System.Windows.Forms.CheckBox chkSeed;
        private System.Windows.Forms.TextBox txtSeed;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.TextBox txtMaxSequences;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.TextBox txtSequenceLength;
        private System.Windows.Forms.TextBox txtMinSequences;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.GroupBox groupBox3;
        private System.Windows.Forms.TextBox txtExerciseName;

    }
}

