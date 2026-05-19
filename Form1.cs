using System.Diagnostics;
using System.Drawing.Drawing2D;
using System.Drawing.Imaging;
using System.Numerics;
using System.Windows.Forms;

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

			sw.Start();
		}

		private GameState gs;

		private List<Keys> keys = new List<Keys>();

		private float dt = 1;

		private Stopwatch sw = new Stopwatch();

		Renderer renderer;

		private void Form1_KeyDown(object sender, KeyEventArgs e) {
			// if the pressed key isn't in the buffer then add it
			if (!keys.Contains(e.KeyCode)) {
				keys.Add(e.KeyCode);
			}

			if (e.KeyCode == Keys.Space || e.KeyCode == Keys.ControlKey) {
				// if control pressed then shoot enemies
				if (e.KeyCode == Keys.ControlKey) {
					gs.player.Shoot(gs);
				}

				// if space pressed then interact with the entity that is in range in front of the player
				if (e.KeyCode == Keys.Space) {
					gs.player.Interact(gs);
				}
			}
		}

		private void Form1_KeyUp(object sender, KeyEventArgs e) {
			// remove key from the buffer
			keys.Remove(e.KeyCode);
		}

		private void Draw(object sender, PaintEventArgs e) {
			sw.Restart();

			Graphics g = e.Graphics;
			// the most primitive interpolation mode to get the fastest results
			g.InterpolationMode = InterpolationMode.NearestNeighbor;

			g.Clear(Color.Black);

			// render walls and entities
			renderer.Render(g);

			sw.Stop();

			dt = sw.ElapsedMilliseconds / 10;
		}

		private void Update(object sender, EventArgs e) {
			if (keys.Contains(Keys.Escape)) {
				this.Close();
			}

			//
			// movement
			//

			if (keys.Contains(Keys.Left)) {
				gs.player.headingAngle -= 0.02f * dt;
				gs.player.NormaliseHeading();

			}

			if (keys.Contains(Keys.Right)) {
				gs.player.headingAngle += 0.02f * dt;
				gs.player.NormaliseHeading();
			}

			if (keys.Contains(Keys.Up)) {
				// move with wall sliding
				if (gs.MapAt((int)(gs.player.position.X + MathF.Cos(gs.player.headingAngle) / 50 * dt), (int)gs.player.position.Y) == 0) {
					gs.player.position.X = gs.player.position.X + MathF.Cos(gs.player.headingAngle) / 50 * dt;
				}
				if (gs.MapAt((int)gs.player.position.X, (int)(gs.player.position.Y + MathF.Sin(gs.player.headingAngle) / 50 * dt)) == 0) {
					gs.player.position.Y = gs.player.position.Y + MathF.Sin(gs.player.headingAngle) / 50 * dt;
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
				if (gs.MapAt((int)(gs.player.position.X - MathF.Cos(gs.player.headingAngle) / 50 * dt), (int)gs.player.position.Y) == 0) {
					gs.player.position.X = gs.player.position.X - MathF.Cos(gs.player.headingAngle) / 50 * dt;
				}
				if (gs.MapAt((int)gs.player.position.X, (int)(gs.player.position.Y - MathF.Sin(gs.player.headingAngle) / 50 * dt)) == 0) {
					gs.player.position.Y = gs.player.position.Y - MathF.Sin(gs.player.headingAngle) / 50 * dt;
				}
			}

			// sort entites by their distance to player
			gs.SortEntities();

			// display score level information etc
			l_score.Text = (gs.score).ToString();
			l_level.Text = (gs.currentLevel + 1).ToString();
			l_health.Text = (gs.player.health).ToString();

			// player position for debug pusposes
			l_playerPos.Text = $"x: {gs.player.position.X} y: {gs.player.position.Y}";

			// primitive muzzle flash from shooting with just more light 
			if (gs.player.shot) {
				gs.shootLightTimer--;
			}
			if (gs.shootLightTimer < 0) {
				gs.player.shot = false;
				gs.shootLightTimer = 5;
				gs.lightLevel = 4;
			}

			// repaint the canvas
			canvas.Invalidate();
		}
	}
}
