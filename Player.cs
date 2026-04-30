using System;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;
using System.Text;
using System.Threading.Tasks;

namespace RaycasterInWF {
	public class Player {
		public Player(float x, float y) {
			position.X = x;
			position.Y = y;
		}

		public Vector2 position;

		public const float fov = MathF.PI / 3;

		public float headingAngle = MathF.PI;

		public bool shot = false;

		// normalising angle of player to be between -PI and +PI
		public void NormaliseHeading() {
			if (headingAngle < -MathF.PI) {
				headingAngle += 2 * MathF.PI;
			}
			else if (headingAngle > MathF.PI) {
				headingAngle -= 2 * MathF.PI;
			}
		}

		public void Shoot(GameState gs) {
			shot = true;

			float step = 0.5f;

			// calculating step size on both axis
			float stepX = step * MathF.Cos(headingAngle);
			float stepY = step * MathF.Sin(headingAngle);

			// bullet rectangle
			RectangleF bullet = new RectangleF(position.X, position.Y, 0.5f, 0.5f);

			bool bulletHit = false;

			for (int i = 0; i < 10; i++) {
				foreach (Entity e in gs.lvl.entities) {
					// checking if the bullet intersects with entity
					if (bullet.IntersectsWith(e.boundingBox) && e.type == 0) {
						e.type = 1;

						gs.score += 100;

						bulletHit = true;
						break;
					}
				}

				// if the bullet hits a wall end
				if (gs.MapAt((int)(bullet.X + bullet.Width / 2), (int)(bullet.Y + bullet.Height / 2)) > 0) {
					bulletHit = true;
				}
				else {
					bullet.X += stepX;
					bullet.Y += stepY;
				}

				if (bulletHit) {
					break;
				}
			}
		}
	}
}
