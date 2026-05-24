using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace RaycasterInWF {
    public partial class EndForm : Form {
        public EndForm(GameState gs) {
            InitializeComponent();

            this.gs = gs;
        }

        private GameState gs;

        private void EndScreen_Load(object sender, EventArgs e) {
            // display score and highscore and if the current score is higher than the previous highscore
            // or the highscore saved than save the current score

            l_score.Text = "Score: " + gs.score.ToString();

            if (File.Exists("stats")) {
                string highscore = File.ReadAllLines("stats")[0];

                if (gs.score > int.Parse(highscore)) {
                    l_highscore.Text = "Highscore: " + gs.score;

                    File.WriteAllLines("stats", new string[] { gs.score.ToString() });
                }
                else {
                    l_highscore.Text = "Highscore: " + highscore;
                }
            }
            else {
                l_highscore.Text = "Highscore: " + gs.score.ToString();

                File.WriteAllLines("stats", new string[] { gs.score.ToString() });
            }
        }

        private void EndForm_FormClosed(object sender, FormClosedEventArgs e) {
            Application.Exit();
        }
    }
}
