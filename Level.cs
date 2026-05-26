namespace RaycasterInWF {
	public class Level {
		public Level(int id) {
            map = new int[] { };

            entities = new List<Entity>();

			LoadLevelID(id);

			LoadEntities(id);
        }

        public int[] map;

		public int mapWidth = 15;
		public int mapHeight = 15;

		public List<Entity> entities;

		private void LoadLevelID(int id) {
			switch (id) {
				case 0:
					map = new [] {
						// 0 - empty space
						// 1 - solid wall
						// 2 - solid wall with a ladder texture
						1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,
						2,0,0,0,1,0,0,0,0,0,0,0,0,0,1,
						1,1,1,0,1,0,1,1,1,1,1,1,1,0,1,
						1,0,0,0,0,0,1,0,0,0,0,0,1,0,1,
						1,0,1,1,1,1,1,0,1,1,1,0,1,0,1,
						1,0,1,0,0,0,0,0,1,0,0,0,1,0,1,
						1,0,1,0,1,1,1,1,1,0,1,1,1,0,1,
						1,0,0,0,1,0,0,0,0,0,1,1,1,0,1,
						1,1,1,0,1,1,1,0,1,1,1,0,1,0,1,
						1,0,0,0,1,0,0,0,1,0,0,0,1,0,1,
						1,0,1,1,1,0,1,1,1,0,1,1,1,0,1,
						1,0,1,0,0,0,0,0,0,0,0,0,0,0,1,
						1,0,1,0,1,1,1,1,1,1,1,1,1,1,1,
						1,0,0,0,0,0,0,0,0,0,0,0,0,0,1,
						1,1,1,1,1,1,1,1,1,1,1,1,1,1,1
					};
					break;
				case 1:
					map = new[] {
						1,2,1,1,1,1,1,1,1,1,1,1,1,1,1,
						1,0,1,0,1,0,1,0,0,0,0,1,0,0,1,
						1,0,1,0,0,0,1,1,0,1,0,0,0,0,1,
						1,0,1,0,1,0,1,0,0,1,0,1,0,0,1,
						1,0,1,0,1,0,0,0,1,1,1,1,1,0,1,
						1,0,1,1,0,0,1,1,1,0,0,1,0,0,1,
						1,0,0,0,0,1,1,0,0,0,0,1,1,0,1,
						1,0,0,1,0,1,0,0,1,0,1,1,0,0,1,
						1,1,0,1,1,1,0,0,1,0,0,1,1,1,1,
						1,0,0,0,0,1,0,0,1,0,0,0,0,0,1,
						1,0,0,1,1,1,1,0,1,1,0,1,1,0,1,
						1,1,0,0,0,0,0,0,0,1,0,1,0,0,1,
						1,1,1,1,0,1,1,1,1,1,0,1,1,1,1,
						1,0,0,0,0,0,0,1,0,0,0,0,0,0,1,
						1,1,1,1,1,1,1,1,1,1,1,1,1,1,1
					};
					break;
			}
		}

		private void LoadEntities(int id) {
			entities.Clear();

			switch (id) {
				case 0:
					// enemies
					NewEntity(2.5f, 1.5f, 0);
					NewEntity(5.5f, 7.5f, 0);
					NewEntity(9.5f, 9.5f, 0);
					NewEntity(1.5f, 7.5f, 0);
					NewEntity(2.5f, 9.5f, 0);
					NewEntity(5.5f, 9.5f, 0);
					break;
				case 1:
					// enemies
					NewEntity(1.5f, 13.5f, 0);
					NewEntity(6.5f, 13.5f, 0);
					NewEntity(4.5f, 9.5f, 0);
					NewEntity(11.5f, 9.5f, 0);
					NewEntity(10.5f, 6.5f, 0);
					NewEntity(6.5f, 7.5f, 0);
					NewEntity(8.5f, 11.5f, 0);
					NewEntity(4.5f, 7.5f, 0);
					NewEntity(5.5f, 1.5f, 0);

					// chests
					NewEntity(1.5f, 7.5f, 2);
					NewEntity(7.5f, 3.5f, 2);
					NewEntity(13.5f, 1.5f, 2);
					NewEntity(12.5f, 5.5f, 2);
					break;
			}
		}

		// type
		// 0 - enemy
		// 1 - dead enemy
		// 2 - chest
		// 3 - open chest
		private void NewEntity(float x, float y, int type) {
			entities.Add(new Entity(x, y, type));
		}
	}
}
