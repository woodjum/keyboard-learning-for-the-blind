namespace KeyboardLesson
{
    partial class LessonForm
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
            this.Lesson1Intro = new System.Windows.Forms.Label();
            this.textBox1 = new System.Windows.Forms.TextBox();
            this.lb_ToTypeString = new System.Windows.Forms.Label();
            this.lb_ToType = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // Lesson1Intro
            // 
            this.Lesson1Intro.AutoSize = true;
            this.Lesson1Intro.Font = new System.Drawing.Font("Microsoft Sans Serif", 36F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Lesson1Intro.Location = new System.Drawing.Point(21, 9);
            this.Lesson1Intro.MaximumSize = new System.Drawing.Size(950, 0);
            this.Lesson1Intro.Name = "Lesson1Intro";
            this.Lesson1Intro.Size = new System.Drawing.Size(147, 55);
            this.Lesson1Intro.TabIndex = 0;
            this.Lesson1Intro.Text = "第1课";
            // 
            // textBox1
            // 
            this.textBox1.BackColor = System.Drawing.Color.White;
            this.textBox1.Font = new System.Drawing.Font("Microsoft Sans Serif", 180F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.textBox1.Location = new System.Drawing.Point(433, 297);
            this.textBox1.Multiline = true;
            this.textBox1.Name = "textBox1";
            this.textBox1.Size = new System.Drawing.Size(440, 302);
            this.textBox1.TabIndex = 1;
            this.textBox1.Text = "f";
            // 
            // lb_ToTypeString
            // 
            this.lb_ToTypeString.AutoSize = true;
            this.lb_ToTypeString.BackColor = System.Drawing.Color.Orange;
            this.lb_ToTypeString.Font = new System.Drawing.Font("Microsoft Sans Serif", 180F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lb_ToTypeString.Location = new System.Drawing.Point(229, 297);
            this.lb_ToTypeString.Name = "lb_ToTypeString";
            this.lb_ToTypeString.Size = new System.Drawing.Size(181, 272);
            this.lb_ToTypeString.TabIndex = 4;
            this.lb_ToTypeString.Text = "f";
            this.lb_ToTypeString.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // lb_ToType
            // 
            this.lb_ToType.AutoSize = true;
            this.lb_ToType.BackColor = System.Drawing.Color.Khaki;
            this.lb_ToType.Font = new System.Drawing.Font("Microsoft Sans Serif", 15.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lb_ToType.Location = new System.Drawing.Point(157, 297);
            this.lb_ToType.Name = "lb_ToType";
            this.lb_ToType.Size = new System.Drawing.Size(75, 25);
            this.lb_ToType.TabIndex = 3;
            this.lb_ToType.Text = "请按键";
            // 
            // ExerciseForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(946, 659);
            this.Controls.Add(this.Lesson1Intro);
            this.Controls.Add(this.lb_ToTypeString);
            this.Controls.Add(this.lb_ToType);
            this.Controls.Add(this.textBox1);
            this.Name = "ExerciseForm";
            this.Text = "Lesson 1";
            this.Load += new System.EventHandler(this.LessonForm_Load);
            this.FormClosed += new System.Windows.Forms.FormClosedEventHandler(this.LessonForm_FormClosed);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label Lesson1Intro;
        private System.Windows.Forms.TextBox textBox1;
        private System.Windows.Forms.Label lb_ToTypeString;
        private System.Windows.Forms.Label lb_ToType;
    }
}

