using System;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;
using System.Text;
using System.Threading.Tasks;

namespace RaycasterInWF {
	public class Entity {
		public Entity(float x, float y, int type) {
			this.type = type;
			this.position.X = x;
			this.position.Y = y;

			boundingBox = new RectangleF(this.position.X - 0.25f, this.position.Y - 0.25f, 0.5f, 0.5f);
		}

		public RectangleF boundingBox;

		public Vector2 position;

		public float distance;

		public int type;

		private int shootCooldown = cooldown;

		private const int cooldown = 40;

		public bool shot = false;

		public	void Update(GameState gs) {
			boundingBox.X = this.position.X - 0.25f;
			boundingBox.Y = this.position.Y - 0.25f;

			if (type == 0 && !shot) {
				Shoot(gs);
			}

			if (shot) {
				shootCooldown--;
			}
			
			if (shootCooldown <= 0) {
				shot = false;
				shootCooldown = cooldown;
			}
		}

		private void Shoot(GameState gs) {
			shot = true;

			float dx = gs.player.position.X - position.X;
			float dy = gs.player.position.Y - position.Y;

            float angle = MathF.Atan2(dy, dx);

			int index = gs.lvl.entities.IndexOf(new Ray(position.X, position.Y, angle, 30, 0.8f, gs).CastEntityRay(0.25f, this)); 
			
			if (index == -1) {
				return;
			}

			if (gs.lvl.entities[index].type == 100) {
				gs.player.health -= 15;
			}
		}
	}
}
