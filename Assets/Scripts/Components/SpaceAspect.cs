using Unity.Entities;
using Unity.Mathematics;
using Unity.Transforms;

namespace Components {
    public readonly partial struct SpaceAspect : IAspect {
        public readonly Entity Entity;
        private readonly RefRW<LocalTransform> transform;

        private readonly RefRO<SpaceProperties> spaceProperties;
        private readonly RefRW<SpaceRandom> spaceRandom;
        private readonly RefRW<AsteroidSpawnTimer> asteroidSpawnTimer;

        public float AsteroidMoveRate => spaceProperties.ValueRO.asteroidMoveRate;
        public int NumberOfAsteroidsToSpawn => spaceProperties.ValueRO.AsteroidsToSpawn;
        public Entity MajorAsteroidPrefab => spaceProperties.ValueRO.MajorAsteroidPrefab;
        public Entity MediumAsteroidPrefabs => spaceProperties.ValueRO.MediumAsteroidPrefab;
        public Entity MinorAsteroidPrefabs => spaceProperties.ValueRO.MinorAsteroidPrefab;

        public LocalTransform GetRandomAsteroidTransform() {
            return new LocalTransform{
                Position = GetRandomPosition(),
                Rotation = quaternion.identity,
                Scale = 1
            };
        }

        private float3 GetRandomPosition() {
            var randomPosition = spaceRandom.ValueRW.Value.NextFloat3(
                new float3(-spaceProperties.ValueRO.ScreenDimensions.x + 1, -spaceProperties.ValueRO.ScreenDimensions.y + 1, 0),
                new float3(spaceProperties.ValueRO.ScreenDimensions.x - 1, spaceProperties.ValueRO.ScreenDimensions.y - 1, 0));
            randomPosition.z = 15f;
            return randomPosition;
        }

        public float3 GetRandomDirection() {
            var randomDirection = spaceRandom.ValueRW.Value.NextFloat3(
                new float3(-spaceProperties.ValueRO.ScreenDimensions.x + 1, -spaceProperties.ValueRO.ScreenDimensions.y + 1, 0),
                new float3(spaceProperties.ValueRO.ScreenDimensions.x - 1, spaceProperties.ValueRO.ScreenDimensions.y - 1, 0));
            math.normalize(randomDirection);
            return randomDirection;
        }

        public float AsteroidSpawnTimer
        {
            get => asteroidSpawnTimer.ValueRO.Value;
            set => asteroidSpawnTimer.ValueRW.Value = value;
        }

        public bool TimeToSpawnAsteroid => AsteroidSpawnTimer <= 0f;
        public float AsteroidSpawnRate => spaceProperties.ValueRO.AsteroidSpawnRate;
    }
}