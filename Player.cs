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

		public void normaliseHeading() {
			if (headingAngle < -MathF.PI) {
				headingAngle += 2 * MathF.PI;
			}
			else if (headingAngle > MathF.PI) {
				headingAngle -= 2 * MathF.PI;
			}
		}
	}
}
