namespace RaycasterInWF {
    partial class MenuForm {
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(MenuForm));
            b_play = new Button();
            b_exit = new Button();
            SuspendLayout();
            // 
            // b_play
            // 
            b_play.BackColor = Color.FromArgb(128, 64, 0);
            b_play.Font = new Font("Segoe UI", 36F);
            b_play.ForeColor = Color.FromArgb(192, 192, 0);
            b_play.Location = new Point(199, 279);
            b_play.Name = "b_play";
            b_play.Size = new Size(233, 72);
            b_play.TabIndex = 0;
            b_play.Text = "PLAY";
            b_play.UseVisualStyleBackColor = false;
            b_play.Click += b_play_Click;
            // 
            // b_exit
            // 
            b_exit.BackColor = Color.FromArgb(128, 64, 0);
            b_exit.Font = new Font("Segoe UI", 36F);
            b_exit.ForeColor = Color.FromArgb(192, 192, 0);
            b_exit.Location = new Point(199, 357);
            b_exit.Name = "b_exit";
            b_exit.Size = new Size(233, 72);
            b_exit.TabIndex = 1;
            b_exit.Text = "EXIT";
            b_exit.UseVisualStyleBackColor = false;
            b_exit.Click += b_exit_Click;
            // 
            // MenuForm
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            BackgroundImage = (Image)resources.GetObject("$this.BackgroundImage");
            BackgroundImageLayout = ImageLayout.Stretch;
            ClientSize = new Size(624, 441);
            Controls.Add(b_exit);
            Controls.Add(b_play);
            MaximizeBox = false;
            MaximumSize = new Size(640, 480);
            MinimumSize = new Size(640, 480);
            Name = "MenuForm";
            Text = "Escape from Vietnam 3D";
            ResumeLayout(false);
        }

        #endregion

        private Button b_play;
        private Button b_exit;
    }
}