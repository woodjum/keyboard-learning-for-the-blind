namespace KeyboardGame.Views
{
    partial class LevelSelectionView
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
            this.tableLayoutPanel1 = new System.Windows.Forms.TableLayoutPanel();
            this.LevelListBox = new System.Windows.Forms.ListBox();
            this.TitleLabel = new System.Windows.Forms.Label();
            this.tableLayoutPanel1.SuspendLayout();
            this.SuspendLayout();
            // 
            // tableLayoutPanel1
            // 
            this.tableLayoutPanel1.ColumnCount = 3;
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 8.602151F));
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 91.39785F));
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 21F));
            this.tableLayoutPanel1.Controls.Add(this.LevelListBox, 1, 1);
            this.tableLayoutPanel1.Controls.Add(this.TitleLabel, 1, 0);
            this.tableLayoutPanel1.Location = new System.Drawing.Point(0, 0);
            this.tableLayoutPanel1.Name = "tableLayoutPanel1";
            this.tableLayoutPanel1.RowCount = 3;
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 22.82609F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 77.17391F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 20F));
            this.tableLayoutPanel1.Size = new System.Drawing.Size(300, 297);
            this.tableLayoutPanel1.TabIndex = 0;
            // 
            // LevelListBox
            // 
            this.LevelListBox.Dock = System.Windows.Forms.DockStyle.Fill;
            this.LevelListBox.FormattingEnabled = true;
            this.LevelListBox.ItemHeight = 20;
            this.LevelListBox.Location = new System.Drawing.Point(27, 66);
            this.LevelListBox.Name = "LevelListBox";
            this.LevelListBox.Size = new System.Drawing.Size(249, 204);
            this.LevelListBox.TabIndex = 0;
            // 
            // TitleLabel
            // 
            this.TitleLabel.Anchor = System.Windows.Forms.AnchorStyles.Bottom;
            this.TitleLabel.AutoSize = true;
            this.TitleLabel.Font = new System.Drawing.Font("Microsoft Sans Serif", 24F, System.Drawing.FontStyle.Bold);
            this.TitleLabel.Location = new System.Drawing.Point(40, 26);
            this.TitleLabel.Name = "TitleLabel";
            this.TitleLabel.Size = new System.Drawing.Size(222, 37);
            this.TitleLabel.TabIndex = 1;
            this.TitleLabel.Text = "选 择 场 比 赛";
            this.TitleLabel.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // LevelSelectionView
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(9F, 20F);
            this.Controls.Add(this.tableLayoutPanel1);
            this.Name = "LevelSelectionView";
            this.tableLayoutPanel1.ResumeLayout(false);
            this.tableLayoutPanel1.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel1;
        private System.Windows.Forms.ListBox LevelListBox;
        private System.Windows.Forms.Label TitleLabel;
    }
}
