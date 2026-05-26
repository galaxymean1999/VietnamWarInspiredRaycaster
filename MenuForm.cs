namespace RaycasterInWF {
    public partial class MenuForm : Form {
        public MenuForm() {
            InitializeComponent();
        }

        GameForm gf;

        private void b_play_Click(object sender, EventArgs e) {
            gf = new GameForm(this);

            this.Hide();
            gf.Show();
        }

        private void b_exit_Click(object sender, EventArgs e) {
            this.Close();
        }
    }
}
