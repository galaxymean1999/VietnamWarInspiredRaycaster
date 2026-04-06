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

			gs = new GameState();

			this.DoubleBuffered = true;

			wallTextures = Image.FromFile("textures/wallTextures.png");
			entityTextures = Image.FromFile("textures/entityTextures.png");

			zBuffer = new float[ClientSize.Width + 1];

			this.Text = "Vietnam Raycaster";

			l_level.Text = (gs.currentLevel + 1).ToString();

			l_score.Text = (gs.score).ToString();

			sw.Start();
		}

		private GameState gs;

		private List<Keys> keys = new List<Keys>();

		private Image wallTextures;
		private Image entityTextures;

		private float[] zBuffer;

		private float dt = 1;

		private Stopwatch sw = new Stopwatch();

		private void Form1_KeyDown(object sender, KeyEventArgs e) {
			if (!keys.Contains(e.KeyCode)) {
				keys.Add(e.KeyCode);
			}

			if (e.KeyCode == Keys.ControlKey) {
				gs.player.Shoot(gs);
				gs.lightLevel = 1;
			}
		}

		private void Form1_KeyUp(object sender, KeyEventArgs e) {
			keys.Remove(e.KeyCode);
		}

		private void Draw(object sender, PaintEventArgs e) {
			sw.Restart();

			Graphics g = e.Graphics;
			g.InterpolationMode = InterpolationMode.NearestNeighbor;

			g.Clear(Color.Black);

			DrawWalls(g);
			DrawEntities(g);
			DrawMinimap(g);

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
				//
				// move with wall sliding
				//

				if (gs.MapAt((int)(gs.player.position.X + MathF.Cos(gs.player.headingAngle) / 50 * dt), (int)gs.player.position.Y) == 0) {
					gs.player.position.X = gs.player.position.X + MathF.Cos(gs.player.headingAngle) / 50 * dt;
				}
				if (gs.MapAt((int)gs.player.position.X, (int)(gs.player.position.Y + MathF.Sin(gs.player.headingAngle) / 50 * dt)) == 0) {
					gs.player.position.Y = gs.player.position.Y + MathF.Sin(gs.player.headingAngle) / 50 * dt;
				}

				//
				// check if ladder in front
				//

				if (new Ray(gs.player.position.X, gs.player.position.Y, gs.player.headingAngle, 10, 0.01f, gs).CastRay().wallTypeHit == 2) {
					gs.currentLevel++;
					gs.lvl = new Level(gs.currentLevel);

					gs.player = new Player(13.5f, 13.5f);
				}
			}

			if (keys.Contains(Keys.Down)) {
				//
				// move with wall sliding
				//

				if (gs.MapAt((int)(gs.player.position.X - MathF.Cos(gs.player.headingAngle) / 50 * dt), (int)gs.player.position.Y) == 0) {
					gs.player.position.X = gs.player.position.X - MathF.Cos(gs.player.headingAngle) / 50 * dt;
				}
				if (gs.MapAt((int)gs.player.position.X, (int)(gs.player.position.Y - MathF.Sin(gs.player.headingAngle) / 50 * dt)) == 0) {
					gs.player.position.Y = gs.player.position.Y - MathF.Sin(gs.player.headingAngle) / 50 * dt;
				}
			}

			gs.SortEntities();

			l_score.Text = (gs.score).ToString();
			l_level.Text = (gs.currentLevel + 1).ToString();

			if (gs.player.shot) {
				gs.shootLightTimer--;
			}
			if (gs.shootLightTimer < 0) {
				gs.player.shot = false;
				gs.shootLightTimer = 5;
				gs.lightLevel = 4;
			}
			
			this.Invalidate();
		}

		private void DrawWalls(Graphics g) {
			int column = 0;

			for (float a = gs.player.headingAngle - Player.fov / 2; a < gs.player.headingAngle + Player.fov / 2; a += Player.fov / ClientSize.Width) {
				Ray ray = new Ray(gs.player.position.X, gs.player.position.Y, a, 200, 0.05f, gs);
				ray = ray.CastRay();

				int columnHeight = 0;

				if (ray.length > 0) {
					columnHeight = (int)((float)ClientSize.Height / (ray.length * MathF.Cos(a - gs.player.headingAngle)));

					if (columnHeight > ClientSize.Height) {
						columnHeight = ClientSize.Height;
					}
				}

				int columnY = ClientSize.Height / 2 - columnHeight / 2;

				if (ray.hitWall) {

					Rectangle source = new Rectangle(1, 0, 1, 32);

					if (ray.horiVerWall == 'v') {
						source.X = (ray.wallTypeHit - 1) * 32 + (int)((ray.endY - MathF.Floor(ray.endY)) * 32);
					}
					else {
						source.X = (ray.wallTypeHit - 1) * 32 + (int)((ray.endX - MathF.Floor(ray.endX)) * 32);
					}

					Rectangle destination = new Rectangle(column, columnY, 1, columnHeight);

					ImageAttributes attr = new ImageAttributes();
					attr.SetGamma((float)gs.lightLevel * ray.length > 6f ? (float)gs.lightLevel * ray.length : (gs.player.shot ? 3f : 6f));

					g.DrawImage(wallTextures, destination, source.X, source.Y, source.Width, source.Height, GraphicsUnit.Pixel, attr);

					zBuffer[column] = ray.length;
				}

				column++;
			}
		}

		private void DrawEntities(Graphics g) {
			foreach (Entity entity in gs.lvl.entities) {
				// distance along x and y
				float dx = entity.position.X - gs.player.position.X;
				float dy = entity.position.Y - gs.player.position.Y;

				// realtive angle between player heading and entity
				float angleEntityToPlayer = MathF.PI / 2 + gs.player.headingAngle + MathF.Atan(dx / dy);
				
				//             |
				// NESAHAT !!! V
				if (entity.position.Y > gs.player.position.Y) {
					dy = gs.player.position.Y - entity.position.Y;
					dx = gs.player.position.X - entity.position.X;

					angleEntityToPlayer = -MathF.PI / 2 + gs.player.headingAngle + MathF.Atan(dx / dy);
				}
				// NESAHAT !!! A
				//             |

				// normalisation of relative angle
				if (angleEntityToPlayer < -MathF.PI) {
					angleEntityToPlayer += 2 * MathF.PI;
				}
				else if (angleEntityToPlayer > MathF.PI) {
					angleEntityToPlayer -= 2 * MathF.PI;
				}

				// converting to relative coordinates
				float rx = entity.distance * MathF.Sin(angleEntityToPlayer);
				float ry = entity.distance * MathF.Cos(angleEntityToPlayer);

				// if behind us skip it
				if (ry > 0) {
					// if entity not in player fov
					if (angleEntityToPlayer >= -Player.fov / 2 && angleEntityToPlayer <= Player.fov / 2) {
						// calculate height of entity
						int height = (int)(ClientSize.Height / (entity.distance));
						if (height > ClientSize.Height) {
							height = ClientSize.Height;
						}

						int width = height;

						// screen x position
						int screenX = (int)MathF.Abs((int)((angleEntityToPlayer - Player.fov / 2) / Player.fov * ClientSize.Width));

						if (entity.position.Y > gs.player.position.Y) {
							screenX = (int)MathF.Abs((int)((angleEntityToPlayer - Player.fov / 2) / Player.fov * ClientSize.Width));
						}

						int startScreenX = screenX - width / 2;

						int screenY = ClientSize.Height / 2 - height / 2;

						// drawing the entity
						for (int i = startScreenX; i < startScreenX + width; i++) {
							// if offscreen then skip
							if (i >= 0 && i < ClientSize.Width) {
								//if behind a wall then skip
								if (entity.distance < zBuffer[i]) {
									Rectangle destination = new Rectangle(i, screenY, 1, height);
									Rectangle source = new Rectangle(32 * entity.type + (int)(32.0f / (float)width * (float)(i - startScreenX)), 0, 1, 32);

									// light to distance
									ImageAttributes attr = new ImageAttributes();

									if (gs.lightLevel - 1 > 0) {
										attr.SetGamma((float)gs.lightLevel * entity.distance - 2 > 1f ? (float)gs.lightLevel * entity.distance - 2 : 1f);
									}
									else {
										attr.SetGamma((float)0.5f * entity.distance > 1f ? (float)0.5f * entity.distance : 1f);
									}
										

									g.DrawImage(entityTextures, destination, source.X, source.Y, source.Width, source.Height, GraphicsUnit.Pixel, attr);
								}
								else {
									continue;
								}
							}

						}
					}
				}
			}
		}

		private void DrawMinimap(Graphics g) {
			for (int y = 0; y < gs.lvl.mapHeight; y++) {
				for (int x = 0; x < gs.lvl.mapWidth; x++) {
					if (gs.MapAt(x, y) >= 1) {
						g.FillRectangle(Brushes.Gray, x * 10, y * 10, 10, 10);
					}
				}
			}

			g.FillEllipse(Brushes.Red, gs.player.position.X * 10, gs.player.position.Y * 10, 2, 2);
		}
	}
}
