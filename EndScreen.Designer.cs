namespace RaycasterInWF {
    partial class EndScreen {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing) {
            if (disposing && (components != null)) {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent() {
            l_heading = new Label();
            l_score = new Label();
            l_highscore = new Label();
            SuspendLayout();
            // 
            // l_heading
            // 
            l_heading.AutoSize = true;
            l_heading.Font = new Font("Segoe UI", 32F, FontStyle.Bold);
            l_heading.ForeColor = Color.FromArgb(192, 192, 0);
            l_heading.Location = new Point(14, 34);
            l_heading.Name = "l_heading";
            l_heading.Size = new Size(594, 59);
            l_heading.TabIndex = 0;
            l_heading.Text = "YOU MANAGED TO ESCAPE!";
            // 
            // l_score
            // 
            l_score.AutoSize = true;
            l_score.Font = new Font("Segoe UI", 24F);
            l_score.ForeColor = Color.FromArgb(192, 192, 0);
            l_score.Location = new Point(14, 172);
            l_score.Name = "l_score";
            l_score.Size = new Size(132, 45);
            l_score.TabIndex = 1;
            l_score.Text = "Score: 0";
            // 
            // l_highscore
            // 
            l_highscore.AutoSize = true;
            l_highscore.Font = new Font("Segoe UI", 24F);
            l_highscore.ForeColor = Color.FromArgb(192, 192, 0);
            l_highscore.Location = new Point(14, 286);
            l_highscore.Name = "l_highscore";
            l_highscore.Size = new Size(197, 45);
            l_highscore.TabIndex = 2;
            l_highscore.Text = "Highscore: 0";
            // 
            // EndScreen
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            BackColor = Color.FromArgb(0, 64, 0);
            ClientSize = new Size(624, 441);
            Controls.Add(l_highscore);
            Controls.Add(l_score);
            Controls.Add(l_heading);
            MaximizeBox = false;
            MaximumSize = new Size(640, 480);
            MinimumSize = new Size(640, 480);
            Name = "EndScreen";
            Text = "Escape from Vietnam 3D";
            Load += EndScreen_Load;
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private Label l_heading;
        private Label l_score;
        private Label l_highscore;
    }
}