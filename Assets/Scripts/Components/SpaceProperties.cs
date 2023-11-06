using Unity.Entities;
using Unity.Mathematics;

namespace Components {
    public struct SpaceProperties : IComponentData {
        public float2 ScreenDimensions;
        public int AsteroidsToSpawn;
        public Entity MajorAsteroidPrefab;
        public Entity MediumAsteroidPrefab;
        public Entity MinorAsteroidPrefab;
        public float AsteroidSpawnRate;
        public float asteroidMoveRate;
    }

    public struct AsteroidSpawnTimer : IComponentData {
        public float Value;
    }
}