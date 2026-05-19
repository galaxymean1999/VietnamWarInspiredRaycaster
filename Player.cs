using System;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace RaycasterInWF {
	public class Player {
		public Player(float x, float y) {
			position.X = x;
			position.Y = y;

			health = 100;
		}

		public Vector2 position;

		public const float fov = MathF.PI / 3;

		public float headingAngle = MathF.PI;

		public bool shot = false;

		public int health;

		private float reach = 1.5f;

		// normalising angle of player to be between -PI and +PI
		public void NormaliseHeading() {
			if (headingAngle < -MathF.PI) {
				headingAngle += 2 * MathF.PI;
			}
			else if (headingAngle > MathF.PI) {
				headingAngle -= 2 * MathF.PI;
			}
		}

		public void Interact(GameState gs) {
			Ray ray = new Ray(position.X, position.Y, headingAngle, (int)(reach / 0.5f), 0.5f, gs);

			int index = gs.lvl.entities.IndexOf(ray.CastEntityRay(0.5f));

			// if entity was hit
			if (index >= 0) {
				switch (gs.lvl.entities[index].type) {
					// chest
					case 2:
						if (Random.Shared.Next(0, 100) <= 40) {
							health = 0;
						}
						else {
							gs.score += 250;
						}

						// to do: change sprite to an opened chest
						break;
					default:
						break;
				}
			}
			
		}

		public void Shoot(GameState gs) {
			shot = true;
			
			gs.lightLevel = 1;

			Ray ray = new Ray(position.X, position.Y, headingAngle, 12, 0.5f, gs);

			int index = gs.lvl.entities.IndexOf(ray.CastEntityRay(0.5f));

			// check if an entity was hit
			if (index >= 0) {
				if (gs.lvl.entities[index].type == 0) {
					gs.lvl.entities[index].type = 1;
					gs.score += 100;
				}
			}
		}
	}
}
