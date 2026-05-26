namespace RaycasterInWF {
	public class GameState {
		public GameState() {
			lvl = new Level(currentLevel);
		}

		public Level lvl;

		public Player player = new Player(13.5f, 13.5f);

		public int currentLevel = 0;

		public const int tileSize = 1;

		public int score = 0;

		public int shootLightTimer = 5;

		// light level - larger number is less light
		public int lightLevel = 4;

		// checks what is on the map at x and y coordinates
		public int MapAt(int x, int y) {
			if (x < lvl.mapWidth && y < lvl.mapHeight && x >= 0 && y >= 0) {
				return lvl.map[(int)(y / tileSize) * lvl.mapWidth + (int)(x / tileSize)];
			}
			else {
				return -1;
			}
		}

		// sort entites by their distance to player to have the correct order of entities
		private void SortEntities() {
			// calculate distances to player
			foreach (Entity entity in lvl.entities) {
				entity.distance = MathF.Sqrt(MathF.Pow(player.position.X - entity.position.X, 2) + MathF.Pow(player.position.Y - entity.position.Y, 2));
			}

			// sort entities by distance
			lvl.entities.Sort((a, b) => b.distance.CompareTo(a.distance));
		}

		public void Update() {
			player.Update(this);

			SortEntities();

			// add player to the entity list for checking if the player was hit by an enemy
			lvl.entities.Add(new Entity(player.position.X, player.position.Y, 100));

			foreach (Entity e in lvl.entities) {
				e.Update(this);
			}

			lvl.entities.RemoveAt(lvl.entities.Count - 1);

			// primitive muzzle flash from shooting with just more light 
			if (player.shot) {
				shootLightTimer--;
			}
			if (shootLightTimer < 0) {
				player.shot = false;
				shootLightTimer = 5;
				lightLevel = 4;
			}
		}
	}
}
