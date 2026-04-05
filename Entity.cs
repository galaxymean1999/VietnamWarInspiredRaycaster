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
		}

		public Vector2 position;

		public float distance;

		public int type;
	}
}
