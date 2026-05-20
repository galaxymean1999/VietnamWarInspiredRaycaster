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

			headingAngle = MathF.PI;
		}

		public Vector2 position;

		public const float fov = MathF.PI / 3;
		public float headingAngle;

		public float movementSpeed = 2f;
		public float rotationSpeed = 1.2f;
		private float reach = 1.5f;

		public bool shot = false;

		public int health;

		// normalising angle of player to be between -PI and +PI
		public void NormaliseHeading() {
			if (headingAngle < -MathF.PI) {
				headingAngle += 2 * MathF.PI;
			}
			else if (headingAngle > MathF.PI) {
				headingAngle -= 2 * MathF.PI;
			}
		}

		public void Update(GameState gs) {
			// check if the players health is too low
			// if the health is too low then restart the level and get the score to 0
			if (health <= 0) {
				position.X = 13.5f;
				position.Y = 13.5f;

				health = 100;

				gs.lvl = new Level(gs.currentLevel);

				gs.score = 0;
			}
		}

		// interact with other entities for example chests by casting a ray that checks if an entity is in front of the player
		// and in reach of the player
		public void Interact(GameState gs) {
			Ray ray = new Ray(position.X, position.Y, headingAngle, (int)(reach / 0.5f), 0.5f, gs);

			int index = gs.lvl.entities.IndexOf(ray.CastEntityRay(0.5f));

			// if entity was hit
			if (index >= 0) {
				switch (gs.lvl.entities[index].type) {
					// chest
					case 2:
						if (Random.Shared.Next(0, 40) <= 40) {
							health = 0;
						}
						else {
							gs.score += 250;

							gs.lvl.entities[index].type = 3;
						}
						break;
					default:
						break;
				}
			}
			
		}

		// shoot by casting a ray that checks if an entity has been hit in a range of the weapon
		// if an entity was hit then score is added and the entity is changed to a dead enemy
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
