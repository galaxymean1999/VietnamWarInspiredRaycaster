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

			List<Entity> rE = gs.lvl.entities;

			float step = 0.5f;

			float stepX = step * MathF.Cos(headingAngle);
			float stepY = step * MathF.Sin(headingAngle);

			RectangleF bullet = new RectangleF(position.X, position.Y, 0.5f, 0.5f);

			bool bulletHit = false;

			for (int i = 0; i < 10; i++) {
				foreach (Entity e in gs.lvl.entities) {
					if (bullet.IntersectsWith(e.boundingBox) && e.type == 0) {
						e.type = 1;

						gs.score += 100;

						bulletHit = true;
						break;
					}
				}

				if (gs.MapAt((int)bullet.X, (int)bullet.Y) > 0) {
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
