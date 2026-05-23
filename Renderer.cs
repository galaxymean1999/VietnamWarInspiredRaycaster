using System;
using System.Collections.Generic;
using System.Drawing.Imaging;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RaycasterInWF {
	public class Renderer {
		public Renderer(GameState gameState, Size clientSize) {
			gs = gameState;
			ClientSize = clientSize;
			
			if (File.Exists("textures/wallTextures.png")) {
				wallTextures = Image.FromFile("textures/wallTextures.png");
			}
            if (File.Exists("textures/entityTextures.png")) {
				entityTextures = Image.FromFile("textures/entityTextures.png");
			}

			zBuffer = new float[ClientSize.Width + 1];

			screen = new Bitmap(clientSize.Width, clientSize.Height);
		}

		private GameState gs;

		private Size ClientSize;

		private float[] zBuffer;

		private Image wallTextures;
		private Image entityTextures;

		private const int textureSize = 32;

		private Bitmap screen;

		public void Render(Graphics g) {
			DrawWalls(g);
			DrawEntities(g);
		}

		private void DrawWalls(Graphics g) {
			int column = 0;

			for (float a = gs.player.headingAngle - Player.fov / 2; a < gs.player.headingAngle + Player.fov / 2; a += Player.fov / ClientSize.Width) {
				// casting a new ray from the player position with the max steps 200, a step of 0.05f
				// and with angle a
				Ray ray = new Ray(gs.player.position.X, gs.player.position.Y, a, 200, 0.05f, gs);
				ray = ray.CastRay();

				int columnHeight = 0;

				// calculating column height
				if (ray.length > 0) {
					columnHeight = (int)((float)ClientSize.Height / (ray.length * MathF.Cos(a - gs.player.headingAngle)));
				}

				int columnY = ClientSize.Height / 2 - columnHeight / 2;

				if (ray.hitWall) {
					// rectangle from the texture pallette
					Rectangle source = new Rectangle(1, 0, 1, textureSize);

					if (ray.horiVerWall == 'v') {
						source.X = (ray.wallTypeHit - 1) * textureSize + (int)((ray.endY - MathF.Floor(ray.endY)) * textureSize);
					}
					else {
						source.X = (ray.wallTypeHit - 1) * textureSize + (int)((ray.endX - MathF.Floor(ray.endX)) * textureSize);
					}

					// rectangle where to print on screen
					Rectangle destination = new Rectangle(column, columnY, 1, columnHeight);

					// low level lighting using Gamma
					ImageAttributes attr = new ImageAttributes();
					attr.SetGamma((float)gs.lightLevel * ray.length > 6f ? (float)gs.lightLevel * ray.length : (gs.player.shot ? 3f : 6f));

					g.DrawImage(wallTextures, destination, source.X, source.Y, source.Width, source.Height, GraphicsUnit.Pixel, attr);

					// saving the distance of each column for further use in rendering entities
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

				// corection if the player y is less than entity y
				if (entity.position.Y > gs.player.position.Y) {
					dy = gs.player.position.Y - entity.position.Y;
					dx = gs.player.position.X - entity.position.X;

					angleEntityToPlayer = -MathF.PI / 2 + gs.player.headingAngle + MathF.Atan(dx / dy);
				}

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
									Rectangle source = new Rectangle(textureSize * entity.type + (int)((float)textureSize / (float)width * (float)(i - startScreenX)), 0, 1, textureSize);

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
	}
}
