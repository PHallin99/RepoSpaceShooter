using Components;
using Unity.Burst;
using Unity.Entities;

namespace Systems {
    [BurstCompile]
    [UpdateInGroup(typeof(InitializationSystemGroup))]
    public partial struct SpawnAsteroidSystem : ISystem {
        [BurstCompile]
        public void OnCreate(ref SystemState state) {
            state.RequireForUpdate<BeginInitializationEntityCommandBufferSystem.Singleton>();
            state.RequireForUpdate<SpaceProperties>();
        }

        [BurstCompile]
        public void OnUpdate(ref SystemState state) {
            var deltaTime = SystemAPI.Time.DeltaTime;
            var ecbSingleton = SystemAPI.GetSingleton<BeginInitializationEntityCommandBufferSystem.Singleton>();

            new SpawnAsteroidJob{
                DeltaTime = deltaTime,
                Ecb = ecbSingleton.CreateCommandBuffer(state.WorldUnmanaged)
            }.Schedule();


            // state.Enabled = false;
            // var spaceEntity = SystemAPI.GetSingletonEntity<SpaceProperties>();
            // var space = SystemAPI.GetAspect<SpaceAspect>(spaceEntity);
            //
            // var ecb = new EntityCommandBuffer(Allocator.Temp);
            //
            // for (var i = 0; i < 3; i++) {
            //     for (var j = 0; j < space.NumberOfAsteroidsToSpawn; j++) {
            //         var newAsteroidTransform = space.GetRandomAsteroidTransform();
            //         Entity newAsteroid;
            //         switch (i) {
            //             case 0:
            //                 newAsteroid = ecb.Instantiate(space.MajorAsteroidPrefab);
            //                 ecb.SetComponent(newAsteroid, newAsteroidTransform);
            //                 break;
            //             case 1:
            //                 newAsteroid = ecb.Instantiate(space.MediumAsteroidPrefabs);
            //                 ecb.SetComponent(newAsteroid, newAsteroidTransform);
            //                 break;
            //             case 2:
            //                 newAsteroid = ecb.Instantiate(space.MinorAsteroidPrefabs);
            //                 ecb.SetComponent(newAsteroid, newAsteroidTransform);
            //                 break;
            //         }
            //     }
            // }
            //
            // ecb.Playback(state.EntityManager);
        }

        [BurstCompile]
        public void OnDestroy(ref SystemState state) {
        }
    }

    [BurstCompile]
    public partial struct SpawnAsteroidJob : IJobEntity {
        public float DeltaTime;
        public EntityCommandBuffer Ecb;

        [BurstCompile]
        private void Execute(SpaceAspect spaceAspect) {
            spaceAspect.AsteroidSpawnTimer -= DeltaTime;
            if (!spaceAspect.TimeToSpawnAsteroid) {
                return;
            }

            spaceAspect.AsteroidSpawnTimer = spaceAspect.AsteroidSpawnRate;
            var newAsteroid = Ecb.Instantiate(spaceAspect.MajorAsteroidPrefab);
            Ecb.AddComponent(newAsteroid, new AsteroidMoveProperties{
                MoveRate = spaceAspect.AsteroidMoveRate,
                MoveDirection = spaceAspect.GetRandomDirection()
            });
            var newAsteroidTransform = spaceAspect.GetRandomAsteroidTransform();
            Ecb.SetComponent(newAsteroid, newAsteroidTransform);
        }
    }
}