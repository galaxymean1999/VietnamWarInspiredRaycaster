using System;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;
using System.Text;
using System.Threading.Tasks;

namespace RaycasterInWF {
	public class GameState {
		public GameState() {

		}

		public Level lvl = new Level(0);

		public Player player = new Player(13.5f, 13.5f);

		public int currentLevel = 0;

		public const int tileSize = 1;

		public int MapAt(int x, int y) {
			if (x < lvl.mapWidth && y < lvl.mapHeight) {
				return lvl.map[(int)(y / tileSize) * lvl.mapWidth + (int)(x / tileSize)];
			}
			else {
				return -1;
			}
		}

		public void SortEntities() {
			// calculate distances to player
			foreach (Entity entity in lvl.entities) {
				entity.distance = MathF.Sqrt(MathF.Pow(player.position.X - entity.position.X, 2) + MathF.Pow(player.position.Y - entity.position.Y, 2));
			}

			lvl.entities.Sort((b, a) => b.distance.CompareTo(a.distance));
		}
	}
}
