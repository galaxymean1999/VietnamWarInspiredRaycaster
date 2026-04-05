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
		}

		private GameState gs;

		Player player = new Player(13.5f, 13.5f);

		private List<Keys> keys = new List<Keys>();

		private Image wallTextures;
		private Image entityTextures;

		private float[] zBuffer;

		private void Form1_KeyDown(object sender, KeyEventArgs e) {
			if (!keys.Contains(e.KeyCode)) {
				keys.Add(e.KeyCode);
			}
		}

		private void Form1_KeyUp(object sender, KeyEventArgs e) {
			keys.Remove(e.KeyCode);
		}

		private void Draw(object sender, PaintEventArgs e) {
			Graphics g = e.Graphics;
			g.InterpolationMode = InterpolationMode.NearestNeighbor;

			g.Clear(Color.FromArgb((int)(194), (int)(178), (int)(128)));

			g.Clear(Color.Black);

			//
			// Draw Walls
			//
			int column = 0;

			for (float a = player.headingAngle - Player.fov / 2; a < player.headingAngle + Player.fov / 2; a += Player.fov / ClientSize.Width) {
				Ray ray = new Ray(player.position.X, player.position.Y, a, 200, 0.05f, gs);
				ray = ray.castRay();

				int columnHeight = 0;

				if (ray.length > 0) {
					columnHeight = (int)((float)ClientSize.Height / (ray.length * MathF.Cos(a - player.headingAngle)));

					if (columnHeight > ClientSize.Height) {
						columnHeight = ClientSize.Height;
					}
				}

				int columnY = ClientSize.Height / 2 - columnHeight / 2;

				if (ray.hitWall) {
					int light = 4;

					Rectangle source = new Rectangle(1, 0, 1, 32);

					if (ray.horiVerWall == 'v') {
						source.X = (ray.wallTypeHit - 1) * 32 + (int)((ray.endY - MathF.Floor(ray.endY)) * 32);
					}
					else {
						source.X = (ray.wallTypeHit - 1) * 32 + (int)((ray.endX - MathF.Floor(ray.endX)) * 32);
					}

					Rectangle destination = new Rectangle(column, columnY, 1, columnHeight);

					ImageAttributes attr = new ImageAttributes();
					attr.SetGamma((float)light * ray.length);

					g.DrawImage(wallTextures, destination, source.X, source.Y, source.Width, source.Height, GraphicsUnit.Pixel, attr);

					zBuffer[column] = ray.length;
				}

				column++;
			}

			//
			// Draw Entities
			//
			foreach (Entity entity in gs.lvl.entities) {
				entity.distance = MathF.Sqrt(MathF.Pow(player.position.X -  entity.position.X, 2) + MathF.Pow(player.position.Y - entity.position.Y, 2));
			}
			
			gs.lvl.entities.Sort((b, a) => b.distance.CompareTo(a.distance));

			foreach (Entity entity in gs.lvl.entities) {
				float dx = entity.position.X - player.position.X;
				float dy = entity.position.Y - player.position.Y;

				float angleEntityToPlayer = MathF.PI / 2 + player.headingAngle + MathF.Atan(dx / dy);

				if (angleEntityToPlayer < -MathF.PI) {
					angleEntityToPlayer += 2 * MathF.PI;
				}
				else if (angleEntityToPlayer > MathF.PI) {
					angleEntityToPlayer -= 2 * MathF.PI;
				}

				//float rx = entity.distance * MathF.Sin(angleEntityToPlayer);
				float ry = entity.distance * MathF.Cos(angleEntityToPlayer);

				if (ry > 0) {
					if (angleEntityToPlayer >= - Player.fov / 2 && angleEntityToPlayer <= Player.fov / 2) {
						int height = (int)(ClientSize.Height / (entity.distance));
						if (height > ClientSize.Height) {
							height = ClientSize.Height;
						}
						
						int width = height;

						int screenX = (int)MathF.Abs((int)((angleEntityToPlayer - Player.fov / 2) / Player.fov * ClientSize.Width));

						int startScreenX = screenX - width / 2;

						int screenY = ClientSize.Height / 2 - height / 2;
						
						for (int i = startScreenX; i < startScreenX + width; i++) {
							if (i >= 0 && i < ClientSize.Width) {
								if (entity.distance < zBuffer[i]) {
									Rectangle destination = new Rectangle(i, screenY, 1, height);
									Rectangle source = new Rectangle((int)(32.0f / (float)width * (float)(i - startScreenX)), 0, 1, 32);

									ImageAttributes attr = new ImageAttributes();
									attr.SetGamma((float)1 * entity.distance);

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

			//
			// Draw Minimap
			//
			for (int y = 0; y < gs.lvl.mapHeight; y++) {
				for (int x = 0; x < gs.lvl.mapWidth; x++) {
					if (gs.mapAt(x, y) == 1) {
						g.FillRectangle(Brushes.Gray, x * 10, y * 10, 10, 10);
					}
				}
			}

			g.FillEllipse(Brushes.Red, player.position.X * 10, player.position.Y * 10, 2, 2);
		}

		private void Update(object sender, EventArgs e) {
			if (keys.Contains(Keys.Escape)) {
				this.Close();
			}

			if (keys.Contains(Keys.A)) {
				player.headingAngle -= 0.05f;
				player.normaliseHeading();
			}

			if (keys.Contains(Keys.D)) {
				player.headingAngle += 0.05f;
				player.normaliseHeading();
			}

			if (keys.Contains(Keys.W)) {
				if (gs.mapAt((int)(player.position.X + MathF.Cos(player.headingAngle) / 20), (int)player.position.Y) == 0) {
					player.position.X = player.position.X + MathF.Cos(player.headingAngle) / 20;
				}
				if (gs.mapAt((int)player.position.X, (int)(player.position.Y + MathF.Sin(player.headingAngle) / 20)) == 0) {
					player.position.Y = player.position.Y + MathF.Sin(player.headingAngle) / 20;
				}
			}

			if (keys.Contains(Keys.S)) {
				if (gs.mapAt((int)(player.position.X - MathF.Cos(player.headingAngle) / 20), (int)player.position.Y) == 0) {
					player.position.X = player.position.X - MathF.Cos(player.headingAngle) / 20;
				}
				if (gs.mapAt((int)player.position.X, (int)(player.position.Y - MathF.Sin(player.headingAngle) / 20)) == 0) {
					player.position.Y = player.position.Y - MathF.Sin(player.headingAngle) / 20;
				}
			}

			this.Invalidate();
		}
	}
}
