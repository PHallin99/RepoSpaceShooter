using Components;
using Unity.Burst;
using Unity.Entities;

namespace Systems {
    [BurstCompile]
    public partial struct AsteroidMoveSystem : ISystem {
        [BurstCompile]
        public void OnCreate(ref SystemState state) {
            state.RequireForUpdate<AsteroidMoveProperties>();
        }

        [BurstCompile]
        public void OnUpdate(ref SystemState state) {
            var deltaTime = SystemAPI.Time.DeltaTime;

            new AsteroidMoveJob{
                DeltaTime = deltaTime
            }.ScheduleParallel();
        }

        [BurstCompile]
        public void OnDestroy(ref SystemState state) {
        }
    }

    [BurstCompile]
    public partial struct AsteroidMoveJob : IJobEntity {
        public float DeltaTime;

        [BurstCompile]
        private void Execute(AsteroidMoveAspect asteroidMoveAspect) {
            asteroidMoveAspect.Move(DeltaTime);
        }
    }
}