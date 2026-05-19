namespace RaycasterInWF
{
    partial class Form1
    {
        /// <summary>
        ///  Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        ///  Clean up any resources being used.
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
		///  Required method for Designer support - do not modify
		///  the contents of this method with the code editor.
		/// </summary>
		private void InitializeComponent() {
			components = new System.ComponentModel.Container();
			updateTimer = new System.Windows.Forms.Timer(components);
			hudPanel = new Panel();
			l_health = new Label();
			l_healthHeading = new Label();
			l_playerPos = new Label();
			l_score = new Label();
			l_level = new Label();
			l_levelHeading = new Label();
			l_scoreHeading = new Label();
			canvas = new PictureBox();
			hudPanel.SuspendLayout();
			((System.ComponentModel.ISupportInitialize)canvas).BeginInit();
			SuspendLayout();
			// 
			// updateTimer
			// 
			updateTimer.Enabled = true;
			updateTimer.Interval = 1;
			updateTimer.Tick += Update;
			// 
			// hudPanel
			// 
			hudPanel.Anchor = AnchorStyles.Bottom | AnchorStyles.Left;
			hudPanel.BackColor = Color.FromArgb(0, 64, 0);
			hudPanel.Controls.Add(l_health);
			hudPanel.Controls.Add(l_healthHeading);
			hudPanel.Controls.Add(l_playerPos);
			hudPanel.Controls.Add(l_score);
			hudPanel.Controls.Add(l_level);
			hudPanel.Controls.Add(l_levelHeading);
			hudPanel.Controls.Add(l_scoreHeading);
			hudPanel.Location = new Point(0, 370);
			hudPanel.Name = "hudPanel";
			hudPanel.Size = new Size(624, 71);
			hudPanel.TabIndex = 0;
			// 
			// l_health
			// 
			l_health.Anchor = AnchorStyles.Top | AnchorStyles.Right;
			l_health.BackColor = Color.Transparent;
			l_health.Font = new Font("Segoe UI", 15F, FontStyle.Bold, GraphicsUnit.Point, 0);
			l_health.ForeColor = Color.White;
			l_health.Location = new Point(234, 28);
			l_health.Name = "l_health";
			l_health.Size = new Size(67, 28);
			l_health.TabIndex = 6;
			l_health.Text = "0";
			l_health.TextAlign = ContentAlignment.MiddleRight;
			// 
			// l_healthHeading
			// 
			l_healthHeading.AutoSize = true;
			l_healthHeading.BackColor = Color.Transparent;
			l_healthHeading.Font = new Font("Segoe UI", 15F, FontStyle.Bold, GraphicsUnit.Point, 0);
			l_healthHeading.ForeColor = Color.FromArgb(128, 255, 128);
			l_healthHeading.Location = new Point(206, 2);
			l_healthHeading.Name = "l_healthHeading";
			l_healthHeading.Size = new Size(88, 28);
			l_healthHeading.TabIndex = 5;
			l_healthHeading.Text = "HEALTH";
			// 
			// l_playerPos
			// 
			l_playerPos.Anchor = AnchorStyles.Top | AnchorStyles.Right;
			l_playerPos.BackColor = Color.Transparent;
			l_playerPos.Font = new Font("Segoe UI", 15F, FontStyle.Bold, GraphicsUnit.Point, 0);
			l_playerPos.ForeColor = Color.White;
			l_playerPos.Location = new Point(311, 28);
			l_playerPos.Name = "l_playerPos";
			l_playerPos.Size = new Size(301, 28);
			l_playerPos.TabIndex = 4;
			l_playerPos.Text = "0";
			l_playerPos.TextAlign = ContentAlignment.MiddleRight;
			// 
			// l_score
			// 
			l_score.Anchor = AnchorStyles.Top | AnchorStyles.Right;
			l_score.BackColor = Color.Transparent;
			l_score.Font = new Font("Segoe UI", 15F, FontStyle.Bold, GraphicsUnit.Point, 0);
			l_score.ForeColor = Color.White;
			l_score.Location = new Point(101, 28);
			l_score.Name = "l_score";
			l_score.Size = new Size(67, 28);
			l_score.TabIndex = 3;
			l_score.Text = "0";
			l_score.TextAlign = ContentAlignment.MiddleRight;
			// 
			// l_level
			// 
			l_level.Anchor = AnchorStyles.Top | AnchorStyles.Right;
			l_level.BackColor = Color.Transparent;
			l_level.Font = new Font("Segoe UI", 15F, FontStyle.Bold, GraphicsUnit.Point, 0);
			l_level.ForeColor = Color.White;
			l_level.Location = new Point(12, 28);
			l_level.Name = "l_level";
			l_level.Size = new Size(67, 28);
			l_level.TabIndex = 2;
			l_level.Text = "0";
			l_level.TextAlign = ContentAlignment.MiddleRight;
			// 
			// l_levelHeading
			// 
			l_levelHeading.AutoSize = true;
			l_levelHeading.BackColor = Color.Transparent;
			l_levelHeading.Font = new Font("Segoe UI", 15F, FontStyle.Bold, GraphicsUnit.Point, 0);
			l_levelHeading.ForeColor = Color.FromArgb(128, 255, 128);
			l_levelHeading.Location = new Point(12, 0);
			l_levelHeading.Name = "l_levelHeading";
			l_levelHeading.Size = new Size(67, 28);
			l_levelHeading.TabIndex = 1;
			l_levelHeading.Text = "LEVEL";
			// 
			// l_scoreHeading
			// 
			l_scoreHeading.AutoSize = true;
			l_scoreHeading.BackColor = Color.Transparent;
			l_scoreHeading.Font = new Font("Segoe UI", 15F, FontStyle.Bold, GraphicsUnit.Point, 0);
			l_scoreHeading.ForeColor = Color.FromArgb(128, 255, 128);
			l_scoreHeading.Location = new Point(101, 0);
			l_scoreHeading.Name = "l_scoreHeading";
			l_scoreHeading.Size = new Size(74, 28);
			l_scoreHeading.TabIndex = 0;
			l_scoreHeading.Text = "SCORE";
			// 
			// canvas
			// 
			canvas.BackColor = Color.Black;
			canvas.Location = new Point(0, 0);
			canvas.Margin = new Padding(3, 2, 3, 2);
			canvas.Name = "canvas";
			canvas.Size = new Size(624, 370);
			canvas.TabIndex = 1;
			canvas.TabStop = false;
			canvas.Paint += Draw;
			// 
			// Form1
			// 
			AutoScaleDimensions = new SizeF(7F, 15F);
			AutoScaleMode = AutoScaleMode.Font;
			ClientSize = new Size(624, 441);
			Controls.Add(canvas);
			Controls.Add(hudPanel);
			MaximizeBox = false;
			Name = "Form1";
			Text = "Raycaster";
			KeyDown += Form1_KeyDown;
			KeyUp += Form1_KeyUp;
			hudPanel.ResumeLayout(false);
			hudPanel.PerformLayout();
			((System.ComponentModel.ISupportInitialize)canvas).EndInit();
			ResumeLayout(false);
		}

		#endregion

		private System.Windows.Forms.Timer updateTimer;
		private Panel hudPanel;
		private Label l_scoreHeading;
		private Label l_score;
		private Label l_level;
		private Label l_levelHeading;
		private Label l_playerPos;
        private PictureBox canvas;
        private Label l_health;
        private Label l_healthHeading;
    }
}
