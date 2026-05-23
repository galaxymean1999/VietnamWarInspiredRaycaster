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
    public partial class EndScreen : Form {
        public EndScreen(GameState gs) {
            InitializeComponent();

            this.gs = gs;
        }

        private GameState gs;

        private void EndScreen_Load(object sender, EventArgs e) {
            l_score.Text = "Score: " + gs.score.ToString();

            if (File.Exists("stats")) {
                string highscore = File.ReadAllLines("stats")[0];

                if (gs.score > int.Parse(highscore)) {
                    l_highscore.Text = "Score: " + gs.score;

                    File.WriteAllLines("stats", new string[] { gs.score.ToString() });
                }
                else {
                    l_highscore.Text = "Score: " + highscore;
                }
            }
            else {
                l_highscore.Text = "Score: " + gs.score.ToString();

                File.WriteAllLines("stats", new string[] { gs.score.ToString() });
            }
        }
    }
}
