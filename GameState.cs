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

		public const int tileSize = 1;

		public int mapAt(int x, int y) {
			if (x < lvl.mapWidth && y < lvl.mapHeight) {
				return lvl.map[(int)(y / tileSize) * lvl.mapWidth + (int)(x / tileSize)];
			}
			else {
				return -1;
			}
		}
	}
}
