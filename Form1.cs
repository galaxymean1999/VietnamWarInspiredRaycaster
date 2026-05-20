using System.Diagnostics;
using System.Drawing.Drawing2D;
using System.Drawing.Imaging;
using System.Numerics;
using System.Windows.Forms;
using System.Xml;

namespace RaycasterInWF
{
	public partial class Form1 : Form {
		public Form1() {
			InitializeComponent();

			// setting up the game
			gs = new GameState();

			this.DoubleBuffered = true;

			this.Text = "Vietnam Raycaster";

			l_level.Text = (gs.currentLevel + 1).ToString();

			l_score.Text = (gs.score).ToString();

			renderer = new Renderer(gs, canvas.ClientSize);
		}

		private GameState gs;

		private List<Keys> keys = new List<Keys>();

		private float dt = 1;

		Renderer renderer;

		private void Form1_KeyDown(object sender, KeyEventArgs e) {
			// if the pressed key isn't in the buffer then add it
			if (!keys.Contains(e.KeyCode)) {
				keys.Add(e.KeyCode);
			}

			// if control pressed then shoot
			if (e.KeyCode == Keys.ControlKey) {
				gs.player.Shoot(gs);
			}

			// if space pressed then interact with entities
			if (e.KeyCode == Keys.Space) {
				gs.player.Interact(gs);
			}
		}

		private void Form1_KeyUp(object sender, KeyEventArgs e) {
			// remove key from the buffer
			keys.Remove(e.KeyCode);
		}

		private void Draw(object sender, PaintEventArgs e) {
			DateTime timeBefore = DateTime.Now;

			Graphics g = e.Graphics;
			// the most primitive interpolation mode to get the fastest results
			g.InterpolationMode = InterpolationMode.NearestNeighbor;

			g.Clear(Color.Black);

			// render walls and entities
			renderer.Render(g);

			dt = (DateTime.Now.Millisecond - timeBefore.Millisecond) / 1000.0f;

			if (dt <= 0) {
				dt = 0;
			}
		}

		private void Update(object sender, EventArgs e) {
			if (keys.Contains(Keys.Escape)) {
				this.Close();
			}

			//
			// movement
			//

			if (keys.Contains(Keys.Left)) {
				gs.player.headingAngle -= gs.player.rotationSpeed * dt;
				gs.player.NormaliseHeading();

			}

			if (keys.Contains(Keys.Right)) {
				gs.player.headingAngle += gs.player.rotationSpeed * dt;
				gs.player.NormaliseHeading();
			}

			if (keys.Contains(Keys.Up)) {
				// move with wall sliding
				if (gs.MapAt((int)(gs.player.position.X + MathF.Cos(gs.player.headingAngle) * gs.player.movementSpeed * dt), (int)gs.player.position.Y) == 0) {
					gs.player.position.X = gs.player.position.X + MathF.Cos(gs.player.headingAngle) * gs.player.movementSpeed * dt;
				}
				if (gs.MapAt((int)gs.player.position.X, (int)(gs.player.position.Y + MathF.Sin(gs.player.headingAngle) * gs.player.movementSpeed * dt)) == 0) {
					gs.player.position.Y = gs.player.position.Y + MathF.Sin(gs.player.headingAngle) * gs.player.movementSpeed * dt;
				}

				// check if ladder in front
				if (new Ray(gs.player.position.X, gs.player.position.Y, gs.player.headingAngle, 10, 0.01f, gs).CastRay().wallTypeHit == 2) {
					gs.currentLevel++;
					gs.lvl = new Level(gs.currentLevel);

					gs.player = new Player(13.5f, 13.5f);
				}
			}

			if (keys.Contains(Keys.Down)) {
				// move with wall sliding
				if (gs.MapAt((int)(gs.player.position.X - MathF.Cos(gs.player.headingAngle) * gs.player.movementSpeed * dt), (int)gs.player.position.Y) == 0) {
					gs.player.position.X = gs.player.position.X - MathF.Cos(gs.player.headingAngle) * gs.player.movementSpeed * dt;
				}
				if (gs.MapAt((int)gs.player.position.X, (int)(gs.player.position.Y - MathF.Sin(gs.player.headingAngle) * gs.player.movementSpeed * dt)) == 0) {
					gs.player.position.Y = gs.player.position.Y - MathF.Sin(gs.player.headingAngle) * gs.player.movementSpeed * dt;
				}
			}

			// display score level information etc
			l_score.Text = (gs.score).ToString();
			l_level.Text = (gs.currentLevel + 1).ToString();
			l_health.Text = (gs.player.health).ToString();

			// player position for debug pusposes
			l_playerPos.Text = $"x: {MathF.Round(gs.player.position.X, 2)} y: {MathF.Round(gs.player.position.Y, 2)} dt: {MathF.Round(dt, 3)}";

			gs.Update();

			// repaint the canvas
			canvas.Invalidate();
		}
	}
}
