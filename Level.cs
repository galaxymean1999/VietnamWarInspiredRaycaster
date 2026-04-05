using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RaycasterInWF {
	public class Level {
		public Level(int current) {
			loadLevel(current);

			entities = loadEntities(current);
		}

		public int[] map;

		public int mapWidth = 15;
		public int mapHeight = 15;

		public List<Entity> entities;

		void loadLevel(int lvl) {
			switch (lvl) {
				case 0:
					map = new [] {
						1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,
						1,0,0,0,1,0,0,0,0,0,0,0,0,0,1,
						1,1,1,0,1,0,1,1,1,1,1,1,1,0,1,
						1,0,0,0,0,0,1,0,0,0,0,0,1,0,1,
						1,0,1,1,1,1,1,0,1,1,1,0,1,0,1,
						1,0,1,0,0,0,0,0,1,0,0,0,1,0,1,
						1,0,1,0,1,1,1,1,1,0,1,1,1,0,1,
						1,0,0,0,1,0,0,0,0,0,1,0,0,0,1,
						1,1,1,0,1,1,1,0,1,1,1,0,1,1,1,
						1,0,0,0,1,0,0,0,1,0,0,0,1,0,1,
						1,0,1,1,1,0,1,1,1,0,1,1,1,0,1,
						1,0,1,0,0,0,0,0,0,0,0,0,0,0,1,
						1,0,1,0,1,1,1,1,1,1,1,1,1,1,1,
						1,0,0,0,0,0,0,0,0,0,0,0,0,0,1,
						1,1,1,1,1,1,1,1,1,1,1,1,1,1,1
					};
					break;
			}
		}

		List<Entity> loadEntities(int lvl) {
			List<Entity> entityList = new List<Entity>();

			switch (lvl) {
				case 0:
					entityList.Add(new Entity(2.5f, 1.5f, 0));

					entityList.Add(new Entity(5.5f, 7.5f, 0));
					break;
			}

			return entityList;
		}
	}
}
