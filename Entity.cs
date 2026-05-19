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

			boundingBox = new RectangleF(this.position.X, this.position.Y, 0.5f, 0.5f);
		}

		public RectangleF boundingBox;

		public Vector2 position;

		public float distance;

		public int type;

		public	void UpdateEntity() {
			boundingBox.X = this.position.X - 0.5f;
			boundingBox.Y = this.position.Y - 0.5f;
		}
	}
}
