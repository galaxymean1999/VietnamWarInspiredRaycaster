using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RaycasterInWF {
	public class Ray {
		public Ray(float x, float y, float angle, int maxSteps, float stepSize, GameState gs) {
			startX = x; startY = y;

			this.angle = angle;
			this.maxSteps = maxSteps;
			this.stepSize = stepSize;

			this.gs = gs;
		}

		private int maxSteps;
		private float stepSize;

		private GameState gs;

		private float startX;
		private float startY;

		private float angle;

		public float endX;
		public float endY;

		public bool hitWall = false;

		public char horiVerWall = ' ';

		public int wallTypeHit = 0;
		
		public float length;

		public Ray CastRay() {
			float stepX = MathF.Cos(angle) * stepSize;
			float stepY = MathF.Sin(angle) * stepSize;

			float currentX = startX;
			float currentY = startY;

			for (int i = 0; i < maxSteps; i++) {
				currentX += stepX;
				currentY += stepY;

				length += stepSize;

				wallTypeHit = gs.MapAt((int)MathF.Floor(currentX), (int)MathF.Floor(currentY));

				if (wallTypeHit >= 1) {
					hitWall = true;

					float unitX = currentX - MathF.Floor(currentX);
					float unitY = currentY - MathF.Floor(currentY);

					if (unitX < 0.05f || unitX > 0.95f) {
						horiVerWall = 'v';
					}
					if (unitY < 0.05f || unitY > 0.95f) {
						horiVerWall = 'h';
					}
					if ((unitX < 0.05f || unitX > 0.95f) && (unitY < 0.05f || unitY > 0.95f)) {
						horiVerWall = 'c';
					}

					endX = currentX;
					endY = currentY;

					break;
				}
				else {
					continue;
				}
			}

			if (hitWall) {
				float unitX = currentX - MathF.Floor(currentX);
				float unitY = currentY - MathF.Floor(currentY);

				if (horiVerWall == 'h') {
					if (unitX < 0.05f) {
						currentX = MathF.Floor(currentX);
					}
					else if (unitX > 0.95f) {
						currentX = MathF.Ceiling(currentX);
					}
				}
				else if (horiVerWall == 'v') {
					if (unitY < 0.05f) {
						currentY = MathF.Floor(currentY);
					}
					else if (unitY > 0.95f) {
						currentY = MathF.Ceiling(currentY);
					}
				}

				endX = currentX;
				endY = currentY;

				length = MathF.Sqrt(MathF.Pow(startX - endX, 2) + MathF.Pow(startY - endY, 2));
			}

			return this;
		}
	}
}
